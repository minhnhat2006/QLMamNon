using QLMamNon.Dao;
using System.Collections.Generic;

namespace QLMamNon.Entity
{
    public class AuthenticatedEntity
    {
        public AuthenticatedEntity(user user, List<user_privilege> table)
        {
            this.User = user;
            this.UserPrivilegeTable = table;
        }

        public user User { get; set; }

        public List<user_privilege> UserPrivilegeTable { get; set; }
    }
}
