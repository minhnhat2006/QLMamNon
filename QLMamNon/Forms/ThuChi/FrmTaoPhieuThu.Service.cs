using System;
using System.Collections.Generic;
using System.Linq;
using ACG.Core.WinForm.Util;
using QLMamNon.Components.Command;
using QLMamNon.Components.Command.QLMNDao;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmTaoPhieuThu
    {
        private void updateSoTienTruyThuForBangThuTienNextMonths(DateTime ngay, int hocSinhId)
        {
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();

            QLMNDaoJobInvoker qlmnDaoJobInvoker = new QLMNDaoJobInvoker();
            qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTienCommand = new UpdateSoTienTruyThuOfBangThuTienCommand();
            List<viewbangthutien> table = entities.getViewBangThuTienByNgayTinhAndHocSinhId(ngay.AddMonths(1), hocSinhId).ToList();

            if (ListUtil.IsEmpty(table))
            {
                return;
            }

            viewbangthutien viewBangThuTienRow = table[0];
            List<bangthutien_khoanthu> bTTKTDataTable = entities.getBangThuTienKhoanThuByBangThuTienIds(viewBangThuTienRow.BangThuTienId.ToString()).ToList();
            List<phieuthu> phieuThuDataTable = entities.getPhieuThuByHocSinhIdAndNgayTinh(-1, ngay).ToList();
            Dictionary<int, viewbangthutien> prevMonthRowDictionary = evaluatePrevMonthViewBangThuTienTable(ngay.AddMonths(-1), hocSinhId);
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

        private Dictionary<int, viewbangthutien> evaluatePrevMonthViewBangThuTienTable(DateTime ngayTinh, int hocSinhId)
        {
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            Dictionary<int, viewbangthutien> prevMonthRowDictionary = null;
            List<viewbangthutien> prevMonthBangThuTienTable = entities.getViewBangThuTienByNgayTinhAndHocSinhId(ngayTinh, hocSinhId).ToList();
            List<int> prevMonthBangThuTienIds = new List<int>(prevMonthBangThuTienTable.Count);

            foreach (viewbangthutien row in prevMonthBangThuTienTable)
            {
                prevMonthBangThuTienIds.Add(row.BangThuTienId);
            }

            if (!ListUtil.IsEmpty(prevMonthBangThuTienIds))
            {
                List<bangthutien_khoanthu> prevMonthBTTKTDataTable = entities.getBangThuTienKhoanThuByBangThuTienIds(String.Join(",", prevMonthBangThuTienIds)).ToList();
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
