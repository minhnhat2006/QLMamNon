namespace QLMamNon.Forms.GiaoVien
{
    partial class FrmGiaoVien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGiaoVien));
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.giaoVienRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colHoTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGioiTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgaySinhNgay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienThoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayVaoLop = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChucVu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTinhTPId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuanHuyenId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhuongXaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiaChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnReset = new DevExpress.XtraEditors.SimpleButton();
            this.btnTimKiem = new DevExpress.XtraEditors.SimpleButton();
            this.cmbQuanHuyen = new DevExpress.XtraEditors.LookUpEdit();
            this.quanHuyenRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbPhuongXa = new DevExpress.XtraEditors.LookUpEdit();
            this.phuongXaRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuyBo = new DevExpress.XtraEditors.SimpleButton();
            this.btnChinhSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnThem = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.giaoVienRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuanHuyen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quanHuyenRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPhuongXa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phuongXaRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.lcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lcMain.Controls.Add(this.gcMain);
            this.lcMain.Controls.Add(this.btnReset);
            this.lcMain.Controls.Add(this.btnTimKiem);
            this.lcMain.Controls.Add(this.cmbQuanHuyen);
            this.lcMain.Controls.Add(this.cmbPhuongXa);
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
            // gcMain
            // 
            this.gcMain.DataSource = this.giaoVienRowBindingSource;
            this.gcMain.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(1);
            this.gcMain.Location = new System.Drawing.Point(8, 81);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gcMain.Size = new System.Drawing.Size(880, 435);
            this.gcMain.TabIndex = 8;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // giaoVienRowBindingSource
            // 
            this.giaoVienRowBindingSource.DataSource = typeof(GiaoVien);
            // 
            // gvMain
            // 
            this.gvMain.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.gvMain.Appearance.Row.Options.UseFont = true;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSTT,
            this.colHoTen,
            this.colGioiTinh,
            this.colNgaySinhNgay,
            this.colDienThoai,
            this.colEmail,
            this.colNgayVaoLop,
            this.colChucVu,
            this.colTinhTPId,
            this.colQuanHuyenId,
            this.colPhuongXaId,
            this.colDiaChi});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gvMain.OptionsEditForm.FormCaptionFormat = "Chỉnh sửa thông tin Giáo viên";
            this.gvMain.OptionsPrint.PrintDetails = true;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
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
            this.colSTT.VisibleIndex = 0;
            this.colSTT.Width = 40;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // colHoTen
            // 
            this.colHoTen.Caption = "Họ tên";
            this.colHoTen.FieldName = "HoTen";
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.OptionsEditForm.Caption = "Họ đệm:";
            this.colHoTen.OptionsEditForm.StartNewRow = true;
            this.colHoTen.Visible = true;
            this.colHoTen.VisibleIndex = 1;
            this.colHoTen.Width = 150;
            // 
            // colGioiTinh
            // 
            this.colGioiTinh.Caption = "Giới tính";
            this.colGioiTinh.ColumnEdit = this.repositoryItemTextEdit1;
            this.colGioiTinh.FieldName = "GioiTinh";
            this.colGioiTinh.Name = "colGioiTinh";
            this.colGioiTinh.OptionsEditForm.Caption = "Giới tính:";
            this.colGioiTinh.Visible = true;
            this.colGioiTinh.VisibleIndex = 2;
            this.colGioiTinh.Width = 90;
            // 
            // colNgaySinhNgay
            // 
            this.colNgaySinhNgay.Caption = "Ngày sinh";
            this.colNgaySinhNgay.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colNgaySinhNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgaySinhNgay.FieldName = "NgaySinh";
            this.colNgaySinhNgay.Name = "colNgaySinhNgay";
            this.colNgaySinhNgay.OptionsEditForm.Caption = "Ngày sinh:";
            this.colNgaySinhNgay.OptionsEditForm.StartNewRow = true;
            this.colNgaySinhNgay.Visible = true;
            this.colNgaySinhNgay.VisibleIndex = 3;
            this.colNgaySinhNgay.Width = 90;
            // 
            // colDienThoai
            // 
            this.colDienThoai.Caption = "Điện thoại";
            this.colDienThoai.FieldName = "DienThoai";
            this.colDienThoai.Name = "colDienThoai";
            this.colDienThoai.OptionsEditForm.Caption = "Điện thoại:";
            this.colDienThoai.OptionsEditForm.StartNewRow = true;
            this.colDienThoai.Visible = true;
            this.colDienThoai.VisibleIndex = 4;
            this.colDienThoai.Width = 90;
            // 
            // colEmail
            // 
            this.colEmail.Caption = "Email";
            this.colEmail.FieldName = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.Visible = true;
            this.colEmail.VisibleIndex = 5;
            // 
            // colNgayVaoLop
            // 
            this.colNgayVaoLop.Caption = "Ngày vào làm";
            this.colNgayVaoLop.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colNgayVaoLop.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayVaoLop.FieldName = "NgayVaoLam";
            this.colNgayVaoLop.Name = "colNgayVaoLop";
            this.colNgayVaoLop.OptionsEditForm.Caption = "Ngày vào lớp:";
            this.colNgayVaoLop.OptionsEditForm.StartNewRow = true;
            this.colNgayVaoLop.Visible = true;
            this.colNgayVaoLop.VisibleIndex = 6;
            this.colNgayVaoLop.Width = 90;
            // 
            // colChucVu
            // 
            this.colChucVu.Caption = "Chức vụ";
            this.colChucVu.FieldName = "ChucVu";
            this.colChucVu.Name = "colChucVu";
            this.colChucVu.Visible = true;
            this.colChucVu.VisibleIndex = 7;
            // 
            // colTinhTPId
            // 
            this.colTinhTPId.Caption = "Tỉnh/Thành phố";
            this.colTinhTPId.FieldName = "TinhTPId";
            this.colTinhTPId.Name = "colTinhTPId";
            this.colTinhTPId.Visible = true;
            this.colTinhTPId.VisibleIndex = 8;
            // 
            // colQuanHuyenId
            // 
            this.colQuanHuyenId.Caption = "Quận/huyện";
            this.colQuanHuyenId.FieldName = "QuanHuyenId";
            this.colQuanHuyenId.Name = "colQuanHuyenId";
            this.colQuanHuyenId.Visible = true;
            this.colQuanHuyenId.VisibleIndex = 9;
            // 
            // colPhuongXaId
            // 
            this.colPhuongXaId.Caption = "Phường/xã";
            this.colPhuongXaId.FieldName = "PhuongXaId";
            this.colPhuongXaId.Name = "colPhuongXaId";
            this.colPhuongXaId.Visible = true;
            this.colPhuongXaId.VisibleIndex = 10;
            // 
            // colDiaChi
            // 
            this.colDiaChi.Caption = "Địa chỉ";
            this.colDiaChi.FieldName = "DiaChi";
            this.colDiaChi.Name = "colDiaChi";
            this.colDiaChi.Visible = true;
            this.colDiaChi.VisibleIndex = 11;
            // 
            // btnReset
            // 
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.Location = new System.Drawing.Point(818, 39);
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
            this.btnTimKiem.Location = new System.Drawing.Point(732, 39);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(82, 22);
            this.btnTimKiem.StyleController = this.lcMain;
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // cmbQuanHuyen
            // 
            this.cmbQuanHuyen.Location = new System.Drawing.Point(70, 39);
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
            this.cmbQuanHuyen.Size = new System.Drawing.Size(249, 26);
            this.cmbQuanHuyen.StyleController = this.lcMain;
            this.cmbQuanHuyen.TabIndex = 2;
            this.cmbQuanHuyen.EditValueChanged += new System.EventHandler(this.cmbQuanHuyen_EditValueChanged);
            // 
            // quanHuyenRowBindingSource
            // 
            this.quanHuyenRowBindingSource.DataSource = typeof(QuanHuyenRow);
            // 
            // cmbPhuongXa
            // 
            this.cmbPhuongXa.Location = new System.Drawing.Point(373, 39);
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
            this.cmbPhuongXa.Size = new System.Drawing.Size(355, 26);
            this.cmbPhuongXa.StyleController = this.lcMain;
            this.cmbPhuongXa.TabIndex = 3;
            // 
            // phuongXaRowBindingSource
            // 
            this.phuongXaRowBindingSource.DataSource = typeof(PhuongXaRow);
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
            this.Root.Size = new System.Drawing.Size(896, 524);
            this.Root.Text = "Tìm kiếm";
            this.Root.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gcMain;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 73);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(884, 439);
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
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(884, 73);
            this.layoutControlGroup1.Text = "Tìm kiếm";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnReset;
            this.layoutControlItem10.Location = new System.Drawing.Point(798, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(62, 30);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnTimKiem;
            this.layoutControlItem9.Location = new System.Drawing.Point(712, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(86, 30);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbPhuongXa;
            this.layoutControlItem2.Location = new System.Drawing.Point(303, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(409, 30);
            this.layoutControlItem2.Text = "Phường";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(47, 19);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cmbQuanHuyen;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(303, 30);
            this.layoutControlItem5.Text = "Quận";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(47, 19);
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
            this.panelControl1.Size = new System.Drawing.Size(896, 43);
            this.panelControl1.TabIndex = 3;
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.Location = new System.Drawing.Point(786, 10);
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
            this.btnHuyBo.Location = new System.Drawing.Point(675, 10);
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
            this.btnChinhSua.Location = new System.Drawing.Point(564, 10);
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
            this.btnXoa.Location = new System.Drawing.Point(453, 10);
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
            this.btnThem.Location = new System.Drawing.Point(342, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(99, 25);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            // 
            // giaoVienTableAdapter
            // 
            this.giaoVienTableAdapter.ClearBeforeFill = true;
            // 
            // FrmGiaoVien
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 573);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.lcMain);
            this.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmGiaoVien";
            this.Text = "Thông tin Giáo viên";
            this.Load += new System.EventHandler(this.FrmGiaoVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.giaoVienRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuanHuyen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quanHuyenRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPhuongXa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phuongXaRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
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
        private System.Windows.Forms.BindingSource quanHuyenRowBindingSource;
        private System.Windows.Forms.BindingSource phuongXaRowBindingSource;
        private DevExpress.XtraEditors.LookUpEdit cmbQuanHuyen;
        private DevExpress.XtraEditors.LookUpEdit cmbPhuongXa;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.SimpleButton btnReset;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnHuyBo;
        private DevExpress.XtraEditors.SimpleButton btnChinhSua;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraEditors.SimpleButton btnThem;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.BindingSource giaoVienRowBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colHoTen;
        private DevExpress.XtraGrid.Columns.GridColumn colGioiTinh;
        private DevExpress.XtraGrid.Columns.GridColumn colNgaySinhNgay;
        private DevExpress.XtraGrid.Columns.GridColumn colDienThoai;
        private DevExpress.XtraGrid.Columns.GridColumn colEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayVaoLop;
        private DevExpress.XtraGrid.Columns.GridColumn colChucVu;
        private DevExpress.XtraGrid.Columns.GridColumn colTinhTPId;
        private DevExpress.XtraGrid.Columns.GridColumn colQuanHuyenId;
        private DevExpress.XtraGrid.Columns.GridColumn colPhuongXaId;
        private DevExpress.XtraGrid.Columns.GridColumn colDiaChi;
    }
}