using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.UserControls;
using System;
using System.Data.Entity;

namespace QLMamNon.Forms.DanhMuc
{
    public partial class FrmKhoanThu : CRUDForm<khoanthu>
    {
        public FrmKhoanThu()
        {
            InitializeComponent();

            this.TablePrimaryKey = "KhoanThuId";
            this.DanhMuc = DanhMucConstant.KhoanThu;
            this.FormKey = AppForms.FormDanhMucKhoanThu;
            Entities.khoanthus.Load();
            this.khoanThuRowBindingSource.DataSource = Entities.khoanthus.Local.ToBindingList();
            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormKhoanThu();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.khoanThuRowBindingSource.DataSource);
        }

        private void FrmKhoanThu_Load(object sender, EventArgs e)
        {

        }
    }
}