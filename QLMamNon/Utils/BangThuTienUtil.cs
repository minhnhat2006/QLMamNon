using ACG.Core.WinForm.Util;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLMamNon
{
    public static class BangThuTienUtil
    {
        public static long SXToSoTienSX(int sx, long tienAnChinh)
        {
            return sx * tienAnChinh;
        }

        public static long SXAnSangToSoTienAnSang(int sx, long tienAnSang)
        {
            return sx * tienAnSang;
        }

        public static long SXAnToiToSoTienAnToi(int sx, long tienAnToi)
        {
            return sx * tienAnToi;
        }

        public static long CalculateThanhTien(viewbangthutien bangThuTienRow)
        {
            return bangThuTienRow.TienAnSua + bangThuTienRow.PhuPhi + bangThuTienRow.BanTru + bangThuTienRow.HocPhi + bangThuTienRow.TienSua -
                        (bangThuTienRow.SoTienSXThangTruoc.HasValue ? bangThuTienRow.SoTienSXThangTruoc.Value : 0) +
                        bangThuTienRow.SoTienAnSangConLai + bangThuTienRow.SoTienAnToiConLai +
                        (bangThuTienRow.SoTienNangKhieu.HasValue ? bangThuTienRow.SoTienNangKhieu.Value : 0) +
                        (bangThuTienRow.SoTienTruyThu.HasValue ? bangThuTienRow.SoTienTruyThu.Value : 0) +
                        (bangThuTienRow.SoTienDieuHoa.HasValue ? bangThuTienRow.SoTienDieuHoa.Value : 0) +
                        (bangThuTienRow.SoTienDoDung.HasValue ? bangThuTienRow.SoTienDoDung.Value : 0);
        }

        public static long CalculateTruyThu(viewbangthutien row, viewbangthutien bangThuTienThangTruocRow)
        {
            if (Settings.Default.AppLannchDate.Year == row.NgayTinh.Year && Settings.Default.AppLannchDate.Month == row.NgayTinh.Month)
            {
                return row.SoTienTruyThu == null ? 0 : row.SoTienTruyThu.Value;
            }

            if (bangThuTienThangTruocRow != null)
            {
                return bangThuTienThangTruocRow.ThanhTien - bangThuTienThangTruocRow.SoTienNopLan1 - bangThuTienThangTruocRow.SoTienNopLan2;
            }

            return 0;
        }

        public static void CalculateSummaryFields(viewbangthutien row)
        {
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            List<bangthutiengenhistory> bangThuTienGenHistoryDataTable = entities.getBangThuTienGenHistoryByLopAndNgay(row.LopId, row.NgayTinh).ToList();

            if (!ListUtil.IsEmpty(bangThuTienGenHistoryDataTable))
            {
                bangthutiengenhistory bangThuTienGenHistoryRow = bangThuTienGenHistoryDataTable[0];
                row.SoTienSXThangTruoc = BangThuTienUtil.SXToSoTienSX(row.SXThangTruoc == null ? 0 : row.SXThangTruoc.Value, (long)bangThuTienGenHistoryRow.SoTienAnChinh);
                row.SoTienAnSangThangTruoc = BangThuTienUtil.SXAnSangToSoTienAnSang(row.AnSangThangTruoc == null ? 0 : row.AnSangThangTruoc.Value, (long)bangThuTienGenHistoryRow.SoTienAnSang);
                row.SoTienAnToiThangTruoc = BangThuTienUtil.SXAnToiToSoTienAnToi(row.AnToiThangTruoc, (long)bangThuTienGenHistoryRow.SoTienAnToi);
            }

            row.SoTienAnSangConLai = row.SoTienAnSangThangNay - (row.SoTienAnSangThangTruoc == null ? 0 : row.SoTienAnSangThangTruoc.Value);
            row.SoTienAnToiConLai = row.SoTienAnToiThangNay - (row.SoTienAnToiThangTruoc == null ? 0 : row.SoTienAnToiThangTruoc.Value);
            row.KhoanThuChinh = row.TienAnSua + row.PhuPhi + row.BanTru + row.HocPhi + row.TienSua;
            row.ThanhTien = BangThuTienUtil.CalculateThanhTien(row);
        }

        public static void EvaluateValuesForViewBangThuTienRow(viewbangthutien row, viewbangthutien bangThuTienThangTruocRow,
            List<bangthutien_khoanthu> bTTKTDataTable, List<phieuthu> phieuThuDataTable,
            bool ignoreTruyThu, bool isCalculateAnSangAnToi, bool isCalculateHocPhi)
        {
            evaluateValuesForAdditionalFields(row, bTTKTDataTable, isCalculateAnSangAnToi, isCalculateHocPhi);
            evaluateValuesForTienNop(row, phieuThuDataTable);

            if (!ignoreTruyThu)
            {
                row.SoTienTruyThu = BangThuTienUtil.CalculateTruyThu(row, bangThuTienThangTruocRow);
            }

            CalculateSummaryFields(row);
        }

        private static void evaluateValuesForAdditionalFields(viewbangthutien bangthutienItem, List<bangthutien_khoanthu> bTTKTDataTable,
            bool isCalculateAnSangAnToi, bool isCalculateHocPhi)
        {
            if (isCalculateHocPhi)
            {
                bangthutienItem.TienAnSua = 0;
                bangthutienItem.PhuPhi = 0;
                bangthutienItem.BanTru = 0;
                bangthutienItem.HocPhi = 0;
                bangthutienItem.TienSua = 0;
            }

            if (isCalculateAnSangAnToi)
            {
                bangthutienItem.SoTienAnSangThangNay = 0;
                bangthutienItem.SoTienAnToiThangNay = 0;
            }

            List<bangthutien_khoanthu> bangThuTienKhoanThuRows = bTTKTDataTable.FindAll(b => b.BangThuTienId == bangthutienItem.BangThuTienId);

            if (!ListUtil.IsEmpty(bangThuTienKhoanThuRows))
            {
                foreach (bangthutien_khoanthu bangThuTienKhoanThuRow in bangThuTienKhoanThuRows)
                {
                    switch (bangThuTienKhoanThuRow.KhoanThuId)
                    {
                        case BangThuTienConstant.KhoanThuIdTienAnSua:
                            if (isCalculateHocPhi)
                            {
                                bangthutienItem.TienAnSua = bangThuTienKhoanThuRow.SoTien;
                            }
                            break;
                        case BangThuTienConstant.KhoanThuIdPhuPhi:
                            if (isCalculateHocPhi)
                            {
                                bangthutienItem.PhuPhi = bangThuTienKhoanThuRow.SoTien;
                            }
                            break;
                        case BangThuTienConstant.KhoanThuIdBanTru:
                            if (isCalculateHocPhi)
                            {
                                bangthutienItem.BanTru = bangThuTienKhoanThuRow.SoTien;
                            }
                            break;
                        case BangThuTienConstant.KhoanThuIdHocPhi:
                            if (isCalculateHocPhi)
                            {
                                bangthutienItem.HocPhi = bangThuTienKhoanThuRow.SoTien;
                            }
                            break;
                        case BangThuTienConstant.KhoanThuIdAnSang:
                            if (isCalculateAnSangAnToi)
                            {
                                bangthutienItem.SoTienAnSangThangNay = bangThuTienKhoanThuRow.SoTien;
                            }
                            break;
                        case BangThuTienConstant.KhoanThuIdAnToi:
                            if (isCalculateAnSangAnToi)
                            {
                                bangthutienItem.SoTienAnToiThangNay = bangThuTienKhoanThuRow.SoTien;
                            }
                            break;
                        case BangThuTienConstant.KhoanThuIdTienSua:
                            if (isCalculateHocPhi)
                            {
                                bangthutienItem.TienSua = bangThuTienKhoanThuRow.SoTien;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void evaluateValuesForTienNop(viewbangthutien row, List<phieuthu> phieuThuDataTable)
        {
            row.SoTienNopLan1 = 0;
            row.SoTienNopLan2 = 0;

            List<phieuthu> phieuThuRows = phieuThuDataTable.FindAll(p => p.HocSinhId == row.HocSinhId);

            if (ListUtil.IsEmpty(phieuThuRows))
            {
                return;
            }

            phieuthu firstRow = phieuThuRows.First();
            row.NgayNopLan1 = firstRow.Ngay;
            row.SoTienNopLan1 = firstRow.SoTien;

            if (phieuThuRows.Count > 1)
            {
                phieuthu secondRow = phieuThuRows[1];
                row.NgayNopLan2 = secondRow.Ngay;
                row.SoTienNopLan2 = 0;

                for (int i = 1; i < phieuThuRows.Count; i++)
                {
                    phieuthu phieuThuRow = phieuThuRows[i];
                    row.SoTienNopLan2 += phieuThuRow.SoTien;
                }
            }
        }

        public static long CalculateSoTienPhi(int? khoiId, int soNgayNghiThang, long orgSoTien, int khoanThuId)
        {
            long soTien = orgSoTien;

            if (khoiId.HasValue)
            {
                List<bangtinhphi> bangTinhPhiTable = (List<bangtinhphi>)StaticDataFacade.Get(StaticDataKeys.BangTinhPhi);
                List<bangtinhphi> bangTinhPhiRows = bangTinhPhiTable.FindAll(b => b.KhoiId == khoiId.Value && b.SoNgayNghiMin <= soNgayNghiThang && b.SoNgayNghiMax >= soNgayNghiThang && b.KhoanThuId == khoanThuId);
                if (!ListUtil.IsEmpty(bangTinhPhiRows))
                {
                    soTien = bangTinhPhiRows[0].SoTien;
                }
            }

            return soTien;
        }

        public static void RecalculateBangThuTienKhoanThuList(qlmamnonEntities entities, viewbangthutien viewBangThuTienRow)
        {
            int khoiId = StaticDataUtil.GetKhoiIdByLopId(entities, viewBangThuTienRow.LopId).Value;
            int[] khoanThuIds = new int[] {
                BangThuTienConstant.KhoanThuIdBanTru,
                BangThuTienConstant.KhoanThuIdHocPhi,
                BangThuTienConstant.KhoanThuIdPhuPhi,
                BangThuTienConstant.KhoanThuIdTienAnSua,
                BangThuTienConstant.KhoanThuIdAnSang,
                BangThuTienConstant.KhoanThuIdAnToi,
                BangThuTienConstant.KhoanThuIdTienSua
            };
            List<khoanthuhangnam> khoanThuHangNamTable = entities.getKhoanThuHangNamByParams(String.Join(",", khoanThuIds), khoiId, viewBangThuTienRow.NgayTinh).ToList();

            foreach (khoanthuhangnam row in khoanThuHangNamTable)
            {
                long soTien = CalculateSoTienPhi(khoiId, viewBangThuTienRow.SXThangTruoc.Value, row.SoTien, row.KhoanThuId);
                switch (row.KhoanThuId)
                {
                    case BangThuTienConstant.KhoanThuIdTienAnSua:
                        viewBangThuTienRow.TienAnSua = soTien;
                        break;
                    case BangThuTienConstant.KhoanThuIdPhuPhi:
                        viewBangThuTienRow.PhuPhi = soTien;
                        break;
                    case BangThuTienConstant.KhoanThuIdBanTru:
                        viewBangThuTienRow.BanTru = soTien;
                        break;
                    case BangThuTienConstant.KhoanThuIdHocPhi:
                        viewBangThuTienRow.HocPhi = soTien;
                        break;
                    case BangThuTienConstant.KhoanThuIdTienSua:
                        viewBangThuTienRow.TienSua = soTien;
                        break;
                    default:
                        break;
                }
            }

            viewBangThuTienRow.PhucVuBanTru = viewBangThuTienRow.HocPhi + viewBangThuTienRow.BanTru;
            viewBangThuTienRow.KhoanThuChinh = viewBangThuTienRow.TienAnSua + viewBangThuTienRow.PhuPhi + viewBangThuTienRow.BanTru + viewBangThuTienRow.HocPhi + viewBangThuTienRow.TienSua;
            viewBangThuTienRow.ThanhTien = CalculateThanhTien(viewBangThuTienRow);
        }
    }
}
