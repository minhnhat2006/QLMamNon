using QLMamNon.Dao;

namespace QLMamNon.Forms.HocSinh
{
    partial class FrmThongTinHocSinh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThongTinHocSinh));
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.cmbNamSinh = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbThoiHoc = new DevExpress.XtraEditors.LookUpEdit();
            this.keyValuePairBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbLop = new DevExpress.XtraEditors.LookUpEdit();
            this.lopRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.hocSinhRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand16 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colSTT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridBand17 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colHoTen = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand19 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colGioiTinh = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand20 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand21 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colNgaySinhNgay = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand22 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colNgaySinhThang = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand23 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colNgaySinhNam = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand27 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand29 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colHoTenCha = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand28 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colDienThoai = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand30 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colHoTenMe = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colDienThoaiMe = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colDiaChi = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colPhuong = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colQuan = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colTinh = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colTinhTrang = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colLopDangHoc = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand24 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colNgayVaoLop = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colHoDem = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTen = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colThangSinh = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNgaySinh = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.btnReset = new DevExpress.XtraEditors.SimpleButton();
            this.btnTimKiem = new DevExpress.XtraEditors.SimpleButton();
            this.cmbQuanHuyen = new DevExpress.XtraEditors.LookUpEdit();
            this.quanHuyenRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbPhuongXa = new DevExpress.XtraEditors.LookUpEdit();
            this.phuongXaRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbNgaySinh = new DevExpress.XtraEditors.DateEdit();
            this.cmbThang = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnIn = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuyBo = new DevExpress.XtraEditors.SimpleButton();
            this.btnChinhSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnThem = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNamSinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbThoiHoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keyValuePairBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hocSinhRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuanHuyen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quanHuyenRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPhuongXa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phuongXaRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNgaySinh.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNgaySinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.lcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lcMain.Controls.Add(this.cmbNamSinh);
            this.lcMain.Controls.Add(this.cmbThoiHoc);
            this.lcMain.Controls.Add(this.cmbLop);
            this.lcMain.Controls.Add(this.gcMain);
            this.lcMain.Controls.Add(this.btnReset);
            this.lcMain.Controls.Add(this.btnTimKiem);
            this.lcMain.Controls.Add(this.cmbQuanHuyen);
            this.lcMain.Controls.Add(this.cmbPhuongXa);
            this.lcMain.Controls.Add(this.cmbNgaySinh);
            this.lcMain.Controls.Add(this.cmbThang);
            this.lcMain.Location = new System.Drawing.Point(0, 0);
            this.lcMain.Name = "lcMain";
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
            this.lcMain.Root = this.Root;
            this.lcMain.Size = new System.Drawing.Size(896, 524);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "layoutControl1";
            // 
            // cmbNamSinh
            // 
            this.cmbNamSinh.EditValue = "[Chọn năm]";
            this.cmbNamSinh.Location = new System.Drawing.Point(818, 42);
            this.cmbNamSinh.Name = "cmbNamSinh";
            this.cmbNamSinh.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmbNamSinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNamSinh.Properties.NullText = "[Chọn năm]";
            this.cmbNamSinh.Size = new System.Drawing.Size(50, 22);
            this.cmbNamSinh.StyleController = this.lcMain;
            this.cmbNamSinh.TabIndex = 8;
            // 
            // cmbThoiHoc
            // 
            this.cmbThoiHoc.Location = new System.Drawing.Point(534, 42);
            this.cmbThoiHoc.Name = "cmbThoiHoc";
            this.cmbThoiHoc.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmbThoiHoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbThoiHoc.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value", "Value", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cmbThoiHoc.Properties.DataSource = this.keyValuePairBindingSource;
            this.cmbThoiHoc.Properties.DisplayMember = "Value";
            this.cmbThoiHoc.Properties.NullText = "[Chọn trạng thái]";
            this.cmbThoiHoc.Properties.ShowHeader = false;
            this.cmbThoiHoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbThoiHoc.Properties.ValueMember = "Key";
            this.cmbThoiHoc.Size = new System.Drawing.Size(50, 22);
            this.cmbThoiHoc.StyleController = this.lcMain;
            this.cmbThoiHoc.TabIndex = 5;
            // 
            // keyValuePairBindingSource
            // 
            this.keyValuePairBindingSource.DataSource = typeof(QLMamNon.Entity.Form.KeyValuePair);
            // 
            // cmbLop
            // 
            this.cmbLop.Location = new System.Drawing.Point(392, 42);
            this.cmbLop.Name = "cmbLop";
            this.cmbLop.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmbLop.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLop.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 37, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cmbLop.Properties.DataSource = this.lopRowBindingSource;
            this.cmbLop.Properties.DisplayMember = "Name";
            this.cmbLop.Properties.NullText = "[Chọn lớp]";
            this.cmbLop.Properties.ShowHeader = false;
            this.cmbLop.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbLop.Properties.ValueMember = "LopId";
            this.cmbLop.Size = new System.Drawing.Size(50, 22);
            this.cmbLop.StyleController = this.lcMain;
            this.cmbLop.TabIndex = 4;
            // 
            // lopRowBindingSource
            // 
            this.lopRowBindingSource.DataSource = typeof(QLMamNon.Dao.lop);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.hocSinhRowBindingSource;
            this.gcMain.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(1);
            this.gcMain.Location = new System.Drawing.Point(8, 86);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gcMain.Size = new System.Drawing.Size(1169, 409);
            this.gcMain.TabIndex = 8;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            this.gcMain.Load += new System.EventHandler(this.gcMain_Load);
            // 
            // hocSinhRowBindingSource
            // 
            this.hocSinhRowBindingSource.DataSource = typeof(QLMamNon.Dao.hocsinh);
            // 
            // gvMain
            // 
            this.gvMain.Appearance.BandPanel.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.gvMain.Appearance.BandPanel.Options.UseFont = true;
            this.gvMain.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.gvMain.Appearance.Row.Options.UseFont = true;
            this.gvMain.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand16,
            this.gridBand17,
            this.gridBand19,
            this.gridBand20,
            this.gridBand27,
            this.gridBand1,
            this.gridBand2,
            this.gridBand3,
            this.gridBand4,
            this.gridBand5,
            this.gridBand6,
            this.gridBand24});
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colSTT,
            this.colHoTen,
            this.colGioiTinh,
            this.colNgaySinhNgay,
            this.colNgaySinhThang,
            this.colNgaySinhNam,
            this.colLopDangHoc,
            this.colNgayVaoLop,
            this.colDienThoai,
            this.colHoTenCha,
            this.colHoTenMe,
            this.colDiaChi,
            this.colPhuong,
            this.colQuan,
            this.colTinh,
            this.colTinhTrang,
            this.colHoDem,
            this.colTen,
            this.colThangSinh,
            this.colNgaySinh,
            this.colDienThoaiMe});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gvMain.OptionsEditForm.FormCaptionFormat = "Chỉnh sửa thông tin Học sinh";
            this.gvMain.OptionsPrint.PrintDetails = true;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.ShowColumnHeaders = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvMain_CustomColumnDisplayText);
            // 
            // gridBand16
            // 
            this.gridBand16.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand16.Caption = "STT";
            this.gridBand16.Columns.Add(this.colSTT);
            this.gridBand16.Name = "gridBand16";
            this.gridBand16.RowCount = 2;
            this.gridBand16.VisibleIndex = 0;
            this.gridBand16.Width = 40;
            // 
            // colSTT
            // 
            this.colSTT.Caption = "STT";
            this.colSTT.ColumnEdit = this.repositoryItemTextEdit1;
            this.colSTT.FieldNameSortGroup = "HocSinhId";
            this.colSTT.Name = "colSTT";
            this.colSTT.OptionsEditForm.Caption = "STT:";
            this.colSTT.ShowUnboundExpressionMenu = true;
            this.colSTT.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colSTT.Visible = true;
            this.colSTT.Width = 40;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // gridBand17
            // 
            this.gridBand17.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand17.Caption = "Họ tên";
            this.gridBand17.Columns.Add(this.colHoTen);
            this.gridBand17.Name = "gridBand17";
            this.gridBand17.VisibleIndex = 1;
            this.gridBand17.Width = 150;
            // 
            // colHoTen
            // 
            this.colHoTen.Caption = "Họ tên";
            this.colHoTen.FieldName = "HoTen";
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.OptionsEditForm.Caption = "Họ đệm:";
            this.colHoTen.OptionsEditForm.StartNewRow = true;
            this.colHoTen.Visible = true;
            this.colHoTen.Width = 150;
            // 
            // gridBand19
            // 
            this.gridBand19.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand19.Caption = "Giới tính";
            this.gridBand19.Columns.Add(this.colGioiTinh);
            this.gridBand19.Name = "gridBand19";
            this.gridBand19.VisibleIndex = 2;
            this.gridBand19.Width = 90;
            // 
            // colGioiTinh
            // 
            this.colGioiTinh.AppearanceCell.Options.UseTextOptions = true;
            this.colGioiTinh.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colGioiTinh.Caption = "Giới tính";
            this.colGioiTinh.ColumnEdit = this.repositoryItemTextEdit1;
            this.colGioiTinh.FieldName = "GioiTinh";
            this.colGioiTinh.Name = "colGioiTinh";
            this.colGioiTinh.OptionsEditForm.Caption = "Giới tính:";
            this.colGioiTinh.Visible = true;
            this.colGioiTinh.Width = 90;
            // 
            // gridBand20
            // 
            this.gridBand20.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand20.Caption = "Ngày sinh";
            this.gridBand20.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand21,
            this.gridBand22,
            this.gridBand23});
            this.gridBand20.Name = "gridBand20";
            this.gridBand20.VisibleIndex = 3;
            this.gridBand20.Width = 270;
            // 
            // gridBand21
            // 
            this.gridBand21.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand21.Caption = "Ngày";
            this.gridBand21.Columns.Add(this.colNgaySinhNgay);
            this.gridBand21.Name = "gridBand21";
            this.gridBand21.VisibleIndex = 0;
            this.gridBand21.Width = 90;
            // 
            // colNgaySinhNgay
            // 
            this.colNgaySinhNgay.Caption = "NgaySinh";
            this.colNgaySinhNgay.Name = "colNgaySinhNgay";
            this.colNgaySinhNgay.OptionsEditForm.Caption = "Ngày sinh:";
            this.colNgaySinhNgay.OptionsEditForm.StartNewRow = true;
            this.colNgaySinhNgay.Visible = true;
            this.colNgaySinhNgay.Width = 90;
            // 
            // gridBand22
            // 
            this.gridBand22.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand22.Caption = "Tháng";
            this.gridBand22.Columns.Add(this.colNgaySinhThang);
            this.gridBand22.Name = "gridBand22";
            this.gridBand22.VisibleIndex = 1;
            this.gridBand22.Width = 90;
            // 
            // colNgaySinhThang
            // 
            this.colNgaySinhThang.Caption = "ThangSinh";
            this.colNgaySinhThang.Name = "colNgaySinhThang";
            this.colNgaySinhThang.Visible = true;
            this.colNgaySinhThang.Width = 90;
            // 
            // gridBand23
            // 
            this.gridBand23.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand23.Caption = "Năm";
            this.gridBand23.Columns.Add(this.colNgaySinhNam);
            this.gridBand23.Name = "gridBand23";
            this.gridBand23.VisibleIndex = 2;
            this.gridBand23.Width = 90;
            // 
            // colNgaySinhNam
            // 
            this.colNgaySinhNam.Caption = "NamSinh";
            this.colNgaySinhNam.Name = "colNgaySinhNam";
            this.colNgaySinhNam.Visible = true;
            this.colNgaySinhNam.Width = 90;
            // 
            // gridBand27
            // 
            this.gridBand27.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand27.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand27.Caption = "Thông tin liên lạc";
            this.gridBand27.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand29,
            this.gridBand28,
            this.gridBand30,
            this.gridBand7});
            this.gridBand27.Name = "gridBand27";
            this.gridBand27.VisibleIndex = 4;
            this.gridBand27.Width = 360;
            // 
            // gridBand29
            // 
            this.gridBand29.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand29.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand29.Caption = "Họ tên cha";
            this.gridBand29.Columns.Add(this.colHoTenCha);
            this.gridBand29.Name = "gridBand29";
            this.gridBand29.VisibleIndex = 0;
            this.gridBand29.Width = 90;
            // 
            // colHoTenCha
            // 
            this.colHoTenCha.FieldName = "HoTenCha";
            this.colHoTenCha.Name = "colHoTenCha";
            this.colHoTenCha.OptionsEditForm.Caption = "Họ tên cha:";
            this.colHoTenCha.OptionsEditForm.StartNewRow = true;
            this.colHoTenCha.Visible = true;
            this.colHoTenCha.Width = 90;
            // 
            // gridBand28
            // 
            this.gridBand28.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand28.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand28.Caption = "Điện thoại";
            this.gridBand28.Columns.Add(this.colDienThoai);
            this.gridBand28.Name = "gridBand28";
            this.gridBand28.VisibleIndex = 1;
            this.gridBand28.Width = 90;
            // 
            // colDienThoai
            // 
            this.colDienThoai.FieldName = "DienThoai";
            this.colDienThoai.Name = "colDienThoai";
            this.colDienThoai.OptionsEditForm.Caption = "Điện thoại:";
            this.colDienThoai.OptionsEditForm.StartNewRow = true;
            this.colDienThoai.Visible = true;
            this.colDienThoai.Width = 90;
            // 
            // gridBand30
            // 
            this.gridBand30.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand30.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand30.Caption = "Họ tên mẹ";
            this.gridBand30.Columns.Add(this.colHoTenMe);
            this.gridBand30.Name = "gridBand30";
            this.gridBand30.VisibleIndex = 2;
            this.gridBand30.Width = 90;
            // 
            // colHoTenMe
            // 
            this.colHoTenMe.FieldName = "HoTenMe";
            this.colHoTenMe.Name = "colHoTenMe";
            this.colHoTenMe.OptionsEditForm.Caption = "Họ tên mẹ:";
            this.colHoTenMe.OptionsEditForm.StartNewRow = true;
            this.colHoTenMe.Visible = true;
            this.colHoTenMe.Width = 90;
            // 
            // gridBand7
            // 
            this.gridBand7.Caption = "Điện thoại";
            this.gridBand7.Columns.Add(this.colDienThoaiMe);
            this.gridBand7.Name = "gridBand7";
            this.gridBand7.VisibleIndex = 3;
            this.gridBand7.Width = 90;
            // 
            // colDienThoaiMe
            // 
            this.colDienThoaiMe.FieldName = "DienThoaiMe";
            this.colDienThoaiMe.Name = "colDienThoaiMe";
            this.colDienThoaiMe.Visible = true;
            this.colDienThoaiMe.Width = 90;
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.Caption = "Địa chỉ";
            this.gridBand1.Columns.Add(this.colDiaChi);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 5;
            this.gridBand1.Width = 90;
            // 
            // colDiaChi
            // 
            this.colDiaChi.FieldName = "DiaChi";
            this.colDiaChi.Name = "colDiaChi";
            this.colDiaChi.OptionsEditForm.Caption = "Số nhà/đường:";
            this.colDiaChi.OptionsEditForm.StartNewRow = true;
            this.colDiaChi.Visible = true;
            this.colDiaChi.Width = 90;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.Caption = "Phường/Xã";
            this.gridBand2.Columns.Add(this.colPhuong);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 6;
            this.gridBand2.Width = 90;
            // 
            // colPhuong
            // 
            this.colPhuong.AppearanceCell.Options.UseTextOptions = true;
            this.colPhuong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colPhuong.ColumnEdit = this.repositoryItemTextEdit1;
            this.colPhuong.FieldName = "PhuongXaId";
            this.colPhuong.Name = "colPhuong";
            this.colPhuong.OptionsEditForm.Caption = "Phường/xã:";
            this.colPhuong.OptionsEditForm.StartNewRow = true;
            this.colPhuong.Visible = true;
            this.colPhuong.Width = 90;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.Caption = "Quận/Huyện";
            this.gridBand3.Columns.Add(this.colQuan);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 7;
            this.gridBand3.Width = 90;
            // 
            // colQuan
            // 
            this.colQuan.AppearanceCell.Options.UseTextOptions = true;
            this.colQuan.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colQuan.ColumnEdit = this.repositoryItemTextEdit1;
            this.colQuan.FieldName = "QuanHuyenId";
            this.colQuan.Name = "colQuan";
            this.colQuan.OptionsEditForm.Caption = "Quận/huyện:";
            this.colQuan.OptionsEditForm.StartNewRow = true;
            this.colQuan.Visible = true;
            this.colQuan.Width = 90;
            // 
            // gridBand4
            // 
            this.gridBand4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand4.Caption = "Tỉnh/Thành phố";
            this.gridBand4.Columns.Add(this.colTinh);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = 8;
            this.gridBand4.Width = 90;
            // 
            // colTinh
            // 
            this.colTinh.AppearanceCell.Options.UseTextOptions = true;
            this.colTinh.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colTinh.ColumnEdit = this.repositoryItemTextEdit1;
            this.colTinh.FieldName = "TinhThanhPhoId";
            this.colTinh.Name = "colTinh";
            this.colTinh.OptionsEditForm.Caption = "Tính/thành phố:";
            this.colTinh.OptionsEditForm.StartNewRow = true;
            this.colTinh.Visible = true;
            this.colTinh.Width = 90;
            // 
            // gridBand5
            // 
            this.gridBand5.Caption = "Tình trạng";
            this.gridBand5.Columns.Add(this.colTinhTrang);
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.VisibleIndex = 9;
            this.gridBand5.Width = 90;
            // 
            // colTinhTrang
            // 
            this.colTinhTrang.Caption = "Tình trạng";
            this.colTinhTrang.ColumnEdit = this.repositoryItemTextEdit1;
            this.colTinhTrang.FieldName = "ThoiHoc";
            this.colTinhTrang.Name = "colTinhTrang";
            this.colTinhTrang.Visible = true;
            this.colTinhTrang.Width = 90;
            // 
            // gridBand6
            // 
            this.gridBand6.Caption = "Lớp đang học";
            this.gridBand6.Columns.Add(this.colLopDangHoc);
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.VisibleIndex = 10;
            this.gridBand6.Width = 90;
            // 
            // colLopDangHoc
            // 
            this.colLopDangHoc.Caption = "Lop dang hoc";
            this.colLopDangHoc.FieldName = "LopDangHoc";
            this.colLopDangHoc.Name = "colLopDangHoc";
            this.colLopDangHoc.Visible = true;
            this.colLopDangHoc.Width = 90;
            // 
            // gridBand24
            // 
            this.gridBand24.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand24.Caption = "Ngày vào lớp";
            this.gridBand24.Columns.Add(this.colNgayVaoLop);
            this.gridBand24.Name = "gridBand24";
            this.gridBand24.VisibleIndex = 11;
            this.gridBand24.Width = 90;
            // 
            // colNgayVaoLop
            // 
            this.colNgayVaoLop.FieldName = "NgayVaoLop";
            this.colNgayVaoLop.Name = "colNgayVaoLop";
            this.colNgayVaoLop.OptionsEditForm.Caption = "Ngày vào lớp:";
            this.colNgayVaoLop.OptionsEditForm.StartNewRow = true;
            this.colNgayVaoLop.Visible = true;
            this.colNgayVaoLop.Width = 90;
            // 
            // colHoDem
            // 
            this.colHoDem.FieldName = "HoDem";
            this.colHoDem.Name = "colHoDem";
            // 
            // colTen
            // 
            this.colTen.FieldName = "Ten";
            this.colTen.Name = "colTen";
            // 
            // colThangSinh
            // 
            this.colThangSinh.FieldName = "ThangSinh";
            this.colThangSinh.Name = "colThangSinh";
            // 
            // colNgaySinh
            // 
            this.colNgaySinh.FieldName = "NgaySinh";
            this.colNgaySinh.Name = "colNgaySinh";
            // 
            // btnReset
            // 
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.Location = new System.Drawing.Point(1103, 42);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(62, 23);
            this.btnReset.StyleController = this.lcMain;
            this.btnReset.TabIndex = 11;
            this.btnReset.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("btnTimKiem.Appearance.Image")));
            this.btnTimKiem.Appearance.Options.UseImage = true;
            this.btnTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("btnTimKiem.Image")));
            this.btnTimKiem.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnTimKiem.Location = new System.Drawing.Point(1014, 42);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(85, 24);
            this.btnTimKiem.StyleController = this.lcMain;
            this.btnTimKiem.TabIndex = 10;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // cmbQuanHuyen
            // 
            this.cmbQuanHuyen.Location = new System.Drawing.Point(108, 42);
            this.cmbQuanHuyen.Name = "cmbQuanHuyen";
            this.cmbQuanHuyen.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmbQuanHuyen.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.cmbQuanHuyen.Properties.Appearance.Options.UseFont = true;
            this.cmbQuanHuyen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQuanHuyen.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Tên")});
            this.cmbQuanHuyen.Properties.DataSource = this.quanHuyenRowBindingSource;
            this.cmbQuanHuyen.Properties.DisplayMember = "Name";
            this.cmbQuanHuyen.Properties.NullText = "[Chọn quận huyện]";
            this.cmbQuanHuyen.Properties.PopupSizeable = false;
            this.cmbQuanHuyen.Properties.ShowHeader = false;
            this.cmbQuanHuyen.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbQuanHuyen.Properties.ValueMember = "QuanHuyenId";
            this.cmbQuanHuyen.Size = new System.Drawing.Size(50, 28);
            this.cmbQuanHuyen.StyleController = this.lcMain;
            this.cmbQuanHuyen.TabIndex = 2;
            this.cmbQuanHuyen.EditValueChanged += new System.EventHandler(this.cmbQuanHuyen_EditValueChanged);
            // 
            // quanHuyenRowBindingSource
            // 
            this.quanHuyenRowBindingSource.DataSource = typeof(QLMamNon.Dao.quanhuyen);
            // 
            // cmbPhuongXa
            // 
            this.cmbPhuongXa.Location = new System.Drawing.Point(250, 42);
            this.cmbPhuongXa.Name = "cmbPhuongXa";
            this.cmbPhuongXa.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmbPhuongXa.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.cmbPhuongXa.Properties.Appearance.Options.UseFont = true;
            this.cmbPhuongXa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPhuongXa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Tên", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.True)});
            this.cmbPhuongXa.Properties.DataSource = this.phuongXaRowBindingSource;
            this.cmbPhuongXa.Properties.DisplayMember = "Name";
            this.cmbPhuongXa.Properties.NullText = "[Chọn phường xã]";
            this.cmbPhuongXa.Properties.PopupSizeable = false;
            this.cmbPhuongXa.Properties.ShowHeader = false;
            this.cmbPhuongXa.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbPhuongXa.Properties.ValueMember = "PhuongXaId";
            this.cmbPhuongXa.Size = new System.Drawing.Size(50, 28);
            this.cmbPhuongXa.StyleController = this.lcMain;
            this.cmbPhuongXa.TabIndex = 3;
            // 
            // phuongXaRowBindingSource
            // 
            this.phuongXaRowBindingSource.DataSource = typeof(QLMamNon.Dao.phuongxa);
            // 
            // cmbNgaySinh
            // 
            this.cmbNgaySinh.EditValue = null;
            this.cmbNgaySinh.Location = new System.Drawing.Point(960, 42);
            this.cmbNgaySinh.Name = "cmbNgaySinh";
            this.cmbNgaySinh.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmbNgaySinh.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.cmbNgaySinh.Properties.Appearance.Options.UseFont = true;
            this.cmbNgaySinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNgaySinh.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNgaySinh.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.cmbNgaySinh.Properties.NullText = "[Chọn ngày sinh]";
            this.cmbNgaySinh.Size = new System.Drawing.Size(50, 28);
            this.cmbNgaySinh.StyleController = this.lcMain;
            this.cmbNgaySinh.TabIndex = 9;
            // 
            // cmbThang
            // 
            this.cmbThang.Location = new System.Drawing.Point(676, 42);
            this.cmbThang.Name = "cmbThang";
            this.cmbThang.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmbThang.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.cmbThang.Properties.Appearance.Options.UseFont = true;
            this.cmbThang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbThang.Properties.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cmbThang.Properties.NullText = "[Chọn tháng]";
            this.cmbThang.Size = new System.Drawing.Size(50, 28);
            this.cmbThang.StyleController = this.lcMain;
            this.cmbThang.TabIndex = 6;
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
            this.Root.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlGroup1});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(1185, 503);
            this.Root.Text = "Tìm kiếm";
            this.Root.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gcMain;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1173, 413);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup1.ExpandButtonVisible = true;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10,
            this.layoutControlItem9,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1173, 78);
            this.layoutControlGroup1.Text = "Tìm kiếm";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnReset;
            this.layoutControlItem10.Location = new System.Drawing.Point(1083, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(66, 32);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnTimKiem;
            this.layoutControlItem9.Location = new System.Drawing.Point(994, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(89, 32);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbNgaySinh;
            this.layoutControlItem4.Location = new System.Drawing.Point(852, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(142, 32);
            this.layoutControlItem4.Text = "Ngày sinh";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(85, 22);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cmbThang;
            this.layoutControlItem3.Location = new System.Drawing.Point(568, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(142, 32);
            this.layoutControlItem3.Text = "Tháng sinh";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(85, 22);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbPhuongXa;
            this.layoutControlItem2.Location = new System.Drawing.Point(142, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(142, 32);
            this.layoutControlItem2.Text = "Phường";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(85, 22);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cmbQuanHuyen;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(142, 32);
            this.layoutControlItem5.Text = "Quận";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(85, 22);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbLop;
            this.layoutControlItem1.Location = new System.Drawing.Point(284, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(142, 32);
            this.layoutControlItem1.Text = "Lớp";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(85, 22);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cmbThoiHoc;
            this.layoutControlItem7.Location = new System.Drawing.Point(426, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(142, 32);
            this.layoutControlItem7.Text = "Trạng thái";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(85, 22);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.cmbNamSinh;
            this.layoutControlItem8.Location = new System.Drawing.Point(710, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(142, 32);
            this.layoutControlItem8.Text = "Năm sinh";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(85, 22);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnIn);
            this.panelControl1.Controls.Add(this.btnLuu);
            this.panelControl1.Controls.Add(this.btnHuyBo);
            this.panelControl1.Controls.Add(this.btnChinhSua);
            this.panelControl1.Controls.Add(this.btnXoa);
            this.panelControl1.Controls.Add(this.btnThem);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 530);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(896, 43);
            this.panelControl1.TabIndex = 3;
            // 
            // btnIn
            // 
            this.btnIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIn.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnIn.Appearance.Options.UseFont = true;
            this.btnIn.Image = ((System.Drawing.Image)(resources.GetObject("btnIn.Image")));
            this.btnIn.Location = new System.Drawing.Point(778, 10);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(99, 25);
            this.btnIn.TabIndex = 1;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.Location = new System.Drawing.Point(668, 10);
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
            this.btnHuyBo.Location = new System.Drawing.Point(557, 10);
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
            this.btnChinhSua.Location = new System.Drawing.Point(446, 10);
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
            this.btnXoa.Location = new System.Drawing.Point(335, 10);
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
            this.btnThem.Location = new System.Drawing.Point(224, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(99, 25);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            // 
            // FrmThongTinHocSinh
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 573);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.lcMain);
            this.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmThongTinHocSinh";
            this.Text = "Thông tin Học sinh";
            this.Load += new System.EventHandler(this.FrmThongTinHocSinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbNamSinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbThoiHoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keyValuePairBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hocSinhRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuanHuyen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quanHuyenRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPhuongXa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phuongXaRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNgaySinh.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNgaySinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvMain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSTT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colHoTen;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGioiTinh;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNgaySinhNgay;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNgayVaoLop;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDienThoai;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colHoTenCha;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colHoTenMe;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDiaChi;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPhuong;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colQuan;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTinh;
        private System.Windows.Forms.BindingSource quanHuyenRowBindingSource;
        private System.Windows.Forms.BindingSource phuongXaRowBindingSource;
        private DevExpress.XtraEditors.LookUpEdit cmbQuanHuyen;
        private DevExpress.XtraEditors.LookUpEdit cmbPhuongXa;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNgaySinhThang;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNgaySinhNam;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.SimpleButton btnReset;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private System.Windows.Forms.BindingSource hocSinhRowBindingSource;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTinhTrang;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colHoDem;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTen;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnHuyBo;
        private DevExpress.XtraEditors.SimpleButton btnChinhSua;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraEditors.SimpleButton btnThem;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.DateEdit cmbNgaySinh;
        private DevExpress.XtraEditors.ComboBoxEdit cmbThang;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colThangSinh;
        private DevExpress.XtraEditors.SimpleButton btnIn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colLopDangHoc;
        private DevExpress.XtraEditors.LookUpEdit cmbLop;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource lopRowBindingSource;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNgaySinh;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand16;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand17;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand19;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand20;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand21;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand22;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand23;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand27;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand29;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand28;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand30;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDienThoaiMe;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand24;
        private DevExpress.XtraEditors.LookUpEdit cmbThoiHoc;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private System.Windows.Forms.BindingSource keyValuePairBindingSource;
        private DevExpress.XtraEditors.ComboBoxEdit cmbNamSinh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;


    }
}