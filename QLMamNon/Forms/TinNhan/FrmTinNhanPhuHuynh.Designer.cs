namespace QLMamNon.Forms.TinNhan
{
    partial class FrmTinNhanPhuHuynh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTinNhanPhuHuynh));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.hocsinhBindingSource = new System.Windows.Forms.BindingSource();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colHoTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienThoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHoTenCha = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienThoaiMe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHoTenMe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.cmbLop = new DevExpress.XtraEditors.LookUpEdit();
            this.lopBindingSource = new System.Windows.Forms.BindingSource();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDefault = new DevExpress.XtraEditors.SimpleButton();
            this.txtInterval = new System.Windows.Forms.NumericUpDown();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnConnect = new DevExpress.XtraEditors.SimpleButton();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.smstemplateBindingSource = new System.Windows.Forms.BindingSource();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSmsTemplateId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTemplate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnGui = new DevExpress.XtraEditors.SimpleButton();
            this.txtNoiDung = new DevExpress.XtraEditors.MemoEdit();
            this.lblKiTu = new DevExpress.XtraEditors.LabelControl();
            this.txtGuide = new System.Windows.Forms.RichTextBox();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.serialPort1 = new System.IO.Ports.SerialPort();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hocsinhBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterval)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smstemplateBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoiDung.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.lcMain);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panel2);
            this.splitContainerControl1.Panel2.Controls.Add(this.panel1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(893, 411);
            this.splitContainerControl1.SplitterPosition = 407;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // lcMain
            // 
            this.lcMain.Controls.Add(this.gcMain);
            this.lcMain.Controls.Add(this.cmbLop);
            this.lcMain.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.lcMain.Root = this.layoutControlGroup1;
            this.lcMain.Size = new System.Drawing.Size(407, 411);
            this.lcMain.TabIndex = 15;
            this.lcMain.Text = "layoutControl1";
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.hocsinhBindingSource;
            this.gcMain.Location = new System.Drawing.Point(12, 36);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcMain.Size = new System.Drawing.Size(383, 363);
            this.gcMain.TabIndex = 16;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // hocsinhBindingSource
            // 
            this.hocsinhBindingSource.DataSource = typeof(QLMamNon.Dao.hocsinh);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colHoTen,
            this.colDienThoai,
            this.colHoTenCha,
            this.colDienThoaiMe,
            this.colHoTenMe});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colHoTen
            // 
            this.colHoTen.Caption = "Họ tên";
            this.colHoTen.FieldName = "HoTen";
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.Visible = true;
            this.colHoTen.VisibleIndex = 1;
            this.colHoTen.Width = 100;
            // 
            // colDienThoai
            // 
            this.colDienThoai.Caption = "Số điện thoại cha";
            this.colDienThoai.FieldName = "DienThoai";
            this.colDienThoai.Name = "colDienThoai";
            this.colDienThoai.Visible = true;
            this.colDienThoai.VisibleIndex = 2;
            this.colDienThoai.Width = 60;
            // 
            // colHoTenCha
            // 
            this.colHoTenCha.Caption = "Họ tên cha";
            this.colHoTenCha.FieldName = "HoTenCha";
            this.colHoTenCha.Name = "colHoTenCha";
            this.colHoTenCha.Visible = true;
            this.colHoTenCha.VisibleIndex = 3;
            this.colHoTenCha.Width = 89;
            // 
            // colDienThoaiMe
            // 
            this.colDienThoaiMe.Caption = "Số điện thoại mẹ";
            this.colDienThoaiMe.FieldName = "DienThoaiMe";
            this.colDienThoaiMe.Name = "colDienThoaiMe";
            this.colDienThoaiMe.Visible = true;
            this.colDienThoaiMe.VisibleIndex = 4;
            // 
            // colHoTenMe
            // 
            this.colHoTenMe.Caption = "Họ tên mẹ";
            this.colHoTenMe.FieldName = "HoTenMe";
            this.colHoTenMe.Name = "colHoTenMe";
            this.colHoTenMe.Visible = true;
            this.colHoTenMe.VisibleIndex = 5;
            this.colHoTenMe.Width = 95;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // cmbLop
            // 
            this.cmbLop.Location = new System.Drawing.Point(52, 12);
            this.cmbLop.Name = "cmbLop";
            this.cmbLop.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmbLop.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLop.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 35, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cmbLop.Properties.DataSource = this.lopBindingSource;
            this.cmbLop.Properties.DisplayMember = "Name";
            this.cmbLop.Properties.NullText = "[Chọn lớp]";
            this.cmbLop.Properties.ShowHeader = false;
            this.cmbLop.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbLop.Properties.ValueMember = "LopId";
            this.cmbLop.Size = new System.Drawing.Size(343, 20);
            this.cmbLop.StyleController = this.lcMain;
            this.cmbLop.TabIndex = 15;
            this.cmbLop.EditValueChanged += new System.EventHandler(this.cmbLop_EditValueChanged);
            // 
            // lopBindingSource
            // 
            this.lopBindingSource.DataSource = typeof(QLMamNon.Dao.lop);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(407, 411);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbLop;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(387, 24);
            this.layoutControlItem2.Text = "Lớp học";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(37, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcMain;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(387, 367);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnDefault);
            this.panel2.Controls.Add(this.txtInterval);
            this.panel2.Controls.Add(this.labelControl2);
            this.panel2.Controls.Add(this.btnConnect);
            this.panel2.Controls.Add(this.cmbPort);
            this.panel2.Controls.Add(this.labelControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 304);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(481, 107);
            this.panel2.TabIndex = 5;
            // 
            // btnDefault
            // 
            this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefault.Image = ((System.Drawing.Image)(resources.GetObject("btnDefault.Image")));
            this.btnDefault.Location = new System.Drawing.Point(397, 72);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(77, 23);
            this.btnDefault.TabIndex = 11;
            this.btnDefault.Text = "Mặc định";
            // 
            // txtInterval
            // 
            this.txtInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInterval.Location = new System.Drawing.Point(9, 74);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(382, 21);
            this.txtInterval.TabIndex = 10;
            this.txtInterval.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl2.Location = new System.Drawing.Point(8, 57);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(173, 13);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "Thời gian chờ giữa 2 tin nhắn (giây):";
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Image = ((System.Drawing.Image)(resources.GetObject("btnConnect.Image")));
            this.btnConnect.Location = new System.Drawing.Point(397, 24);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(77, 23);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "Kiểm tra";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cmbPort
            // 
            this.cmbPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(8, 26);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(383, 21);
            this.cmbPort.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl1.Location = new System.Drawing.Point(8, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(77, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Chọn cổng USB:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.gridControl1);
            this.panel1.Controls.Add(this.labelControl3);
            this.panel1.Controls.Add(this.btnGui);
            this.panel1.Controls.Add(this.txtNoiDung);
            this.panel1.Controls.Add(this.lblKiTu);
            this.panel1.Controls.Add(this.txtGuide);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(481, 298);
            this.panel1.TabIndex = 4;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.DataSource = this.smstemplateBindingSource;
            this.gridControl1.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gridControl1_EmbeddedNavigator_ButtonClick);
            this.gridControl1.Location = new System.Drawing.Point(5, 83);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(469, 88);
            this.gridControl1.TabIndex = 9;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // smstemplateBindingSource
            // 
            this.smstemplateBindingSource.DataSource = typeof(QLMamNon.Dao.smstemplate);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSmsTemplateId,
            this.colTemplate,
            this.colCreatedDate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // colSmsTemplateId
            // 
            this.colSmsTemplateId.FieldName = "SmsTemplateId";
            this.colSmsTemplateId.Name = "colSmsTemplateId";
            // 
            // colTemplate
            // 
            this.colTemplate.FieldName = "Template";
            this.colTemplate.Name = "colTemplate";
            this.colTemplate.Visible = true;
            this.colTemplate.VisibleIndex = 0;
            // 
            // colCreatedDate
            // 
            this.colCreatedDate.FieldName = "CreatedDate";
            this.colCreatedDate.Name = "colCreatedDate";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 63);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(128, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Chọn mẫu tin nhắn có sẵn:";
            // 
            // btnGui
            // 
            this.btnGui.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGui.Image = ((System.Drawing.Image)(resources.GetObject("btnGui.Image")));
            this.btnGui.Location = new System.Drawing.Point(397, 269);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(77, 23);
            this.btnGui.TabIndex = 7;
            this.btnGui.Text = "Gửi";
            this.btnGui.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoiDung.Location = new System.Drawing.Point(5, 197);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Properties.MaxLength = 160;
            this.txtNoiDung.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNoiDung.Size = new System.Drawing.Size(469, 66);
            this.txtNoiDung.TabIndex = 6;
            this.txtNoiDung.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtNoiDung_EditValueChanging);
            // 
            // lblKiTu
            // 
            this.lblKiTu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblKiTu.Location = new System.Drawing.Point(5, 177);
            this.lblKiTu.Name = "lblKiTu";
            this.lblKiTu.Size = new System.Drawing.Size(263, 13);
            this.lblKiTu.TabIndex = 5;
            this.lblKiTu.Text = "Nội dung tin nhắn tối đa 160 kí tự (số kí tự còn lại: 160)";
            // 
            // txtGuide
            // 
            this.txtGuide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGuide.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtGuide.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGuide.Location = new System.Drawing.Point(5, 5);
            this.txtGuide.Name = "txtGuide";
            this.txtGuide.Size = new System.Drawing.Size(469, 50);
            this.txtGuide.TabIndex = 4;
            this.txtGuide.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Image = ((System.Drawing.Image)(resources.GetObject("btnSend.Image")));
            this.btnSend.Location = new System.Drawing.Point(0, 88);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(65, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Gửi";
            // 
            // memoEdit1
            // 
            this.memoEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoEdit1.Location = new System.Drawing.Point(1, 87);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoEdit1.Size = new System.Drawing.Size(343, 312);
            this.memoEdit1.TabIndex = 2;
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.DtrEnable = true;
            this.serialPort1.Handshake = System.IO.Ports.Handshake.RequestToSend;
            this.serialPort1.RtsEnable = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // FrmTinNhanPhuHuynh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 411);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmTinNhanPhuHuynh";
            this.Text = "Gửi tin nhắn cho phụ huynh học sinh";
            this.Load += new System.EventHandler(this.FrmTinNhanPhuHuynh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hocsinhBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterval)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smstemplateBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoiDung.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.LookUpEdit cmbLop;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colHoTen;
        private DevExpress.XtraGrid.Columns.GridColumn colDienThoai;
        private DevExpress.XtraGrid.Columns.GridColumn colHoTenCha;
        private DevExpress.XtraGrid.Columns.GridColumn colHoTenMe;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colDienThoaiMe;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnGui;
        private DevExpress.XtraEditors.MemoEdit txtNoiDung;
        private DevExpress.XtraEditors.LabelControl lblKiTu;
        private System.Windows.Forms.RichTextBox txtGuide;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox cmbPort;
        private DevExpress.XtraEditors.SimpleButton btnConnect;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.NumericUpDown txtInterval;
        private DevExpress.XtraEditors.SimpleButton btnDefault;
        private System.IO.Ports.SerialPort serialPort1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colSmsTemplateId;
        private DevExpress.XtraGrid.Columns.GridColumn colTemplate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedDate;
        private System.Windows.Forms.BindingSource lopBindingSource;
        private System.Windows.Forms.BindingSource hocsinhBindingSource;
        private System.Windows.Forms.BindingSource smstemplateBindingSource;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}