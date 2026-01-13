namespace QLMamNon.Dao
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class viewbangthutien
    {
        [NotMapped]
        public long SoTienAnSangConLai { get; set; }
        [NotMapped]
        public long SoTienAnToiConLai { get; set; }
        [NotMapped]
        public long ThanhTien { get; set; }
        [NotMapped]
        public long OriginalThanhTien { get; set; }
        [NotMapped]
        public long TienAnSua { get; set; }
        [NotMapped]
        public long TienSua { get; set; }
        [NotMapped]
        public long PhuPhi { get; set; }
        [NotMapped]
        public long BanTru { get; set; }
        [NotMapped]
        public long HocPhi { get; set; }
        [NotMapped]
        public long KhoanThuChinh { get; set; }
        [NotMapped]
        public long SoTienNopLan1 { get; set; }
        [NotMapped]
        public long SoTienNopLan2 { get; set; }
        [NotMapped]
        public long TienAn { get; set; }
        [NotMapped]
        public string Ten { get; set; }
        [NotMapped]
        public string HoTen { get; set; }
        [NotMapped]
        public string SoDienThoai { get; set; }
        [NotMapped]
        public Nullable<System.DateTime> NgayNopLan1 { get; set; }
        [NotMapped]
        public Nullable<System.DateTime> NgayNopLan2 { get; set; }
        [NotMapped]
        public long PhucVuBanTru { get; set; }
    }
}
