using ACG.Core.WinForm.Util;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Entity.Form;
using QLMamNon.Facade;
using QLMamNon.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmBangKeThuTheoLop : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        private qlmamnonEntities entities;

        #endregion

        public FrmBangKeThuTheoLop()
        {
            FormKey = AppForms.FormBangKeThuHocPhi;
            entities = StaticDataFacade.GetQLMNEntities();

            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (this.dateTuNgay.DateTime == null || this.dateDenNgay.DateTime == null)
            {
                MessageBox.Show("Xin vui lòng chọn ngày", "Chọn ngày", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            RptBangKeThuTheoLop rpt = new RptBangKeThuTheoLop();
            DateTime fromDate = DateTimeUtil.StartOfDate(dateTuNgay.DateTime);
            rpt.FromDate.Value = fromDate;
            DateTime toDate = DateTimeUtil.EndOfDate(dateDenNgay.DateTime);
            rpt.ToDate.Value = toDate;
            string fromDateString = fromDate.ToShortDateString();
            string toDateString = toDate.ToShortDateString();
            List<phieuthu> phieuThuDataTable = entities.phieuthus.Where(pt =>
                (pt.Ngay.Year > fromDate.Year || (pt.Ngay.Year == fromDate.Year && pt.Ngay.Month > fromDate.Month) || (pt.Ngay.Year == fromDate.Year && pt.Ngay.Month == fromDate.Month && pt.Ngay.Day >= fromDate.Day)) &&
                (pt.Ngay.Year < toDate.Year || (pt.Ngay.Year == toDate.Year && pt.Ngay.Month < toDate.Month) || (pt.Ngay.Year == toDate.Year && pt.Ngay.Month == toDate.Month && pt.Ngay.Day <= toDate.Day)) &&
                pt.HocSinhId.HasValue).ToList();

            Dictionary<string, Dictionary<string, BangKeThuTheoLopItem>> bangKeThuTheoLopItemsByLop = new Dictionary<string, Dictionary<string, BangKeThuTheoLopItem>>();
            List<BangKeThuTheoLopItem> bangKeThuTheoLopItems = new List<BangKeThuTheoLopItem>();
            foreach (phieuthu item in phieuThuDataTable)
            {
                var lops = StaticDataUtil.GetLopsByHocSinhIds(entities, new List<int>() { item.HocSinhId.Value }, fromDate);
                string lop = (lops.Count > 0 ? lops.First().Value.Name : string.Empty).Trim();
                string ngay = item.Ngay.ToShortDateString();

                if (!string.IsNullOrWhiteSpace(lop))
                {
                    Dictionary<string, BangKeThuTheoLopItem> bangKeThuTheoLopItemsByNgay = new Dictionary<string, BangKeThuTheoLopItem>();
                    if (bangKeThuTheoLopItemsByLop.ContainsKey(lop))
                    {
                        bangKeThuTheoLopItemsByNgay = bangKeThuTheoLopItemsByLop[lop];
                    }
                    else
                    {
                        bangKeThuTheoLopItemsByLop.Add(lop, bangKeThuTheoLopItemsByNgay);
                    }

                    if (bangKeThuTheoLopItemsByNgay.ContainsKey(ngay))
                    {
                        BangKeThuTheoLopItem bangKeThuTheoLopItem = bangKeThuTheoLopItemsByNgay[ngay];
                        bangKeThuTheoLopItem.SoTienNop += item.SoTien;
                        bangKeThuTheoLopItem.SoTienChuyenKhoan += item.SoTienChuyenKhoan;
                    }
                    else
                    {
                        BangKeThuTheoLopItem bangKeThuTheoLopItem = new BangKeThuTheoLopItem()
                        {
                            Ngay = ngay,
                            Lop = lop,
                            SoTienNop = item.SoTien,
                            SoTienChuyenKhoan = item.SoTienChuyenKhoan

                        };
                        bangKeThuTheoLopItemsByNgay.Add(ngay, bangKeThuTheoLopItem);
                        bangKeThuTheoLopItems.Add(bangKeThuTheoLopItem);
                    }
                }
            }

            rpt.bangKeThuTheoLopBindingSource.DataSource = bangKeThuTheoLopItems;
            FormMainFacade.ShowReport(rpt);
        }

        private void FrmBaoCaoHoatDongTaiChinh_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = DateTime.Now;
            dateDenNgay.DateTime = DateTime.Now;
        }
    }
}