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

        /// <summary>
        /// Tên cột HocSinhId trên lưới phiếu thu.
        /// </summary>
        private const string HocSinhIdFieldName = "HocSinhId";

        /// <summary>
        /// Tên cột PhanLoaiThuId trên lưới phiếu thu.
        /// </summary>
        private const string PhanLoaiThuIdFieldName = "PhanLoaiThuId";

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

        /// <summary>
        /// True khi dữ liệu của lần mở form này đã được nạp. Đặt lại về false khi form đóng, nên
        /// mỗi lần mở form dữ liệu được nạp đúng một lần, bất kể form được mở từ FrmPhieuThu
        /// (có gọi InitFormData trước ShowDialog) hay trực tiếp từ ribbon (không gọi).
        /// </summary>
        private bool perOpenDataLoaded;

        #endregion

        public FrmTaoPhieuThu()
        {
            this.FormKey = AppForms.FormTaoPhieuThu;
            InitializeComponent();
            this.Shown += new EventHandler(this.FrmTaoPhieuThu_Shown);
        }

        private void FrmTaoPhieuThu_Load(object sender, EventArgs e)
        {
            if (!this.perOpenDataLoaded)
            {
                this.InitFormData();
            }
        }

        private void FrmTaoPhieuThu_Shown(object sender, EventArgs e)
        {
            // Mở trực tiếp từ ribbon thì không ai gọi InitFormData, và sự kiện Load chỉ chạy một
            // lần cho cả đời form vì FormFactory dùng lại cùng một instance.
            if (!this.perOpenDataLoaded)
            {
                this.InitFormData();
            }
        }

        /// <summary>
        /// Nạp dữ liệu vào các control của form. FrmPhieuThu gọi trước mỗi lần ShowDialog; nếu
        /// không ai gọi thì sự kiện Shown gọi thay, nên dữ liệu luôn được nạp đúng một lần cho
        /// mỗi lần mở form.
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

            this.perOpenDataLoaded = true;
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

                this.hocSinhIdToBangKeThuTienItems = bangKeThuTienItems;
                this.hocSinhRowBindingSource.DataSource = hocSinhTable;
                this.hocSinhRowBindingSource.Filter = "LopDangHoc IS NOT NULL";
                // Gán cuối cùng: nếu một trong các lệnh trên lỗi thì form vẫn giữ context cũ đang
                // dùng được, còn context mới bị hủy trong catch.
                this.entities = newEntities;
            }
            catch (Exception ex)
            {
                if (!object.ReferenceEquals(this.entities, newEntities))
                {
                    newEntities.Dispose();
                }

                LoggerFacade.Error(ex, this.FormKey);
                LoggerFacade.Error("Không nạp được danh sách học sinh và bảng kê thu tiền cho form Tạo phiếu thu.", this.FormKey);
                MessageBox.Show("Không nạp được danh sách học sinh và số tiền còn lại. Xin vui lòng thử lại.",
                    "Lỗi nạp dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (previousEntities != null && !object.ReferenceEquals(previousEntities, this.entities))
            {
                previousEntities.Dispose();
            }
        }

        private void FrmTaoPhieuThu_Enter(object sender, EventArgs e)
        {
            // GridView chỉ được FrmPhieuThu gán; mở form trực tiếp từ ribbon thì nó là null.
            if (this.GridView == null)
            {
                return;
            }

            int? hocSinhId = toNullableInt(this.GridView.GetFocusedRowCellValue(HocSinhIdFieldName));
            int? phanLoaiThuId = toNullableInt(this.GridView.GetFocusedRowCellValue(PhanLoaiThuIdFieldName));

            if (hocSinhId != null)
            {
                this.cmbHocSinh.EditValue = hocSinhId.Value;
            }

            if (phanLoaiThuId != null)
            {
                this.cmbPhanLoaiThu.EditValue = phanLoaiThuId.Value;
            }
        }

        protected void FrmTaoPhieuThu_Activated(object sender, EventArgs e)
        {
            FormMainFacade.SetFormCaption(this.FormKey);
        }

        private void FrmTaoPhieuThu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.IsEditing = false;
            // Lần mở sau phải nạp lại dữ liệu: học sinh mới và số đã thu đều có thể đã thay đổi.
            this.perOpenDataLoaded = false;
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

            bool isPhanLoaiThuByHocSinh = this.isSelectedPhanLoaiThuByHocSinh();
            int? hocSinhId = this.getSelectedHocSinhId();

            if (isPhanLoaiThuByHocSinh && hocSinhId == null)
            {
                // Phải chặn trước khi lưu: một phiếu thu học phí với HocSinhId NULL vừa không in
                // được lớp, vừa không tính lại được truy thu của tháng sau.
                LoggerFacade.Error("Không lưu phiếu thu: phân loại thu theo học sinh nhưng chưa chọn học sinh.", this.FormKey);
                MessageBox.Show("Xin vui lòng chọn học sinh", "Chọn học sinh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            this.luuPhieuThu();
            this.IsSaved = true;

            if (isPhanLoaiThuByHocSinh)
            {
                // Context riêng cho lần tính truy thu này: context của form đang giữ các dòng
                // viewbangthutien của tháng hiện tại đã bị sửa trong bộ nhớ.
                try
                {
                    using (qlmamnonEntities truyThuEntities = StaticDataFacade.GetQLMNEntities())
                    {
                        TruyThuService truyThuService = new TruyThuService();
                        truyThuService.UpdateSoTienTruyThuForBangThuTienNextMonths(truyThuEntities, this.dateNgay.DateTime, hocSinhId.Value);
                    }
                }
                catch (Exception ex)
                {
                    // Phiếu thu đã được lưu ở trên: chỉ báo lỗi, không để người dùng lưu lại lần nữa.
                    LoggerFacade.Error(ex, this.FormKey);
                    MessageBox.Show(String.Format("Đã lưu phiếu thu nhưng không thể cập nhật lại số tiền truy thu của tháng sau.\n\n{0}", ex.Message),
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            return toNullableInt(this.cmbHocSinh.EditValue);
        }

        /// <summary>
        /// Giá trị int của một EditValue hay một ô trên lưới, hoặc null khi trống, DBNull hoặc
        /// không phải số.
        /// </summary>
        private static int? toNullableInt(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }

            int result;

            if (!int.TryParse(string.Format("{0}", value), out result))
            {
                return null;
            }

            return result;
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