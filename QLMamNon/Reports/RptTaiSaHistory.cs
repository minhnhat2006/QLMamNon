using QLMamNon.Properties;

namespace QLMamNon.Reports
{
    public partial class RptTaiSaHistory : DevExpress.XtraReports.UI.XtraReport
    {
        public RptTaiSaHistory()
        {
            InitializeComponent();
        }

        private void RptTaiSaHistory_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!Settings.Default.IsMNHongMinh)
            {
                lblTruong.Text = $"UBND QUẬN SƠN TRÀ\n{ Settings.Default.Domity}";
            }
        }
    }
}
