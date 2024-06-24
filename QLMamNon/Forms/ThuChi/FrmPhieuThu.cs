using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Service.Data;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Windows.Forms;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmPhieuThu : CRUDForm<phieuthu>
    {
        #region Properties

        private BindingList<hocsinh> hocSinhTable;

        #endregion

        public FrmPhieuThu()
        {
            InitializeComponent();

            this.TablePrimaryKey = "PhieuThuId";
            this.DanhMuc = DanhMucConstant.PhieuThu;
            this.FormKey = AppForms.FormPhieuThu;
            Entities.hocsinhs.Load();
            this.hocSinhTable = this.Entities.hocsinhs.Local.ToBindingList();
            this.loadPhieuThu();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, null, null, this.gvMain, this.phieuThuRowBindingSource.DataSource);
        }

        private void loadPhieuThu()
        {
            PhieuThuService phieuThuService = new PhieuThuService();
            this.phieuThuRowBindingSource.DataSource = phieuThuService.LoadPhieuThu(hocSinhTable);
        }

        protected override void onAdding()
        {
            FrmTaoPhieuThu frm = (FrmTaoPhieuThu)FormMainFacade.GetForm(AppForms.FormTaoPhieuThu);
            frm.GridView = this.GridViewMain;
            frm.IsEditing = false;

            FormMainFacade.ShowDialog(AppForms.FormTaoPhieuThu);
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.AddedCaption);
        }

        protected override void onEditing()
        {
            FrmTaoPhieuThu frm = (FrmTaoPhieuThu)FormMainFacade.GetForm(AppForms.FormTaoPhieuThu);
            frm.GridView = this.GridViewMain;
            frm.IsEditing = true;
            phieuthu phieuThu = this.phieuThuRowBindingSource.Current as phieuthu;
            frm.PhieuThuRow = phieuThu;

            FormMainFacade.ShowDialog(AppForms.FormTaoPhieuThu);
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
                int phieuThuId = (int)this.GridViewMain.GetFocusedRowCellValue("PhieuThuId");
                DateTime ngay = (DateTime)this.GridViewMain.GetFocusedRowCellValue("Ngay");
                long soTien = (long)this.GridViewMain.GetFocusedRowCellValue("SoTien");
                string maPhieu = (string)this.GridViewMain.GetFocusedRowCellValue("MaPhieu");
                int hocSinhId = (int)this.GridViewMain.GetFocusedRowCellValue("HocSinhId");
                DateTime createdDate = (DateTime)this.GridViewMain.GetFocusedRowCellValue("CreatedDate");
                int phanLoaiThuId = (int)this.GridViewMain.GetFocusedRowCellValue("PhanLoaiThuId");
                // Delete Phieu Thu
                var phieuThu = new phieuthu() { PhieuThuId=phieuThuId };
                this.Entities.Entry(phieuThu).State = EntityState.Deleted;
                this.Entities.SaveChanges();

                this.loadPhieuThu();
                FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.DeletedCaption);
            }
        }

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            this.onEditing();
        }
    }
}