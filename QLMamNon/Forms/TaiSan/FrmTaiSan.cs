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

            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormTaiSan();
            this.loadTaiSanData();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.viewTaiSanRowBindingSource.DataSource);
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