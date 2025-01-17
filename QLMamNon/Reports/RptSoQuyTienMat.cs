using QLMamNon.Properties;
using System;

namespace QLMamNon.Reports
{
    public partial class RptSoQuyTienMat : DevExpress.XtraReports.UI.XtraReport
    {
        public RptSoQuyTienMat()
        {
            InitializeComponent();
        }

        private void RptSoQuyTienMat_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!Settings.Default.IsMNHongMinh)
            {
                lblTruong.Text = $"UBND QUẬN SƠN TRÀ\n{ Settings.Default.Domity}";
            }

            DateTime tuNgay = (DateTime)FromDate.Value;
            DateTime denNgay = (DateTime)ToDate.Value;
            lblNgay.Text = String.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", tuNgay, denNgay);
        }
    }
}
