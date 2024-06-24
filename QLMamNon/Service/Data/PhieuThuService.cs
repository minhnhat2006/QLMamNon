using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ACG.Core.WinForm.Util;
using QLMamNon.Dao;
using QLMamNon.Facade;

namespace QLMamNon.Service.Data
{
    public class PhieuThuService : BaseDataService
    {
        public List<phieuthu> LoadPhieuThu(BindingList<hocsinh> hocSinhTable)
        {
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
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

        public void InsertPhieuThu(DateTime ngay, long soTien, string maPhieu, string ghiChu, int? hocSinhId, int? phanLoaiThuId)
        {
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            phieuthu phieuThu = new phieuthu()
            {
                Ngay = ngay,
                SoTien = soTien,
                MaPhieu = maPhieu,
                GhiChu = ghiChu,
                HocSinhId = hocSinhId,
                CreatedDate = DateTime.Now,
                PhanLoaiThuId = phanLoaiThuId
            };
            entities.phieuthus.Add(phieuThu);
            entities.SaveChanges();
        }

        public void UpdatePhieuThu(phieuthu phieuThuRow, DateTime ngay, long soTien, string maPhieu, string ghiChu, int? hocSinhId, int? phanLoaiThuId)
        {
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            phieuthu phieuThu = entities.phieuthus.FirstOrDefault(p => p.PhieuThuId == phieuThuRow.PhieuThuId);
            phieuThu.SoTien = soTien;
            phieuThu.MaPhieu = maPhieu;
            phieuThu.GhiChu = ghiChu;
            phieuThu.HocSinhId = hocSinhId;
            phieuThu.PhanLoaiThuId = phanLoaiThuId;
            phieuThu.Ngay = ngay;
            entities.SaveChanges();
        }

        public List<phieuthu> LoadPhieuThuByDateRangeWithGroupPhanLoaiThu(DateTime? fromDate, DateTime? toDate, List<int> phanLoaiThuIds)
        {
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            List<phieuthu> table = entities.getPhieuThuByDateRangeWithGroupPhanLoaiThu(fromDate, toDate, StringUtil.JoinWithCommas(phanLoaiThuIds)).ToList();

            foreach (phieuthu phieuThuRow in table)
            {
                phieuThuRow.PhanLoaiThu = StaticDataUtil.GetPhanLoaiThuById(phieuThuRow.PhanLoaiThuId.Value);
            }

            return table;
        }
    }
}
