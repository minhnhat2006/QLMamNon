using QLMamNon.Dao;

namespace QLMamNon.Forms.DanhMuc
{
    partial class FrmTaiSan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTaiSan));
            this.panelMain = new System.Windows.Forms.Panel();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.viewTaiSanRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhanLoaiChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonViTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThanhTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayNhap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoChungTu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayChungTu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhanLoaiChiId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuyBo = new DevExpress.XtraEditors.SimpleButton();
            this.btnChinhSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnThem = new DevExpress.XtraEditors.SimpleButton();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTaiSanRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.gcMain);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(792, 573);
            this.panelMain.TabIndex = 0;
            // 
            // gcMain
            // 
            this.gcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcMain.DataSource = this.viewTaiSanRowBindingSource;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(792, 524);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // viewTaiSanRowBindingSource
            // 
            this.viewTaiSanRowBindingSource.DataSource = typeof(viewtaisan);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSTT,
            this.colTen,
            this.colPhanLoaiChi,
            this.colDonViTinh,
            this.colSoLuong,
            this.colDonGia,
            this.colThanhTien,
            this.colNgayNhap,
            this.colSoChungTu,
            this.colNgayChungTu,
            this.colPhanLoaiChiId});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gvMain.OptionsEditForm.EditFormColumnCount = 2;
            this.gvMain.OptionsEditForm.FormCaptionFormat = "Chỉnh sửa thông tin Tài sản";
            this.gvMain.OptionsEditForm.PopupEditFormWidth = 700;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvMain_CustomColumnDisplayText);
            // 
            // colSTT
            // 
            this.colSTT.Caption = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 0;
            this.colSTT.Width = 42;
            // 
            // colTen
            // 
            this.colTen.Caption = "Tên, nhãn hiệu quy cách";
            this.colTen.FieldName = "Ten";
            this.colTen.Name = "colTen";
            this.colTen.Visible = true;
            this.colTen.VisibleIndex = 1;
            this.colTen.Width = 119;
            // 
            // colPhanLoaiChi
            // 
            this.colPhanLoaiChi.Caption = "Phân loại Tài sản";
            this.colPhanLoaiChi.FieldName = "PhanLoaiTaiSan";
            this.colPhanLoaiChi.Name = "colPhanLoaiChi";
            this.colPhanLoaiChi.Visible = true;
            this.colPhanLoaiChi.VisibleIndex = 2;
            this.colPhanLoaiChi.Width = 110;
            // 
            // colDonViTinh
            // 
            this.colDonViTinh.Caption = "Đơn vị tính";
            this.colDonViTinh.FieldName = "DonViTinh";
            this.colDonViTinh.Name = "colDonViTinh";
            this.colDonViTinh.Visible = true;
            this.colDonViTinh.VisibleIndex = 3;
            this.colDonViTinh.Width = 99;
            // 
            // colSoLuong
            // 
            this.colSoLuong.Caption = "Số lượng";
            this.colSoLuong.DisplayFormat.FormatString = "n2";
            this.colSoLuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSoLuong.FieldName = "SoLuong";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Visible = true;
            this.colSoLuong.VisibleIndex = 4;
            this.colSoLuong.Width = 52;
            // 
            // colDonGia
            // 
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.DisplayFormat.FormatString = "n0";
            this.colDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 5;
            this.colDonGia.Width = 68;
            // 
            // colThanhTien
            // 
            this.colThanhTien.Caption = "Thành tiền";
            this.colThanhTien.DisplayFormat.FormatString = "n0";
            this.colThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colThanhTien.FieldName = "ThanhTien";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.Visible = true;
            this.colThanhTien.VisibleIndex = 6;
            this.colThanhTien.Width = 68;
            // 
            // colNgayNhap
            // 
            this.colNgayNhap.Caption = "Ngày nhập";
            this.colNgayNhap.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colNgayNhap.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayNhap.FieldName = "NgayNhap";
            this.colNgayNhap.Name = "colNgayNhap";
            this.colNgayNhap.Visible = true;
            this.colNgayNhap.VisibleIndex = 7;
            this.colNgayNhap.Width = 68;
            // 
            // colSoChungTu
            // 
            this.colSoChungTu.Caption = "Số chứng từ";
            this.colSoChungTu.FieldName = "SoChungTu";
            this.colSoChungTu.Name = "colSoChungTu";
            this.colSoChungTu.Visible = true;
            this.colSoChungTu.VisibleIndex = 8;
            this.colSoChungTu.Width = 68;
            // 
            // colNgayChungTu
            // 
            this.colNgayChungTu.Caption = "Ngày chứng từ";
            this.colNgayChungTu.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colNgayChungTu.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayChungTu.FieldName = "NgayChungTu";
            this.colNgayChungTu.Name = "colNgayChungTu";
            this.colNgayChungTu.Visible = true;
            this.colNgayChungTu.VisibleIndex = 9;
            this.colNgayChungTu.Width = 80;
            // 
            // colPhanLoaiChiId
            // 
            this.colPhanLoaiChiId.Caption = "PhanLoaiChiId";
            this.colPhanLoaiChiId.FieldName = "PhanLoaiChiId";
            this.colPhanLoaiChiId.Name = "colPhanLoaiChiId";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnLuu);
            this.panelControl1.Controls.Add(this.btnHuyBo);
            this.panelControl1.Controls.Add(this.btnChinhSua);
            this.panelControl1.Controls.Add(this.btnXoa);
            this.panelControl1.Controls.Add(this.btnThem);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 530);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(792, 43);
            this.panelControl1.TabIndex = 8;
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.Location = new System.Drawing.Point(681, 10);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(99, 25);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "Lưu";
            // 
            // btnHuyBo
            // 
            this.btnHuyBo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuyBo.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnHuyBo.Appearance.Options.UseFont = true;
            this.btnHuyBo.Image = ((System.Drawing.Image)(resources.GetObject("btnHuyBo.Image")));
            this.btnHuyBo.Location = new System.Drawing.Point(574, 10);
            this.btnHuyBo.Name = "btnHuyBo";
            this.btnHuyBo.Size = new System.Drawing.Size(99, 25);
            this.btnHuyBo.TabIndex = 0;
            this.btnHuyBo.Text = "Hủy bỏ";
            // 
            // btnChinhSua
            // 
            this.btnChinhSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChinhSua.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnChinhSua.Appearance.Options.UseFont = true;
            this.btnChinhSua.Image = ((System.Drawing.Image)(resources.GetObject("btnChinhSua.Image")));
            this.btnChinhSua.Location = new System.Drawing.Point(467, 10);
            this.btnChinhSua.Name = "btnChinhSua";
            this.btnChinhSua.Size = new System.Drawing.Size(99, 25);
            this.btnChinhSua.TabIndex = 0;
            this.btnChinhSua.Text = "Chỉnh sửa";
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoa.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnXoa.Appearance.Options.UseFont = true;
            this.btnXoa.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.Image")));
            this.btnXoa.Location = new System.Drawing.Point(360, 10);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(99, 25);
            this.btnXoa.TabIndex = 0;
            this.btnXoa.Text = "Xóa";
            // 
            // btnThem
            // 
            this.btnThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThem.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnThem.Appearance.Options.UseFont = true;
            this.btnThem.Image = ((System.Drawing.Image)(resources.GetObject("btnThem.Image")));
            this.btnThem.Location = new System.Drawing.Point(253, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(99, 25);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            // 
            // FrmTaiSan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelMain);
            this.Name = "FrmTaiSan";
            this.Text = "Thông tin Tài sản";
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTaiSanRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnHuyBo;
        private DevExpress.XtraEditors.SimpleButton btnChinhSua;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraEditors.SimpleButton btnThem;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colTen;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuong;
        private DevExpress.XtraGrid.Columns.GridColumn colDonViTinh;
        private DevExpress.XtraGrid.Columns.GridColumn colPhanLoaiChi;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colThanhTien;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayNhap;
        private DevExpress.XtraGrid.Columns.GridColumn colSoChungTu;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayChungTu;
        private System.Windows.Forms.BindingSource viewTaiSanRowBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colPhanLoaiChiId;
    }
}