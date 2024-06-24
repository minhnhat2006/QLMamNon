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

        #endregion

        public UCEditFormUser()
        {
            InitializeComponent();
        }

        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            this.GridView.SetFocusedRowCellValue("Password", PasswordUtil.GetMd5Hash(txtPassword.Text));
        }

        private void UCEditFormUser_Load(object sender, EventArgs e)
        {
            Entities.privileges.Load();
            this.privilegeRowBindingSource.DataSource = Entities.privileges.Local.ToBindingList();
        }

        private void UCEditFormUser_Enter(object sender, EventArgs e)
        {
            this._selectGvMainRowQuietMode = true;
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
