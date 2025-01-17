using QLMamNon.Components.Data.Static;
using QLMamNon.Dao;
using QLMamNon.Entity.Form;
using QLMamNon.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLMamNon.Reports
{
    public partial class RptBaoCaoHoatDongTaiChinh : DevExpress.XtraReports.UI.XtraReport
    {
        public RptBaoCaoHoatDongTaiChinh()
        {
            InitializeComponent();
        }

        private void RptBaoCaoHoatDongTaiChinh_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            DateTime tuNgay = (DateTime)TuNgay.Value;
            DateTime denNgay = (DateTime)DenNgay.Value;
            this.lblNgay.Text = String.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", tuNgay, denNgay);
            long soTienThu = Convert.ToInt64(entities.getPhieuThuForReportBCHDTC(tuNgay, denNgay));
            this.lblSoTienThu.Text = String.Format("{0:n0}", soTienThu);

            List<PhieuChiBCHDTC> phieuChiList = new List<PhieuChiBCHDTC>();
            List<phieuchi> phieuChiTable = entities.getPhieuChiListForReportBCHDTC(tuNgay, denNgay, (string)this.PhanLoaiChiIds.Value).ToList();
            long totalSoTien = 0;
            foreach (phieuchi row in phieuChiTable)
            {
                int phanLoaiChiId = row.PhanLoaiChiId;
                string dienGiai = row.DienGiai;
                string maLoaiChi = row.MaPhanLoai;
                long soTien = row.SoTien;
                PhieuChiBCHDTC phieuChiBCHDTC = new PhieuChiBCHDTC(phanLoaiChiId, dienGiai, maLoaiChi, soTien);
                phieuChiList.Add(phieuChiBCHDTC);
                totalSoTien += soTien;
            }

            this.lblLoiNhuan.Text = String.Format("{0:n0}", soTienThu - totalSoTien);

            this.phieuChiBCHDTCDataSource.DataSource = phieuChiList;
            this.phieuChiBCHDTCDataSource.Fill();
        }
    }
}
