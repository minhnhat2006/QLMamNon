using QLMamNon.Dao;

namespace QLMamNon.Forms.HocSinh
{
    partial class FrmPhanBoTaiSan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPhanBoTaiSan));
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.gcLop = new DevExpress.XtraGrid.GridControl();
            this.viewBanGiaoTaiSanRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvLop = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSTTLop = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenLop = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonViTinhLop = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuongBanGiao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGiaLop = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoChungTuLop = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayChungTuLop = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayBanGiao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayHetHan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGhiChu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuong1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThanhTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLopName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayNhap2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLopId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhieuChiId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTaiSanLopId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoveLeft = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoveRight = new DevExpress.XtraEditors.SimpleButton();
            this.gcTaiSan = new DevExpress.XtraGrid.GridControl();
            this.viewTaiSanRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvTaiSan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonViTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoChungTu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayChungTu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayNhap1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbLopHoc = new DevExpress.XtraEditors.LookUpEdit();
            this.lopRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciLopHocDen = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBanGiaoTaiSanRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTaiSan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTaiSanRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTaiSan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLopHoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciLopHocDen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.lcMain.Controls.Add(this.gcLop);
            this.lcMain.Controls.Add(this.btnSave);
            this.lcMain.Controls.Add(this.btnMoveLeft);
            this.lcMain.Controls.Add(this.btnMoveRight);
            this.lcMain.Controls.Add(this.gcTaiSan);
            this.lcMain.Controls.Add(this.cmbLopHoc);
            this.lcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcMain.Location = new System.Drawing.Point(0, 0);
            this.lcMain.Name = "lcMain";
            this.lcMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2476, 162, 392, 484);
            this.lcMain.OptionsFocus.EnableAutoTabOrder = false;
            this.lcMain.OptionsPrint.AppearanceGroupCaption.BackColor = System.Drawing.Color.LightGray;
            this.lcMain.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.lcMain.OptionsPrint.AppearanceGroupCaption.Options.UseBackColor = true;
            this.lcMain.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.lcMain.OptionsPrint.AppearanceGroupCaption.Options.UseTextOptions = true;
            this.lcMain.OptionsPrint.AppearanceGroupCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcMain.OptionsPrint.AppearanceGroupCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcMain.OptionsPrint.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcMain.OptionsPrint.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lcMain.OptionsPrint.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcMain.Root = this.layoutControlGroup1;
            this.lcMain.Size = new System.Drawing.Size(992, 573);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "Lên lớp - Xếp lớp";
            // 
            // gcLop
            // 
            this.gcLop.DataSource = this.viewBanGiaoTaiSanRowBindingSource;
            this.gcLop.Location = new System.Drawing.Point(552, 36);
            this.gcLop.MainView = this.gvLop;
            this.gcLop.Name = "gcLop";
            this.gcLop.Size = new System.Drawing.Size(428, 525);
            this.gcLop.TabIndex = 20;
            this.gcLop.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLop});
            // 
            // viewBanGiaoTaiSanRowBindingSource
            // 
            this.viewBanGiaoTaiSanRowBindingSource.DataSource = typeof(viewbangiaotaisan);
            // 
            // gvLop
            // 
            this.gvLop.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSTTLop,
            this.colTenLop,
            this.colDonViTinhLop,
            this.colSoLuongBanGiao,
            this.colDonGiaLop,
            this.colSoChungTuLop,
            this.colNgayChungTuLop,
            this.colNgayBanGiao,
            this.colNgayHetHan,
            this.colGhiChu,
            this.colSoLuong1,
            this.colThanhTien,
            this.colLopName,
            this.colNgayNhap2,
            this.colLopId,
            this.colPhieuChiId,
            this.colTaiSanLopId});
            this.gvLop.GridControl = this.gcLop;
            this.gvLop.Name = "gvLop";
            this.gvLop.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gvLop.OptionsEditForm.EditFormColumnCount = 2;
            this.gvLop.OptionsEditForm.FormCaptionFormat = "Chỉnh sửa thông tin bàn giao Tài sản";
            this.gvLop.OptionsEditForm.PopupEditFormWidth = 700;
            this.gvLop.OptionsView.ShowGroupPanel = false;
            this.gvLop.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvLop_RowUpdated);
            // 
            // colSTTLop
            // 
            this.colSTTLop.Caption = "STT";
            this.colSTTLop.FieldName = "TaiSanId";
            this.colSTTLop.Name = "colSTTLop";
            this.colSTTLop.Visible = true;
            this.colSTTLop.VisibleIndex = 0;
            this.colSTTLop.Width = 33;
            // 
            // colTenLop
            // 
            this.colTenLop.Caption = "Tên";
            this.colTenLop.FieldName = "Ten";
            this.colTenLop.Name = "colTenLop";
            this.colTenLop.Visible = true;
            this.colTenLop.VisibleIndex = 1;
            this.colTenLop.Width = 50;
            // 
            // colDonViTinhLop
            // 
            this.colDonViTinhLop.Caption = "Đơn vị tính";
            this.colDonViTinhLop.FieldName = "DonViTinh";
            this.colDonViTinhLop.Name = "colDonViTinhLop";
            this.colDonViTinhLop.Visible = true;
            this.colDonViTinhLop.VisibleIndex = 2;
            this.colDonViTinhLop.Width = 67;
            // 
            // colSoLuongBanGiao
            // 
            this.colSoLuongBanGiao.Caption = "Số lượng bàn giao";
            this.colSoLuongBanGiao.FieldName = "SoLuongBanGiao";
            this.colSoLuongBanGiao.Name = "colSoLuongBanGiao";
            this.colSoLuongBanGiao.Visible = true;
            this.colSoLuongBanGiao.VisibleIndex = 3;
            this.colSoLuongBanGiao.Width = 94;
            // 
            // colDonGiaLop
            // 
            this.colDonGiaLop.Caption = "Đơn giá";
            this.colDonGiaLop.FieldName = "DonGia";
            this.colDonGiaLop.Name = "colDonGiaLop";
            this.colDonGiaLop.Width = 46;
            // 
            // colSoChungTuLop
            // 
            this.colSoChungTuLop.Caption = "Số chứng từ";
            this.colSoChungTuLop.FieldName = "SoChungTu";
            this.colSoChungTuLop.Name = "colSoChungTuLop";
            this.colSoChungTuLop.Width = 46;
            // 
            // colNgayChungTuLop
            // 
            this.colNgayChungTuLop.Caption = "Ngày chứng từ";
            this.colNgayChungTuLop.FieldName = "NgayChungTu";
            this.colNgayChungTuLop.Name = "colNgayChungTuLop";
            this.colNgayChungTuLop.Width = 46;
            // 
            // colNgayBanGiao
            // 
            this.colNgayBanGiao.Caption = "Ngày bàn giao";
            this.colNgayBanGiao.FieldName = "NgayBanGiao";
            this.colNgayBanGiao.Name = "colNgayBanGiao";
            this.colNgayBanGiao.Visible = true;
            this.colNgayBanGiao.VisibleIndex = 4;
            this.colNgayBanGiao.Width = 85;
            // 
            // colNgayHetHan
            // 
            this.colNgayHetHan.Caption = "Ngày thu hồi";
            this.colNgayHetHan.FieldName = "NgayHetHan";
            this.colNgayHetHan.Name = "colNgayHetHan";
            this.colNgayHetHan.Visible = true;
            this.colNgayHetHan.VisibleIndex = 5;
            this.colNgayHetHan.Width = 81;
            // 
            // colGhiChu
            // 
            this.colGhiChu.Caption = "Ghi chú";
            this.colGhiChu.FieldName = "GhiChu";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.Width = 63;
            // 
            // colSoLuong1
            // 
            this.colSoLuong1.FieldName = "SoLuong";
            this.colSoLuong1.Name = "colSoLuong1";
            // 
            // colThanhTien
            // 
            this.colThanhTien.FieldName = "ThanhTien";
            this.colThanhTien.Name = "colThanhTien";
            // 
            // colLopName
            // 
            this.colLopName.FieldName = "LopName";
            this.colLopName.Name = "colLopName";
            // 
            // colNgayNhap2
            // 
            this.colNgayNhap2.FieldName = "NgayNhap";
            this.colNgayNhap2.Name = "colNgayNhap2";
            // 
            // colLopId
            // 
            this.colLopId.FieldName = "LopId";
            this.colLopId.Name = "colLopId";
            // 
            // colPhieuChiId
            // 
            this.colPhieuChiId.FieldName = "PhieuChiId";
            this.colPhieuChiId.Name = "colPhieuChiId";
            // 
            // colTaiSanLopId
            // 
            this.colTaiSanLopId.FieldName = "TaiSanLopId";
            this.colTaiSanLopId.Name = "colTaiSanLopId";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(479, 539);
            this.btnSave.MaximumSize = new System.Drawing.Size(60, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 22);
            this.btnSave.StyleController = this.lcMain;
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Lưu";
            // 
            // btnMoveLeft
            // 
            this.btnMoveLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveLeft.Image")));
            this.btnMoveLeft.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnMoveLeft.Location = new System.Drawing.Point(484, 148);
            this.btnMoveLeft.MaximumSize = new System.Drawing.Size(50, 30);
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.Size = new System.Drawing.Size(50, 30);
            this.btnMoveLeft.StyleController = this.lcMain;
            this.btnMoveLeft.TabIndex = 16;
            this.btnMoveLeft.Text = " ";
            this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
            // 
            // btnMoveRight
            // 
            this.btnMoveRight.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveRight.Image")));
            this.btnMoveRight.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnMoveRight.Location = new System.Drawing.Point(484, 75);
            this.btnMoveRight.MaximumSize = new System.Drawing.Size(50, 30);
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.Size = new System.Drawing.Size(50, 30);
            this.btnMoveRight.StyleController = this.lcMain;
            this.btnMoveRight.TabIndex = 15;
            this.btnMoveRight.Click += new System.EventHandler(this.btnMoveRight_Click);
            // 
            // gcTaiSan
            // 
            this.gcTaiSan.DataSource = this.viewTaiSanRowBindingSource;
            this.gcTaiSan.Location = new System.Drawing.Point(12, 12);
            this.gcTaiSan.MainView = this.gvTaiSan;
            this.gcTaiSan.Name = "gcTaiSan";
            this.gcTaiSan.Size = new System.Drawing.Size(454, 549);
            this.gcTaiSan.TabIndex = 13;
            this.gcTaiSan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTaiSan});
            // 
            // viewTaiSanRowBindingSource
            // 
            this.viewTaiSanRowBindingSource.DataSource = typeof(viewtaisan);
            // 
            // gvTaiSan
            // 
            this.gvTaiSan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSTT,
            this.colTen,
            this.colDonViTinh,
            this.colSoLuong,
            this.colDonGia,
            this.colSoChungTu,
            this.colNgayChungTu,
            this.colNgayNhap1});
            this.gvTaiSan.GridControl = this.gcTaiSan;
            this.gvTaiSan.Name = "gvTaiSan";
            this.gvTaiSan.OptionsBehavior.Editable = false;
            this.gvTaiSan.OptionsBehavior.ReadOnly = true;
            this.gvTaiSan.OptionsView.ShowGroupPanel = false;
            // 
            // colSTT
            // 
            this.colSTT.Caption = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 0;
            this.colSTT.Width = 30;
            // 
            // colTen
            // 
            this.colTen.Caption = "Tên";
            this.colTen.FieldName = "Ten";
            this.colTen.Name = "colTen";
            this.colTen.Visible = true;
            this.colTen.VisibleIndex = 1;
            this.colTen.Width = 66;
            // 
            // colDonViTinh
            // 
            this.colDonViTinh.Caption = "Đơn vị tính";
            this.colDonViTinh.FieldName = "DonViTinh";
            this.colDonViTinh.Name = "colDonViTinh";
            this.colDonViTinh.Visible = true;
            this.colDonViTinh.VisibleIndex = 2;
            this.colDonViTinh.Width = 66;
            // 
            // colSoLuong
            // 
            this.colSoLuong.Caption = "Số lượng";
            this.colSoLuong.FieldName = "SoLuong";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Visible = true;
            this.colSoLuong.VisibleIndex = 3;
            this.colSoLuong.Width = 66;
            // 
            // colDonGia
            // 
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 4;
            this.colDonGia.Width = 66;
            // 
            // colSoChungTu
            // 
            this.colSoChungTu.Caption = "Số chứng từ";
            this.colSoChungTu.FieldName = "SoChungTu";
            this.colSoChungTu.Name = "colSoChungTu";
            this.colSoChungTu.Visible = true;
            this.colSoChungTu.VisibleIndex = 5;
            this.colSoChungTu.Width = 66;
            // 
            // colNgayChungTu
            // 
            this.colNgayChungTu.Caption = "Ngày chứng từ";
            this.colNgayChungTu.FieldName = "NgayChungTu";
            this.colNgayChungTu.Name = "colNgayChungTu";
            this.colNgayChungTu.Visible = true;
            this.colNgayChungTu.VisibleIndex = 6;
            this.colNgayChungTu.Width = 76;
            // 
            // colNgayNhap1
            // 
            this.colNgayNhap1.FieldName = "NgayNhap";
            this.colNgayNhap1.Name = "colNgayNhap1";
            // 
            // cmbLopHoc
            // 
            this.cmbLopHoc.Location = new System.Drawing.Point(592, 12);
            this.cmbLopHoc.Name = "cmbLopHoc";
            this.cmbLopHoc.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmbLopHoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLopHoc.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name")});
            this.cmbLopHoc.Properties.DataSource = this.lopRowBindingSource;
            this.cmbLopHoc.Properties.DisplayMember = "Name";
            this.cmbLopHoc.Properties.NullText = "";
            this.cmbLopHoc.Properties.PopupSizeable = false;
            this.cmbLopHoc.Properties.ShowHeader = false;
            this.cmbLopHoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbLopHoc.Properties.ValueMember = "LopId";
            this.cmbLopHoc.Size = new System.Drawing.Size(388, 20);
            this.cmbLopHoc.StyleController = this.lcMain;
            this.cmbLopHoc.TabIndex = 10;
            this.cmbLopHoc.EditValueChanged += new System.EventHandler(this.cmbLopHoc_EditValueChanged);
            // 
            // lopRowBindingSource
            // 
            this.lopRowBindingSource.DataSource = typeof(lop);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciLopHocDen,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(992, 573);
            this.layoutControlGroup1.Text = "Lên lớp - Xếp lớp";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciLopHocDen
            // 
            this.lciLopHocDen.Control = this.cmbLopHoc;
            this.lciLopHocDen.Location = new System.Drawing.Point(540, 0);
            this.lciLopHocDen.Name = "lciLopHocDen";
            this.lciLopHocDen.Size = new System.Drawing.Size(432, 24);
            this.lciLopHocDen.Text = "Lớp học";
            this.lciLopHocDen.TextSize = new System.Drawing.Size(37, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcTaiSan;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(458, 553);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnMoveRight;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.layoutControlItem4.FillControlToClientArea = false;
            this.layoutControlItem4.Location = new System.Drawing.Point(458, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(46, 42);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(82, 105);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Default;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnMoveLeft;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem5.FillControlToClientArea = false;
            this.layoutControlItem5.Location = new System.Drawing.Point(458, 105);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(46, 42);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(82, 105);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnSave;
            this.layoutControlItem6.ControlAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.layoutControlItem6.FillControlToClientArea = false;
            this.layoutControlItem6.Location = new System.Drawing.Point(458, 210);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(52, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(82, 343);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcLop;
            this.layoutControlItem1.Location = new System.Drawing.Point(540, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(432, 529);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // FrmPhanBoTaiSan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 573);
            this.Controls.Add(this.lcMain);
            this.Name = "FrmPhanBoTaiSan";
            this.Text = "Bàn giao Tài sản";
            this.Load += new System.EventHandler(this.FrmPhanBoTaiSan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcLop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBanGiaoTaiSanRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTaiSan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTaiSanRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTaiSan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLopHoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciLopHocDen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lciLopHocDen;
        private DevExpress.XtraGrid.GridControl gcTaiSan;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTaiSan;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnMoveRight;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnMoveLeft;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.LookUpEdit cmbLopHoc;
        private System.Windows.Forms.BindingSource lopRowBindingSource;
        private System.Windows.Forms.BindingSource viewTaiSanRowBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colTen;
        private DevExpress.XtraGrid.Columns.GridColumn colDonViTinh;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuong;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colSoChungTu;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayChungTu;
        private System.Windows.Forms.BindingSource viewBanGiaoTaiSanRowBindingSource;
        private DevExpress.XtraGrid.GridControl gcLop;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLop;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colSTTLop;
        private DevExpress.XtraGrid.Columns.GridColumn colTenLop;
        private DevExpress.XtraGrid.Columns.GridColumn colDonViTinhLop;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuongBanGiao;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGiaLop;
        private DevExpress.XtraGrid.Columns.GridColumn colSoChungTuLop;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayChungTuLop;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayBanGiao;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayHetHan;
        private DevExpress.XtraGrid.Columns.GridColumn colGhiChu;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuong1;
        private DevExpress.XtraGrid.Columns.GridColumn colThanhTien;
        private DevExpress.XtraGrid.Columns.GridColumn colLopName;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayNhap1;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayNhap2;
        private DevExpress.XtraGrid.Columns.GridColumn colLopId;
        private DevExpress.XtraGrid.Columns.GridColumn colPhieuChiId;
        private DevExpress.XtraGrid.Columns.GridColumn colTaiSanLopId;
    }
}