namespace QLMamNon.Dao
{
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class lop
    {
        [NotMapped]
        public int? KhoiId { get; set; }
    }
}
