using ACG.Core.WinForm.Util;
using QLMamNon.Dao;
using QLMamNon.Entity.Form;
using QLMamNon.Properties;
using QLMamNon.Reports;
using QLMamNon.Service.Data;
using System;
using System.Collections.Generic;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmSoThuTien
    {
        private List<viewbangthutien> getViewBangThuTienRowsFromMainGrid()
        {
            List<viewbangthutien> rows = new List<viewbangthutien>();
            for (int i = 0; i < this.GridViewMain.DataRowCount; i++)
            {
                viewbangthutien viewBangThuTienRow = (viewbangthutien)this.GridViewMain.GetRow(this.GridViewMain.GetVisibleRowHandle(i));
                rows.Add(viewBangThuTienRow);
            }

            return rows;
        }

        private void fillRptSoThuTien(RptSoThuTien rpt)
        {
            SoThuTienService soThuTienService = new SoThuTienService();
            List<viewbangthutien> rows = soThuTienService.EvaluateViewBangThuTienRowsForReport(Entities, this.getViewBangThuTienRowsFromMainGrid(), this.ngayTinh);

            for (int idx = 0; idx < rows.Count; idx++)
            {
                rows[idx].STT = idx + 1;
            }

            rpt.viewBangThuTienRowbindingSource.DataSource = rows;
            rpt.Ngay.Value = this.ngayTinh;
        }

        private void fillRptSoThuTienTrang1(RptSoThuTienTrang1 rpt)
        {
            SoThuTienService soThuTienService = new SoThuTienService();
            List<viewbangthutien> rows = soThuTienService.EvaluateViewBangThuTienRowsForReport(Entities, this.getViewBangThuTienRowsFromMainGrid(), this.ngayTinh);
            rpt.viewBangThuTienRowbindingSource.DataSource = rows;
            rpt.Ngay.Value = this.ngayTinh;
        }

        private void fillRptSoThuTienTrang2(RptSoThuTienTrang2 rpt)
        {
            rpt.viewBangThuTienRowbindingSource.DataSource = this.viewBangThuTienRowBindingSource.DataSource;
            rpt.Ngay.Value = this.ngayTinh;
        }

        private void fillRptGiayBaoNopTien(RptGiayBaoNopTien rpt)
        {
            rpt.ShowDieuHoa.Value = Settings.Default.ShowGiayBaoNopTienDieuHoa;
            rpt.ShowNote.Value = Settings.Default.ShowGiayBaoNopTienNote;
            rpt.NgayLapPhieu.Value = DateTime.Now;
            DateTime ngayTinh = this.ngayTinh;
            rpt.NgayNop.Value = ngayTinh;
            rpt.SoXuat.Value = DateTime.DaysInMonth(ngayTinh.Year, ngayTinh.Month) - DateTimeUtil.GetNumberDayOfWeekInMonth(ngayTinh.Year, ngayTinh.Month, DayOfWeek.Sunday);

            List<GiayBaoNopTientem> giayBaoNopTiens = new List<GiayBaoNopTientem>();

            SoThuTienService soThuTienService = new SoThuTienService();
            List<viewbangthutien> rows = soThuTienService.EvaluateViewBangThuTienRowsForReport(Entities, this.getViewBangThuTienRowsFromMainGrid(), this.ngayTinh);

            foreach (viewbangthutien viewBangThuTienRow in rows)
            {
                if (viewBangThuTienRow.NgayNopLan2 != null)
                {
                    continue;
                }

                GiayBaoNopTientem giayBaoNopTien = new GiayBaoNopTientem()
                {
                    HoTen = viewBangThuTienRow.HoTen,
                    Lop = viewBangThuTienRow.Lop,
                    Lan = viewBangThuTienRow.NgayNopLan1 == null ? 1 : 2,
                    SoTienAnSang = viewBangThuTienRow.SoTienAnSangThangNay,
                    SoTienAnToi = viewBangThuTienRow.SoTienAnToiThangNay,
                    SoTienKhoanThuChinh = viewBangThuTienRow.KhoanThuChinh,
                    SoTienDieuHoa = viewBangThuTienRow.SoTienDieuHoa == null ? 0 : viewBangThuTienRow.SoTienDieuHoa.Value,
                    SoTienNangKhieu = viewBangThuTienRow.SoTienNangKhieu == null ? 0 : viewBangThuTienRow.SoTienNangKhieu.Value,
                    SoTienDoDung = viewBangThuTienRow.SoTienDoDung == null ? 0 : viewBangThuTienRow.SoTienDoDung.Value,
                    SoTienNoThangTruoc = viewBangThuTienRow.SoTienTruyThu.Value,
                    SoTienAnSangThuaThangTruoc = viewBangThuTienRow.SoTienAnSangThangTruoc.Value,
                    SoTienAnToiThuaThangTruoc = viewBangThuTienRow.SoTienAnToiThangTruoc.Value,
                    SoTienAnTruaThuaThangTruoc = viewBangThuTienRow.SoTienSXThangTruoc.Value,
                    SoXuatAnSangThuaThangTruoc = viewBangThuTienRow.AnSangThangTruoc.Value,
                    SoXuatAnToiThuaThangTruoc = viewBangThuTienRow.AnToiThangTruoc,
                    SoXuatAnTruaThuaThangTruoc = viewBangThuTienRow.SXThangTruoc.Value
                };

                giayBaoNopTiens.Add(giayBaoNopTien);
            }

            rpt.GiayBaoNopTienDataSource.DataSource = giayBaoNopTiens;
        }
    }
}
