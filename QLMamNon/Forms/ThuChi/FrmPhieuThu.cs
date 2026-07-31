using DevExpress.XtraGrid.Views.Grid;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Service.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
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
            frm.IsSaved = false;
            frm.PhieuThuRow = null;
            frm.InitFormData();

            FormMainFacade.ShowDialog(AppForms.FormTaoPhieuThu);

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
            phieuthu phieuThu = this.phieuThuRowBindingSource.Current as phieuthu;

            FrmTaoPhieuThu frm = (FrmTaoPhieuThu)FormMainFacade.GetForm(AppForms.FormTaoPhieuThu);
            frm.GridView = this.GridViewMain;
            frm.IsEditing = true;
            frm.IsSaved = false;
            frm.PhieuThuRow = phieuThu;
            frm.InitFormData();

            FormMainFacade.ShowDialog(AppForms.FormTaoPhieuThu);

            if (!frm.IsSaved)
            {
                // Không có gì được lưu: giữ nguyên bảng như trước khi mở form.
                return;
            }

            this.reloadKeepingGridState(phieuThu != null ? phieuThu.PhieuThuId : 0);
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.ModifiedCaption);
        }

        /// <summary>
        /// Nạp lại dữ liệu nhưng giữ nguyên trạng thái hiển thị của bảng: các nhóm
        /// đang mở, dòng đang chọn và vị trí thanh cuộn.
        /// </summary>
        private void reloadKeepingGridState(int phieuThuIdToFocus)
        {
            GridView view = this.GridViewMain;
            int topRowIndex = view.TopRowIndex;
            List<object> expandedGroupValues = getExpandedGroupValues(view);

            view.BeginUpdate();
            try
            {
                this.Entities.hocsinhs.Load();
                this.hocSinhTable = this.Entities.hocsinhs.Local.ToBindingList();
                this.loadPhieuThu();
            }
            finally
            {
                view.EndUpdate();
            }

            // Chỉ mở lại đúng những nhóm đã mở trước đó, các nhóm khác phải đóng.
            restoreExpandedGroups(view, expandedGroupValues);
            this.focusPhieuThu(phieuThuIdToFocus);
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
        /// Chọn lại dòng theo PhieuThuId qua BindingSource. Không dùng LocateByValue vì
        /// gvMain đang group nên hàm đó chỉ tìm trong các dòng đang hiển thị và
        /// PhieuThuId cũng không được bind vào cột nào của gvMain.
        /// </summary>
        private void focusPhieuThu(int phieuThuId)
        {
            if (phieuThuId <= 0)
            {
                return;
            }

            List<phieuthu> rows = this.phieuThuRowBindingSource.DataSource as List<phieuthu>;

            if (rows == null)
            {
                return;
            }

            int index = rows.FindIndex(row => row.PhieuThuId == phieuThuId);

            if (index >= 0)
            {
                this.phieuThuRowBindingSource.Position = index;
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
                int phieuThuId = (int)this.GridViewMain.GetFocusedRowCellValue("PhieuThuId");
                DateTime ngay = (DateTime)this.GridViewMain.GetFocusedRowCellValue("Ngay");
                long soTien = (long)this.GridViewMain.GetFocusedRowCellValue("SoTien");
                string maPhieu = (string)this.GridViewMain.GetFocusedRowCellValue("MaPhieu");
                int? hocSinhId = (int?)this.GridViewMain.GetFocusedRowCellValue("HocSinhId");
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