using System;
using DevExpress.XtraGrid.Views.Base;
using QLMamNon.Constant;
using QLMamNon.UserControls;
using QLMamNon.Dao;
using System.ComponentModel;
using System.Data.Entity;

namespace QLMamNon.Forms.DanhMuc
{
    public partial class FrmTaiSan : CRUDForm<taisan>
    {
        public FrmTaiSan()
        {
            InitializeComponent();

            this.TablePrimaryKey = "TaiSanId";
            this.DanhMuc = DanhMucConstant.QuanLyTaiSan;
            this.FormKey = AppForms.FormTaiSan;

            // This grid is bound to viewtaisan, which is a read-only view entity (DefiningQuery,
            // no UpdateFunction).  SaveChanges() on any Modified viewtaisan row throws UpdateException.
            // Writes belong on the taisan table through a dedicated service; until that service
            // exists this form is intentionally read-only.  Pass null for all five buttons so
            // CRUDForm wires no Add/Edit/Delete/Save/Cancel behaviour.
            this.btnThem.Visible = false;
            this.btnChinhSua.Visible = false;
            this.btnXoa.Visible = false;
            this.btnLuu.Visible = false;
            this.btnHuyBo.Visible = false;
            this.gvMain.OptionsBehavior.Editable = false;

            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormTaiSan();
            this.loadTaiSanData();
            this.InitForm(null, null, null, null, null, this.gvMain, this.viewTaiSanRowBindingSource.DataSource);
        }

        private void loadTaiSanData()
        {
            Entities.viewtaisans.Load();
            BindingList<viewtaisan> dataTable = Entities.viewtaisans.Local.ToBindingList();
            this.viewTaiSanRowBindingSource.DataSource = dataTable;
        }

        private void gvMain_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (e.Column.FieldName == "PhanLoaiTaiSan" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                object objPhieuChiId = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "PhieuChiId");
                if (objPhieuChiId != null)
                {
                    e.DisplayText = StaticDataUtil.GetPhanLoaiChiNameByPhieuChiId(Entities, (int)objPhieuChiId);
                }
            }
        }
    }
}