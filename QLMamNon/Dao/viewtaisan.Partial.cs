namespace QLMamNon.Dao
{
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class viewtaisan
    {
        [NotMapped]
        public string PhanLoaiTaiSan { get; set; }
    }
}
