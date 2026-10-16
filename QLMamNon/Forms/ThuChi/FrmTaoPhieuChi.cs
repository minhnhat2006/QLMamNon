using System;
using System.Data.Entity;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Service.Data;
using static QLMamNon.Constant.PhanLoaiThuConstant;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmTaoPhieuChi : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        /// <summary>
        /// Tên cột DienGiai của phân loại chi trong cmbPhanLoaiChi.
        /// </summary>
        private const string PhanLoaiChiDienGiaiFieldName = "DienGiai";

        protected string FormKey { get; set; }

        public GridView GridView { get; set; }

        public bool IsEditing { get; set; }

        public bool IsLoading { get; set; }

        /// <summary>
        /// True khi phiếu chi đã được lưu thành công trong lần mở form này.
        /// Form gọi phải tự đặt lại về false trước khi ShowDialog.
        /// </summary>
        public bool IsSaved { get; set; }

        public phieuchi PhieuChiRow { get; set; }

        private qlmamnonEntities entities;

        /// <summary>
        /// Diễn giải của phân loại chi được chọn trước đó, để biết nội dung đang hiển thị là do
        /// chương trình điền hay do người dùng tự nhập.
        /// </summary>
        private string previousPhanLoaiChiDienGiai;

        /// <summary>
        /// True khi dữ liệu của lần mở form này đã được nạp. Đặt lại về false khi form đóng, nên
        /// mỗi lần mở form dữ liệu được nạp đúng một lần, bất kể form được mở từ FrmPhieuChi
        /// (có gọi InitFormData trước ShowDialog) hay trực tiếp từ ribbon (không gọi).
        /// </summary>
        private bool perOpenDataLoaded;

        #endregion

        public FrmTaoPhieuChi()
        {
            this.FormKey = AppForms.FormTaoPhieuChi;
            // Context được tạo lại trong InitFormData() mỗi lần mở form.
            InitializeComponent();
            this.Shown += new EventHandler(this.FrmTaoPhieuChi_Shown);
        }

        private void FrmTaoPhieuChi_Load(object sender, EventArgs e)
        {
            if (!this.perOpenDataLoaded)
            {
                this.InitFormData();
            }
        }

        private void FrmTaoPhieuChi_Shown(object sender, EventArgs e)
        {
            // Mở trực tiếp từ ribbon thì không ai gọi InitFormData, và sự kiện Load chỉ chạy một
            // lần cho cả đời form vì FormFactory dùng lại cùng một instance.
            if (!this.perOpenDataLoaded)
            {
                this.InitFormData();
            }
        }

        /// <summary>
        /// Nạp dữ liệu vào các control của form. FrmPhieuChi gọi trước mỗi lần ShowDialog; nếu
        /// không ai gọi thì sự kiện Shown gọi thay, nên dữ liệu luôn được nạp đúng một lần cho
        /// mỗi lần mở form.
        /// </summary>
        public void InitFormData()
        {
            // Phân loại chi phải nạp lại mỗi lần mở form: FormFactory dùng lại cùng một instance
            // nên nếu chỉ nạp một lần thì phân loại chi mới thêm trong Danh mục không xuất hiện
            // cho tới khi khởi động lại chương trình.
            this.reloadPhanLoaiChi();

            if (this.IsEditing && this.PhieuChiRow != null)
            {
                this.loadPhieuChi();
            }
            else
            {
                this.resetForm();
            }

            this.perOpenDataLoaded = true;
        }

        /// <summary>
        /// Nạp lại danh sách phân loại chi trên một context mới, rồi hủy context cũ. Chỉ gán vào
        /// form sau khi nạp xong, để khi nạp lỗi thì form vẫn giữ nguyên dữ liệu của lần mở trước.
        /// </summary>
        private void reloadPhanLoaiChi()
        {
            qlmamnonEntities previousEntities = this.entities;
            qlmamnonEntities newEntities = StaticDataFacade.GetQLMNEntities();

            try
            {
                newEntities.phanloaichis.Load();
                this.phanLoaiChiRowBindingSource.DataSource = newEntities.phanloaichis.Local.ToBindingList();
                // Gán cuối cùng: nếu các lệnh trên lỗi thì form vẫn giữ context cũ đang dùng được,
                // còn context mới bị hủy trong catch.
                this.entities = newEntities;
            }
            catch (Exception ex)
            {
                if (!object.ReferenceEquals(this.entities, newEntities))
                {
                    newEntities.Dispose();
                }

                LoggerFacade.Error(ex, this.FormKey);
                LoggerFacade.Error("Không nạp được danh sách phân loại chi cho form Tạo phiếu chi.", this.FormKey);
                MessageBox.Show("Không nạp được danh sách phân loại chi. Xin vui lòng thử lại.",
                    "Lỗi nạp dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (previousEntities != null && !object.ReferenceEquals(previousEntities, this.entities))
            {
                previousEntities.Dispose();
            }
        }

        protected void FrmTaoPhieuChi_Activated(object sender, EventArgs e)
        {
            FormMainFacade.SetFormCaption(this.FormKey);
        }

        private void FrmTaoPhieuChi_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.IsEditing = false;
            // Lần mở sau phải nạp lại danh sách phân loại chi.
            this.perOpenDataLoaded = false;
        }

        private void btnLuuTao_Click(object sender, EventArgs e)
        {
            if (this.onSavePhieuChi())
            {
                this.resetForm();
                FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.AddedAndAddingPhieuChiCaption);
                // Phải trả về chế độ thêm mới, nếu không lần lưu kế tiếp sẽ ghi đè phiếu chi vừa sửa.
                this.IsEditing = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (this.onSavePhieuChi())
            {
                this.Close();
                FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.AddedPhieuChiCaption);
            }
        }

        /// <summary>
        /// Kiểm tra và lưu phiếu chi. Trả về false khi không lưu được, để người gọi không đóng
        /// form hay đặt lại trạng thái như khi đã lưu thành công.
        /// </summary>
        private bool onSavePhieuChi()
        {
            if (!this.dxValidationProvider.Validate())
            {
                return false;
            }

            int? phanLoaiChiId = this.getSelectedPhanLoaiChiId();

            if (phanLoaiChiId == null)
            {
                MessageBox.Show("Xin vui lòng chọn phân loại chi", "Chọn phân loại chi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            this.luuPhieuChi(phanLoaiChiId.Value);
            this.IsSaved = true;

            return true;
        }

        /// <summary>
        /// Id phân loại chi đang chọn, hoặc null khi chưa chọn. EditValue có thể là null, DBNull
        /// hoặc chuỗi rỗng nên không ép kiểu trực tiếp.
        /// </summary>
        private int? getSelectedPhanLoaiChiId()
        {
            object editValue = this.cmbPhanLoaiChi.EditValue;

            if (editValue == null || editValue == DBNull.Value)
            {
                return null;
            }

            int phanLoaiChiId;

            if (!int.TryParse(string.Format("{0}", editValue), out phanLoaiChiId))
            {
                return null;
            }

            return phanLoaiChiId;
        }

        private void loadPhieuChi()
        {
            this.dateNgay.DateTime = this.PhieuChiRow.Ngay;
            this.txtSoLuong.Value = this.PhieuChiRow.SoLuong == null ? 0m : (decimal)this.PhieuChiRow.SoLuong.Value;
            this.txtDonGia.Value = this.PhieuChiRow.DonGia == null ? 0m : (decimal)this.PhieuChiRow.DonGia.Value;
            this.txtSoTien.Value = this.PhieuChiRow.SoTien;
            this.cbPaymentType.SelectedIndex = this.PhieuChiRow.PaymentTypeEnum == PaymentType.TRANSFER ? 1 : 0;
            this.txtMaPhieu.Text = this.PhieuChiRow.MaPhieu;
            this.txtGhiChu.Text = this.PhieuChiRow.GhiChu;
            this.cmbPhanLoaiChi.EditValue = this.PhieuChiRow.PhanLoaiChiId;
            this.txtNoiDung.Text = this.PhieuChiRow.NoiDung;
        }

        private void luuPhieuChi(int phanLoaiChiId)
        {
            if (this.IsEditing)
            {
                this.updatePhieuChi(phanLoaiChiId);
            }
            else
            {
                this.insertPhieuChi(phanLoaiChiId);
            }

            //if (this.GridView != null)
            //{
            //    this.GridView.BeginUpdate();
            //    try
            //    {
            //        object focusedRowId = this.GridView.GetFocusedRowCellValue("PhieuChiId");

            //        BindingSource phieuChiBindingSource = this.GridView.GridControl.DataSource as BindingSource;
            //        PhieuChiService phieuChiService = new PhieuChiService();
            //        phieuChiBindingSource.DataSource = phieuChiService.LoadPhieuChi(entities);

            //        if (focusedRowId != null)
            //        {
            //            int newRowHandle = this.GridView.LocateByValue("PhieuChiId", focusedRowId);
            //            if (newRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            //            {
            //                this.GridView.FocusedRowHandle = newRowHandle;
            //                this.GridView.MakeRowVisible(newRowHandle);
            //            }
            //        }
            //    }
            //    finally
            //    {
            //        this.GridView.EndUpdate();
            //    }
            //}
        }

        private void insertPhieuChi(int phanLoaiChiId)
        {
            DateTime ngay = this.dateNgay.DateTime;
            string maPhieu = this.txtMaPhieu.Text;
            string ghiChu = this.txtGhiChu.Text;
            string noiDung = txtNoiDung.Text;
            double soLuong = (double)txtSoLuong.Value;
            double donGia = (double)txtDonGia.Value;

            long soTien = this.cbPaymentType.SelectedIndex == 0 ? (long)this.txtSoTien.Value : 0;
            long soTienChuyenKhoan = this.cbPaymentType.SelectedIndex == 1 ? (long)this.txtSoTien.Value : 0;

            PhieuChiService phieuChiService = new PhieuChiService();
            phieuChiService.InsertPhieuChi(entities, ngay, soTien, soTienChuyenKhoan, maPhieu, ghiChu, phanLoaiChiId, noiDung, soLuong, donGia);
        }

        private void updatePhieuChi(int phanLoaiChiId)
        {
            DateTime ngay = this.dateNgay.DateTime;
            string maPhieu = this.txtMaPhieu.Text;
            string ghiChu = this.txtGhiChu.Text;
            string noiDung = txtNoiDung.Text;
            double soLuong = (double)txtSoLuong.Value;
            double donGia = (double)txtDonGia.Value;

            long soTien = this.cbPaymentType.SelectedIndex == 0 ? (long)this.txtSoTien.Value : 0;
            long soTienChuyenKhoan = this.cbPaymentType.SelectedIndex == 1 ? (long)this.txtSoTien.Value : 0;

            PhieuChiService phieuChiService = new PhieuChiService();
            phieuChiService.UpdatePhieuChi(entities, this.PhieuChiRow, ngay, soTien, soTienChuyenKhoan, maPhieu, ghiChu, phanLoaiChiId, noiDung, soLuong, donGia);
        }

        private void resetForm()
        {
            this.txtMaPhieu.Text = "";
            this.txtGhiChu.Text = "";
            this.cmbPhanLoaiChi.EditValue = null;
            this.txtNoiDung.Text = "";
            this.txtSoLuong.Value = 0;
            this.txtDonGia.Value = 0;
            this.txtSoTien.Value = 0;
            this.cbPaymentType.SelectedIndex = 0;
        }

        private void txtSoLuong_EditValueChanged(object sender, EventArgs e)
        {
            this.txtSoTien.Value = this.txtSoLuong.Value * this.txtDonGia.Value;
        }

        private void txtDonGia_EditValueChanged(object sender, EventArgs e)
        {
            this.txtSoTien.Value = this.txtSoLuong.Value * this.txtDonGia.Value;
        }

        private void cmbPhanLoaiChi_EditValueChanged(object sender, EventArgs e)
        {
            // GetColumnValue trả về null hoặc DBNull khi chưa chọn phân loại chi hoặc khi
            // DienGiai để trống, nên không ép kiểu trực tiếp sang String.
            string dienGiai = this.cmbPhanLoaiChi.GetColumnValue(PhanLoaiChiDienGiaiFieldName) as String;

            if (dienGiai == null)
            {
                dienGiai = "";
            }

            // Chỉ điền nội dung khi người dùng chưa tự nhập gì, hoặc nội dung đang hiển thị chính
            // là diễn giải của phân loại chi trước đó. Nếu không, giữ nguyên nội dung người dùng
            // đã nhập (kể cả khi đang sửa một phiếu chi có sẵn).
            if (string.IsNullOrEmpty(this.txtNoiDung.Text) || this.txtNoiDung.Text == this.previousPhanLoaiChiDienGiai)
            {
                this.txtNoiDung.Text = dienGiai;
            }

            this.previousPhanLoaiChiDienGiai = dienGiai;
        }
    }
}