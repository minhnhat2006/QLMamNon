using QLMamNon.Properties;

namespace QLMamNon.Reports
{
    public partial class RptSoTheoDoiTaiSan : DevExpress.XtraReports.UI.XtraReport
    {
        public RptSoTheoDoiTaiSan()
        {
            InitializeComponent();
        }

        private void RptSoTheoDoiTaiSan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!Settings.Default.IsMNHongMinh)
            {
                lblTruong.Text = $"UBND QUẬN SƠN TRÀ\n{ Settings.Default.Domity}";
            }
        }
    }
}
