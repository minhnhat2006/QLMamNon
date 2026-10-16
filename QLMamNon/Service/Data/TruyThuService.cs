using ACG.Core.WinForm.Util;
using QLMamNon.Components.Command;
using QLMamNon.Components.Command.QLMNDao;
using QLMamNon.Constant;
using QLMamNon.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace QLMamNon.Service.Data
{
    /// <summary>
    /// Tính lại số tiền truy thu trên bảng thu tiền của tháng kế tiếp cho một học sinh,
    /// sau khi các phiếu thu của học sinh đó trong tháng của <c>ngay</c> thay đổi.
    /// Tách ra từ FrmTaoPhieuThu để FrmPhieuThu cũng gọi được khi xóa phiếu thu.
    /// </summary>
    public class TruyThuService : BaseDataService
    {
        /// <summary>
        /// Tính lại SoTienTruyThu của bảng thu tiền tháng sau (<c>ngay.AddMonths(1)</c>) cho học
        /// sinh <c>hocSinhId</c> và cập nhật phần chênh lệch qua stored procedure.
        /// </summary>
        /// <remarks>
        /// Truy thu tháng M+1 được tính lại từ tháng M (tháng của <c>ngay</c>) bằng
        /// BangThuTienUtil.CalculateTruyThu, so với SoTienTruyThu đang lưu, và phần chênh lệch được
        /// cộng cho mọi bảng thu tiền từ tháng M+1 trở đi qua updateSoTienTruyThuByHocSinhAndNgayUpdated.
        /// Mọi dòng viewbangthutien đều được đọc NoTracking nên hàm không để lại thay đổi nào trong
        /// context của người gọi. Vẫn nên dùng một context riêng, ngắn hạn
        /// (xem FrmTaoPhieuThu.onSavePhieuThu và FrmPhieuThu).
        /// </remarks>
        public void UpdateSoTienTruyThuForBangThuTienNextMonths(qlmamnonEntities entities, DateTime ngay, int hocSinhId)
        {
            // Dòng bảng thu tiền của tháng sau (M+1). NoTracking: chỉ đọc, không bao giờ lưu qua view.
            List<viewbangthutien> table = entities
                .getViewBangThuTienByNgayTinhAndHocSinhId(ngay.AddMonths(1), hocSinhId, MergeOption.NoTracking).ToList();

            if (ListUtil.IsEmpty(table))
            {
                return;
            }

            viewbangthutien nextMonthRow = table[0];

            // Truy thu của tháng M+1 là số còn nợ của tháng M (tháng của ngay), nên "tháng trước" của
            // dòng này là ngay, không phải ngay.AddMonths(-1).
            Dictionary<int, viewbangthutien> thisMonthRowDictionary = this.evaluatePrevMonthViewBangThuTienTable(entities, ngay, hocSinhId);
            viewbangthutien thisMonthRow = thisMonthRowDictionary != null && thisMonthRowDictionary.ContainsKey(hocSinhId)
                ? thisMonthRowDictionary[hocSinhId] : null;

            // So với SoTienTruyThu đang lưu trong bangthutien. Không dùng ThanhTien: đó là thuộc tính
            // [NotMapped], EF không có giá trị gốc cho nó (OriginalValues["ThanhTien"] sẽ throw).
            long soTienTruyThuDangLuu = nextMonthRow.SoTienTruyThu == null ? 0 : nextMonthRow.SoTienTruyThu.Value;
            long soTienTruyThuMoi = BangThuTienUtil.CalculateTruyThu(nextMonthRow, thisMonthRow);
            long chenhLech = soTienTruyThuMoi - soTienTruyThuDangLuu;

            if (chenhLech == 0)
            {
                return;
            }

            // Thủ tục cộng chênh lệch cho mọi bangthutien của học sinh có NgayTinh > ngày truyền vào.
            // Truyền ngày cuối tháng M (không có giờ) để cập nhật từ tháng M+1 trở đi mà không đụng tháng M.
            DateTime ngayCuoiThang = new DateTime(ngay.Year, ngay.Month, DateTime.DaysInMonth(ngay.Year, ngay.Month));
            entities.updateSoTienTruyThuByHocSinhAndNgayUpdated(hocSinhId, ngayCuoiThang, chenhLech);
        }

        private Dictionary<int, viewbangthutien> evaluatePrevMonthViewBangThuTienTable(qlmamnonEntities entities, DateTime ngayTinh, int hocSinhId)
        {
            Dictionary<int, viewbangthutien> prevMonthRowDictionary = null;

            // NoTracking: các dòng này chỉ được tính lại trong bộ nhớ, và viewbangthutien /
            // bangthutien_khoanthu của tháng trước không bao giờ được lưu. Giữ chúng ở trạng thái
            // Modified trong context dùng chung sẽ làm SaveChanges() của người gọi throw.
            List<viewbangthutien> prevMonthBangThuTienTable = entities.getViewBangThuTienByNgayTinhAndHocSinhId(ngayTinh, hocSinhId, MergeOption.NoTracking).ToList();
            List<int> prevMonthBangThuTienIds = new List<int>(prevMonthBangThuTienTable.Count);

            foreach (viewbangthutien row in prevMonthBangThuTienTable)
            {
                prevMonthBangThuTienIds.Add(row.BangThuTienId);
            }

            if (!ListUtil.IsEmpty(prevMonthBangThuTienIds))
            {
                List<bangthutien_khoanthu> prevMonthBTTKTDataTable = entities.getBangThuTienKhoanThuByBangThuTienIds(String.Join(",", prevMonthBangThuTienIds), MergeOption.NoTracking).ToList();
                List<phieuthu> prevMonthPhieuThuDataTable = entities.getPhieuThuByHocSinhIdAndNgayTinh(-1, ngayTinh).ToList();
                prevMonthRowDictionary = new Dictionary<int, viewbangthutien>();

                foreach (viewbangthutien row in prevMonthBangThuTienTable)
                {
                    BangThuTienUtil.EvaluateValuesForViewBangThuTienRow(row, null, prevMonthBTTKTDataTable, prevMonthPhieuThuDataTable, true, false, true);
                    prevMonthRowDictionary.Add(row.HocSinhId, row);
                }
            }

            return prevMonthRowDictionary;
        }
    }
}
