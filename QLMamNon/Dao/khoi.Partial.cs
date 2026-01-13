namespace QLMamNon.Dao
{
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class khoi
    {
        [NotMapped]
        public int? TruongId { get; set; }
    }
}
