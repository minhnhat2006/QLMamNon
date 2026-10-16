using System;
using System.Data.Entity;
using DevExpress.XtraGrid.Views.Base;
using QLMamNon.Constant;
using QLMamNon.Components.Data.Static;
using QLMamNon.Dao;
using QLMamNon.Facade;
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

        // base.onSaving() tự bắt lỗi và báo qua onSavingError, không trả về trạng thái thành công,
        // nên dùng cờ này để biết lần lưu có lỗi hay không.
        private bool savingFailed;

        protected override void onSaving()
        {
            this.savingFailed = false;

            base.onSaving();

            if (this.savingFailed)
            {
                return;
            }

            // Các form khác đọc danh mục này từ cache tĩnh: bỏ cache để lần đọc sau lấy dữ liệu mới.
            StaticDataFacade.Reload(StaticDataKeys.BangTinhPhi);
        }

        protected override void onSavingError(Exception ex)
        {
            this.savingFailed = true;
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SaveFailedCaption);
            base.onSavingError(ex);
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