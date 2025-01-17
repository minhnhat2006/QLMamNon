using QLMamNon.Properties;
using System;

namespace QLMamNon.Reports
{
    public partial class RptBangKeTongHopThuTienHS : DevExpress.XtraReports.UI.XtraReport
    {
        public RptBangKeTongHopThuTienHS()
        {
            InitializeComponent();
        }

        private void RptBangKeTongHopThuTienHS_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!Settings.Default.IsMNHongMinh)
            {
                lblTruong.Text = $"UBND QUẬN SƠN TRÀ\n{ Settings.Default.Domity}";
            }

            DateTime fromDate = (DateTime)this.FromDate.Value;
            DateTime toDate = (DateTime)this.ToDate.Value;
            this.lblTitle.Text = String.Format("BẢNG KÊ THU HỌC PHÍ THÁNG {0}-{1}", toDate.Month, toDate.Year);
            //this.FilterString = String.Format("[NgayNop] >= #{0:yyyy-MM-dd}# AND [NgayNop] <= #{1:yyyy-MM-dd}#", fromDate, toDate);
        }
    }
}
