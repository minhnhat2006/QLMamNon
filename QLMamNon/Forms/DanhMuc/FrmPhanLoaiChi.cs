using QLMamNon.Constant;
using QLMamNon.Dao;
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

        private void FrmPhanLoaiChi_Load(object sender, EventArgs e)
        {

        }
    }
}