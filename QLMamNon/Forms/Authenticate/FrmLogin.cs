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

            this.FormClosed += (sender, e) => { entities.Dispose(); entities = StaticDataFacade.GetQLMNEntities(); };
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            AuthenService authenService = new AuthenService();
            string username = txtUsername.Text;

            if (StringUtil.IsEmpty(username) || StringUtil.IsEmpty(txtPassword.Text))
            {
                MessageBox.Show("Xin vui lòng nhập Tên đăng nhập và Mật khẩu.", "Lỗi đăng nhập",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            user loginUser = authenService.Login(entities, username, txtPassword.Text);

            if (loginUser == null)
            {
                MessageBox.Show("Tên đăng nhập hoặc Mật khẩu không đúng. Xin vui lòng thử lại.", "Lỗi đăng nhập",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<user_privilege> upTable = authenService.LoadUserPrivileges(entities, loginUser.UserId);
                authenService.SetAuthenticatedUser(loginUser, upTable);
                this.Close();

                showCurrentForm();
            }
        }

        private static void showCurrentForm()
        {
            String form = (String)StaticDataFacade.Get(StaticDataKeys.CurrentForm);
            object showTypeValue = StaticDataFacade.Get(StaticDataKeys.CurrentFormShowType);

            if (form == null || showTypeValue == null)
            {
                return;
            }

            FormShowType showType = (FormShowType)showTypeValue;

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