using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Entity.Form;
using QLMamNon.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLMamNon.Reports
{
    public partial class RptBaoCaoChiTietHoatDongTaiChinh : DevExpress.XtraReports.UI.XtraReport
    {
        public RptBaoCaoChiTietHoatDongTaiChinh()
        {
            InitializeComponent();
        }

        private void RptBaoCaoHoatDongTaiChinh_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DateTime tuNgay = (DateTime)TuNgay.Value;
            DateTime denNgay = (DateTime)DenNgay.Value;
            this.lblNgay.Text = String.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", tuNgay, denNgay);

            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            List<PhieuChiBCHDTC> phieuChiList = new List<PhieuChiBCHDTC>();
            List<phieuchi> phieuChiTable = entities.getPhieuChiListForReportBCHDTC(tuNgay, denNgay, CommonConstant.EMPTY).ToList();

            foreach (phieuchi row in phieuChiTable)
            {
                int phanLoaiChiId = row.PhanLoaiChiId;
                string dienGiai = row.DienGiai;
                string maLoaiChi = null;
                long soTien = row.SoTien;
                PhieuChiBCHDTC phieuChiBCHDTC = new PhieuChiBCHDTC(phanLoaiChiId, dienGiai, maLoaiChi, soTien);
                phieuChiList.Add(phieuChiBCHDTC);
            }

            this.phieuChiBCHDTCDataSource.DataSource = phieuChiList;
            this.phieuChiBCHDTCDataSource.Fill();
        }
    }
}
