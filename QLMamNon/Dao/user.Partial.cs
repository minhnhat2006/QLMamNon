namespace QLMamNon.Dao
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class user
    {
        [NotMapped]
        public List<int> UserPrivileges { get; set; }
    }
}
