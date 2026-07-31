using DevExpress.XtraGrid.Views.Grid;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Service.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            // Phải dùng context mới mỗi lần nạp. this.Entities là context dài hạn đã
            // track sẵn các phieuchi cũ, nên với MergeOption.AppendOnly mặc định của
            // EF, câu query vẫn chạy nhưng giá trị từ DB bị bỏ qua và trả lại đúng các
            // entity cũ -> bảng không cập nhật sau khi FrmTaoPhieuChi lưu bằng context
            // khác.
            PhieuChiService phieuChiService = new PhieuChiService();
            this.phieuChiRowBindingSource.DataSource = phieuChiService.LoadPhieuChi(StaticDataFacade.GetQLMNEntities());
        }

        protected override void onAdding()
        {
            FrmTaoPhieuChi frm = (FrmTaoPhieuChi)FormMainFacade.GetForm(AppForms.FormTaoPhieuChi);
            frm.GridView = this.GridViewMain;
            frm.IsEditing = false;
            frm.IsSaved = false;
            frm.PhieuChiRow = null;
            frm.InitFormData();

            FormMainFacade.ShowDialog(AppForms.FormTaoPhieuChi);

            if (!frm.IsSaved)
            {
                // Không có gì được lưu: giữ nguyên bảng như trước khi mở form.
                return;
            }

            this.reloadKeepingGridState(0);
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.AddedCaption);
        }

        protected override void onEditing()
        {
            phieuchi phieuChi = this.phieuChiRowBindingSource.Current as phieuchi;

            FrmTaoPhieuChi frm = (FrmTaoPhieuChi)FormMainFacade.GetForm(AppForms.FormTaoPhieuChi);
            frm.GridView = this.GridViewMain;
            frm.IsEditing = true;
            frm.IsSaved = false;
            frm.PhieuChiRow = phieuChi;
            frm.InitFormData();

            FormMainFacade.ShowDialog(AppForms.FormTaoPhieuChi);

            if (!frm.IsSaved)
            {
                return;
            }

            this.reloadKeepingGridState(phieuChi != null ? phieuChi.PhieuChiId : 0);
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.ModifiedCaption);
        }

        private void reloadKeepingGridState(int phieuChiIdToFocus)
        {
            GridView view = this.GridViewMain;
            int topRowIndex = view.TopRowIndex;
            List<object> expandedGroupValues = getExpandedGroupValues(view);

            view.BeginUpdate();
            try
            {
                this.loadPhieuChi();
            }
            finally
            {
                view.EndUpdate();
            }

            // Chỉ mở lại đúng những nhóm đã mở trước đó, các nhóm khác phải đóng.
            restoreExpandedGroups(view, expandedGroupValues);
            this.focusPhieuChi(phieuChiIdToFocus);
            view.TopRowIndex = topRowIndex;
        }

        private static List<object> getExpandedGroupValues(GridView view)
        {
            List<object> expandedGroupValues = new List<object>();

            for (int groupRowHandle = -1; view.IsValidRowHandle(groupRowHandle); groupRowHandle--)
            {
                if (view.GetRowExpanded(groupRowHandle))
                {
                    expandedGroupValues.Add(view.GetGroupRowValue(groupRowHandle));
                }
            }

            return expandedGroupValues;
        }

        private static void restoreExpandedGroups(GridView view, List<object> expandedGroupValues)
        {
            for (int groupRowHandle = -1; view.IsValidRowHandle(groupRowHandle); groupRowHandle--)
            {
                object groupValue = view.GetGroupRowValue(groupRowHandle);
                bool wasExpanded = expandedGroupValues.Any(value => object.Equals(value, groupValue));
                view.SetRowExpanded(groupRowHandle, wasExpanded, false);
            }
        }

        /// <summary>
        /// Chọn lại dòng theo PhieuChiId qua BindingSource. Không dùng LocateByValue vì
        /// gvMain đang group nên hàm đó chỉ tìm trong các dòng đang hiển thị và
        /// PhieuChiId cũng không được bind vào cột nào của gvMain.
        /// </summary>
        private void focusPhieuChi(int phieuChiId)
        {
            if (phieuChiId <= 0)
            {
                return;
            }

            List<phieuchi> rows = this.phieuChiRowBindingSource.DataSource as List<phieuchi>;

            if (rows == null)
            {
                return;
            }

            int index = rows.FindIndex(row => row.PhieuChiId == phieuChiId);

            if (index >= 0)
            {
                this.phieuChiRowBindingSource.Position = index;
            }
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