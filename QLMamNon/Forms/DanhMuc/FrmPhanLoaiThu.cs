using QLMamNon.Constant;
using QLMamNon.Components.Data.Static;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.UserControls;
using System;
using System.Data.Entity;

namespace QLMamNon.Forms.DanhMuc
{
    public partial class FrmPhanLoaiThu : CRUDForm<phanloaithu>
    {
        public FrmPhanLoaiThu()
        {
            InitializeComponent();

            this.TablePrimaryKey = "PhanLoaiThuId";
            this.DanhMuc = DanhMucConstant.PhanLoaiThu;
            this.FormKey = AppForms.FormDanhMucPhanLoaiThu;
            Entities.phanloaithus.Load();
            this.phanLoaiThuRowBindingSource.DataSource = Entities.phanloaithus.Local.ToBindingList();
            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormPhanLoaiThu();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.phanLoaiThuRowBindingSource.DataSource);
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
            StaticDataFacade.Reload(StaticDataKeys.PhanLoaiThu);
        }

        protected override void onSavingError(Exception ex)
        {
            this.savingFailed = true;
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SaveFailedCaption);
            base.onSavingError(ex);
        }

        private void FrmPhanLoaiThu_Load(object sender, EventArgs e)
        {

        }
    }
}