using QLMamNon.Properties;

namespace QLMamNon.Reports
{
    public partial class RptPhieuThuTien : DevExpress.XtraReports.UI.XtraReport
    {
        public RptPhieuThuTien()
        {
            InitializeComponent();
        }

        private void report_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!Settings.Default.IsMNHongMinh)
            {
                lblTruong.Text = Settings.Default.Domity;
                lblTruongAddress.Text = Settings.Default.DomityAddress;
            }
        }


    }
}
