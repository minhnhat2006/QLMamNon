using System;

namespace QLMamNon.Reports
{
    public partial class RptSoThuTienTrang2 : DevExpress.XtraReports.UI.XtraReport
    {
        public RptSoThuTienTrang2()
        {
            InitializeComponent();
        }

        private void RptSoThuTienTrang2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DateTime ngayThangNam = (DateTime)this.Ngay.Value;
        }
    }
}
