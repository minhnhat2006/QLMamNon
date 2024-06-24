using ACG.Core.WinForm.Util;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLMamNon
{
    public static class StaticDataUtil
    {
        public static string GetThanhPhoById(List<thanhpho> table, Int32 id)
        {
            if (id <= 0)
            {
                return CommonConstant.EMPTY;
            }

            var rows = table.FindAll(t => t.ThanhPhoId == id);
            if (rows != null && rows.Count > 0)
            {
                return rows[0].Name;
            }

            return CommonConstant.EMPTY;
        }

        public static string GetQuanHuyenById(List<quanhuyen> table, Int32 id)
        {
            if (id <= 0)
            {
                return CommonConstant.EMPTY;
            }

            var rows = table.FindAll(q => q.QuanHuyenId == id);
            if (rows != null && rows.Count > 0)
            {
                return rows[0].Name as string;
            }

            return CommonConstant.EMPTY;
        }

        public static string GetPhuongXaById(List<phuongxa> table, Int32 id)
        {
            if (id <= 0)
            {
                return CommonConstant.EMPTY;
            }

            var rows = table.FindAll(p => p.PhuongXaId == id);
            if (rows != null && rows.Count > 0)
            {
                return rows[0].Name;
            }

            return CommonConstant.EMPTY;
        }

        public static int? GetTruongIdByKhoiId(qlmamnonEntities entities, Int32 khoiId)
        {
            if (khoiId <= 0)
            {
                return null;
            }

            List<khoi_truong> table = entities.getKhoiTruongByKhoiId(khoiId).ToList();

            if (table != null && table.Count > 0)
            {
                khoi_truong row = table.First();
                return row.TruongId;
            }

            return null;
        }

        public static khoi_truong GetKhoiTruongByKhoiId(qlmamnonEntities entities, Int32 khoiId)
        {
            if (khoiId <= 0)
            {
                return null;
            }

            List<khoi_truong> table = entities.getKhoiTruongByKhoiId(khoiId).ToList();

            if (table != null && table.Count > 0)
            {
                khoi_truong row = table.First();
                return row;
            }

            return null;
        }

        public static string GetTruongNameByTruongId(Int32 truongId)
        {
            if (truongId > 0)
            {
                List<truong> truongTable = (List<truong>)StaticDataFacade.Get(StaticDataKeys.TruongHoc);
                truong truongRow = truongTable.Find(t => t.TruongId == truongId);
                return truongRow.Name;
            }

            return CommonConstant.EMPTY;
        }

        public static string GetTruongByKhoiId(qlmamnonEntities entities, Int32 khoiId)
        {
            int? truongId = GetTruongIdByKhoiId(entities, khoiId);

            if (truongId != null)
            {
                List<truong> truongTable = (List<truong>)StaticDataFacade.Get(StaticDataKeys.TruongHoc);
                truong truongRow = truongTable.Find(t => t.TruongId == truongId.Value);
                return truongRow.Name;
            }

            return CommonConstant.EMPTY;
        }

        public static int? GetKhoiIdByLopId(qlmamnonEntities entities, Int32 lopId)
        {
            if (lopId <= 0)
            {
                return null;
            }

            List<lop_khoi> table = entities.getLopKhoiByLopId(lopId).ToList();

            if (table != null && table.Count > 0)
            {
                lop_khoi row = table.First();
                return row.KhoiId;
            }

            return null;
        }

        public static string GetKhoiNameByKhoiId(Int32 khoiId)
        {
            if (khoiId <= 0)
            {
                return CommonConstant.EMPTY;
            }

            List<khoi> khoiTable = (List<khoi>)StaticDataFacade.Get(StaticDataKeys.KhoiHoc);

            if (khoiTable != null && khoiTable.Count > 0)
            {
                khoi khoiRow = khoiTable.Find(k => k.KhoiId == khoiId);
                return khoiRow.Name;
            }

            return CommonConstant.EMPTY;
        }

        public static string GetLopNameByLopId(Int32 lopId)
        {
            if (lopId <= 0)
            {
                return CommonConstant.EMPTY;
            }

            List<lop> lopTable = StaticDataFacade.Get(StaticDataKeys.LopHoc) as List<lop>;

            if (lopTable != null && lopTable.Count > 0)
            {
                lop lopRow = lopTable.Find(l => l.LopId == lopId);
                return lopRow.Name;
            }

            return CommonConstant.EMPTY;
        }

        public static string GetHocSinhById(List<hocsinh> table, Int32 id)
        {
            if (id <= 0)
            {
                return CommonConstant.EMPTY;
            }

            var rows = table.FindAll(h => h.HocSinhId == id);

            if (rows != null && rows.Count > 0)
            {
                return rows.First().HoTen;
            }

            return CommonConstant.EMPTY;
        }

        public static string GetKhoanThuNameByKhoanThuId(Int32 khoanThuId)
        {
            if (khoanThuId <= 0)
            {
                return CommonConstant.EMPTY;
            }

            List<khoanthu> khoanThuTable = StaticDataFacade.Get(StaticDataKeys.KhoanThu) as List<khoanthu>;

            if (khoanThuTable != null && khoanThuTable.Count > 0)
            {
                khoanthu khoanThuRow = khoanThuTable.Find(k => k.KhoanThuId == khoanThuId);

                if (khoanThuRow != null)
                {
                    return khoanThuRow.Ten;
                }
            }

            return CommonConstant.EMPTY;
        }

        public static string GetHocSinhNameByHocSinhId(List<hocsinh> hocSinhDataTable, int hocSinhId)
        {
            if (hocSinhId <= 0)
            {
                return CommonConstant.EMPTY;
            }

            hocsinh hocSinhRow = hocSinhDataTable.Find(h => h.HocSinhId == hocSinhId);

            if (hocSinhRow != null)
            {
                return hocSinhRow.Ten;
            }

            return CommonConstant.EMPTY;
        }

        public static string GetHocSinhFullNameByHocSinhId(List<hocsinh> hocSinhDataTable, int hocSinhId)
        {
            if (hocSinhId <= 0)
            {
                return CommonConstant.EMPTY;
            }

            hocsinh hocSinhRow = hocSinhDataTable.Find(h => h.HocSinhId == hocSinhId);

            if (hocSinhRow != null)
            {
                return hocSinhRow.HoTen;
            }

            return CommonConstant.EMPTY;
        }

        public static Dictionary<int, hocsinh_lop> GetHocSinhLopsByHocSinhIds(qlmamnonEntities entities, List<int> hocSinhIds, DateTime ngay)
        {
            if (ListUtil.IsEmpty(hocSinhIds))
            {
                return null;
            }

            Dictionary<int, hocsinh_lop> hocSinhIdsToHocSinhLops = new Dictionary<int, hocsinh_lop>();
            List<hocsinh_lop> table = entities.getHocSinhLopByHocSinhIdsAndNgay(StringUtil.Join(hocSinhIds, ","), ngay).ToList();

            if (!ListUtil.IsEmpty(table))
            {
                foreach (hocsinh_lop row in table)
                {
                    if (!hocSinhIdsToHocSinhLops.ContainsKey(row.HocSinhId))
                    {
                        hocSinhIdsToHocSinhLops.Add(row.HocSinhId, row);
                    }
                }
            }

            return hocSinhIdsToHocSinhLops;
        }

        public static Dictionary<int, lop> GetLopsByHocSinhIds(qlmamnonEntities entities, List<int> hocSinhIds, DateTime ngay)
        {
            Dictionary<int, hocsinh_lop> hocSinhIdsToLopIds = GetHocSinhLopsByHocSinhIds(entities, hocSinhIds, ngay);
            List<lop> lopTable = StaticDataFacade.Get(StaticDataKeys.LopHoc) as List<lop>;
            Dictionary<int, lop> hocSinhIdsToLopNames = new Dictionary<int, lop>();

            if (ListUtil.IsEmpty(hocSinhIdsToLopIds))
            {
                return hocSinhIdsToLopNames;
            }

            foreach (KeyValuePair<int, hocsinh_lop> pair in hocSinhIdsToLopIds)
            {
                if (pair.Value == null)
                {
                    continue;
                }

                var rows = lopTable.FindAll(l => l.LopId == pair.Value.LopId);

                if (!ListUtil.IsEmpty(rows) && !hocSinhIdsToLopNames.ContainsKey(pair.Key))
                {
                    hocSinhIdsToLopNames.Add(pair.Key, rows[0]);
                }
            }

            return hocSinhIdsToLopNames;
        }

        public static string GetPhanLoaiChiNameByPhieuChiId(qlmamnonEntities entities, Int32 phieuChiId)
        {
            if (phieuChiId < 0)
            {
                return CommonConstant.EMPTY;
            }

            List<phieuchi> phieChiTable = entities.getPhieuChiById(phieuChiId).ToList();

            if (ListUtil.IsEmpty(phieChiTable))
            {
                return CommonConstant.EMPTY;
            }

            phieuchi phieChiRow = phieChiTable.FirstOrDefault();

            if (phieChiRow != null)
            {
                List<phanloaichi> table = StaticDataFacade.Get(StaticDataKeys.PhanLoaiChi) as List<phanloaichi>;
                List<phanloaichi> rows = table.FindAll(p => p.PhanLoaiChiId == phieChiRow.PhanLoaiChiId).ToList();

                if (!ListUtil.IsEmpty(rows))
                {
                    return rows[0].DienGiai;
                }
            }

            return CommonConstant.EMPTY;
        }

        public static string GetMaPhanLoaiChiNameByPhieuChiId(qlmamnonEntities entities, Int32 phieuChiId)
        {
            if (phieuChiId < 0)
            {
                return CommonConstant.EMPTY;
            }

            List<phieuchi> phieChiTable = entities.getPhieuChiById(phieuChiId).ToList();

            if (ListUtil.IsEmpty(phieChiTable))
            {
                return CommonConstant.EMPTY;
            }

            phieuchi phieChiRow = phieChiTable.FirstOrDefault();

            if (phieChiRow != null)
            {
                List<phanloaichi> table = StaticDataFacade.Get(StaticDataKeys.PhanLoaiChi) as List<phanloaichi>;
                List<phanloaichi> rows = table.FindAll(p => p.PhanLoaiChiId == phieChiRow.PhanLoaiChiId);

                if (!ListUtil.IsEmpty(rows))
                {
                    return rows[0].MaPhanLoai;
                }
            }

            return CommonConstant.EMPTY;
        }

        public static string GetPhanLoaiThuById(Int32 phanLoaiThuId)
        {
            if (phanLoaiThuId < 0)
            {
                return CommonConstant.EMPTY;
            }

            List<phanloaithu> table = StaticDataFacade.Get(StaticDataKeys.PhanLoaiThu) as List<phanloaithu>;
            List<phanloaithu> rows = table.FindAll(p => p.PhanLoaiThuId == phanLoaiThuId);

            if (!ListUtil.IsEmpty(rows))
            {
                return rows[0].DienGiai;
            }

            return CommonConstant.EMPTY;
        }
    }
}
