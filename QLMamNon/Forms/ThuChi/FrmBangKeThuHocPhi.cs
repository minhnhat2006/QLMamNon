using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ACG.Core.WinForm.Util;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Entity.Form;
using QLMamNon.Facade;
using QLMamNon.Reports;
using QLMamNon.Service.Data;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmBangKeThuHocPhi : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        private qlmamnonEntities entities;

        #endregion

        public FrmBangKeThuHocPhi()
        {
            FormKey = AppForms.FormBangKeThuHocPhi;
            entities = StaticDataFacade.GetQLMNEntities();

            InitializeComponent();

            this.FormClosed += new FormClosedEventHandler(FrmBangKeThuHocPhi_FormClosed);
        }

        private void FrmBangKeThuHocPhi_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.entities != null)
            {
                this.entities.Dispose();
                this.entities = null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (this.dateTuNgay.DateTime == DateTime.MinValue || this.dateDenNgay.DateTime == DateTime.MinValue)
            {
                MessageBox.Show("Xin vui lòng chọn ngày", "Chọn ngày", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.dateTuNgay.DateTime > this.dateDenNgay.DateTime)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc", "Chọn ngày", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SoThuTienService soThuTienService = new SoThuTienService();
            RptBangKeTongHopThuTienHS rpt = new RptBangKeTongHopThuTienHS();
            DateTime fromDate = DateTimeUtil.StartOfDate(dateTuNgay.DateTime);
            DateTime toDate = DateTimeUtil.EndOfDate(dateDenNgay.DateTime);
            rpt.FromDate.Value = fromDate;
            rpt.ToDate.Value = toDate;

            List<BangKeThuTienItem> allBangKeThuTienItems = soThuTienService.GetBangKeTongHopThuTien(entities, toDate, (int?)cmLop.EditValue);
            List<BangKeThuTienItem> displayedBangKeThuTienItems = new List<BangKeThuTienItem>();
            int stt = 1;

            foreach (BangKeThuTienItem item in allBangKeThuTienItems)
            {
                if (item.NgayNop >= fromDate && item.NgayNop <= toDate)
                {
                    item.STT = stt++;
                    displayedBangKeThuTienItems.Add(item);
                }
            }

            rpt.viewBangThuTienRowbindingSource.DataSource = displayedBangKeThuTienItems;
            FormMainFacade.ShowReport(rpt);
        }

        private void FrmBaoCaoHoatDongTaiChinh_Load(object sender, EventArgs e)
        {
            this.lopRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.LopHoc);
        }
    }
}