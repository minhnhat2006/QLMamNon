namespace QLMamNon.Dao
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using static QLMamNon.Constant.PhanLoaiThuConstant;

    public partial class phieuchi
    {
        [NotMapped]
        public string DienGiai { get; set; }

        [NotMapped]
        public string MaPhanLoai { get; set; }

        [NotMapped]
        public string PhanLoaiChi { get; set; }

        [NotMapped]
        public PaymentType PaymentTypeEnum
        {
            get
            {
                // Converts the string from MySQL into your C# Enum
                if (Enum.TryParse(this.PaymentType, out PaymentType result))
                    return result;
                return Constant.PhanLoaiThuConstant.PaymentType.CASH; // Default fallback
            }
            set
            {
                // Converts the C# Enum back to a string for MySQL
                this.PaymentType = value.ToString();
            }
        }
    }
}
