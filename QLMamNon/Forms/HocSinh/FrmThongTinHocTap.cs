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
            this.loadThongTinHocTap();
            this.InitForm(null, null, null, this.btnLuu, this.btnHuyBo, this.gvMain, this.viewHocTapRowBindingSource.DataSource);
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
            int thang = IntUtil.StringToInt((string)cmbThang.EditValue).Value;
            DateTime ngay = new DateTime(nam, thang, DateTime.DaysInMonth(nam, thang));
            this.DataTable = new BindingList<viewhoctap>(Entities.getViewHocTapByLopAndNgay(lopId, ngay).ToList());
            this.viewHocTapRowBindingSource.DataSource = this.DataTable;

            foreach (viewhoctap row in this.DataTable)
            {
                if (row.NgayTinh == null)
                {
                    row.NgayTinh = new DateTime(nam, thang, DateTime.DaysInMonth(nam, thang), 0, 0, 0, 0);
                }

                if (row.SoNgayNghiThang == null)
                {
                    row.SoNgayNghiThang = 0;
                }
            }
        }

        #endregion
    }
}