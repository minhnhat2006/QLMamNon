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
        /// Hàm không phụ thuộc vào việc người gọi truyền context nào: dòng viewbangthutien của
        /// tháng sau phải được theo dõi (tracked) để đọc được OriginalValues/CurrentValues, nên nó
        /// được đọc với MergeOption.OverwriteChanges - nếu context đã nạp sẵn dòng đó từ trước
        /// (ví dụ context của lưới phiếu thu) thì giá trị gốc vẫn được lấy lại từ DB, thay vì
        /// MergeOption.AppendOnly trả về đúng instance đã bị sửa và làm chênh lệch sai.
        /// Dòng đó cũng được gỡ khỏi change tracker ở cuối hàm, vì viewbangthutien là entity set
        /// chỉ đọc (DefiningQuery) - để nguyên trạng thái Modified sẽ làm SaveChanges() của
        /// người gọi throw UpdateException.
        /// Dù vậy, một context riêng và ngắn hạn vẫn là cách dùng được khuyến nghị
        /// (xem FrmTaoPhieuThu.onSavePhieuThu).
        /// </remarks>
        public void UpdateSoTienTruyThuForBangThuTienNextMonths(qlmamnonEntities entities, DateTime ngay, int hocSinhId)
        {
            QLMNDaoJobInvoker qlmnDaoJobInvoker = new QLMNDaoJobInvoker();
            qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTienCommand = new UpdateSoTienTruyThuOfBangThuTienCommand();

            // OverwriteChanges thay vì AppendOnly (mặc định): xem <remarks>. Với một context mới
            // thì hai MergeOption này cho kết quả như nhau, vì chưa có gì được theo dõi.
            List<viewbangthutien> table = entities
                .getViewBangThuTienByNgayTinhAndHocSinhId(ngay.AddMonths(1), hocSinhId, MergeOption.OverwriteChanges).ToList();

            if (ListUtil.IsEmpty(table))
            {
                return;
            }

            viewbangthutien viewBangThuTienRow = table[0];

            try
            {
                List<bangthutien_khoanthu> bTTKTDataTable = entities.getBangThuTienKhoanThuByBangThuTienIds(viewBangThuTienRow.BangThuTienId.ToString()).ToList();
                List<phieuthu> phieuThuDataTable = entities.getPhieuThuByHocSinhIdAndNgayTinh(-1, ngay).ToList();
                Dictionary<int, viewbangthutien> prevMonthRowDictionary = this.evaluatePrevMonthViewBangThuTienTable(entities, ngay.AddMonths(-1), hocSinhId);
                BangThuTienUtil.EvaluateValuesForViewBangThuTienRow(viewBangThuTienRow,
                    prevMonthRowDictionary != null && prevMonthRowDictionary.ContainsKey(viewBangThuTienRow.HocSinhId) ? prevMonthRowDictionary[viewBangThuTienRow.HocSinhId] : null,
                    bTTKTDataTable, phieuThuDataTable, false, false, true);

                if (entities.Entry(viewBangThuTienRow).OriginalValues[ViewBangThuTienFieldName.ThanhTien] != null
                    && entities.Entry(viewBangThuTienRow).CurrentValues[ViewBangThuTienFieldName.ThanhTien] != null)
                {
                    long originalVersionToCompare = (long)entities.Entry(viewBangThuTienRow).OriginalValues[ViewBangThuTienFieldName.ThanhTien];
                    long currentVersionToCompare = (long)entities.Entry(viewBangThuTienRow).CurrentValues[ViewBangThuTienFieldName.ThanhTien];

                    if (originalVersionToCompare != currentVersionToCompare)
                    {
                        qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTienCommand.CommandParameter.Remove(UpdateSoTienTruyThuOfBangThuTienCommand.ParameterCurrentViewBangThuTien);
                        qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTienCommand.CommandParameter.Remove(UpdateSoTienTruyThuOfBangThuTienCommand.ParameterSoTienAdded);
                        qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTienCommand.CommandParameter.Add(UpdateSoTienTruyThuOfBangThuTienCommand.ParameterCurrentViewBangThuTien, viewBangThuTienRow);
                        qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTienCommand.CommandParameter.Add(UpdateSoTienTruyThuOfBangThuTienCommand.ParameterSoTienAdded, currentVersionToCompare - originalVersionToCompare);
                        qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTien();
                    }
                }
            }
            finally
            {
                // Dòng view đã bị sửa trong bộ nhớ ở trên. viewbangthutien là entity set chỉ đọc nên
                // phải gỡ khỏi change tracker, kể cả khi ở trên có lỗi: nếu không, SaveChanges() của
                // người gọi sẽ throw UpdateException và che mất lỗi gốc.
                entities.Entry(viewBangThuTienRow).State = EntityState.Detached;
            }
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
