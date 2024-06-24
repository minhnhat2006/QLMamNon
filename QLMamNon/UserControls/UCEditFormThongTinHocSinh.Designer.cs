using QLMamNon.Dao;

namespace QLMamNon.UserControls
{
    partial class UCEditFormThongTinHocSinh
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule3 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtDienThoaiMe = new DevExpress.XtraEditors.TextEdit();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtSoNha = new DevExpress.XtraEditors.TextEdit();
            this.txtMe = new DevExpress.XtraEditors.TextEdit();
            this.txtCha = new DevExpress.XtraEditors.TextEdit();
            this.txtDienThoai = new DevExpress.XtraEditors.TextEdit();
            this.txtNgaySinh = new DevExpress.XtraEditors.DateEdit();
            this.chkGioiTinh = new DevExpress.XtraEditors.RadioGroup();
            this.txtTen = new DevExpress.XtraEditors.TextEdit();
            this.txtHoDem = new DevExpress.XtraEditors.TextEdit();
            this.cmbTinh = new DevExpress.XtraEditors.LookUpEdit();
            this.thanhPhoRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbQuan = new DevExpress.XtraEditors.LookUpEdit();
            this.quanHuyenRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbPhuong = new DevExpress.XtraEditors.LookUpEdit();
            this.phuongXaRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienThoaiMe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoNha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienThoai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNgaySinh.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNgaySinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGioiTinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoDem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thanhPhoRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quanHuyenRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPhuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phuongXaRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.SetBoundPropertyName(this.layoutControl1, "");
            this.layoutControl1.Controls.Add(this.txtDienThoaiMe);
            this.layoutControl1.Controls.Add(this.checkBox1);
            this.layoutControl1.Controls.Add(this.txtSoNha);
            this.layoutControl1.Controls.Add(this.txtMe);
            this.layoutControl1.Controls.Add(this.txtCha);
            this.layoutControl1.Controls.Add(this.txtDienThoai);
            this.layoutControl1.Controls.Add(this.txtNgaySinh);
            this.layoutControl1.Controls.Add(this.chkGioiTinh);
            this.layoutControl1.Controls.Add(this.txtTen);
            this.layoutControl1.Controls.Add(this.txtHoDem);
            this.layoutControl1.Controls.Add(this.cmbTinh);
            this.layoutControl1.Controls.Add(this.cmbQuan);
            this.layoutControl1.Controls.Add(this.cmbPhuong);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.BackColor = System.Drawing.Color.LightGray;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseBackColor = true;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseTextOptions = true;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControl1.OptionsPrint.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControl1.OptionsPrint.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.layoutControl1.OptionsPrint.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(600, 165);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtDienThoaiMe
            // 
            this.SetBoundFieldName(this.txtDienThoaiMe, "DienThoaiMe");
            this.SetBoundPropertyName(this.txtDienThoaiMe, "EditValue");
            this.txtDienThoaiMe.Location = new System.Drawing.Point(373, 102);
            this.txtDienThoaiMe.Name = "txtDienThoaiMe";
            this.txtDienThoaiMe.Size = new System.Drawing.Size(198, 20);
            this.txtDienThoaiMe.StyleController = this.layoutControl1;
            this.txtDienThoaiMe.TabIndex = 7;
            // 
            // checkBox1
            // 
            this.SetBoundFieldName(this.checkBox1, "ThoiHoc");
            this.SetBoundPropertyName(this.checkBox1, "Checked");
            this.checkBox1.Location = new System.Drawing.Point(107, 195);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(464, 20);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtSoNha
            // 
            this.SetBoundFieldName(this.txtSoNha, "DiaChi");
            this.SetBoundPropertyName(this.txtSoNha, "EditValue");
            this.txtSoNha.Location = new System.Drawing.Point(373, 165);
            this.txtSoNha.Name = "txtSoNha";
            this.txtSoNha.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtSoNha.Properties.Appearance.Options.UseFont = true;
            this.txtSoNha.Size = new System.Drawing.Size(198, 26);
            this.txtSoNha.StyleController = this.layoutControl1;
            this.txtSoNha.TabIndex = 11;
            // 
            // txtMe
            // 
            this.SetBoundFieldName(this.txtMe, "HoTenMe");
            this.SetBoundPropertyName(this.txtMe, "EditValue");
            this.txtMe.Location = new System.Drawing.Point(107, 105);
            this.txtMe.Name = "txtMe";
            this.txtMe.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtMe.Properties.Appearance.Options.UseFont = true;
            this.txtMe.Size = new System.Drawing.Size(167, 26);
            this.txtMe.StyleController = this.layoutControl1;
            this.txtMe.TabIndex = 6;
            // 
            // txtCha
            // 
            this.SetBoundFieldName(this.txtCha, "HoTenCha");
            this.SetBoundPropertyName(this.txtCha, "EditValue");
            this.txtCha.Location = new System.Drawing.Point(107, 75);
            this.txtCha.Name = "txtCha";
            this.txtCha.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtCha.Properties.Appearance.Options.UseFont = true;
            this.txtCha.Size = new System.Drawing.Size(167, 26);
            this.txtCha.StyleController = this.layoutControl1;
            this.txtCha.TabIndex = 4;
            // 
            // txtDienThoai
            // 
            this.SetBoundFieldName(this.txtDienThoai, "DienThoai");
            this.SetBoundPropertyName(this.txtDienThoai, "EditValue");
            this.txtDienThoai.Location = new System.Drawing.Point(373, 72);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtDienThoai.Properties.Appearance.Options.UseFont = true;
            this.txtDienThoai.Size = new System.Drawing.Size(198, 26);
            this.txtDienThoai.StyleController = this.layoutControl1;
            this.txtDienThoai.TabIndex = 5;
            // 
            // txtNgaySinh
            // 
            this.SetBoundFieldName(this.txtNgaySinh, "NgaySinh");
            this.SetBoundPropertyName(this.txtNgaySinh, "EditValue");
            this.txtNgaySinh.EditValue = null;
            this.txtNgaySinh.Location = new System.Drawing.Point(373, 42);
            this.txtNgaySinh.Name = "txtNgaySinh";
            this.txtNgaySinh.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtNgaySinh.Properties.Appearance.Options.UseFont = true;
            this.txtNgaySinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtNgaySinh.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtNgaySinh.Size = new System.Drawing.Size(198, 26);
            this.txtNgaySinh.StyleController = this.layoutControl1;
            this.txtNgaySinh.TabIndex = 3;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            this.dxValidationProvider.SetValidationRule(this.txtNgaySinh, conditionValidationRule1);
            // 
            // chkGioiTinh
            // 
            this.chkGioiTinh.AutoSizeInLayoutControl = true;
            this.SetBoundFieldName(this.chkGioiTinh, "GioiTinh");
            this.SetBoundPropertyName(this.chkGioiTinh, "EditValue");
            this.chkGioiTinh.Location = new System.Drawing.Point(107, 42);
            this.chkGioiTinh.Name = "chkGioiTinh";
            this.chkGioiTinh.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.chkGioiTinh.Properties.Appearance.Options.UseFont = true;
            this.chkGioiTinh.Properties.Columns = 2;
            this.chkGioiTinh.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((sbyte)(1)), "Nam"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((sbyte)(0)), "Nữ")});
            this.chkGioiTinh.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkGioiTinh.Size = new System.Drawing.Size(167, 29);
            this.chkGioiTinh.StyleController = this.layoutControl1;
            this.chkGioiTinh.TabIndex = 2;
            // 
            // txtTen
            // 
            this.SetBoundFieldName(this.txtTen, "Ten");
            this.SetBoundPropertyName(this.txtTen, "EditValue");
            this.txtTen.Location = new System.Drawing.Point(373, 12);
            this.txtTen.Name = "txtTen";
            this.txtTen.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtTen.Properties.Appearance.Options.UseFont = true;
            this.txtTen.Size = new System.Drawing.Size(198, 26);
            this.txtTen.StyleController = this.layoutControl1;
            this.txtTen.TabIndex = 1;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            this.dxValidationProvider.SetValidationRule(this.txtTen, conditionValidationRule2);
            // 
            // txtHoDem
            // 
            this.SetBoundFieldName(this.txtHoDem, "HoDem");
            this.SetBoundPropertyName(this.txtHoDem, "EditValue");
            this.txtHoDem.Location = new System.Drawing.Point(107, 12);
            this.txtHoDem.Name = "txtHoDem";
            this.txtHoDem.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtHoDem.Properties.Appearance.Options.UseFont = true;
            this.txtHoDem.Size = new System.Drawing.Size(167, 26);
            this.txtHoDem.StyleController = this.layoutControl1;
            this.txtHoDem.TabIndex = 0;
            conditionValidationRule3.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule3.ErrorText = "This value is not valid";
            this.dxValidationProvider.SetValidationRule(this.txtHoDem, conditionValidationRule3);
            // 
            // cmbTinh
            // 
            this.SetBoundFieldName(this.cmbTinh, "TinhThanhPhoId");
            this.SetBoundPropertyName(this.cmbTinh, "EditValue");
            this.cmbTinh.Location = new System.Drawing.Point(107, 135);
            this.cmbTinh.Name = "cmbTinh";
            this.cmbTinh.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.cmbTinh.Properties.Appearance.Options.UseFont = true;
            this.cmbTinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTinh.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Tên")});
            this.cmbTinh.Properties.DataSource = this.thanhPhoRowBindingSource;
            this.cmbTinh.Properties.DisplayMember = "Name";
            this.cmbTinh.Properties.NullText = "";
            this.cmbTinh.Properties.PopupSizeable = false;
            this.cmbTinh.Properties.ShowHeader = false;
            this.cmbTinh.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbTinh.Properties.ValueMember = "ThanhPhoId";
            this.cmbTinh.Size = new System.Drawing.Size(167, 26);
            this.cmbTinh.StyleController = this.layoutControl1;
            this.cmbTinh.TabIndex = 8;
            this.cmbTinh.EditValueChanged += new System.EventHandler(this.cmbTinh_EditValueChanged);
            // 
            // thanhPhoRowBindingSource
            // 
            this.thanhPhoRowBindingSource.DataSource = typeof(thanhpho);
            // 
            // cmbQuan
            // 
            this.SetBoundFieldName(this.cmbQuan, "QuanHuyenId");
            this.SetBoundPropertyName(this.cmbQuan, "EditValue");
            this.cmbQuan.Location = new System.Drawing.Point(373, 126);
            this.cmbQuan.Name = "cmbQuan";
            this.cmbQuan.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.cmbQuan.Properties.Appearance.Options.UseFont = true;
            this.cmbQuan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQuan.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Tên")});
            this.cmbQuan.Properties.DataSource = this.quanHuyenRowBindingSource;
            this.cmbQuan.Properties.DisplayMember = "Name";
            this.cmbQuan.Properties.NullText = "";
            this.cmbQuan.Properties.ShowHeader = false;
            this.cmbQuan.Properties.ValueMember = "QuanHuyenId";
            this.cmbQuan.Size = new System.Drawing.Size(198, 26);
            this.cmbQuan.StyleController = this.layoutControl1;
            this.cmbQuan.TabIndex = 9;
            this.cmbQuan.EditValueChanged += new System.EventHandler(this.cmbQuan_EditValueChanged);
            // 
            // quanHuyenRowBindingSource
            // 
            this.quanHuyenRowBindingSource.DataSource = typeof(quanhuyen);
            // 
            // cmbPhuong
            // 
            this.SetBoundFieldName(this.cmbPhuong, "PhuongXaId");
            this.SetBoundPropertyName(this.cmbPhuong, "EditValue");
            this.cmbPhuong.Location = new System.Drawing.Point(107, 165);
            this.cmbPhuong.Name = "cmbPhuong";
            this.cmbPhuong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.cmbPhuong.Properties.Appearance.Options.UseFont = true;
            this.cmbPhuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPhuong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Tên")});
            this.cmbPhuong.Properties.DataSource = this.phuongXaRowBindingSource;
            this.cmbPhuong.Properties.DisplayMember = "Name";
            this.cmbPhuong.Properties.NullText = "";
            this.cmbPhuong.Properties.ShowHeader = false;
            this.cmbPhuong.Properties.ValueMember = "PhuongXaId";
            this.cmbPhuong.Size = new System.Drawing.Size(167, 26);
            this.cmbPhuong.StyleController = this.layoutControl1;
            this.cmbPhuong.TabIndex = 10;
            // 
            // phuongXaRowBindingSource
            // 
            this.phuongXaRowBindingSource.DataSource = typeof(phuongxa);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem9,
            this.layoutControlItem15,
            this.layoutControlItem11,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem12,
            this.layoutControlItem10,
            this.layoutControlItem8,
            this.layoutControlItem5});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(583, 227);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.BackColor = System.Drawing.Color.Transparent;
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseBackColor = true;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem1.Control = this.txtHoDem;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(266, 30);
            this.layoutControlItem1.Text = "Họ đệm";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(92, 19);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem2.Control = this.txtTen;
            this.layoutControlItem2.Location = new System.Drawing.Point(266, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(297, 30);
            this.layoutControlItem2.Text = "Tên";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(92, 19);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem3.Control = this.chkGioiTinh;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(266, 33);
            this.layoutControlItem3.Text = "Giới tính";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(92, 19);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlItem4.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem4.Control = this.txtNgaySinh;
            this.layoutControlItem4.Location = new System.Drawing.Point(266, 30);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(297, 30);
            this.layoutControlItem4.Text = "Ngày sinh";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(92, 19);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlItem9.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem9.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem9.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem9.Control = this.txtCha;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 63);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(266, 30);
            this.layoutControlItem9.Text = "Họ tên cha";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(92, 19);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.checkBox1;
            this.layoutControlItem15.CustomizationFormText = "Thôi học:";
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 183);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(563, 24);
            this.layoutControlItem15.Text = "Thôi học:";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlItem11.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem11.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem11.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem11.Control = this.txtSoNha;
            this.layoutControlItem11.Location = new System.Drawing.Point(266, 153);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(297, 30);
            this.layoutControlItem11.Text = "Số nhà/đường:";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(92, 19);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlItem13.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem13.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem13.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem13.Control = this.cmbQuan;
            this.layoutControlItem13.Location = new System.Drawing.Point(266, 114);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(297, 39);
            this.layoutControlItem13.Text = "Quận/huyện:";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(92, 19);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlItem14.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem14.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem14.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem14.Control = this.cmbTinh;
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 123);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(266, 30);
            this.layoutControlItem14.Text = "Tỉnh/thành phố:";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(92, 19);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlItem12.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem12.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem12.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem12.Control = this.cmbPhuong;
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 153);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(266, 30);
            this.layoutControlItem12.Text = "Phường/xã:";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(92, 19);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlItem10.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem10.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem10.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem10.Control = this.txtMe;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 93);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(266, 30);
            this.layoutControlItem10.Text = "Họ tên mẹ";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(92, 19);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlItem8.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem8.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem8.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem8.Control = this.txtDienThoai;
            this.layoutControlItem8.Location = new System.Drawing.Point(266, 60);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(297, 30);
            this.layoutControlItem8.Text = "Điện thoại";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(92, 19);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtDienThoaiMe;
            this.layoutControlItem5.Location = new System.Drawing.Point(266, 90);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(297, 24);
            this.layoutControlItem5.Text = "Điện thoại";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(92, 13);
            // 
            // dxValidationProvider
            // 
            this.dxValidationProvider.ValidationMode = DevExpress.XtraEditors.DXErrorProvider.ValidationMode.Auto;
            // 
            // UCEditFormThongTinHocSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCEditFormThongTinHocSinh";
            this.Size = new System.Drawing.Size(600, 165);
            this.Load += new System.EventHandler(this.UCEditFormThongTinHocSinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDienThoaiMe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoNha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienThoai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNgaySinh.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNgaySinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGioiTinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoDem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thanhPhoRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quanHuyenRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPhuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phuongXaRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txtTen;
        private DevExpress.XtraEditors.TextEdit txtHoDem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.RadioGroup chkGioiTinh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.DateEdit txtNgaySinh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit txtDienThoai;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.TextEdit txtMe;
        private DevExpress.XtraEditors.TextEdit txtCha;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.TextEdit txtSoNha;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
        private DevExpress.XtraEditors.LookUpEdit cmbTinh;
        private DevExpress.XtraEditors.LookUpEdit cmbQuan;
        private DevExpress.XtraEditors.LookUpEdit cmbPhuong;
        private System.Windows.Forms.BindingSource thanhPhoRowBindingSource;
        private System.Windows.Forms.BindingSource quanHuyenRowBindingSource;
        private System.Windows.Forms.BindingSource phuongXaRowBindingSource;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraEditors.TextEdit txtDienThoaiMe;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}
