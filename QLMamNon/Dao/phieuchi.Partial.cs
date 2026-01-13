namespace QLMamNon.Dao
{
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class phieuchi
    {
        [NotMapped]
        public string DienGiai { get; set; }

        [NotMapped]
        public string MaPhanLoai { get; set; }

        [NotMapped]
        public string PhanLoaiChi { get; set; }
    }
}
