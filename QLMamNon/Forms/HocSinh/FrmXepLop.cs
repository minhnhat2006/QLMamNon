using ACG.Core.WinForm.Util;
using DevExpress.XtraGrid.Views.Base;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Service.Data;
using QLMamNon.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace QLMamNon.Forms.HocSinh
{
    public partial class FrmXepLop : CRUDForm<hocsinh>
    {
        // Số tên học sinh liệt kê tối đa trong hộp thoại xác nhận xóa, để hộp thoại không quá dài.
        private const int MaxHocSinhNamesInConfirmDialog = 10;

        public FrmXepLop()
        {
            InitializeComponent();

            this.FormKey = AppForms.FormXepLop;
            // Dùng cho thông báo lỗi của CRUDForm.onSavingError ("Không thể lưu Xếp lớp...").
            this.DanhMuc = "Xếp lớp";
            this.InitForm(null, null, null, this.btnSave, null, this.gvDen, null);
        }

        private void FrmXepLop_Load(object sender, EventArgs e)
        {

            this.gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(GridViewMain_CustomDrawCell);
            this.gridView1.CustomColumnDisplayText += new CustomColumnDisplayTextEventHandler(gvMain_CustomColumnDisplayText);

            this.gvDen.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(GridViewMain_CustomDrawCell);
            this.gvDen.CustomColumnDisplayText += new CustomColumnDisplayTextEventHandler(gvMain_CustomColumnDisplayText);
            this.gvDen.OptionsEditForm.CustomEditFormLayout = new UCEditFormXepLop();

            this.Entities.hocsinhs.Load();
            this.hocSinhRowBindingSourceDi.DataSource = this.Entities.hocsinhs.Local.ToBindingList();
            this.namHocBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.NamHoc);
            this.lopRowBindingSourceDi.DataSource = StaticDataFacade.Get(StaticDataKeys.LopHoc);
            this.lopRowBindingSourceDen.DataSource = StaticDataFacade.Get(StaticDataKeys.LopHoc);

            this.cmbNamHocDi.EditValue = DateTime.Now.Year;
        }

        private void gvMain_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (e.Column.FieldName == "GioiTinh" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                sbyte gioiTinh = (sbyte)view.GetListSourceRowCellValue(e.ListSourceRowIndex, "GioiTinh");
                switch (gioiTinh)
                {
                    case 0: e.DisplayText = "Nữ"; break;
                    case 1: e.DisplayText = "Nam"; break;
                }
            }
            else if (e.Column.FieldName == "NgayVaoLop" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                // Ưu tiên NgayVaoLopDisplay (hocsinh_lop.DateJoin thật do util gán); học sinh vừa thêm vào lớp
                // chưa có giá trị này nên dùng NgayVaoLop (DateTime.Now hoặc ngày người dùng nhập).
                object ngayVaoLop = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "NgayVaoLopDisplay");
                if (!(ngayVaoLop is DateTime))
                {
                    ngayVaoLop = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "NgayVaoLop");
                }

                if (ngayVaoLop is DateTime)
                {
                    e.DisplayText = ((DateTime)ngayVaoLop).ToString("d");
                }
            }
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            if (ControlUtil.IsEditValueNull(this.cmbLopHocDen))
            {
                MessageBox.Show("Xin vui long chọn Lớp học", "Chọn lớp học", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.hocSinhRowBindingSourceDi.Current == null)
            {
                return;
            }

            hocsinh oldRow = this.hocSinhRowBindingSourceDi.Current as hocsinh;

            if (this.isHocSinhExisted(this.hocSinhRowBindingSourceDen, oldRow.HocSinhId))
            {
                return;
            }

            hocsinh newRow = this.hocSinhRowBindingSourceDen.AddNew() as hocsinh;
            copyHocSinhRow(oldRow, newRow);
            newRow.LopDangHoc = this.cmbLopHocDen.Text;
            newRow.NgayVaoLop = DateTime.Now;

            this.gvDen.ShowEditForm();
        }

        private static void copyHocSinhRow(hocsinh oldRow, hocsinh newRow)
        {
            newRow.HocSinhId = oldRow.HocSinhId;
            newRow.HoDem = oldRow.HoDem;
            newRow.Ten = oldRow.Ten;
            newRow.GioiTinh = oldRow.GioiTinh;
            newRow.ThoiHoc = oldRow.ThoiHoc;
            newRow.NgaySinh = oldRow.NgaySinh;
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            if (this.hocSinhRowBindingSourceDen.Current == null)
            {
                return;
            }

            hocsinh oldRow = this.hocSinhRowBindingSourceDen.Current as hocsinh;

            if (this.isHocSinhExisted(this.hocSinhRowBindingSourceDi, oldRow.HocSinhId))
            {
                hocsinh hocSinhRowDi = findHocSinhById(this.hocSinhRowBindingSourceDi, oldRow.HocSinhId);
                if (hocSinhRowDi != null)
                {
                    hocSinhRowDi.LopDangHoc = CommonConstant.EMPTY;
                }

                this.gvDen.DeleteSelectedRows();

                return;
            }

            hocsinh newRow = this.hocSinhRowBindingSourceDi.AddNew() as hocsinh;
            copyHocSinhRow(oldRow, newRow);

            this.gvDen.DeleteSelectedRows();
        }

        private void cmbLopHocDi_EditValueChanged(object sender, EventArgs e)
        {
            this.loadHocSinhToGridDi();
        }

        private void cmbNamHocDi_EditValueChanged(object sender, EventArgs e)
        {
            this.loadHocSinhToGridDi();
        }

        private void cmbLopHocDen_EditValueChanged(object sender, EventArgs e)
        {
            this.loadHocSinhToGridDen();
        }

        // Form này lưu hoàn toàn bằng stored procedure (insertHocSinhToLop, deleteHocSinhLopByHocSinhIds)
        // và SoThuTienService. Cố ý KHÔNG gọi base.onSaving() và KHÔNG gọi SaveChanges(): các entity
        // hocsinh ở hai lưới chỉ dùng để chở dữ liệu hiển thị, ghi bằng SaveChanges() sẽ sửa sai bảng
        // hocsinh (ví dụ LopDangHoc/NgayVaoLop đang được dùng như giá trị tạm). Đừng "sửa" lại bằng cách
        // thêm base.onSaving() hoặc SaveChanges().
        protected override void onSaving()
        {
            try
            {
                this.saveXepLop();
            }
            catch (Exception ex)
            {
                // CRUDForm.onSaving bọc thân hàm trong try/catch rồi gọi onSavingError; override này cố ý
                // không gọi base nên phải tự bắt lỗi, nếu không một stored procedure lỗi (hoặc không tồn
                // tại trên CSDL đang dùng) sẽ thành hộp thoại crash, không có log và không rõ đã ghi được
                // tới đâu. Ba procedure ở đây không nằm trong cùng một transaction.
                this.onSavingError(ex);
            }
        }

        // Thân thật của onSaving (xem comment ở trên); mọi lỗi do onSaving bắt và báo qua onSavingError.
        private void saveXepLop()
        {
            if (ControlUtil.IsEditValueNull(this.cmbLopHocDen))
            {
                MessageBox.Show("Xin vui long chọn Lớp học", "Chọn lớp học", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int lop = (int)cmbLopHocDen.EditValue;

            if (this.loadedLopDen == null || this.loadedLopDen.Value != lop)
            {
                // Lưới "chuyển đến" không phải của lớp đang chọn: chưa nạp được lần nào, hoặc đổi lớp mà
                // lần nạp lại bị lỗi. So lưới của lớp khác với dữ liệu của lớp này sẽ biến toàn bộ lớp
                // này thành "đã bị bỏ khỏi lớp", nên không cho lưu, kể cả khi người dùng xác nhận.
                string loadedLopText = this.loadedLopDen == null ? "(chưa nạp)" : this.loadedLopDen.Value.ToString();
                LoggerFacade.Error($"Không lưu xếp lớp: lưới đang chứa dữ liệu của lớp {loadedLopText} nhưng đang lưu cho lớp {lop}.", this.FormKey);
                MessageBox.Show($"Danh sách học sinh chưa được nạp cho lớp {this.cmbLopHocDen.Text}.\n\n" +
                    "Vui lòng chọn lại lớp học để nạp lại danh sách rồi lưu.",
                    "Không thể lưu xếp lớp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<hocsinh> hocSinhTable = Entities.getHocSinhByParams(DateTime.Now, null, lop, null).ToList();

            List<int> hocSinhIds = new List<int>();
            foreach (hocsinh hocSinhRow in hocSinhRowBindingSourceDen)
            {
                hocSinhIds.Add(hocSinhRow.HocSinhId);
            }

            // Học sinh CSDL còn xếp trong lớp nhưng lưới "chuyển đến" không còn: sẽ bị xóa khỏi lớp và
            // xóa luôn bảng thu tiền. Tính trước khi ghi bất cứ thứ gì để người dùng trả lời Không là
            // không có gì thay đổi.
            List<hocsinh> deletingHocSinhRows = new List<hocsinh>();
            List<int> deletingHocSinhIds = new List<int>();
            foreach (hocsinh hsRow in hocSinhTable)
            {
                // getHocSinhByParams có thể trả về hai dòng cho cùng một học sinh (dòng hocsinh_lop cũ
                // với DateLeft ở tương lai và dòng mới), nên phải lọc trùng theo HocSinhId: nếu không,
                // hộp thoại xác nhận sẽ đếm và liệt kê trùng tên.
                if (!hocSinhIds.Contains(hsRow.HocSinhId) && !deletingHocSinhIds.Contains(hsRow.HocSinhId))
                {
                    deletingHocSinhRows.Add(hsRow);
                    deletingHocSinhIds.Add(hsRow.HocSinhId);
                }
            }

            if (!ListUtil.IsEmpty(deletingHocSinhRows) && !this.confirmXoaHocSinhKhoiLop(deletingHocSinhRows))
            {
                // Chưa gọi stored procedure nào: hocsinh_lop và bangthutien giữ nguyên.
                return;
            }

            List<string> skippedHocSinhNames = new List<string>();

            foreach (hocsinh hocSinhRow in hocSinhRowBindingSourceDen)
            {
                int hocSinh = hocSinhRow.HocSinhId;
                bool ngayVaoLopChanged = this.isNgayVaoLopChangedByUser(hocSinhRow);
                EntityState state = Entities.Entry(hocSinhRow).State;

                bool needsInsert = ngayVaoLopChanged
                    || state == EntityState.Modified
                    || state == EntityState.Added
                    || state == EntityState.Detached;

                if (!needsInsert)
                {
                    continue;
                }

                DateTime? ngayVaoLop;
                if (ngayVaoLopChanged)
                {
                    // Người dùng vừa sửa ngày vào lớp trong form: giá trị của editor (NgayVaoLop) mới là
                    // ý muốn của họ, không phải DateJoin cũ trong NgayVaoLopDisplay.
                    ngayVaoLop = hocSinhRow.NgayVaoLop;
                }
                else
                {
                    // Display-first: thành viên cũ có DateJoin thật từ util; dòng mới chỉ có NgayVaoLop.
                    ngayVaoLop = hocSinhRow.NgayVaoLopDisplay != null ? hocSinhRow.NgayVaoLopDisplay : hocSinhRow.NgayVaoLop;
                }

                if (ngayVaoLop == null)
                {
                    // DateJoin NULL sẽ làm hỏng hocsinh_lop (học sinh biến mất khỏi lớp), nên bỏ qua dòng này.
                    LoggerFacade.Error("Bỏ qua xếp lớp vì thiếu ngày vào lớp: HocSinhId=" + hocSinh, this.FormKey);
                    skippedHocSinhNames.Add(hocSinhRow.HoTen);
                    continue;
                }

                Entities.insertHocSinhToLop(hocSinh, lop, ngayVaoLop);

                // Ghi xong thì DateJoin thật trong hocsinh_lop chính là ngayVaoLop: cập nhật lại giá trị
                // hiển thị và ảnh chụp để lần lưu sau không ghi lùi về ngày cũ.
                hocSinhRow.NgayVaoLopDisplay = ngayVaoLop;
                this.ngayVaoLopSnapshots[hocSinh] = hocSinhRow.NgayVaoLop;
            }

            if (skippedHocSinhNames.Count > 0)
            {
                MessageBox.Show("Không thể xếp lớp cho các học sinh sau vì thiếu ngày vào lớp:\n" + string.Join("\n", skippedHocSinhNames),
                    "Thiếu ngày vào lớp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (!ListUtil.IsEmpty(deletingHocSinhIds))
            {
                DateTime ngayRoiLop = DateTime.Now.AddDays(-1);
                DateTime ngayXoaBangThuTien = DateTimeUtil.DateEndOfMonth(DateTime.Now);

                // Ghi log trước khi xóa để một lần xóa nhầm vẫn truy được ra đã xóa ai, ngày nào.
                LoggerFacade.Info($"Xóa học sinh khỏi lớp {lop}: HocSinhIds={StringUtil.JoinWithCommas(deletingHocSinhIds)}; DateLeft={ngayRoiLop:dd/MM/yyyy}; xóa bảng thu tiền đến {ngayXoaBangThuTien:dd/MM/yyyy}.", this.FormKey);

                Entities.deleteHocSinhLopByHocSinhIds(StringUtil.Join(deletingHocSinhIds, ","), ngayRoiLop);

                SoThuTienService soThuTienService = new SoThuTienService();
                soThuTienService.DeleteBangThuTienByHocSinhIdsAndDate(Entities, deletingHocSinhIds, ngayXoaBangThuTien);

                foreach (int deletedHocSinhId in deletingHocSinhIds)
                {
                    this.ngayVaoLopSnapshots.Remove(deletedHocSinhId);
                }
            }

            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SavedCaption);
        }

        private bool confirmXoaHocSinhKhoiLop(List<hocsinh> deletingHocSinhRows)
        {
            List<string> hocSinhNames = new List<string>();
            for (int i = 0; i < deletingHocSinhRows.Count && i < MaxHocSinhNamesInConfirmDialog; i++)
            {
                hocSinhNames.Add("- " + deletingHocSinhRows[i].HoTen);
            }

            // deleteHocSinhLopByHocSinhIds chỉ lọc theo HocSinhId, không lọc LopId (DB/qlmamnon.sql), nên
            // nó đóng mọi dòng hocsinh_lop còn mở của những học sinh này - ở tất cả các lớp. Hộp thoại
            // xác nhận phải nói đúng phạm vi đó.
            string message = $"Có {deletingHocSinhRows.Count} học sinh không còn trong danh sách của lớp {this.cmbLopHocDen.Text}. "
                + "Các học sinh sau sẽ bị xóa khỏi tất cả các lớp đang theo học (không chỉ lớp này), "
                + "đồng thời xóa bảng thu tiền của tháng này:\n\n"
                + string.Join("\n", hocSinhNames);

            if (deletingHocSinhRows.Count > MaxHocSinhNamesInConfirmDialog)
            {
                message += $"\n… và {deletingHocSinhRows.Count - MaxHocSinhNamesInConfirmDialog} học sinh khác";
            }

            message += "\n\nBạn có chắc muốn xóa không? Chọn Không để hủy toàn bộ việc lưu.";

            DialogResult confirmResult = MessageBox.Show(message, "Xóa học sinh khỏi lớp", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (confirmResult != DialogResult.Yes)
            {
                LoggerFacade.Info($"Người dùng hủy việc xóa {deletingHocSinhRows.Count} học sinh khỏi lớp {this.cmbLopHocDen.Text}.", this.FormKey);
                return false;
            }

            return true;
        }

        private void gvDen_RowUpdated(object sender, RowObjectEventArgs e)
        {
            if (!this.gvDen.IsNewItemRow(e.RowHandle))
            {
                return;
            }

            hocsinh addedRow = e.Row as hocsinh;
            if (addedRow == null)
            {
                return;
            }

            // Xóa đúng học sinh vừa được chuyển sang lớp mới khỏi lưới "chuyển đi": RemoveCurrent() xóa
            // dòng đang được chọn ở lưới đó, không chắc là dòng vừa chuyển.
            removeHocSinhById(this.hocSinhRowBindingSourceDi, addedRow.HocSinhId);
        }
    }
}