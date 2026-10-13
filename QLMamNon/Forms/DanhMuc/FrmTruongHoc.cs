using System;
using System.Data.Entity;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.UserControls;

namespace QLMamNon.Forms.DanhMuc
{
    public partial class FrmTruongHoc : CRUDForm<truong>
    {
        // base.onSaving() tự bắt lỗi và báo qua onSavingError, không trả về trạng thái thành công,
        // nên dùng cờ này để biết SaveChanges() của lớp cơ sở có lỗi hay không.
        private bool savingFailed;

        public FrmTruongHoc()
        {
            InitializeComponent();

            this.TablePrimaryKey = "TruongId";
            this.DanhMuc = DanhMucConstant.TruongHoc;
            this.FormKey = AppForms.FormDanhMucTruongHoc;

            // Phải bind bằng Local.ToBindingList(): CRUDForm.InitForm chỉ nhận DataTable qua
            // "dataTable as BindingList<T>", nếu bind bằng ToList() thì DataTable = null và
            // onSaving() bỏ qua SaveChanges() - thêm, sửa, xóa đều bị mất mà không báo lỗi.
            Entities.truongs.Load();
            this.truongRowBindingSource.DataSource = Entities.truongs.Local.ToBindingList();
            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormTruongHoc();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.truongRowBindingSource.DataSource);
        }

        private void FrmTruongHoc_Load(object sender, EventArgs e)
        {

        }

        protected override void onSaving()
        {
            this.savingFailed = false;

            base.onSaving();

            if (this.savingFailed)
            {
                return;
            }

            // Các form khác lấy tên trường từ cache tĩnh (StaticDataUtil.GetTruongNameByTruongId,
            // GetTruongByKhoiId dùng thẳng kết quả Find nên trường mới mà cache chưa biết sẽ gây
            // NullReferenceException), vì vậy phải bỏ cache sau khi lưu thành công.
            StaticDataFacade.Reload(StaticDataKeys.TruongHoc);
        }

        protected override void onSavingError(Exception ex)
        {
            this.savingFailed = true;
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SaveFailedCaption);
            base.onSavingError(ex);
        }
    }
}