using ACG.Core.WinForm.Util;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Entity;
using QLMamNon.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLMamNon.Service.Data
{
    public class AuthenService : BaseDataService
    {
        public bool IsAuthenticated()
        {
            bool isAuthenticated = false;

            if (StaticDataFacade.Contains(StaticDataKeys.AuthenticatedData))
            {
                isAuthenticated = true;
            }

            return isAuthenticated;
        }

        public void SetAuthenticatedUser(user user, List<user_privilege> table)
        {
            StaticDataFacade.Remove(StaticDataKeys.AuthenticatedData);
            StaticDataFacade.Add(StaticDataKeys.AuthenticatedData, new AuthenticatedEntity(user, table));
        }

        public bool CanAccess(String formKey, List<user_privilege> table)
        {
            if (!FormPrivilegeConstant.FormKeyToPrivilegeId.ContainsKey(formKey))
            {
                return true;
            }

            int privilegeId = FormPrivilegeConstant.FormKeyToPrivilegeId[formKey];

            return CanAccess(privilegeId, table);
        }

        public bool CanAccess(int privilegeId, List<user_privilege> table)
        {
            bool canAccess = false;

            if (table != null)
            {
                List<user_privilege> rows = table.FindAll(u => u.PrivilegeId == privilegeId);

                if (!ListUtil.IsEmpty(rows))
                {
                    canAccess = rows[0].Value;
                }
            }

            return canAccess;
        }

        public List<user> GetUsersForLogin(qlmamnonEntities entities, string userName, string password)
        {
            List<user> table = entities.getUser(userName, password).ToList();
            return table;
        }

        public List<user> LoadUsers(qlmamnonEntities entities, int? tinhTPId, int? quanHuyenId, int? phuongXaId, DateTime? ngaySinh)
        {
            List<user> table = entities.users.ToList();
            return table;
        }

        public List<user_privilege> LoadUserPrivileges(qlmamnonEntities entities, int userId)
        {
            List<user_privilege> table = entities.getUserPriviledgeByUserId(userId).ToList();
            return table;
        }

        public void UpdateUserPrivileges(qlmamnonEntities entities, int userId, List<int> privilegeIds)
        {
            if (privilegeIds == null) return;

            if (ListUtil.IsEmpty(privilegeIds))
            {
                entities.deleteUserPrivileges("0", userId);
            }
            else
            {
                entities.deleteUserPrivileges(StringUtil.JoinWithCommas(privilegeIds), userId);

                foreach (int privilegeId in privilegeIds)
                {
                    entities.insertUserPrivilegeIfNotExists(userId, privilegeId, 1);
                }
            }
        }
    }
}
