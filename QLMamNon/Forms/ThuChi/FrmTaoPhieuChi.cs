using System;
using System.Data.Entity;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Service.Data;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmTaoPhieuChi : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        public GridView GridView { get; set; }

        public bool IsEditing { get; set; }

        public bool IsLoading { get; set; }

        public phieuchi PhieuChiRow { get; set; }

        private qlmamnonEntities entities;

        #endregion

        public FrmTaoPhieuChi()
        {
            this.FormKey = AppForms.FormTaoPhieuChi;
            entities = StaticDataFacade.GetQLMNEntities();
            InitializeComponent();
        }

        private void FrmTaoPhieuChi_Load(object sender, EventArgs e)
        {
            entities.phanloaichis.Load();
            this.phanLoaiChiRowBindingSource.DataSource = entities.phanloaichis.Local.ToBindingList();

            if (this.IsEditing)
            {
                this.loadPhieuChi();
            }
            else
            {
                this.resetForm();
            }
        }

        protected void FrmTaoPhieuChi_Activated(object sender, EventArgs e)
        {
            FormMainFacade.SetFormCaption(this.FormKey);
        }

        private void FrmTaoPhieuChi_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.IsEditing = false;
        }

        private void btnLuuTao_Click(object sender, EventArgs e)
        {
            if (!this.dxValidationProvider.Validate())
            {
                return;
            }

            this.luuPhieuChi();
            this.resetForm();
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.AddedAndAddingPhieuChiCaption);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!this.dxValidationProvider.Validate())
            {
                return;
            }

            this.btnLuuTao_Click(sender, e);
            this.Close();
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.AddedPhieuChiCaption);
        }

        private void loadPhieuChi()
        {
            this.dateNgay.DateTime = this.PhieuChiRow.Ngay;
            this.txtSoLuong.Value = (decimal)this.PhieuChiRow.SoLuong;
            this.txtDonGia.Value = (decimal)this.PhieuChiRow.DonGia;
            this.txtSoTien.Value = this.PhieuChiRow.SoTien;
            this.txtMaPhieu.Text = this.PhieuChiRow.MaPhieu;
            this.txtGhiChu.Text = this.PhieuChiRow.GhiChu;
            this.cmbPhanLoaiChi.EditValue = this.PhieuChiRow.PhanLoaiChiId;
            this.txtNoiDung.Text = this.PhieuChiRow.NoiDung;
        }

        private void luuPhieuChi()
        {
            if (this.IsEditing)
            {
                this.updatePhieuChi();
            }
            else
            {
                this.insertPhieuChi();
            }

            if (this.GridView != null)
            {
                BindingSource phieuChiBindingSource = this.GridView.GridControl.DataSource as BindingSource;
                PhieuChiService phieuChiService = new PhieuChiService();
                phieuChiBindingSource.DataSource = phieuChiService.LoadPhieuChi(entities);
            }
        }

        private void insertPhieuChi()
        {
            DateTime ngay = this.dateNgay.DateTime;
            string maPhieu = this.txtMaPhieu.Text;
            string ghiChu = this.txtGhiChu.Text;
            int phanLoaiChiId = (int)this.cmbPhanLoaiChi.EditValue;
            string noiDung = txtNoiDung.Text;
            double soLuong = (double)txtSoLuong.Value;
            double donGia = (double)txtDonGia.Value;
            long soTien = (long)this.txtSoTien.Value;
            PhieuChiService phieuChiService = new PhieuChiService();
            phieuChiService.InsertPhieuChi(entities, ngay, soTien, maPhieu, ghiChu, phanLoaiChiId, noiDung, soLuong, donGia);
        }

        private void updatePhieuChi()
        {
            DateTime ngay = this.dateNgay.DateTime;
            string maPhieu = this.txtMaPhieu.Text;
            string ghiChu = this.txtGhiChu.Text;
            int phanLoaiChiId = (int)this.cmbPhanLoaiChi.EditValue;
            string noiDung = txtNoiDung.Text;
            double soLuong = (double)txtSoLuong.Value;
            double donGia = (double)txtDonGia.Value;
            long soTien = (long)this.txtSoTien.Value;
            PhieuChiService phieuChiService = new PhieuChiService();
            phieuChiService.UpdatePhieuChi(entities, this.PhieuChiRow, ngay, soTien, maPhieu, ghiChu, phanLoaiChiId, noiDung, soLuong, donGia);
        }

        private void resetForm()
        {
            this.txtMaPhieu.Text = "";
            this.txtGhiChu.Text = "";
            this.cmbPhanLoaiChi.EditValue = null;
            this.txtNoiDung.Text = "";
            this.txtSoLuong.Value = 0;
            this.txtDonGia.Value = 0;
            this.txtSoTien.Value = 0;
        }

        private void txtSoLuong_EditValueChanged(object sender, EventArgs e)
        {
            this.txtSoTien.Value = this.txtSoLuong.Value * this.txtDonGia.Value;
        }

        private void txtDonGia_EditValueChanged(object sender, EventArgs e)
        {
            this.txtSoTien.Value = this.txtSoLuong.Value * this.txtDonGia.Value;
        }

        private void cmbPhanLoaiChi_EditValueChanged(object sender, EventArgs e)
        {
            this.txtNoiDung.Text = (String)cmbPhanLoaiChi.GetColumnValue("DienGiai");
        }
    }
}