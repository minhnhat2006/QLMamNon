using ACG.Core.WinForm.Util;
using QLMamNon.Dao;
using QLMamNon.Facade;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static QLMamNon.Constant.PhanLoaiThuConstant;

namespace QLMamNon.Service.Data
{
    public class PhieuThuService : BaseDataService
    {
        public List<phieuthu> LoadPhieuThu(BindingList<hocsinh> hocSinhTable)
        {
            using (qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities())
            {
                List<phieuthu> table = entities.phieuthus.ToList();

                foreach (phieuthu row in table)
                {
                    if (row.HocSinhId != null)
                    {
                        row.HocSinh = StaticDataUtil.GetHocSinhFullNameByHocSinhId(hocSinhTable.ToList(), row.HocSinhId.Value);
                    }

                    if (row.PhanLoaiThuId != null)
                    {
                        row.PhanLoaiThu = StaticDataUtil.GetPhanLoaiThuById(row.PhanLoaiThuId.Value);
                    }
                }

                return table;
            }
        }

        public void InsertPhieuThu(DateTime ngay, long soTien, long soTienChuyenKhoan, string maPhieu, string ghiChu, int? hocSinhId, int? phanLoaiThuId)
        {
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            phieuthu phieuThu = new phieuthu()
            {
                Ngay = ngay,
                SoTien = soTienChuyenKhoan > 0 ? soTienChuyenKhoan : soTien,
                PaymentType = (soTienChuyenKhoan > 0 ? PaymentType.TRANSFER : PaymentType.CASH).ToString(),
                MaPhieu = maPhieu,
                GhiChu = ghiChu,
                HocSinhId = hocSinhId,
                CreatedDate = DateTime.Now,
                PhanLoaiThuId = phanLoaiThuId
            };
            entities.phieuthus.Add(phieuThu);
            entities.SaveChanges();
        }

        public void UpdatePhieuThu(phieuthu phieuThuRow, DateTime ngay, long soTien, long soTienChuyenKhoan, string maPhieu, string ghiChu, int? hocSinhId, int? phanLoaiThuId)
        {
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            phieuthu phieuThu = entities.phieuthus.FirstOrDefault(p => p.PhieuThuId == phieuThuRow.PhieuThuId);

            if (phieuThu == null)
            {
                // Phiếu thu đã bị xóa ở nơi khác.
                throw new InvalidOperationException("Phiếu thu này không còn tồn tại. Vui lòng tải lại danh sách.");
            }

            phieuThu.SoTien = soTienChuyenKhoan > 0 ? soTienChuyenKhoan : soTien;
            phieuThu.PaymentType = (soTienChuyenKhoan > 0 ? PaymentType.TRANSFER : PaymentType.CASH).ToString();
            phieuThu.MaPhieu = maPhieu;
            phieuThu.GhiChu = ghiChu;
            phieuThu.HocSinhId = hocSinhId;
            phieuThu.PhanLoaiThuId = phanLoaiThuId;
            phieuThu.Ngay = ngay;
            entities.SaveChanges();
        }

        public List<phieuthu> LoadPhieuThuByDateRangeWithGroupPhanLoaiThu(DateTime? fromDate, DateTime? toDate, List<int> phanLoaiThuIds)
        {
            using (qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities())
            {
                List<phieuthu> table = entities.getPhieuThuByDateRangeWithGroupPhanLoaiThu(fromDate, toDate, StringUtil.JoinWithCommas(phanLoaiThuIds)).ToList();

                foreach (phieuthu phieuThuRow in table)
                {
                    if (phieuThuRow.PhanLoaiThuId != null)
                    {
                        phieuThuRow.PhanLoaiThu = StaticDataUtil.GetPhanLoaiThuById(phieuThuRow.PhanLoaiThuId.Value);
                    }
                }

                return table;
            }
        }
    }
}
