using System;
using System.Data.Entity;
using DevExpress.XtraGrid.Views.Base;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.UserControls;

namespace QLMamNon.Forms.DanhMuc
{
    public partial class FrmBangTinhPhi : CRUDForm<bangtinhphi>
    {
        public FrmBangTinhPhi()
        {
            InitializeComponent();

            this.TablePrimaryKey = "BangTinhPhiId";
            this.DanhMuc = DanhMucConstant.BangTinhPhi;
            this.FormKey = AppForms.FormBangTinhPhi;

            Entities.bangtinhphis.Load();
            this.bangTinhPhiRowBindingSource.DataSource = Entities.bangtinhphis.Local.ToBindingList();
            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormBangTinhPhi();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.bangTinhPhiRowBindingSource.DataSource);
        }

        private void FrmBangTinhPhi_Load(object sender, EventArgs e)
        {

        }

        private void gvMain_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (e.Column.FieldName == "KhoiId" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                Object khoiIdObj = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "KhoiId");

                if (khoiIdObj != null)
                {
                    int khoiId = (int)khoiIdObj;
                    e.DisplayText = StaticDataUtil.GetKhoiNameByKhoiId(khoiId);
                }
            }
            else if (e.Column.FieldName == "KhoanThuId" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                Object khoanThuIdObj = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "KhoanThuId");

                if (khoanThuIdObj != null)
                {
                    int khoanThuId = (int)khoanThuIdObj;
                    e.DisplayText = StaticDataUtil.GetKhoanThuNameByKhoanThuId(khoanThuId);
                }
            }
        }
    }
}