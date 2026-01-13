using System;
using System.ComponentModel.DataAnnotations.Schema;
using static QLMamNon.Constant.PhanLoaiThuConstant;

namespace QLMamNon.Dao
{
    public partial class phieuthu
    {
        [NotMapped]
        public string HocSinh { get; set; }

        [NotMapped]
        public string PhanLoaiThu { get; set; }

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