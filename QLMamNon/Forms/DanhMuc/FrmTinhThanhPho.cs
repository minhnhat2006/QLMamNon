using QLMamNon.Constant;
using QLMamNon.Dao;
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
            this.FormKey = AppForms.FormDanhMucTruongHoc;
            Entities.thanhphoes.Load();
            this.thanhPhoRowBindingSource.DataSource = Entities.thanhphoes.Local.ToBindingList();
            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormTinhThanhPho();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.thanhPhoRowBindingSource.DataSource);
        }
    }
}