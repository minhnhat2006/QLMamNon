using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Reports;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmBaoCaoChiTietHoatDongTaiChinh : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        public GridView GridView { get; set; }

        public bool IsEditing { get; set; }

        public phieuthu PhieuThuRow { get; set; }

        private qlmamnonEntities entities;

        #endregion

        public FrmBaoCaoChiTietHoatDongTaiChinh()
        {
            FormKey = AppForms.FormBaoCaoChiTietHoatDongTaiChinh;
            entities = StaticDataFacade.GetQLMNEntities();

            InitializeComponent();

            this.FormClosed += new FormClosedEventHandler(FrmBaoCaoChiTietHoatDongTaiChinh_FormClosed);
        }

        private void FrmBaoCaoChiTietHoatDongTaiChinh_FormClosed(object sender, FormClosedEventArgs e)
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

            RptBaoCaoChiTietHoatDongTaiChinh rpt = new RptBaoCaoChiTietHoatDongTaiChinh();
            rpt.TuNgay.Value = this.dateTuNgay.DateTime;
            rpt.DenNgay.Value = this.dateDenNgay.DateTime;

            FormMainFacade.ShowReport(rpt);
        }

        private void FrmBaoCaoHoatDongTaiChinh_Load(object sender, EventArgs e)
        {
            this.phanLoaiChiRowBindingSource.DataSource = entities.phanloaichis.ToList();
        }
    }
}