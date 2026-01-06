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
    public partial class FrmBaoCaoSoLuongHocSinhTheoLop : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        private qlmamnonEntities entities;

        #endregion

        public FrmBaoCaoSoLuongHocSinhTheoLop()
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
            if (this.dateFromMonth.DateTime == null || this.dateToMonth.DateTime == null)
            {
                MessageBox.Show("Xin vui lòng chọn ngày", "Chọn ngày", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.dateFromMonth.DateTime > this.dateToMonth.DateTime)
            {
                MessageBox.Show("Tháng/năm không hợp lệ", "Chọn ngày", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime selectedFromDate = dateFromMonth.DateTime;
            DateTime selectedToDate = dateToMonth.DateTime;
            int monthCount = ((selectedToDate.Year - selectedFromDate.Year) * 12) + selectedToDate.Month - selectedFromDate.Month + 1;
            if (monthCount > 12)
            {
                MessageBox.Show("Xin chọn tối đa 12 tháng", "Chọn ngày", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int fromMonth = selectedFromDate.Month;
            int fromYear = selectedFromDate.Year;
            int toMonth = selectedToDate.Month;
            int toYear = selectedToDate.Year;
            DateTime fromDate = DateTimeUtil.DateEndOfMonth(dateFromMonth.DateTime);
            DateTime toDate = DateTimeUtil.DateEndOfMonth(dateToMonth.DateTime);

            List<BaoCaoSoLuongHocSinhTheoLopItem> baoCaoSoLuongHocSinhTheoLopItems = new List<BaoCaoSoLuongHocSinhTheoLopItem>();
            for (DateTime date = fromDate; date < toDate; date = date.AddMonths(1))
            {
                Dictionary<int, BaoCaoSoLuongHocSinhTheoLopItem> baoCaoSoLuongHocSinhTheoLopItemsMap = new Dictionary<int, BaoCaoSoLuongHocSinhTheoLopItem>();
                List<hocsinh> hocsinhList = entities.getHocSinhByLopAndNgay(null, date).ToList();
                foreach (var hocsinhItem in hocsinhList)
                {
                    Dictionary<int, lop> lopMap = StaticDataUtil.GetLopsByHocSinhIds(entities, new List<int>() { hocsinhItem.HocSinhId }, date);
                    if (lopMap.Count == 0) continue;

                    lop lop = lopMap.First().Value;
                    BaoCaoSoLuongHocSinhTheoLopItem baoCaoSoLuongHocSinhTheoLopItem = null;
                    if (baoCaoSoLuongHocSinhTheoLopItemsMap.ContainsKey(lop.LopId))
                    {
                        baoCaoSoLuongHocSinhTheoLopItem = baoCaoSoLuongHocSinhTheoLopItemsMap[lop.LopId];
                    }
                    else
                    {
                        baoCaoSoLuongHocSinhTheoLopItem = new BaoCaoSoLuongHocSinhTheoLopItem()
                        {
                            Lop = lop.Name,
                            NamThang = date,
                            SoLuong = 0
                        };
                        baoCaoSoLuongHocSinhTheoLopItemsMap.Add(lop.LopId, baoCaoSoLuongHocSinhTheoLopItem);
                    }

                    baoCaoSoLuongHocSinhTheoLopItem.SoLuong++;
                }

                baoCaoSoLuongHocSinhTheoLopItems.AddRange(baoCaoSoLuongHocSinhTheoLopItemsMap.Values);
            }

            RptBaoCaoSoLuongHocSinhTheoLop rpt = new RptBaoCaoSoLuongHocSinhTheoLop();
            rpt.FromYearMonth.Value = fromDate;
            rpt.ToYearMonth.Value = toDate;
            rpt.baoCaoSoLuongHocSinhTheoLopDataSource.DataSource = baoCaoSoLuongHocSinhTheoLopItems.OrderBy(item => item.NamThang).ToList();
            FormMainFacade.ShowReport(rpt);
        }

        private void FrmBaoCaoHoatDongTaiChinh_Load(object sender, EventArgs e)
        {
            dateFromMonth.DateTime = DateTime.Now.AddMonths(-11);
            dateToMonth.DateTime = DateTime.Now;
        }
    }
}