using ACG.Core.WinForm.Util;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Facade;
using QLMamNon.Properties;
using System;
using System.Windows.Forms;

namespace QLMamNon.Forms.SystemSetting
{
    public partial class FrmPreference : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        #endregion

        public FrmPreference()
        {
            this.FormKey = AppForms.FormBangKeThuHocPhi;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            Settings.Default["qlmamnonConnectionString1"] = txtConnectionString.Text;
            Settings.Default["AppLannchDate"] = dateAppLaunchDate.DateTime;
            Settings.Default["TienAnSang"] = (long)txtSoTienAnSang.Value;
            Settings.Default["TienAnToi"] = (long)txtSoTienAnToi.Value;
            Settings.Default["TienAnChinh"] = (long)txtSoTienAnChinh.Value;
            Settings.Default["SoTienTonDauKy"] = (long)txtSoTienTonDauKy.Value;
            Settings.Default["StartYearForDropDown"] = (int)txtYearForDropDown.Value;
            Settings.Default["NamSinhStart"] = (int)txtTTHSSearchYearFrom.Value;
            Settings.Default["PhanLoaiThuByHocSinh"] = txtThuByHS.Text;
            Settings.Default["ShowGiayBaoNopTienDieuHoa"] = chkGiayBaoNopTienDieuHoa.Checked;
            Settings.Default["ShowGiayBaoNopTienNote"] = chkGiayBaoNopTienGhiChu.Checked;
            Settings.Default.Save();

            MessageBox.Show("Số liệu đã được lưu thành công", "Đã lưu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmBaoCaoHoatDongTaiChinh_Load(object sender, EventArgs e)
        {
            txtConnectionString.Text = (string)Settings.Default["qlmamnonConnectionString1"];
            dateAppLaunchDate.DateTime = (DateTime)Settings.Default["AppLannchDate"];
            txtSoTienAnSang.Value = (long)Settings.Default["TienAnSang"];
            txtSoTienAnToi.Value = (long)Settings.Default["TienAnToi"];
            txtSoTienAnChinh.Value = (long)Settings.Default["TienAnChinh"];
            txtSoTienTonDauKy.Value = (long)Settings.Default["SoTienTonDauKy"];
            txtYearForDropDown.Value = (int)Settings.Default["StartYearForDropDown"];
            txtTTHSSearchYearFrom.Value = (int)Settings.Default["NamSinhStart"];
            txtThuByHS.Text = (string)Settings.Default["PhanLoaiThuByHocSinh"];
            chkGiayBaoNopTienDieuHoa.Checked = (bool)Settings.Default["ShowGiayBaoNopTienDieuHoa"];
            chkGiayBaoNopTienGhiChu.Checked = (bool)Settings.Default["ShowGiayBaoNopTienNote"];

            phanLoaiThuRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.PhanLoaiThu);
        }

        private void btnAddPhanLoaiThu_Click(object sender, EventArgs e)
        {
            if (ControlUtil.IsEditValueNull(cmbPhanLoaiThu))
            {
                MessageBox.Show("Xin vui lòng chọn Phân loại thu", "Chọn Phân loại thu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (StringUtil.IsEmpty(txtThuByHS.Text))
            {
                txtThuByHS.Text = cmbPhanLoaiThu.EditValue.ToString();
            }
            else
            {
                txtThuByHS.Text = string.Format("{0},{1}", txtThuByHS.Text, cmbPhanLoaiThu.EditValue);
            }
        }
    }
}