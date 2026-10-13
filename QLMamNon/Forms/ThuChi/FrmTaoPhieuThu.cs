using ACG.Core.WinForm.Util;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Entity;
using QLMamNon.Entity.Form;
using QLMamNon.Facade;
using QLMamNon.Properties;
using QLMamNon.Reports;
using QLMamNon.Service.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using static QLMamNon.Constant.PhanLoaiThuConstant;
using LayoutVisibility = DevExpress.XtraLayout.Utils.LayoutVisibility;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmTaoPhieuThu : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        public GridView GridView { get; set; }

        public bool IsEditing { get; set; }

        /// <summary>
        /// True khi phiếu thu đã được lưu thành công trong lần mở form này.
        /// Form gọi phải tự đặt lại về false trước khi ShowDialog.
        /// </summary>
        public bool IsSaved { get; set; }

        public phieuthu PhieuThuRow { get; set; }

        private Dictionary<int, BangKeThuTienItem> hocSinhIdToBangKeThuTienItems;

        private qlmamnonEntities entities;

        private bool isStaticDataLoaded;

        #endregion

        public FrmTaoPhieuThu()
        {
            this.FormKey = AppForms.FormTaoPhieuThu;
            InitializeComponent();
        }

        private void FrmTaoPhieuThu_Load(object sender, EventArgs e)
        {
            this.InitFormData();
        }

        /// <summary>
        /// Nạp dữ liệu vào các control của form. Phải gọi trước mỗi lần ShowDialog:
        /// FormFactory dùng lại cùng một instance nên sự kiện Load chỉ chạy đúng một
        /// lần, các lần mở sau form sẽ còn nguyên giá trị rỗng do resetForm() để lại
        /// và dxValidationProvider.Validate() sẽ chặn việc lưu.
        /// </summary>
        public void InitFormData()
        {
            if (!this.isStaticDataLoaded)
            {
                // Phân loại thu lấy từ cache dữ liệu tĩnh, không thay đổi trong một phiên làm việc.
                this.phanLoaiThuRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.PhanLoaiThu);
                this.isStaticDataLoaded = true;
            }

            // Danh sách học sinh và bảng kê thu tiền phải nạp lại mỗi lần mở form: FormFactory
            // dùng lại cùng một instance nên nếu chỉ nạp một lần thì học sinh mới không xuất hiện
            // và "Còn lại" vẫn tính theo số đã thu lúc mở form lần đầu.
            this.reloadPerOpenData();

            if (this.IsEditing && this.PhieuThuRow != null)
            {
                loadPhieuThu();
            }
            else
            {
                resetForm();
            }
        }

        /// <summary>
        /// Nạp lại danh sách học sinh và bảng kê thu tiền trên một context mới, rồi hủy context cũ.
        /// Dữ liệu được dựng xong vào biến cục bộ trước khi gán vào form, để khi nạp lỗi thì form
        /// vẫn giữ nguyên dữ liệu của lần mở trước thay vì còn một nửa.
        /// </summary>
        private void reloadPerOpenData()
        {
            qlmamnonEntities previousEntities = this.entities;
            qlmamnonEntities newEntities = StaticDataFacade.GetQLMNEntities();

            try
            {
                List<hocsinh> hocSinhTable = newEntities.getHocSinhForThongTinHocSinh(null, null, null, null, null, false, null).ToList();
                ThongTinHocSinhUtil.EvaluateLopInfoForHocSinhTable(newEntities, hocSinhTable);

                SoThuTienService soThuTienService = new SoThuTienService();
                DateTime endOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                List<BangKeThuTienItem> allBangKeThuTienItems = soThuTienService.GetBangKeTongHopThuTienForTaoPhieuThu(newEntities, endOfMonth, null);

                Dictionary<int, BangKeThuTienItem> bangKeThuTienItems = new Dictionary<int, BangKeThuTienItem>();

                foreach (BangKeThuTienItem item in allBangKeThuTienItems)
                {
                    // Một học sinh có thể có nhiều dòng bảng kê: lấy dòng đầu tiên, không để Add throw.
                    if (!bangKeThuTienItems.ContainsKey(item.HocSinhId))
                    {
                        bangKeThuTienItems.Add(item.HocSinhId, item);
                    }
                }

                this.entities = newEntities;
                this.hocSinhIdToBangKeThuTienItems = bangKeThuTienItems;
                this.hocSinhRowBindingSource.DataSource = hocSinhTable;
                this.hocSinhRowBindingSource.Filter = "LopDangHoc IS NOT NULL";
            }
            catch (Exception ex)
            {
                newEntities.Dispose();
                LoggerFacade.Error(ex, this.FormKey);
                LoggerFacade.Error("Không nạp được danh sách học sinh và bảng kê thu tiền cho form Tạo phiếu thu.", this.FormKey);
                MessageBox.Show("Không nạp được danh sách học sinh và số tiền còn lại. Xin vui lòng thử lại.",
                    "Lỗi nạp dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (previousEntities != null)
            {
                previousEntities.Dispose();
            }
        }

        private void FrmTaoPhieuThu_Enter(object sender, EventArgs e)
        {
            int hocSinhId = (int)this.GridView.GetFocusedRowCellValue("HocSinhId");
            int phanLoaiThuId = (int)this.GridView.GetFocusedRowCellValue("PhanLoaiThuId");
            this.cmbHocSinh.EditValue = hocSinhId;
            this.cmbPhanLoaiThu.EditValue = phanLoaiThuId;
        }

        protected void FrmTaoPhieuThu_Activated(object sender, EventArgs e)
        {
            FormMainFacade.SetFormCaption(this.FormKey);
        }

        private void FrmTaoPhieuThu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.IsEditing = false;
        }

        private void btnLuuTao_Click(object sender, EventArgs e)
        {
            if (this.onSavePhieuThu())
            {
                this.btnIn_Click(sender, e);
                // Form không đóng sau khi lưu, nên phải nạp lại bảng kê thu tiền: nếu không,
                // phiếu thu kế tiếp của cùng học sinh vẫn tính "Còn lại" theo số đã thu cũ.
                this.reloadPerOpenData();
                this.resetForm();
                FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.AddedAndAddingPhieuThuCaption);
                this.IsEditing = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (this.onSavePhieuThu())
            {
                this.Close();
                FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.AddedPhieuThuCaption);
            }
        }

        private bool onSavePhieuThu()
        {
            if (!this.dxValidationProvider.Validate())
            {
                return false;
            }

            this.luuPhieuThu();
            this.IsSaved = true;

            if (isSelectedPhanLoaiThuByHocSinh())
            {
                int? hocSinhId = this.getSelectedHocSinhId();

                if (hocSinhId == null)
                {
                    LoggerFacade.Error("Không tính lại truy thu vì phiếu thu theo học sinh nhưng chưa chọn học sinh.", this.FormKey);
                }
                else
                {
                    // Context riêng cho lần tính truy thu này: context của form đang giữ các dòng
                    // viewbangthutien của tháng hiện tại đã bị sửa trong bộ nhớ, dùng lại sẽ làm
                    // OriginalValues của bảng thu tiền tháng sau sai.
                    using (qlmamnonEntities truyThuEntities = StaticDataFacade.GetQLMNEntities())
                    {
                        TruyThuService truyThuService = new TruyThuService();
                        truyThuService.UpdateSoTienTruyThuForBangThuTienNextMonths(truyThuEntities, this.dateNgay.DateTime, hocSinhId.Value);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Id học sinh đang chọn, hoặc null khi phân loại thu không theo học sinh (cmbHocSinh
        /// để trống). EditValue có thể là null, DBNull hoặc chuỗi rỗng nên không ép kiểu trực tiếp.
        /// </summary>
        private int? getSelectedHocSinhId()
        {
            object editValue = this.cmbHocSinh.EditValue;

            if (editValue == null || editValue == DBNull.Value)
            {
                return null;
            }

            int hocSinhId;

            if (!int.TryParse(string.Format("{0}", editValue), out hocSinhId))
            {
                return null;
            }

            return hocSinhId;
        }

        private void loadPhieuThu()
        {
            this.dateNgay.DateTime = this.PhieuThuRow.Ngay;
            this.txtSoTien.Value = this.PhieuThuRow.SoTien;
            this.cbPaymentType.SelectedIndex = this.PhieuThuRow.PaymentType == PaymentType.TRANSFER.ToString() ? 1 : 0;
            this.txtMaPhieu.Text = this.PhieuThuRow.MaPhieu;
            this.txtGhiChu.Text = this.PhieuThuRow.GhiChu;
            this.cmbHocSinh.EditValue = this.PhieuThuRow.HocSinhId;
            this.cmbPhanLoaiThu.EditValue = this.PhieuThuRow.PhanLoaiThuId;
            this.txtSoTien_EditValueChanged(this.txtSoTien, EventArgs.Empty);
        }

        private void luuPhieuThu()
        {
            if (this.IsEditing)
            {
                this.updatePhieuThu();
            }
            else
            {
                this.insertPhieuThu();
            }
        }

        private void insertPhieuThu()
        {
            DateTime ngay = this.dateNgay.DateTime;
            long soTien = this.cbPaymentType.SelectedIndex == 0 ? (long)this.txtSoTien.Value : 0;
            long soTienChuyenKhoan = this.cbPaymentType.SelectedIndex == 1 ? (long)this.txtSoTien.Value : 0;
            string maPhieu = this.txtMaPhieu.Text;
            string ghiChu = this.txtGhiChu.Text;
            int? hocSinhId = this.getSelectedHocSinhId();
            int? phanLoaiThuId = (int?)this.cmbPhanLoaiThu.EditValue;
            PhieuThuService phieuThuService = new PhieuThuService();
            phieuThuService.InsertPhieuThu(ngay, soTien, soTienChuyenKhoan, maPhieu, ghiChu, hocSinhId, phanLoaiThuId);

            Settings.Default["LastMaPhieuThu"] = Settings.Default.LastMaPhieuThu + 1;
            Settings.Default.Save();
        }

        private void updatePhieuThu()
        {
            DateTime ngay = this.dateNgay.DateTime;
            long soTien = this.cbPaymentType.SelectedIndex == 0 ? (long)this.txtSoTien.Value : 0;
            long soTienChuyenKhoan = this.cbPaymentType.SelectedIndex == 1 ? (long)this.txtSoTien.Value : 0;
            string maPhieu = this.txtMaPhieu.Text;
            string ghiChu = this.txtGhiChu.Text;
            int? hocSinhId = this.getSelectedHocSinhId();
            int? phanLoaiThuId = (int?)this.cmbPhanLoaiThu.EditValue;
            PhieuThuService phieuThuService = new PhieuThuService();
            phieuThuService.UpdatePhieuThu(this.PhieuThuRow, ngay, soTien, soTienChuyenKhoan, maPhieu, ghiChu, hocSinhId, phanLoaiThuId);
        }

        private void resetForm()
        {
            this.txtSoTien.Value = 0;
            this.cbPaymentType.SelectedIndex = 0;
            this.txtMaPhieu.Text = $"{Settings.Default.LastMaPhieuThu + 1}";
            this.txtGhiChu.Text = "";
            this.txtConLai.Text = "";
            this.cmbHocSinh.EditValue = null;
        }

        private void cmbPhanLoaiThu_EditValueChanged(object sender, EventArgs e)
        {
            LayoutVisibility enableCmbHocSinhId = isSelectedPhanLoaiThuByHocSinh() ? LayoutVisibility.Always : LayoutVisibility.Never;
            // lciCmbHocSinh.Visibility = enableCmbHocSinhId;
            lciTxtConLai.Visibility = enableCmbHocSinhId;
        }

        private bool isSelectedPhanLoaiThuByHocSinh()
        {
            string selectedPhanLoaiThuId = string.Format("{0}", cmbPhanLoaiThu.EditValue);
            string[] phanLoaiThuByHocSinhs = StringUtil.Split(Settings.Default.PhanLoaiThuByHocSinh, ",");

            return ArrayUtil.Contains(phanLoaiThuByHocSinhs, selectedPhanLoaiThuId);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            RptPhieuThuTien rptPhieuThuTien = new RptPhieuThuTien();
            rptPhieuThuTien.NgayNop.Value = dateNgay.DateTime;
            rptPhieuThuTien.HoTenHS.Value = cmbHocSinh.Text;
            rptPhieuThuTien.SoTien.Value = this.txtSoTien.Value;
            //rptPhieuThuTien.SoTienChuyenKhoan.Value = this.cbPaymentType.SelectedIndex == 1 ? (long)this.txtSoTien.Value : 0;
            rptPhieuThuTien.ConLai.Value = txtConLai.EditValue;

            // Phiếu thu của phân loại thu không theo học sinh thì không có học sinh được chọn:
            // bỏ qua việc tra lớp và không in tham số Lop.
            int? hocSinhId = this.getSelectedHocSinhId();

            if (hocSinhId != null && this.entities != null)
            {
                Dictionary<int, lop> hocSinhIdToLops = StaticDataUtil.GetLopsByHocSinhIds(this.entities, new List<int>() { hocSinhId.Value }, dateNgay.DateTime);

                if (hocSinhIdToLops.ContainsKey(hocSinhId.Value))
                {
                    rptPhieuThuTien.Lop.Value = hocSinhIdToLops[hocSinhId.Value].Name;
                }
            }

            AuthenticatedEntity authenticatedEntity = StaticDataFacade.Get(StaticDataKeys.AuthenticatedData) as AuthenticatedEntity;

            if (authenticatedEntity != null && authenticatedEntity.User != null)
            {
                rptPhieuThuTien.NguoiThu.Value = authenticatedEntity.User.UserName;
            }
            else
            {
                LoggerFacade.Error("Không xác định được người thu khi in phiếu thu: thiếu dữ liệu đăng nhập.", this.FormKey);
            }

            ReportPrintTool printTool = new ReportPrintTool(rptPhieuThuTien);
            printTool.Print((string)Settings.Default["POSPrinterName"]);
        }

        private void btnChonMayIn_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                string printerName = printDialog1.PrinterSettings.PrinterName;
                Settings.Default["POSPrinterName"] = printerName;
                Settings.Default.Save();
            }
        }

        private void cmbHocSinh_EditValueChanged(object sender, EventArgs e)
        {
            if (IsEditing)
            {
                return;
            }

            int? hocSinhId = this.getSelectedHocSinhId();

            if (hocSinhId != null && hocSinhIdToBangKeThuTienItems != null && hocSinhIdToBangKeThuTienItems.ContainsKey(hocSinhId.Value))
            {
                BangKeThuTienItem bangKeThuTienItem = hocSinhIdToBangKeThuTienItems[hocSinhId.Value];
                txtSoTien.Value = (decimal)(bangKeThuTienItem.PhaiThu - bangKeThuTienItem.DaThu);
            }
            else
            {
                txtSoTien.Value = 0;
            }
        }

        private void txtSoTien_EditValueChanged(object sender, EventArgs e)
        {
            int? hocSinhId = this.getSelectedHocSinhId();

            if (hocSinhId != null && hocSinhIdToBangKeThuTienItems != null && hocSinhIdToBangKeThuTienItems.ContainsKey(hocSinhId.Value))
            {
                BangKeThuTienItem bangKeThuTienItem = hocSinhIdToBangKeThuTienItems[hocSinhId.Value];
                double soTienConLai = 0;
                if (IsEditing && PhieuThuRow != null)
                {
                    soTienConLai = bangKeThuTienItem.PhaiThu - bangKeThuTienItem.DaThu + PhieuThuRow.SoTien - Convert.ToDouble(txtSoTien.EditValue);
                }
                else
                {
                    soTienConLai = bangKeThuTienItem.PhaiThu - bangKeThuTienItem.DaThu - Convert.ToDouble(txtSoTien.EditValue);
                }

                txtConLai.EditValue = string.Format("{0:n0}", soTienConLai);
            }
            else
            {
                txtConLai.EditValue = 0;
            }
        }
    }
}