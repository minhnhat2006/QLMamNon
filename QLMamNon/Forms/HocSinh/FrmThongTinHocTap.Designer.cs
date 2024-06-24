using QLMamNon.Dao;

namespace QLMamNon.Forms.HocSinh
{
    partial class FrmThongTinHocTap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThongTinHocTap));
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.cmbLop = new DevExpress.XtraEditors.LookUpEdit();
            this.lopRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.viewHocTapRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand16 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colSTT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridBand17 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colHoTen = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand19 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colGioiTinh = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand20 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colNgaySinhNgay = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand28 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colDienThoai = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colLopDangHoc = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colSoNgayNghiThang = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colHocSinhLopId = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colHocTapId = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.btnReset = new DevExpress.XtraEditors.SimpleButton();
            this.btnTimKiem = new DevExpress.XtraEditors.SimpleButton();
            this.cmbNam = new DevExpress.XtraEditors.LookUpEdit();
            this.namHocBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbThang = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuyBo = new DevExpress.XtraEditors.SimpleButton();
            this.colNgayTinh = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewHocTapRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.namHocBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.lcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lcMain.Controls.Add(this.cmbLop);
            this.lcMain.Controls.Add(this.gcMain);
            this.lcMain.Controls.Add(this.btnReset);
            this.lcMain.Controls.Add(this.btnTimKiem);
            this.lcMain.Controls.Add(this.cmbNam);
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
            this.lcMain.Size = new System.Drawing.Size(839, 524);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "layoutControl1";
            // 
            // cmbLop
            // 
            this.cmbLop.Location = new System.Drawing.Point(542, 39);
            this.cmbLop.Name = "cmbLop";
            this.cmbLop.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmbLop.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLop.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 37, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cmbLop.Properties.DataSource = this.lopRowBindingSource;
            this.cmbLop.Properties.DisplayMember = "Name";
            this.cmbLop.Properties.NullText = "";
            this.cmbLop.Properties.ShowHeader = false;
            this.cmbLop.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbLop.Properties.ValueMember = "LopId";
            this.cmbLop.Size = new System.Drawing.Size(130, 20);
            this.cmbLop.StyleController = this.lcMain;
            this.cmbLop.TabIndex = 9;
            // 
            // lopRowBindingSource
            // 
            this.lopRowBindingSource.DataSource = typeof(lop);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.viewHocTapRowBindingSource;
            this.gcMain.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(1);
            this.gcMain.Location = new System.Drawing.Point(8, 81);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gcMain.Size = new System.Drawing.Size(823, 435);
            this.gcMain.TabIndex = 8;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // viewHocTapRowBindingSource
            // 
            this.viewHocTapRowBindingSource.DataSource = typeof(viewhoctap);
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
            this.gridBand28,
            this.gridBand6,
            this.gridBand1});
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colSTT,
            this.colHoTen,
            this.colGioiTinh,
            this.colNgaySinhNgay,
            this.colLopDangHoc,
            this.colDienThoai,
            this.colSoNgayNghiThang,
            this.colHocSinhLopId,
            this.colHocTapId,
            this.colNgayTinh});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
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
            this.colSTT.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colSTT.AppearanceCell.Options.UseBackColor = true;
            this.colSTT.Caption = "STT";
            this.colSTT.ColumnEdit = this.repositoryItemTextEdit1;
            this.colSTT.FieldNameSortGroup = "HocSinhId";
            this.colSTT.Name = "colSTT";
            this.colSTT.OptionsColumn.AllowEdit = false;
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
            this.colHoTen.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colHoTen.AppearanceCell.Options.UseBackColor = true;
            this.colHoTen.Caption = "Họ tên";
            this.colHoTen.FieldName = "HoTen";
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.OptionsColumn.AllowEdit = false;
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
            this.colGioiTinh.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colGioiTinh.AppearanceCell.Options.UseBackColor = true;
            this.colGioiTinh.Caption = "Giới tính";
            this.colGioiTinh.ColumnEdit = this.repositoryItemTextEdit1;
            this.colGioiTinh.FieldName = "GioiTinh";
            this.colGioiTinh.Name = "colGioiTinh";
            this.colGioiTinh.OptionsColumn.AllowEdit = false;
            this.colGioiTinh.OptionsEditForm.Caption = "Giới tính:";
            this.colGioiTinh.Visible = true;
            this.colGioiTinh.Width = 90;
            // 
            // gridBand20
            // 
            this.gridBand20.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand20.Caption = "Ngày sinh";
            this.gridBand20.Columns.Add(this.colNgaySinhNgay);
            this.gridBand20.Name = "gridBand20";
            this.gridBand20.VisibleIndex = 3;
            this.gridBand20.Width = 90;
            // 
            // colNgaySinhNgay
            // 
            this.colNgaySinhNgay.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colNgaySinhNgay.AppearanceCell.Options.UseBackColor = true;
            this.colNgaySinhNgay.Caption = "NgaySinh";
            this.colNgaySinhNgay.FieldName = "NgaySinh";
            this.colNgaySinhNgay.Name = "colNgaySinhNgay";
            this.colNgaySinhNgay.OptionsColumn.AllowEdit = false;
            this.colNgaySinhNgay.OptionsEditForm.Caption = "Ngày sinh:";
            this.colNgaySinhNgay.OptionsEditForm.StartNewRow = true;
            this.colNgaySinhNgay.Visible = true;
            this.colNgaySinhNgay.Width = 90;
            // 
            // gridBand28
            // 
            this.gridBand28.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand28.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand28.Caption = "Điện thoại";
            this.gridBand28.Columns.Add(this.colDienThoai);
            this.gridBand28.Name = "gridBand28";
            this.gridBand28.VisibleIndex = 4;
            this.gridBand28.Width = 90;
            // 
            // colDienThoai
            // 
            this.colDienThoai.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colDienThoai.AppearanceCell.Options.UseBackColor = true;
            this.colDienThoai.FieldName = "DienThoai";
            this.colDienThoai.Name = "colDienThoai";
            this.colDienThoai.OptionsColumn.AllowEdit = false;
            this.colDienThoai.OptionsEditForm.Caption = "Điện thoại:";
            this.colDienThoai.OptionsEditForm.StartNewRow = true;
            this.colDienThoai.Visible = true;
            this.colDienThoai.Width = 90;
            // 
            // gridBand6
            // 
            this.gridBand6.Caption = "Lớp đang học";
            this.gridBand6.Columns.Add(this.colLopDangHoc);
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.VisibleIndex = 5;
            this.gridBand6.Width = 90;
            // 
            // colLopDangHoc
            // 
            this.colLopDangHoc.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colLopDangHoc.AppearanceCell.Options.UseBackColor = true;
            this.colLopDangHoc.Caption = "Lop dang hoc";
            this.colLopDangHoc.FieldName = "LopName";
            this.colLopDangHoc.Name = "colLopDangHoc";
            this.colLopDangHoc.OptionsColumn.AllowEdit = false;
            this.colLopDangHoc.Visible = true;
            this.colLopDangHoc.Width = 90;
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.Caption = "Số ngày nghỉ trong tháng";
            this.gridBand1.Columns.Add(this.colSoNgayNghiThang);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 6;
            this.gridBand1.Width = 160;
            // 
            // colSoNgayNghiThang
            // 
            this.colSoNgayNghiThang.AppearanceCell.Options.UseTextOptions = true;
            this.colSoNgayNghiThang.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSoNgayNghiThang.FieldName = "SoNgayNghiThang";
            this.colSoNgayNghiThang.Name = "colSoNgayNghiThang";
            this.colSoNgayNghiThang.OptionsEditForm.StartNewRow = true;
            this.colSoNgayNghiThang.Visible = true;
            this.colSoNgayNghiThang.Width = 160;
            // 
            // colHocSinhLopId
            // 
            this.colHocSinhLopId.FieldName = "HocSinhLopId";
            this.colHocSinhLopId.Name = "colHocSinhLopId";
            // 
            // colHocTapId
            // 
            this.colHocTapId.FieldName = "HocTapId";
            this.colHocTapId.Name = "colHocTapId";
            // 
            // btnReset
            // 
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.Location = new System.Drawing.Point(761, 39);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(58, 22);
            this.btnReset.StyleController = this.lcMain;
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("btnTimKiem.Appearance.Image")));
            this.btnTimKiem.Appearance.Options.UseImage = true;
            this.btnTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("btnTimKiem.Image")));
            this.btnTimKiem.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnTimKiem.Location = new System.Drawing.Point(676, 39);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(81, 22);
            this.btnTimKiem.StyleController = this.lcMain;
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // cmbNam
            // 
            this.cmbNam.Location = new System.Drawing.Point(60, 39);
            this.cmbNam.Name = "cmbNam";
            this.cmbNam.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmbNam.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.cmbNam.Properties.Appearance.Options.UseFont = true;
            this.cmbNam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNam.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FromYear", "Năm")});
            this.cmbNam.Properties.DataSource = this.namHocBindingSource;
            this.cmbNam.Properties.DisplayMember = "FromYear";
            this.cmbNam.Properties.NullText = "";
            this.cmbNam.Properties.PopupSizeable = false;
            this.cmbNam.Properties.ShowHeader = false;
            this.cmbNam.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbNam.Properties.ValueMember = "FromYear";
            this.cmbNam.Size = new System.Drawing.Size(203, 26);
            this.cmbNam.StyleController = this.lcMain;
            this.cmbNam.TabIndex = 2;
            // 
            // namHocBindingSource
            // 
            this.namHocBindingSource.DataSource = typeof(QLMamNon.Entity.Form.NamHoc);
            // 
            // cmbThang
            // 
            this.cmbThang.Location = new System.Drawing.Point(307, 39);
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
            this.cmbThang.Size = new System.Drawing.Size(191, 26);
            this.cmbThang.StyleController = this.lcMain;
            this.cmbThang.TabIndex = 3;
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
            this.Root.Size = new System.Drawing.Size(839, 524);
            this.Root.Text = "Tìm kiếm";
            this.Root.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gcMain;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 73);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(827, 439);
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
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(827, 73);
            this.layoutControlGroup1.Text = "Tìm kiếm";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnReset;
            this.layoutControlItem10.Location = new System.Drawing.Point(741, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(62, 30);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnTimKiem;
            this.layoutControlItem9.Location = new System.Drawing.Point(656, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(85, 30);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbThang;
            this.layoutControlItem2.Location = new System.Drawing.Point(247, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(235, 30);
            this.layoutControlItem2.Text = "Tháng";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(37, 19);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cmbNam;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(247, 30);
            this.layoutControlItem5.Text = "Năm";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(37, 19);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbLop;
            this.layoutControlItem1.Location = new System.Drawing.Point(482, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(174, 30);
            this.layoutControlItem1.Text = "Lớp";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(37, 19);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnLuu);
            this.panelControl1.Controls.Add(this.btnHuyBo);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 530);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(839, 43);
            this.panelControl1.TabIndex = 3;
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.Location = new System.Drawing.Point(730, 10);
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
            this.btnHuyBo.Location = new System.Drawing.Point(619, 10);
            this.btnHuyBo.Name = "btnHuyBo";
            this.btnHuyBo.Size = new System.Drawing.Size(99, 25);
            this.btnHuyBo.TabIndex = 0;
            this.btnHuyBo.Text = "Hủy bỏ";
            // 
            // colNgayTinh
            // 
            this.colNgayTinh.FieldName = "NgayTinh";
            this.colNgayTinh.Name = "colNgayTinh";
            // 
            // FrmThongTinHocTap
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 573);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.lcMain);
            this.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmThongTinHocTap";
            this.Text = "Thông tin học tập";
            this.Load += new System.EventHandler(this.FrmThongTinHocTap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbLop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewHocTapRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.namHocBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvMain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSTT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colHoTen;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGioiTinh;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNgaySinhNgay;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDienThoai;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSoNgayNghiThang;
        private DevExpress.XtraEditors.LookUpEdit cmbNam;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.SimpleButton btnReset;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnHuyBo;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colLopDangHoc;
        private DevExpress.XtraEditors.LookUpEdit cmbLop;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource lopRowBindingSource;
        private System.Windows.Forms.BindingSource viewHocTapRowBindingSource;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand16;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand17;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand19;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand20;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand28;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbThang;
        private System.Windows.Forms.BindingSource namHocBindingSource;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colHocSinhLopId;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colHocTapId;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNgayTinh;


    }
}