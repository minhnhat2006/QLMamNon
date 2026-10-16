using QLMamNon.Constant;
using QLMamNon.Components.Data.Static;
using QLMamNon.Dao;
using QLMamNon.Facade;
using System;
using QLMamNon.UserControls;
using System.Data.Entity;

namespace QLMamNon.Forms.DanhMuc
{
    public partial class FrmTinhThanhPho : CRUDForm<thanhpho>
    {
        public FrmTinhThanhPho()
        {
            InitializeComponent();

            this.TablePrimaryKey = "ThanhPhoId";
            this.DanhMuc = DanhMucConstant.TinhThanhPho;
            this.FormKey = AppForms.FormDanhMucTinhThanhPho;
            Entities.thanhphoes.Load();
            this.thanhPhoRowBindingSource.DataSource = Entities.thanhphoes.Local.ToBindingList();
            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormTinhThanhPho();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.thanhPhoRowBindingSource.DataSource);
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
            StaticDataFacade.Reload(StaticDataKeys.TinhThanhPho);
        }

        protected override void onSavingError(Exception ex)
        {
            this.savingFailed = true;
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SaveFailedCaption);
            base.onSavingError(ex);
        }
    }
}