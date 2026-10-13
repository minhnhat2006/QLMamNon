using System;
using System.Data.Entity;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.UserControls;

namespace QLMamNon.Forms.DanhMuc
{
    public partial class FrmTruongHoc : CRUDForm<truong>
    {
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
    }
}