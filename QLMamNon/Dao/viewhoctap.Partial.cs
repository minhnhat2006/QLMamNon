namespace QLMamNon.Dao
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class viewhoctap
    {
        // Display properties for columns that need computed defaults.
        // These are [NotMapped] so assigning them never marks the EF-tracked row as Modified.

        /// <summary>
        /// Ngày tính display value, computed from filter context and stored in loadThongTinHocTap.
        /// Falls back to the raw NgayTinh if not explicitly set.
        /// </summary>
        [NotMapped]
        public Nullable<DateTime> NgayTinhDisplay { get; set; }

        /// <summary>
        /// Số ngày nghỉ display value, defaulted to 0 in loadThongTinHocTap.
        /// </summary>
        [NotMapped]
        public int SoNgayNghiThangDisplay { get; set; }

        /// <summary>
        /// Ngày vào lớp display value.
        /// Resolves DateJoin (from hocsinh_lop, authoritative) first,
        /// then falls back to NgayVaoLop (from hocsinh, no longer written by the app).
        /// Consistent with hocsinh.NgayVaoLopDisplay from Task 2.
        /// </summary>
        [NotMapped]
        public Nullable<DateTime> NgayVaoLopDisplay
        {
            get
            {
                if (DateJoin != null)
                {
                    return DateJoin;
                }
                return NgayVaoLop;
            }
        }
    }
}
