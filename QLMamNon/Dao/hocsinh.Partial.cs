namespace QLMamNon.Dao
{
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class hocsinh
    {
        [NotMapped]
        public int STT { get; set; }

        [NotMapped]
        public string HoTen { get { return $"{HoDem} {Ten}"; } }

        [NotMapped]
        public string LopDangHoc { get; set; }

        [NotMapped]
        public string PhuongXa { get; set; }

        [NotMapped]
        public string QuanHuyen { get; set; }
    }
}
