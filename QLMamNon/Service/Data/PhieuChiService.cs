using System;
using System.Collections.Generic;
using System.Linq;
using ACG.Core.WinForm.Util;
using QLMamNon.Dao;

namespace QLMamNon.Service.Data
{
    public class PhieuChiService : BaseDataService
    {
        public List<phieuchi> LoadPhieuChi(qlmamnonEntities entities)
        {
            List<phieuchi> table = entities.phieuchis.ToList();
            FillPhanLoaiChiForPhieuChiRows(entities, table);

            return table;
        }

        public List<phieuchi> LoadPhieuChiByDateRange(qlmamnonEntities entities, DateTime? fromDate, DateTime? toDate, List<int> phanLoaiChiIds)
        {
            List<phieuchi> table = entities.getPhieuChiByDateRange(fromDate, toDate, StringUtil.JoinWithCommas(phanLoaiChiIds)).ToList();
            FillPhanLoaiChiForPhieuChiRows(entities, table);

            return table;
        }

        public List<phieuchi> LoadPhieuChiByDateRangeWithGroupPhanLoaiChi(qlmamnonEntities entities, DateTime? fromDate, DateTime? toDate, List<int> phanLoaiChiIds)
        {
            List<phieuchi> table = entities.getPhieuChiByDateRangeWithGroupPhanLoaiChi(fromDate, toDate, StringUtil.JoinWithCommas(phanLoaiChiIds)).ToList();
            return table;
        }

        public void FillPhanLoaiChiForPhieuChiRows(qlmamnonEntities entities, List<phieuchi> table)
        {
            foreach (phieuchi row in table)
            {
                row.PhanLoaiChi = StaticDataUtil.GetMaPhanLoaiChiNameByPhieuChiId(entities, row.PhieuChiId);
            }
        }

        public void InsertPhieuChi(qlmamnonEntities entities, DateTime ngay, long soTien, string maPhieu, string ghiChu, int phanLoaiChiId, string noiDung, double soLuong, double donGia)
        {
            phieuchi phieuChi = new phieuchi()
            {
                MaPhieu = maPhieu,
                Ngay = ngay,
                SoTien = soTien,
                GhiChu = ghiChu,
                PhanLoaiChiId = phanLoaiChiId,
                CreatedDate = DateTime.Now,
                NoiDung = noiDung,
                SoLuong = soLuong,
                DonGia = donGia
            };
            entities.phieuchis.Add(phieuChi);
            entities.SaveChanges();
        }

        public void UpdatePhieuChi(qlmamnonEntities entities, phieuchi phieuChiRow, DateTime ngay, long soTien, string maPhieu, string ghiChu, int phanLoaiChiId, string noiDung, double soLuong, double donGia)
        {
            phieuchi phieuChi= entities.phieuchis.Single(p => p.PhieuChiId == phieuChiRow.PhieuChiId);
            phieuChi.MaPhieu = maPhieu;
            phieuChi.SoTien = soTien;
            phieuChi.GhiChu = ghiChu;
            phieuChi.PhanLoaiChiId = phanLoaiChiId;
            phieuChi.NoiDung = noiDung;
            phieuChi.SoLuong = soLuong;
            phieuChi.DonGia = donGia;
            phieuChi.Ngay = ngay;
            entities.SaveChanges();
        }
    }
}
