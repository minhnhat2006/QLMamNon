using ACG.Core.WinForm.Util;
using DevExpress.XtraGrid.Views.Base;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using System;
using System.ComponentModel;
using System.Linq;

namespace QLMamNon.Forms.HocSinh
{
    public partial class FrmThongTinHocTap : CRUDForm<viewhoctap>
    {
        #region Properties

        #endregion

        #region Events

        public FrmThongTinHocTap()
            : base()
        {
            InitializeComponent();

            this.TablePrimaryKey = "HocTapId";
            this.DanhMuc = DanhMucConstant.ThongTinHocTap;
            this.FormKey = AppForms.FormThongTinHocTap;

            // viewhoctap is a read-only view entity (DefiningQuery, no UpdateFunction).
            // loadThongTinHocTap used to write NgayTinh and SoNgayNghiThang directly onto
            // EF-tracked rows, making every row Modified before the user touched anything — Lưu
            // threw UpdateException even with no user edits.  Those defaults are now written into
            // the [NotMapped] display properties (NgayTinhDisplay, SoNgayNghiThangDisplay) so EF
            // tracking is never dirtied.  All five buttons are hidden and null-wired in CRUDForm.
            this.btnLuu.Visible = false;
            this.btnHuyBo.Visible = false;
            this.gvMain.OptionsBehavior.Editable = false;

            // Rebind the two columns to [NotMapped] display properties so the grid shows the
            // computed defaults without involving EF change tracking.
            this.colNgayTinh.FieldName = "NgayTinhDisplay";
            this.colSoNgayNghiThang.FieldName = "SoNgayNghiThangDisplay";

            this.loadThongTinHocTap();
            this.InitForm(null, null, null, null, null, this.gvMain, this.viewHocTapRowBindingSource.DataSource);
        }

        private void FrmThongTinHocTap_Load(object sender, EventArgs e)
        {
            this.lopRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.LopHoc);
            this.namHocBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.NamHoc);
        }

        private void gvMain_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (e.Column.FieldName == "GioiTinh" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                sbyte gioiTinh = (sbyte)view.GetListSourceRowCellValue(e.ListSourceRowIndex, "GioiTinh");
                switch (gioiTinh)
                {
                    case 0: e.DisplayText = "Nữ"; break;
                    case 1: e.DisplayText = "Nam"; break;
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            this.loadThongTinHocTap();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.cmbNam.EditValue = null;
            this.cmbThang.EditValue = null;
            this.cmbLop.EditValue = null;
        }

        #endregion

        #region Validation

        #endregion

        #region Helper

        private void loadThongTinHocTap()
        {
            if (ControlUtil.IsEditValueNull(cmbLop) || ControlUtil.IsEditValueNull(cmbNam) || ControlUtil.IsEditValueNull(cmbThang))
            {
                return;
            }

            int lopId = (int)cmbLop.EditValue;
            int nam = (int)cmbNam.EditValue;

            // cmbThang is an editable combo — EditValue may be any object; parse defensively.
            int? thangNullable = IntUtil.StringToInt((string)cmbThang.EditValue);
            if (thangNullable == null || thangNullable.Value < 1 || thangNullable.Value > 12)
            {
                System.Windows.Forms.MessageBox.Show("Tháng không hợp lệ. Vui lòng chọn tháng từ 1 đến 12.", "Lỗi",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }

            int thang = thangNullable.Value;
            DateTime ngay = new DateTime(nam, thang, DateTime.DaysInMonth(nam, thang));
            this.DataTable = new BindingList<viewhoctap>(Entities.getViewHocTapByLopAndNgay(lopId, ngay).ToList());
            this.viewHocTapRowBindingSource.DataSource = this.DataTable;

            // Compute display defaults into [NotMapped] properties so EF change tracking is
            // never dirtied (writing to NgayTinh / SoNgayNghiThang directly would mark every
            // row as Modified and cause SaveChanges to throw UpdateException).
            DateTime defaultNgayTinh = new DateTime(nam, thang, DateTime.DaysInMonth(nam, thang), 0, 0, 0, 0);
            foreach (viewhoctap row in this.DataTable)
            {
                row.NgayTinhDisplay = row.NgayTinh != null ? row.NgayTinh : defaultNgayTinh;
                row.SoNgayNghiThangDisplay = row.SoNgayNghiThang != null ? row.SoNgayNghiThang.Value : 0;
            }
        }

        #endregion
    }
}