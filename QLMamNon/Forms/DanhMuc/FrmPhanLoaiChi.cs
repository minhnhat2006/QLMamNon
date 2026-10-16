using QLMamNon.Constant;
using QLMamNon.Components.Data.Static;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.UserControls;
using System;
using System.Data.Entity;

namespace QLMamNon.Forms.DanhMuc
{
    public partial class FrmPhanLoaiChi : CRUDForm<phanloaichi>
    {
        public FrmPhanLoaiChi()
        {
            InitializeComponent();

            this.TablePrimaryKey = "PhanLoaiChiId";
            this.DanhMuc = DanhMucConstant.PhanLoaiChi;
            this.FormKey = AppForms.FormDanhMucPhanLoaiChi;
            Entities.phanloaichis.Load();
            this.phanLoaiChiRowBindingSource.DataSource = Entities.phanloaichis.Local.ToBindingList();
            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormPhanLoaiChi();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.phanLoaiChiRowBindingSource.DataSource);
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
            StaticDataFacade.Reload(StaticDataKeys.PhanLoaiChi);
        }

        protected override void onSavingError(Exception ex)
        {
            this.savingFailed = true;
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SaveFailedCaption);
            base.onSavingError(ex);
        }

        private void FrmPhanLoaiChi_Load(object sender, EventArgs e)
        {

        }
    }
}