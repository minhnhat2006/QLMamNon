using QLMamNon.Constant;
using QLMamNon.Dao;
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

        private void FrmPhanLoaiThu_Load(object sender, EventArgs e)
        {

        }
    }
}