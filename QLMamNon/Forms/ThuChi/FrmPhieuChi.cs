using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Service.Data;
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmPhieuChi : CRUDForm<phieuchi>
    {
        #region Properties

        #endregion

        public FrmPhieuChi()
        {
            InitializeComponent();

            this.TablePrimaryKey = "PhieuChiId";
            this.DanhMuc = DanhMucConstant.PhieuChi;
            this.FormKey = AppForms.FormPhieuChi;

            this.loadPhieuChi();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, null, null, this.gvMain, this.phieuChiRowBindingSource.DataSource);
        }

        private void loadPhieuChi()
        {
            PhieuChiService phieuChiService = new PhieuChiService();
            this.phieuChiRowBindingSource.DataSource = phieuChiService.LoadPhieuChi(Entities);
        }

        protected override void onAdding()
        {
            FrmTaoPhieuChi frm = (FrmTaoPhieuChi)FormMainFacade.GetForm(AppForms.FormTaoPhieuChi);
            frm.GridView = this.GridViewMain;
            frm.IsEditing = false;

            FormMainFacade.ShowDialog(AppForms.FormTaoPhieuChi);
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.AddedCaption);
        }

        protected override void onEditing()
        {
            FrmTaoPhieuChi frm = (FrmTaoPhieuChi)FormMainFacade.GetForm(AppForms.FormTaoPhieuChi);
            frm.GridView = this.GridViewMain;
            frm.IsEditing = true;
            frm.PhieuChiRow = this.phieuChiRowBindingSource.Current as phieuchi;

            FormMainFacade.ShowDialog(AppForms.FormTaoPhieuChi);
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.ModifiedCaption);
        }

        protected override void onDeleting()
        {
            if (this.GridViewMain.FocusedRowHandle < 0)
            {
                return;
            }

            var confirmResult = System.Windows.Forms.MessageBox.Show(String.Format("Bạn có chắc muốn xóa {0} được chọn không?", this.DanhMuc), String.Format("Xóa {0}", this.DanhMuc),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                int phieuChiId = (int)this.GridViewMain.GetFocusedRowCellValue("PhieuChiId");
                string maPhieu = (string)this.GridViewMain.GetFocusedRowCellValue("MaPhieu");
                int phanLoaiChiId = (int)this.GridViewMain.GetFocusedRowCellValue("PhanLoaiChiId");
                DateTime ngay = (DateTime)this.GridViewMain.GetFocusedRowCellValue("Ngay");
                long soTien = (long)this.GridViewMain.GetFocusedRowCellValue("SoTien");
                double soLuong = (double)this.GridViewMain.GetFocusedRowCellValue("SoLuong");
                double donGia = (double)this.GridViewMain.GetFocusedRowCellValue("DonGia");
                DateTime createdDate = (DateTime)this.GridViewMain.GetFocusedRowCellValue("CreatedDate");

                phieuchi phieuChi = new phieuchi() { PhieuChiId = phieuChiId };
                Entities.Entry(phieuChi).State = EntityState.Deleted;
                Entities.SaveChanges();
                this.loadPhieuChi();
                FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.DeletedCaption);
            }
        }

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            this.onEditing();
        }
    }
}