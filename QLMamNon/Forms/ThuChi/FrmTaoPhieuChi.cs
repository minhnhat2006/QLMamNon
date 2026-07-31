using System;
using System.Data.Entity;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Service.Data;
using static QLMamNon.Constant.PhanLoaiThuConstant;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmTaoPhieuChi : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        public GridView GridView { get; set; }

        public bool IsEditing { get; set; }

        public bool IsLoading { get; set; }

        /// <summary>
        /// True khi phiếu chi đã được lưu thành công trong lần mở form này.
        /// Form gọi phải tự đặt lại về false trước khi ShowDialog.
        /// </summary>
        public bool IsSaved { get; set; }

        public phieuchi PhieuChiRow { get; set; }

        private qlmamnonEntities entities;

        private bool isStaticDataLoaded;

        #endregion

        public FrmTaoPhieuChi()
        {
            this.FormKey = AppForms.FormTaoPhieuChi;
            entities = StaticDataFacade.GetQLMNEntities();
            InitializeComponent();
        }

        private void FrmTaoPhieuChi_Load(object sender, EventArgs e)
        {
            this.InitFormData();
        }

        /// <summary>
        /// Nạp dữ liệu vào các control của form. Phải gọi trước mỗi lần ShowDialog:
        /// FormFactory dùng lại cùng một instance nên sự kiện Load chỉ chạy đúng một
        /// lần, các lần mở sau form sẽ còn nguyên giá trị rỗng do resetForm() để lại
        /// và dxValidationProvider.Validate() sẽ chặn việc lưu.
        /// </summary>
        public void InitFormData()
        {
            if (!this.isStaticDataLoaded)
            {
                entities.phanloaichis.Load();
                this.phanLoaiChiRowBindingSource.DataSource = entities.phanloaichis.Local.ToBindingList();
                this.isStaticDataLoaded = true;
            }

            if (this.IsEditing && this.PhieuChiRow != null)
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
            this.cbPaymentType.SelectedIndex = this.PhieuChiRow.PaymentTypeEnum == PaymentType.TRANSFER ? 1 : 0;
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

            this.IsSaved = true;

            //if (this.GridView != null)
            //{
            //    this.GridView.BeginUpdate();
            //    try
            //    {
            //        object focusedRowId = this.GridView.GetFocusedRowCellValue("PhieuChiId");

            //        BindingSource phieuChiBindingSource = this.GridView.GridControl.DataSource as BindingSource;
            //        PhieuChiService phieuChiService = new PhieuChiService();
            //        phieuChiBindingSource.DataSource = phieuChiService.LoadPhieuChi(entities);

            //        if (focusedRowId != null)
            //        {
            //            int newRowHandle = this.GridView.LocateByValue("PhieuChiId", focusedRowId);
            //            if (newRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            //            {
            //                this.GridView.FocusedRowHandle = newRowHandle;
            //                this.GridView.MakeRowVisible(newRowHandle);
            //            }
            //        }
            //    }
            //    finally
            //    {
            //        this.GridView.EndUpdate();
            //    }
            //}
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

            long soTien = this.cbPaymentType.SelectedIndex == 0 ? (long)this.txtSoTien.Value : 0;
            long soTienChuyenKhoan = this.cbPaymentType.SelectedIndex == 1 ? (long)this.txtSoTien.Value : 0;

            PhieuChiService phieuChiService = new PhieuChiService();
            phieuChiService.InsertPhieuChi(entities, ngay, soTien, soTienChuyenKhoan, maPhieu, ghiChu, phanLoaiChiId, noiDung, soLuong, donGia);
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

            long soTien = this.cbPaymentType.SelectedIndex == 0 ? (long)this.txtSoTien.Value : 0;
            long soTienChuyenKhoan = this.cbPaymentType.SelectedIndex == 1 ? (long)this.txtSoTien.Value : 0;

            PhieuChiService phieuChiService = new PhieuChiService();
            phieuChiService.UpdatePhieuChi(entities, this.PhieuChiRow, ngay, soTien, soTienChuyenKhoan, maPhieu, ghiChu, phanLoaiChiId, noiDung, soLuong, donGia);
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
            this.cbPaymentType.SelectedIndex = 0;
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