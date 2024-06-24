using System;
using System.Linq;
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

            this.truongRowBindingSource.DataSource = Entities.truongs.ToList();
            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormTruongHoc();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.truongRowBindingSource.DataSource);
        }

        private void FrmTruongHoc_Load(object sender, EventArgs e)
        {

        }
    }
}