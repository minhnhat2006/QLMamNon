
using System;
namespace QLMamNon.Entity.Form
{
    public class SoTheoDoiTaiSanItem
    {
        public int TaiSanId { get; set; }

        public string Ten { get; set; }

        public string DonViTinh { get; set; }

        public double? SoLuong { get; set; }

        public long? DonGia { get; set; }

        public double? ThanhTien { get; set; }

        public string SoChungTu { get; set; }

        public DateTime? NgayChungTu { get; set; }

        public string SoChungTuGiam { get; set; }

        public DateTime NgayChungTuGiam { get; set; }

        public double SoLuongBanGiao { get; set; }

        public double SoTienBanGiao { get; set; }

        public string LyDoGiam { get; set; }

        public string GhiChu { get; set; }
    }
}
