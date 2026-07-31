using System;
using System.Collections.Generic;
using System.Linq;
using ACG.Core.WinForm.Util;
using QLMamNon.Dao;
using static QLMamNon.Constant.PhanLoaiThuConstant;

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
            // Explicitly define nullable DateTime variables so EF maps them correctly to SQL NULL
            DateTime? finalFromDate = fromDate.HasValue ? fromDate.Value.Date : (DateTime?)null;
            DateTime? finalToDate = toDate.HasValue ? toDate.Value.Date : (DateTime?)null;

            // Convert your list to a comma-separated string
            string idsString = StringUtil.JoinWithCommas(phanLoaiChiIds);

            // Execute the stored procedure
            List<phieuchi> table = entities.getPhieuChiByDateRange(finalFromDate, finalToDate, idsString).ToList();

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

        public void InsertPhieuChi(qlmamnonEntities entities, DateTime ngay, long soTien, long soTienChuyenKhoan, string maPhieu, string ghiChu, int phanLoaiChiId, string noiDung, double soLuong, double donGia)
        {
            phieuchi phieuChi = new phieuchi()
            {
                MaPhieu = maPhieu,
                Ngay = ngay,
                SoTien = soTienChuyenKhoan > 0 ? soTienChuyenKhoan : soTien,
                PaymentType = (soTienChuyenKhoan > 0 ? PaymentType.TRANSFER : PaymentType.CASH).ToString(),
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

        public void UpdatePhieuChi(qlmamnonEntities entities, phieuchi phieuChiRow, DateTime ngay, long soTien, long soTienChuyenKhoan, string maPhieu, string ghiChu, int phanLoaiChiId, string noiDung, double soLuong, double donGia)
        {
            phieuchi phieuChi= entities.phieuchis.Single(p => p.PhieuChiId == phieuChiRow.PhieuChiId);
            phieuChi.MaPhieu = maPhieu;
            phieuChi.SoTien = soTienChuyenKhoan > 0 ? soTienChuyenKhoan : soTien;
            phieuChi.PaymentType = (soTienChuyenKhoan > 0 ? PaymentType.TRANSFER : PaymentType.CASH).ToString();
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
