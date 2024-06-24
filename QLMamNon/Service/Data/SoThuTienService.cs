using ACG.Core.WinForm.Util;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Entity.Form;
using QLMamNon.Facade;
using QLMamNon.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLMamNon.Service.Data
{
    public class SoThuTienService : BaseDataService
    {
        #region Generate SoThuTien

        public int GenerateSoThuTienByHocSinhRows(DateTime ngayTinh, List<hocsinh> hocSinhRows)
        {
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            List<viewbangthutien> viewBangThuTienTable = entities.getViewBangThuTienByNgayTinhAndLop(ngayTinh, null).ToList();

            List<int> hocSinhIds = new List<int>();
            List<hocsinh> needToGenerateHocSinhRows = new List<hocsinh>();

            foreach (hocsinh hocSinh in hocSinhRows)
            {
                List<viewbangthutien> rows = viewBangThuTienTable.FindAll(v => v.HocSinhId == hocSinh.HocSinhId);

                if (ListUtil.IsEmpty(rows))
                {
                    hocSinhIds.Add(hocSinh.HocSinhId);
                    needToGenerateHocSinhRows.Add(hocSinh);
                }
            }

            Dictionary<int, hocsinh_lop> hocSinhIdsToHocSinhLops = StaticDataUtil.GetHocSinhLopsByHocSinhIds(entities, hocSinhIds, ngayTinh);
            Dictionary<int, Dictionary<int, viewbangthutien>> lopToHocSinhToViewBangThuTienRowsMap =
                buildLopToHocSinhToViewBangThuTienRowsMap(entities, viewBangThuTienTable, ngayTinh);
            Dictionary<int, Dictionary<int, viewbangthutien>> prevMonthLopToHocSinhToViewBangThuTienRowsMap =
                getAndSortPrevMonthViewBangThuTienRows(entities, ngayTinh, hocSinhIds, hocSinhIdsToHocSinhLops, lopToHocSinhToViewBangThuTienRowsMap);
            Dictionary<int, int> generatedLopToSTTs = buildGeneratedLopIdToSTTMap(lopToHocSinhToViewBangThuTienRowsMap);

            // Generate SoThuTien for HocSinhs that exist in previous month
            foreach (hocsinh hocSinh in needToGenerateHocSinhRows)
            {
                if (hocSinhIdsToHocSinhLops.ContainsKey(hocSinh.HocSinhId))
                {
                    int lopId = hocSinhIdsToHocSinhLops[hocSinh.HocSinhId].LopId;
                    viewbangthutien preMonthViewBangThuTien = null;

                    if (prevMonthLopToHocSinhToViewBangThuTienRowsMap.ContainsKey(lopId))
                    {
                        Dictionary<int, viewbangthutien> prevMonthhocSinhToViewBangThuTienRowsMap = prevMonthLopToHocSinhToViewBangThuTienRowsMap[lopId];

                        if (prevMonthhocSinhToViewBangThuTienRowsMap.ContainsKey(hocSinh.HocSinhId))
                        {
                            preMonthViewBangThuTien = prevMonthhocSinhToViewBangThuTienRowsMap[hocSinh.HocSinhId];
                        }
                    }

                    if (preMonthViewBangThuTien != null)
                    {
                        GenerateSoThuTienByHocSinhAndLopAndNgayTinh(entities, hocSinh.HocSinhId, lopId, ngayTinh, preMonthViewBangThuTien.STT, preMonthViewBangThuTien);

                        if (!generatedLopToSTTs.ContainsKey(lopId))
                        {
                            generatedLopToSTTs.Add(lopId, 0);
                        }

                        int currentSTT = generatedLopToSTTs[lopId];

                        if (currentSTT < preMonthViewBangThuTien.STT)
                        {
                            generatedLopToSTTs[lopId] = preMonthViewBangThuTien.STT;
                        }
                    }
                }
            }

            // Generate SoThuTien for HocSinhs that NOT exist in previous month
            foreach (hocsinh hocSinh in needToGenerateHocSinhRows)
            {
                if (hocSinhIdsToHocSinhLops.ContainsKey(hocSinh.HocSinhId))
                {
                    int lopId = hocSinhIdsToHocSinhLops[hocSinh.HocSinhId].LopId;
                    viewbangthutien preMonthViewBangThuTien = null;

                    if (prevMonthLopToHocSinhToViewBangThuTienRowsMap.ContainsKey(lopId))
                    {
                        Dictionary<int, viewbangthutien> prevMonthhocSinhToViewBangThuTienRowsMap = prevMonthLopToHocSinhToViewBangThuTienRowsMap[lopId];

                        if (prevMonthhocSinhToViewBangThuTienRowsMap.ContainsKey(hocSinh.HocSinhId))
                        {
                            preMonthViewBangThuTien = prevMonthhocSinhToViewBangThuTienRowsMap[hocSinh.HocSinhId];
                        }
                    }

                    if (preMonthViewBangThuTien == null)
                    {
                        if (!generatedLopToSTTs.ContainsKey(lopId))
                        {
                            generatedLopToSTTs.Add(lopId, 0);
                        }

                        int stt = generatedLopToSTTs[lopId] + 1;
                        GenerateSoThuTienByHocSinhAndLopAndNgayTinh(entities, hocSinh.HocSinhId, lopId, ngayTinh, stt, preMonthViewBangThuTien);
                        generatedLopToSTTs[lopId] = stt;
                    }
                }
            }

            return hocSinhIds.Count;
        }

        private static Dictionary<int, int> buildGeneratedLopIdToSTTMap(Dictionary<int, Dictionary<int, viewbangthutien>> lopToHocSinhToViewBangThuTienRowsMap)
        {
            Dictionary<int, int> generatedLopToSTTs = new Dictionary<int, int>();

            foreach (KeyValuePair<int, Dictionary<int, viewbangthutien>> lopToHocSinhToViewBangThuTienRow in lopToHocSinhToViewBangThuTienRowsMap)
            {
                generatedLopToSTTs.Add(lopToHocSinhToViewBangThuTienRow.Key, lopToHocSinhToViewBangThuTienRow.Value.Count);
            }

            return generatedLopToSTTs;
        }

        private static Dictionary<int, Dictionary<int, viewbangthutien>> buildLopToHocSinhToViewBangThuTienRowsMap
            (qlmamnonEntities entities, List<viewbangthutien> viewBangThuTienTable, DateTime ngayTinh)
        {
            List<int> hocSinhIds = new List<int>();

            foreach (viewbangthutien viewBangThuTienRow in viewBangThuTienTable)
            {
                hocSinhIds.Add(viewBangThuTienRow.HocSinhId);
            }

            Dictionary<int, hocsinh_lop> hocSinhIdsToHocSinhLops = StaticDataUtil.GetHocSinhLopsByHocSinhIds(entities, hocSinhIds, ngayTinh);
            Dictionary<int, Dictionary<int, viewbangthutien>> lopToHocSinhViewBangThuTienRows = new Dictionary<int, Dictionary<int, viewbangthutien>>();

            foreach (viewbangthutien viewBangThuTienRow in viewBangThuTienTable)
            {
                if (!hocSinhIdsToHocSinhLops.ContainsKey(viewBangThuTienRow.HocSinhId))
                {
                    continue;
                }

                int lopId = hocSinhIdsToHocSinhLops[viewBangThuTienRow.HocSinhId].LopId;

                if (!lopToHocSinhViewBangThuTienRows.ContainsKey(lopId))
                {
                    lopToHocSinhViewBangThuTienRows.Add(lopId, new Dictionary<int, viewbangthutien>());
                }

                Dictionary<int, viewbangthutien> hocSinhToViewBangThuTienRows = lopToHocSinhViewBangThuTienRows[lopId];

                if (!hocSinhToViewBangThuTienRows.ContainsKey(viewBangThuTienRow.HocSinhId))
                {
                    hocSinhToViewBangThuTienRows.Add(viewBangThuTienRow.HocSinhId, viewBangThuTienRow);
                }
            }

            return lopToHocSinhViewBangThuTienRows;
        }

        private static Dictionary<int, Dictionary<int, viewbangthutien>> getAndSortPrevMonthViewBangThuTienRows(qlmamnonEntities entities, DateTime ngayTinh, List<int> hocSinhIds,
            Dictionary<int, hocsinh_lop> hocSinhIdsToHocSinhLops, Dictionary<int, Dictionary<int, viewbangthutien>> lopToHocSinhToViewBangThuTienRowsMap)
        {
            List<viewbangthutien> prevMonthViewBangThuTienTable = entities.getViewBangThuTienByNgayTinhAndLop(ngayTinh.AddMonths(-1), null).ToList();
            Dictionary<int, Dictionary<int, viewbangthutien>> lopToHocSinhViewBangThuTienRows = new Dictionary<int, Dictionary<int, viewbangthutien>>();

            foreach (viewbangthutien viewBangThuTienRow in prevMonthViewBangThuTienTable)
            {
                if (!hocSinhIds.Contains(viewBangThuTienRow.HocSinhId))
                {
                    continue;
                }

                int lopId = hocSinhIdsToHocSinhLops[viewBangThuTienRow.HocSinhId].LopId;

                if (!lopToHocSinhViewBangThuTienRows.ContainsKey(lopId))
                {
                    lopToHocSinhViewBangThuTienRows.Add(lopId, new Dictionary<int, viewbangthutien>());
                }

                Dictionary<int, viewbangthutien> hocSinhToViewBangThuTienRows = lopToHocSinhViewBangThuTienRows[lopId];

                if (!hocSinhToViewBangThuTienRows.ContainsKey(viewBangThuTienRow.HocSinhId))
                {
                    hocSinhToViewBangThuTienRows.Add(viewBangThuTienRow.HocSinhId, viewBangThuTienRow);
                }
            }

            // Sort HocSinh in each Lop by STT ASC
            foreach (KeyValuePair<int, Dictionary<int, viewbangthutien>> lopToHocSinhViewBangThuTienRow in lopToHocSinhViewBangThuTienRows)
            {
                int lopId = lopToHocSinhViewBangThuTienRow.Key;
                Dictionary<int, viewbangthutien> hocSinhViewBangThuTienRows = lopToHocSinhToViewBangThuTienRowsMap.ContainsKey(lopId) ? lopToHocSinhToViewBangThuTienRowsMap[lopId] : null;
                SortedList<int, viewbangthutien> sortedViewBangThuTienRows = new SortedList<int, viewbangthutien>();
                int currentMaxSTT = ListUtil.IsEmpty(hocSinhViewBangThuTienRows) ? 0 : hocSinhViewBangThuTienRows.Count;

                foreach (KeyValuePair<int, viewbangthutien> hocSinhIdToViewBangThuTienRow in lopToHocSinhViewBangThuTienRow.Value)
                {
                    if (!sortedViewBangThuTienRows.ContainsKey(hocSinhIdToViewBangThuTienRow.Value.STT))
                    {
                        sortedViewBangThuTienRows.Add(hocSinhIdToViewBangThuTienRow.Value.STT, hocSinhIdToViewBangThuTienRow.Value);
                    }
                }

                for (int i = 0; i < sortedViewBangThuTienRows.Count; i++)
                {
                    sortedViewBangThuTienRows.Values[i].STT = i + currentMaxSTT + 1;
                }
            }

            return lopToHocSinhViewBangThuTienRows;
        }

        public void GenerateSoThuTienByHocSinhAndLopAndNgayTinh(qlmamnonEntities entities, int hocSinhId, int lopId, DateTime ngayTinh, int stt, viewbangthutien preMonthViewBangThuTien)
        {
            int sXThangTruoc = 0;
            long soTienSXThangTruoc = 0;
            int anSangThangTruoc = 0;
            long soTienAnSangThangTruoc = 0;
            long soTienAnSangThangNay = 0;
            int anToiThangTruoc = 0;
            long soTienAnToiThangTruoc = 0;
            long soTienAnToiThangNay = 0;
            long soTienNangKhieu = 0;
            long soTienTruyThu = 0;
            long soTienDieuHoa = 0;
            long soTienDoDung = 0;
            String ghiChu = "";
            bangthutien bangThuTienRow = new bangthutien()
            {
                HocSinhId = hocSinhId,
                LopId = lopId,
                SXThangTruoc = sXThangTruoc,
                SoTienSXThangTruoc = soTienSXThangTruoc,
                AnSangThangTruoc = anSangThangTruoc,
                SoTienAnSangThangTruoc = soTienAnSangThangTruoc,
                SoTienAnSangThangNay = soTienAnSangThangNay,
                SoTienAnToiThangTruoc = soTienAnToiThangTruoc,
                AnToiThangTruoc = anToiThangTruoc,
                SoTienAnToiThangNay = soTienAnToiThangNay,
                SoTienDoDung = soTienDoDung,
                SoTienNangKhieu = soTienNangKhieu,
                SoTienTruyThu = soTienTruyThu,
                SoTienDieuHoa = soTienDieuHoa,
                NgayTinh = ngayTinh,
                STT = stt,
                IsDeleted = false,
                DateCreated = DateTime.Now,
                GhiChu = ghiChu
            };
            entities.bangthutiens.Add(bangThuTienRow);
            entities.SaveChanges();
            int bangThuTienId = bangThuTienRow.BangThuTienId;
            lop_khoi lopKhoi = entities.getLopKhoiByLopId(lopId).FirstOrDefault();

            if (lopKhoi != null)
            {
                int khoiId = lopKhoi.KhoiId;
                generateBangThuTienKhoanThu(entities, bangThuTienId, khoiId, ngayTinh, preMonthViewBangThuTien);
            }
            else
            {
                throw new Exception("Không tìm thấy khối cho lớp được chọn");
            }
        }

        private void generateBangThuTienKhoanThu(qlmamnonEntities entities, int bangThuTienId, int khoiId,
            DateTime ngayTinh, viewbangthutien preMonthViewBangThuTien)
        {
            int[] khoanThuIds = new int[] { BangThuTienConstant.KhoanThuIdBanTru, BangThuTienConstant.KhoanThuIdHocPhi, BangThuTienConstant.KhoanThuIdPhuPhi, BangThuTienConstant.KhoanThuIdTienAnSua, BangThuTienConstant.KhoanThuIdAnSang, BangThuTienConstant.KhoanThuIdAnToi, BangThuTienConstant.KhoanThuIdTienSua };
            List<int> ignoreKhoanThuIds = getKhoanThuIdsToIgnoreGenerating(preMonthViewBangThuTien);
            List<khoanthuhangnam> khoanThuHangNamTable = entities.getKhoanThuHangNamByParams(string.Join(",", khoanThuIds), khoiId, ngayTinh).ToList();
            bangthutien bangThuTienRow = entities.bangthutiens.FirstOrDefault(b => b.BangThuTienId == bangThuTienId);

            foreach (khoanthuhangnam row in khoanThuHangNamTable)
            {
                long soTien = 0;

                if (!ignoreKhoanThuIds.Contains(row.KhoanThuId))
                {
                    soTien = BangThuTienUtil.CalculateSoTienPhi(khoiId, 0, row.SoTien, row.KhoanThuId);
                }

                bangthutien_khoanthu bangThuTienKhoanThu = new bangthutien_khoanthu()
                {
                    KhoanThuId = row.KhoanThuId,
                    BangThuTienId = bangThuTienId,
                    SoTien = soTien
                };
                entities.bangthutien_khoanthu.Add(bangThuTienKhoanThu);

                if (soTien != 0)
                {
                    switch (row.KhoanThuId)
                    {
                        case BangThuTienConstant.KhoanThuIdAnSang:
                            bangThuTienRow.SoTienAnSangThangNay = soTien;
                            break;
                        case BangThuTienConstant.KhoanThuIdAnToi:
                            bangThuTienRow.SoTienAnToiThangNay = soTien;
                            break;
                        default:
                            break;
                    }
                }
            }

            //entities.bangthutiens.Update(bangThuTienRow);
            entities.SaveChanges();
        }

        private static List<int> getKhoanThuIdsToIgnoreGenerating(viewbangthutien preMonthViewBangThuTien)
        {
            List<int> ignoreKhoanThuIds = new List<int>();

            if (preMonthViewBangThuTien == null || preMonthViewBangThuTien.SoTienAnSangThangNay == 0)
            {
                ignoreKhoanThuIds.Add(BangThuTienConstant.KhoanThuIdAnSang);
            }

            if (preMonthViewBangThuTien == null || preMonthViewBangThuTien.SoTienAnToiThangNay == 0)
            {
                ignoreKhoanThuIds.Add(BangThuTienConstant.KhoanThuIdAnToi);
            }

            return ignoreKhoanThuIds;
        }

        #endregion

        public List<BangKeThuTienItem> GetBangKeTongHopThuTien(qlmamnonEntities entities, DateTime toDate, int? lopId)
        {
            List<viewbangthutien> viewBangThuTienRows = this.loadViewBangThuTiensToanTruong(entities, toDate, lopId);
            List<viewbangthutien> rows = EvaluateViewBangThuTienRowsForReport(entities, viewBangThuTienRows, toDate);
            List<BangKeThuTienItem> rowsToDisplay = new List<BangKeThuTienItem>();
            Dictionary<int, viewbangthutien> hocSinhIdsToViewBangThuTienRows = new Dictionary<int, viewbangthutien>();

            foreach (viewbangthutien viewBangThuTienRow in rows)
            {
                if (!hocSinhIdsToViewBangThuTienRows.ContainsKey(viewBangThuTienRow.HocSinhId))
                {
                    hocSinhIdsToViewBangThuTienRows.Add(viewBangThuTienRow.HocSinhId, viewBangThuTienRow);
                }
            }

            if (ListUtil.IsEmpty(hocSinhIdsToViewBangThuTienRows))
            {
                return rowsToDisplay;
            }

            List<phieuthu> phieuThuDataTable = entities.getPhieuThuOfMonthByHocSinhIdsAndNgay(StringUtil.Join(new List<int>(hocSinhIdsToViewBangThuTienRows.Keys), ","), toDate).ToList();
            int stt = 1;

            foreach (phieuthu phieuThuRow in phieuThuDataTable)
            {
                viewbangthutien viewBangThuTienRow = hocSinhIdsToViewBangThuTienRows[phieuThuRow.HocSinhId.Value];
                BangKeThuTienItem bangKeThuTienItem = new BangKeThuTienItem()
                {
                    STT = stt++,
                    HocSinhId = viewBangThuTienRow.HocSinhId,
                    HoTen = viewBangThuTienRow.HoTen,
                    Lop = viewBangThuTienRow.Lop,
                    NgayNop = phieuThuRow.Ngay,
                    SoBienLai = phieuThuRow.MaPhieu,
                    PhuPhi = viewBangThuTienRow.PhuPhi,
                    BanTru = viewBangThuTienRow.BanTru,
                    HocPhi = viewBangThuTienRow.HocPhi,
                    AnChinh = viewBangThuTienRow.TienAnSua + viewBangThuTienRow.TienSua,
                    AnSang = viewBangThuTienRow.SoTienAnSangConLai,
                    AnToi = viewBangThuTienRow.SoTienAnToiConLai,
                    NangKhieu = viewBangThuTienRow.SoTienNangKhieu.Value,
                    DoDung = viewBangThuTienRow.SoTienDoDung.Value,
                    DieuHoa = viewBangThuTienRow.SoTienDieuHoa.Value,
                    TruyThu = viewBangThuTienRow.SoTienTruyThu.Value,
                    PhaiThu = viewBangThuTienRow.ThanhTien,
                    DaThu = phieuThuRow.SoTien,
                    ConNo = viewBangThuTienRow.ThanhTien - phieuThuRow.SoTien
                };

                if (!ListUtil.IsEmpty(rowsToDisplay))
                {
                    BangKeThuTienItem prevBangKeThuTienItem = rowsToDisplay[rowsToDisplay.Count - 1];

                    if (prevBangKeThuTienItem.HocSinhId == bangKeThuTienItem.HocSinhId)
                    {
                        bangKeThuTienItem.TruyThu = prevBangKeThuTienItem.ConNo;
                        bangKeThuTienItem.PhaiThu = prevBangKeThuTienItem.ConNo;
                        bangKeThuTienItem.ConNo = bangKeThuTienItem.PhaiThu - bangKeThuTienItem.DaThu;
                    }
                }

                rowsToDisplay.Add(bangKeThuTienItem);
            }

            return rowsToDisplay;
        }

        public List<BangKeThuTienItem> GetBangKeTongHopThuTienForTaoPhieuThu(qlmamnonEntities entities, DateTime toDate, int? lopId)
        {
            List<viewbangthutien> viewBangThuTienRows = this.loadViewBangThuTiensToanTruong(entities, toDate, lopId);
            List<viewbangthutien> rows = EvaluateViewBangThuTienRowsForReport(entities, viewBangThuTienRows, toDate);
            List<BangKeThuTienItem> rowsToDisplay = new List<BangKeThuTienItem>();
            Dictionary<int, viewbangthutien> hocSinhIdsToViewBangThuTienRows = new Dictionary<int, viewbangthutien>();

            foreach (viewbangthutien viewBangThuTienRow in rows)
            {
                if (!hocSinhIdsToViewBangThuTienRows.ContainsKey(viewBangThuTienRow.HocSinhId))
                {
                    hocSinhIdsToViewBangThuTienRows.Add(viewBangThuTienRow.HocSinhId, viewBangThuTienRow);
                }
            }

            if (ListUtil.IsEmpty(hocSinhIdsToViewBangThuTienRows))
            {
                return rowsToDisplay;
            }

            List<phieuthu> phieuThuDataTable = entities.getPhieuThuOfMonthByHocSinhIdsAndNgay(StringUtil.Join(new List<int>(hocSinhIdsToViewBangThuTienRows.Keys), ","), toDate).ToList();
            Dictionary<int, long> hocSinhIdToSoTien = new Dictionary<int, long>();
            foreach (phieuthu phieuThuRow in phieuThuDataTable)
            {
                if (!hocSinhIdToSoTien.ContainsKey(phieuThuRow.HocSinhId.Value))
                {
                    hocSinhIdToSoTien.Add(phieuThuRow.HocSinhId.Value, phieuThuRow.SoTien);
                }
                else
                {
                    long currentSoTien = hocSinhIdToSoTien[phieuThuRow.HocSinhId.Value];
                    hocSinhIdToSoTien.Remove(phieuThuRow.HocSinhId.Value);
                    hocSinhIdToSoTien.Add(phieuThuRow.HocSinhId.Value, phieuThuRow.SoTien + currentSoTien);
                }
            }

            foreach (viewbangthutien viewBangThuTienRow in rows)
            {
                long soTien = 0;
                if (hocSinhIdToSoTien.ContainsKey(viewBangThuTienRow.HocSinhId))
                {
                    soTien = hocSinhIdToSoTien[viewBangThuTienRow.HocSinhId];
                }

                BangKeThuTienItem bangKeThuTienItem = new BangKeThuTienItem()
                {
                    HocSinhId = viewBangThuTienRow.HocSinhId,
                    HoTen = viewBangThuTienRow.HoTen,
                    Lop = viewBangThuTienRow.Lop,
                    PhuPhi = viewBangThuTienRow.PhuPhi,
                    BanTru = viewBangThuTienRow.BanTru,
                    HocPhi = viewBangThuTienRow.HocPhi,
                    AnChinh = viewBangThuTienRow.TienAnSua + viewBangThuTienRow.TienSua,
                    AnSang = viewBangThuTienRow.SoTienAnSangConLai,
                    AnToi = viewBangThuTienRow.SoTienAnToiConLai,
                    NangKhieu = viewBangThuTienRow.SoTienNangKhieu == null ? 0 : viewBangThuTienRow.SoTienNangKhieu.Value,
                    DoDung = viewBangThuTienRow.SoTienDoDung.Value,
                    DieuHoa = viewBangThuTienRow.SoTienDieuHoa.Value,
                    TruyThu = viewBangThuTienRow.SoTienTruyThu.Value,
                    PhaiThu = viewBangThuTienRow.ThanhTien,
                    DaThu = soTien,
                    ConNo = viewBangThuTienRow.ThanhTien - soTien
                };

                if (!ListUtil.IsEmpty(rowsToDisplay))
                {
                    BangKeThuTienItem prevBangKeThuTienItem = rowsToDisplay[rowsToDisplay.Count - 1];

                    if (prevBangKeThuTienItem.HocSinhId == bangKeThuTienItem.HocSinhId)
                    {
                        bangKeThuTienItem.TruyThu = prevBangKeThuTienItem.ConNo;
                        bangKeThuTienItem.PhaiThu = prevBangKeThuTienItem.ConNo;
                        bangKeThuTienItem.ConNo = bangKeThuTienItem.PhaiThu - bangKeThuTienItem.DaThu;
                    }
                }

                rowsToDisplay.Add(bangKeThuTienItem);
            }

            return rowsToDisplay;
        }

        private List<viewbangthutien> loadViewBangThuTiensToanTruong(qlmamnonEntities entities, DateTime toDate, int? lopId)
        {
            List<viewbangthutien> table = entities.getViewBangThuTienByNgayTinhAndLop(toDate, lopId).ToList();
            List<int> bangThuTienIds = new List<int>(table.Count);

            foreach (viewbangthutien row in table)
            {
                bangThuTienIds.Add(row.BangThuTienId);
            }

            if (!ListUtil.IsEmpty(bangThuTienIds))
            {
                List<bangthutien_khoanthu> bTTKTDataTable = entities.getBangThuTienKhoanThuByBangThuTienIds(String.Join(",", bangThuTienIds)).ToList();
                List<phieuthu> phieuThuDataTable = entities.getPhieuThuByHocSinhIdAndNgayTinh(-1, toDate).ToList();

                Dictionary<int, viewbangthutien> prevMonthRowDictionary = this.EvaluatePrevMonthViewBangThuTienTable(entities, toDate.AddMonths(-1), lopId);
                List<hocsinh> hocSinhDataTable = this.getHocSinhData(entities);

                foreach (viewbangthutien row in table)
                {
                    row.HoTen = StaticDataUtil.GetHocSinhFullNameByHocSinhId(hocSinhDataTable, row.HocSinhId);
                    BangThuTienUtil.EvaluateValuesForViewBangThuTienRow(row,
                        prevMonthRowDictionary != null && prevMonthRowDictionary.ContainsKey(row.HocSinhId) ? prevMonthRowDictionary[row.HocSinhId] : null,
                        bTTKTDataTable, phieuThuDataTable, false, false, true);
                }
            }

            List<viewbangthutien> viewBangThuTienRows = new List<viewbangthutien>();

            foreach (viewbangthutien row in table)
            {
                viewBangThuTienRows.Add(row);
            }

            return viewBangThuTienRows;
        }

        public List<viewbangthutien> EvaluateViewBangThuTienRowsForReport(qlmamnonEntities entities, List<viewbangthutien> viewBangThuTienRows, DateTime toDate)
        {
            HashSet<int> hocSinhIds = new HashSet<int>();

            foreach (viewbangthutien viewBangThuTienRow in viewBangThuTienRows)
            {
                if (!hocSinhIds.Contains(viewBangThuTienRow.HocSinhId))
                {
                    hocSinhIds.Add(viewBangThuTienRow.HocSinhId);
                }
            }

            LoggerFacade.Info(string.Format("EvaluateViewBangThuTienRowsForReport for list HocSinhIds=[{0}]", StringUtil.JoinWithCommas(hocSinhIds.ToList())));

            Dictionary<int, lop> hocSinhIdsToLopNames = StaticDataUtil.GetLopsByHocSinhIds(entities, hocSinhIds.ToList(), toDate);
            List<hocsinh> hocSinhDataTable = this.getHocSinhData(entities);

            foreach (viewbangthutien viewBangThuTienRow in viewBangThuTienRows)
            {
                viewBangThuTienRow.HoTen = StaticDataUtil.GetHocSinhFullNameByHocSinhId(hocSinhDataTable, viewBangThuTienRow.HocSinhId);

                if (hocSinhIdsToLopNames.ContainsKey(viewBangThuTienRow.HocSinhId))
                {
                    viewBangThuTienRow.Lop = hocSinhIdsToLopNames[viewBangThuTienRow.HocSinhId].Name;
                }
            }

            return viewBangThuTienRows;
        }

        public Dictionary<int, viewbangthutien> EvaluatePrevMonthViewBangThuTienTable(qlmamnonEntities entities, DateTime ngayTinh, int? lop)
        {
            Dictionary<int, viewbangthutien> prevMonthRowDictionary = new Dictionary<int, viewbangthutien>();

            List<viewbangthutien> prevMonthBangThuTienTable = entities.getViewBangThuTienByNgayTinhAndLop(ngayTinh, null).ToList();
            List<int> prevMonthBangThuTienIds = new List<int>(prevMonthBangThuTienTable.Count);

            foreach (viewbangthutien row in prevMonthBangThuTienTable)
            {
                prevMonthBangThuTienIds.Add(row.BangThuTienId);
            }

            if (!ListUtil.IsEmpty(prevMonthBangThuTienIds))
            {
                List<bangthutien_khoanthu> prevMonthBTTKTDataTable = entities.getBangThuTienKhoanThuByBangThuTienIds(String.Join(",", prevMonthBangThuTienIds)).ToList();
                List<phieuthu> prevMonthPhieuThuDataTable = entities.getPhieuThuByHocSinhIdAndNgayTinh(-1, ngayTinh).ToList();
                prevMonthRowDictionary = new Dictionary<int, viewbangthutien>();

                foreach (viewbangthutien row in prevMonthBangThuTienTable)
                {
                    BangThuTienUtil.EvaluateValuesForViewBangThuTienRow(row, null, prevMonthBTTKTDataTable, prevMonthPhieuThuDataTable, true, false, true);

                    if (row != null && !prevMonthRowDictionary.ContainsKey(row.HocSinhId))
                    {
                        prevMonthRowDictionary.Add(row.HocSinhId, row);
                    }
                }
            }

            return prevMonthRowDictionary;
        }

        public void DeleteBangThuTienByHocSinhIdsAndDate(qlmamnonEntities entities, List<int> hocSinhIds, DateTime date)
        {
            entities.deleteBangThuTienByHocSinhIdsAndDate(StringUtil.JoinWithCommas(hocSinhIds), date);
        }

        public decimal GetSoTienTonDauKy(qlmamnonEntities entities, DateTime toDate)
        {
            DateTime lastDayOfPrevMonth = DateTimeUtil.DateEndOfMonth(toDate.AddMonths(-1));
            decimal tongThu = entities.getSumSoTienThuByDateRange(Settings.Default.AppLannchDate, lastDayOfPrevMonth, null).First().Value;
            decimal tongChi = entities.getSumSoTienChiByDateRange(Settings.Default.AppLannchDate, lastDayOfPrevMonth, null).First().Value;
            decimal chenhLech = tongThu - tongChi + Settings.Default.SoTienTonDauKy;
            return chenhLech;
        }

        private List<hocsinh> getHocSinhData(qlmamnonEntities entities)
        {
            return entities.hocsinhs.ToList();
        }

        public List<HocSinhChuaNopHocPhiItem> GetDanhSachTreChuaNopHocPhiByMonth(int year, int month)
        {
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            List<HocSinhChuaNopHocPhiItem> table = entities.Database.SqlQuery<HocSinhChuaNopHocPhiItem>($"CALL `getDanhSachTreChuaNopHocPhiByMonth`({year}, {month})", new object[] { }).ToList();
            return table;
        }
    }
}
