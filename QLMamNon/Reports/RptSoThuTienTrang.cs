using QLMamNon.Properties;

namespace QLMamNon.Reports
{
    public partial class RptSoThuTien : DevExpress.XtraReports.UI.XtraReport
    {
        public RptSoThuTien()
        {
            InitializeComponent();
        }

        private void RptSoThuTien_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!Settings.Default.IsMNHongMinh)
            {
                lblTruong.Text = Settings.Default.Domity;
            }
        }
    }
}
