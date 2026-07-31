using ACG.Core.WinForm.Util;
using DevExpress.XtraEditors;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Entity.Form;
using QLMamNon.Facade;
using QLMamNon.Reports;
using QLMamNon.Service.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static QLMamNon.Constant.PhanLoaiThuConstant;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmSoQuyTienMatTheoNgay : XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        private qlmamnonEntities entities;

        #endregion

        public FrmSoQuyTienMatTheoNgay()
        {
            FormKey = AppForms.FormSoQuyTienMatTheoNgay;
            entities = StaticDataFacade.GetQLMNEntities();

            InitializeComponent();

            this.dateTuNgay.EditValue = DateTime.Now;
            this.dateDenNgay.EditValue = DateTime.Now;
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

            if (chkTon.Checked && StringUtil.IsEmpty(txtTon.Text))
            {
                MessageBox.Show("Xin vui lòng nhập số tiền tồn tháng trước", "Nhập số tiền tồn tháng trước", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<int> phanLoaiThuIds = new List<int>();
            int[] selectedThuRowHandlers = this.gvThu.GetSelectedRows();

            if (ArrayUtil.IsEmpty(selectedThuRowHandlers))
            {
                MessageBox.Show("Xin vui lòng chọn Phân loại thu", "Chọn Phân loại thu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (int rowHandler in selectedThuRowHandlers)
            {
                int phanLoaiThuId = (int)this.gvThu.GetRowCellValue(rowHandler, "PhanLoaiThuId");
                phanLoaiThuIds.Add(phanLoaiThuId);
            }

            List<int> phanLoaiChiIds = new List<int>();
            int[] selectedRowHandlers = this.gvMain.GetSelectedRows();

            if (ArrayUtil.IsEmpty(selectedRowHandlers))
            {
                MessageBox.Show("Xin vui lòng chọn Mã loại chi", "Chọn Mã loại chi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (int rowHandler in selectedRowHandlers)
            {
                int phanLoaiChiId = (int)this.gvMain.GetRowCellValue(rowHandler, "PhanLoaiChiId");
                phanLoaiChiIds.Add(phanLoaiChiId);
            }

            DateTime fromDate = DateTimeUtil.StartOfDate(this.dateTuNgay.DateTime);
            DateTime toDate = DateTimeUtil.EndOfDate(this.dateDenNgay.DateTime);

            decimal soTienTonDauKy = findSoTienTonDauKy(toDate);

            SortedList SoQuyTienMatTheoNgayMap = new SortedList();
            SoQuyTienMatTheoNgayItem SoQuyTienMatTheoNgayDauKy = new SoQuyTienMatTheoNgayItem()
            {
                Ngay = fromDate,
                GhiChu = "Số tồn đầu kỳ",
                SoTienTon = (double)soTienTonDauKy
            };
            SoQuyTienMatTheoNgayMap.Add(DateTime.MinValue, SoQuyTienMatTheoNgayDauKy);

            addPhieuThuToReport(SoQuyTienMatTheoNgayMap, fromDate, toDate, phanLoaiThuIds);
            addPhieuChiToReport(SoQuyTienMatTheoNgayMap, fromDate, toDate, phanLoaiChiIds);
            List<SoQuyTienMatTheoNgayItem> SoQuyTienMatTheoNgay = calculateSoTienTonForSoQuyTienMatTheoNgayItems(SoQuyTienMatTheoNgayMap);

            RptSoQuyTienMatTheoNgay rpt = new RptSoQuyTienMatTheoNgay();
            rpt.FromDate.Value = fromDate;
            rpt.ToDate.Value = toDate;
            rpt.bindingSource.DataSource = SoQuyTienMatTheoNgay;
            FormMainFacade.ShowReport(rpt);
        }

        private decimal findSoTienTonDauKy(DateTime toDate)
        {
            decimal soTienTonDauKy = txtTon.Value;

            if (!chkTon.Checked)
            {
                SoThuTienService soThuTienService = new SoThuTienService();
                soTienTonDauKy = soThuTienService.GetSoTienTonDauKy(entities, toDate);
            }
            return soTienTonDauKy;
        }

        private static List<SoQuyTienMatTheoNgayItem> calculateSoTienTonForSoQuyTienMatTheoNgayItems(SortedList SoQuyTienMatTheoNgayMap)
        {
            List<SoQuyTienMatTheoNgayItem> SoQuyTienMatTheoNgay = new List<SoQuyTienMatTheoNgayItem>();
            SoQuyTienMatTheoNgayItem prevSoQuyTienMatTheoNgayItem = null;

            foreach (var item in SoQuyTienMatTheoNgayMap.GetValueList())
            {
                SoQuyTienMatTheoNgayItem SoQuyTienMatTheoNgayItem = item as SoQuyTienMatTheoNgayItem;

                if (prevSoQuyTienMatTheoNgayItem != null)
                {
                    SoQuyTienMatTheoNgayItem.SoTienTon = prevSoQuyTienMatTheoNgayItem.SoTienTon + SoQuyTienMatTheoNgayItem.SoTienThuTM + SoQuyTienMatTheoNgayItem.SoTienThuCK - (SoQuyTienMatTheoNgayItem.SoTienChiTM + SoQuyTienMatTheoNgayItem.SoTienChiCK);
                }

                prevSoQuyTienMatTheoNgayItem = SoQuyTienMatTheoNgayItem;
                SoQuyTienMatTheoNgay.Add(SoQuyTienMatTheoNgayItem);
            }
            return SoQuyTienMatTheoNgay;
        }

        private void addPhieuChiToReport(SortedList SoQuyTienMatTheoNgayMap, DateTime fromDate, DateTime toDate, List<int> phanLoaiChiIds)
        {
            PhieuChiService phieuChiService = new PhieuChiService();
            List<phieuchi> phieuChiDataTable = phieuChiService.LoadPhieuChiByDateRange(entities, fromDate, toDate, phanLoaiChiIds);
            foreach (phieuchi phieuChiRow in phieuChiDataTable)
            {
                SoQuyTienMatTheoNgayItem SoQuyTienMatTheoNgayItemChi = new SoQuyTienMatTheoNgayItem()
                {
                    MucChi = phieuChiRow.PhanLoaiChi,
                    NoiDungChi = phieuChiRow.NoiDung,
                    GhiChu = phieuChiRow.GhiChu,
                    Ngay = phieuChiRow.Ngay.AddMilliseconds(SoQuyTienMatTheoNgayMap.Count),
                    SoChungTu = phieuChiRow.MaPhieu,
                    SoTienChiTM = phieuChiRow.PaymentTypeEnum == PaymentType.TRANSFER ? 0 : phieuChiRow.SoTien,
                    SoTienChiCK = phieuChiRow.PaymentTypeEnum != PaymentType.TRANSFER ? 0 : phieuChiRow.SoTien,
                };
                SoQuyTienMatTheoNgayMap.Add(SoQuyTienMatTheoNgayItemChi.Ngay, SoQuyTienMatTheoNgayItemChi);
            }
        }

        private void addPhieuThuToReport(SortedList SoQuyTienMatTheoNgayMap, DateTime fromDate, DateTime toDate, List<int> phanLoaiThuIds)
        {
            bool hasFilterIds = phanLoaiThuIds != null && phanLoaiThuIds.Count > 0;
            List<phieuthu> phieuThuDataTable = entities.phieuthus.Where(pt => pt.Ngay >= fromDate && pt.Ngay <= toDate && pt.PhanLoaiThuId.HasValue && pt.PhanLoaiThuId.Value > 0 && (!hasFilterIds || phanLoaiThuIds.Contains(pt.PhanLoaiThuId.Value))).ToList();
            Dictionary<string, SoQuyTienMatTheoNgayItem> groupDateToSoQuyTienMatTheoNgayItemsMap = new Dictionary<string, SoQuyTienMatTheoNgayItem>();

            foreach (phieuthu phieuThuRow in phieuThuDataTable)
            {
                phieuThuRow.PhanLoaiThu = (!phieuThuRow.PhanLoaiThuId.HasValue || phieuThuRow.PhanLoaiThuId.Value == 0) ? "Thu tiền học phí" : StaticDataUtil.GetPhanLoaiThuById(phieuThuRow.PhanLoaiThuId.Value);

                int groupDate = getGroupDate(fromDate, phieuThuRow);
                DateTime dateOfGroup = fromDate.AddDays(groupDate);

                if (dateOfGroup > toDate)
                {
                    dateOfGroup = toDate;
                }

                string key = StringUtil.Join(new int[] { groupDate, phieuThuRow.PhanLoaiThuId.Value }, "~");
                if (!groupDateToSoQuyTienMatTheoNgayItemsMap.ContainsKey(key))
                {
                    groupDateToSoQuyTienMatTheoNgayItemsMap.Add(key, new SoQuyTienMatTheoNgayItem()
                    {
                        NoiDungThu = phieuThuRow.PhanLoaiThu,
                        Ngay = phieuThuRow.Ngay,
                        SoTienThuTM = 0,
                        SoTienThuCK = 0,
                    });
                }
            }

            foreach (phieuthu phieuThuRow in phieuThuDataTable)
            {
                int groupDate = getGroupDate(fromDate, phieuThuRow);
                string key = StringUtil.Join(new int[] { groupDate, phieuThuRow.PhanLoaiThuId.Value }, "~");
                SoQuyTienMatTheoNgayItem SoQuyTienMatTheoNgayItemThu = groupDateToSoQuyTienMatTheoNgayItemsMap[key];

                if (phieuThuRow.PaymentTypeEnum == PaymentType.TRANSFER)
                {
                    SoQuyTienMatTheoNgayItemThu.SoTienThuCK += phieuThuRow.SoTien;
                }
                else
                {
                    SoQuyTienMatTheoNgayItemThu.SoTienThuTM += phieuThuRow.SoTien;
                }
            }

            int addedCount = 0;
            foreach (SoQuyTienMatTheoNgayItem SoQuyTienMatTheoNgayItemThu in groupDateToSoQuyTienMatTheoNgayItemsMap.Values)
            {
                SoQuyTienMatTheoNgayMap.Add(SoQuyTienMatTheoNgayItemThu.Ngay.AddMilliseconds(addedCount++), SoQuyTienMatTheoNgayItemThu);
            }
        }

        private static int getGroupDate(DateTime fromDate, phieuthu phieuThuRow)
        {
            int numberOfDays = (int)(phieuThuRow.Ngay - fromDate).TotalDays;
            int groupDate = numberOfDays;
            return groupDate;
        }

        private void FrmBaoCaoHoatDongTaiChinh_Load(object sender, EventArgs e)
        {
            this.phanLoaiChiRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.PhanLoaiChi);
            this.phanLoaiThuRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.PhanLoaiThu);
        }

        private void FrmSoQuyTienMatTheoNgay_Shown(object sender, EventArgs e)
        {
            this.gvThu.SelectAll();
            this.gvMain.SelectAll();
        }

        private void chkTon_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chk = (CheckEdit)sender;
            txtTon.Enabled = chk.Checked;
        }
    }
}