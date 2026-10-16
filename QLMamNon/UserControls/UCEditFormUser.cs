using ACG.Core.WinForm.Util;
using DevExpress.XtraGrid;
using QLMamNon.Dao;
using QLMamNon.Service.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace QLMamNon.UserControls
{
    public partial class UCEditFormUser : UCCRUDBase
    {
        #region Properties

        private bool _selectGvMainRowQuietMode = true;

        // Mật khẩu (đã băm) đang lưu của người dùng đang sửa, để khôi phục khi ô mật khẩu bị xóa trắng.
        private object originalPassword;

        #endregion

        public UCEditFormUser()
        {
            InitializeComponent();
        }

        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            // Ô mật khẩu trống nghĩa là giữ nguyên mật khẩu đang lưu (kể cả khi đã gõ rồi xóa hết).
            if (StringUtil.IsEmpty(txtPassword.Text))
            {
                this.GridView.SetFocusedRowCellValue("Password", this.originalPassword);
                return;
            }

            this.GridView.SetFocusedRowCellValue("Password", PasswordUtil.HashPassword(txtPassword.Text));
        }

        private void UCEditFormUser_Load(object sender, EventArgs e)
        {
            Entities.privileges.Load();
            this.privilegeRowBindingSource.DataSource = Entities.privileges.Local.ToBindingList();
        }

        private void UCEditFormUser_Enter(object sender, EventArgs e)
        {
            this._selectGvMainRowQuietMode = true;

            // UserControl này được dùng lại cho mọi lần mở popup: xóa lựa chọn của người dùng trước,
            // nếu không quyền của người đó sẽ bị cộng dồn sang người đang sửa.
            this.gvMain.ClearSelection();

            // Ghi nhớ mật khẩu đang lưu rồi xóa ô mật khẩu còn sót lại từ lần mở popup trước.
            this.originalPassword = this.GridView.GetFocusedRowCellValue("Password");
            this.txtPassword.Text = string.Empty;
            object userPrivilegesObj = this.GridView.GetFocusedRowCellValue("UserPrivileges");

            if (userPrivilegesObj == null)
            {
                object userIdObj = this.GridView.GetFocusedRowCellValue("UserId");

                if (userIdObj == null)
                {
                    this._selectGvMainRowQuietMode = false;
                    return;
                }

                userPrivilegesObj = getUserPrivilegeIds(Entities, userIdObj);
                this.GridView.SetFocusedRowCellValue("UserPrivileges", userPrivilegesObj);
            }

            List<int> privilegeIds = (List<int>)userPrivilegesObj;

            foreach (int privilegeId in privilegeIds)
            {
                int rowHandler = this.gvMain.LocateByValue("PrivilegeId", privilegeId);

                if (rowHandler != GridControl.InvalidRowHandle)
                {
                    this.gvMain.SelectRow(rowHandler);
                }
            }

            this._selectGvMainRowQuietMode = false;
        }

        private static List<int> getUserPrivilegeIds(qlmamnonEntities entities, object userIdObj)
        {
            int userId = (int)userIdObj;
            AuthenService authenService = new AuthenService();
            List<user_privilege> userPrivileges = authenService.LoadUserPrivileges(entities, userId);
            List<int> privilegeIds = new List<int>();

            foreach (user_privilege row in userPrivileges)
            {
                privilegeIds.Add(row.PrivilegeId);
            }

            return privilegeIds;
        }

        private void gvMain_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (this._selectGvMainRowQuietMode)
            {
                return;
            }

            List<int> privilegeIds = new List<int>();

            foreach (int rowHandlers in this.gvMain.GetSelectedRows())
            {
                int privilegeId = (int)this.gvMain.GetRowCellValue(rowHandlers, "PrivilegeId");
                privilegeIds.Add(privilegeId);
            }

            this.GridView.SetFocusedRowCellValue("UserPrivileges", privilegeIds);

            user user = this.GridView.GetRow(this.GridView.FocusedRowHandle) as user;
            if (user != null)
            {
                user.UserPrivileges = privilegeIds;
            }
        }
    }
}
