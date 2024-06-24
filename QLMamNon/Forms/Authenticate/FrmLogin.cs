using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ACG.Core.WinForm.Util;
using QLMamNon.Components.Data.Static;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Service.Data;
using FormShowType = QLMamNon.Facade.FormMainFacade.FormShowType;

namespace QLMamNon.Forms.Authenticate
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {
        private qlmamnonEntities entities;

        public FrmLogin()
        {
            entities = StaticDataFacade.GetQLMNEntities();

            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            AuthenService authenService = new AuthenService();
            string username = txtUsername.Text;
            string password = PasswordUtil.GetMd5Hash(txtPassword.Text);
            List<user> userDataTable = authenService.GetUsersForLogin(entities, username, password);

            if (userDataTable.Count == 0)
            {
                MessageBox.Show("Tên đăng nhập hoặc Mật khẩu không đúng. Xin vui lòng thử lại.", "Lỗi đăng nhập",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<user_privilege> upTable = authenService.LoadUserPrivileges(entities, userDataTable[0].UserId);
                authenService.SetAuthenticatedUser(userDataTable[0], upTable);
                this.Close();

                showCurrentForm();
            }
        }

        private static void showCurrentForm()
        {
            String form = (String)StaticDataFacade.Get(StaticDataKeys.CurrentForm);
            FormShowType showType = (FormShowType)StaticDataFacade.Get(StaticDataKeys.CurrentFormShowType);

            if (FormShowType.Normal == showType)
            {
                FormMainFacade.ShowForm(form);
            }
            else
            {
                FormMainFacade.ShowDialog(form);
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                btnLogin_Click(sender, null);
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                btnLogin_Click(sender, null);
            }
        }
    }
}