using QLMamNon.Dao;

namespace QLMamNon.UserControls
{
    partial class UCEditFormTaiSan
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
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.cmbPhanLoaiChi = new DevExpress.XtraEditors.LookUpEdit();
            this.phanLoaiChiRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dateNgayChungTu = new DevExpress.XtraEditors.DateEdit();
            this.txtSoChungTu = new DevExpress.XtraEditors.TextEdit();
            this.dateNgayNhap = new DevExpress.XtraEditors.DateEdit();
            this.txtThanhTien = new DevExpress.XtraEditors.TextEdit();
            this.txtDonGia = new DevExpress.XtraEditors.CalcEdit();
            this.txtSoLuong = new DevExpress.XtraEditors.SpinEdit();
            this.cmbDonViTinh = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtTen = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPhanLoaiChi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phanLoaiChiRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayChungTu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayChungTu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoChungTu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThanhTien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDonGia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoLuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDonViTinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.SetBoundPropertyName(this.lcMain, "");
            this.lcMain.Controls.Add(this.cmbPhanLoaiChi);
            this.lcMain.Controls.Add(this.dateNgayChungTu);
            this.lcMain.Controls.Add(this.txtSoChungTu);
            this.lcMain.Controls.Add(this.dateNgayNhap);
            this.lcMain.Controls.Add(this.txtThanhTien);
            this.lcMain.Controls.Add(this.txtDonGia);
            this.lcMain.Controls.Add(this.txtSoLuong);
            this.lcMain.Controls.Add(this.cmbDonViTinh);
            this.lcMain.Controls.Add(this.txtTen);
            this.lcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcMain.Location = new System.Drawing.Point(0, 0);
            this.lcMain.Name = "lcMain";
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
            this.lcMain.Root = this.Root;
            this.lcMain.Size = new System.Drawing.Size(700, 190);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "layoutControl1";
            // 
            // cmbPhanLoaiChi
            // 
            this.SetBoundFieldName(this.cmbPhanLoaiChi, "PhanLoaiChiId");
            this.SetBoundPropertyName(this.cmbPhanLoaiChi, "EditValue");
            this.cmbPhanLoaiChi.Location = new System.Drawing.Point(495, 111);
            this.cmbPhanLoaiChi.Name = "cmbPhanLoaiChi";
            this.cmbPhanLoaiChi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPhanLoaiChi.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaPhanLoai", "Ma Phan Loai", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DienGiai", "Dien Giai", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cmbPhanLoaiChi.Properties.DataSource = this.phanLoaiChiRowBindingSource;
            this.cmbPhanLoaiChi.Properties.DisplayMember = "DienGiai";
            this.cmbPhanLoaiChi.Properties.NullText = "";
            this.cmbPhanLoaiChi.Properties.ShowHeader = false;
            this.cmbPhanLoaiChi.Properties.ValueMember = "PhanLoaiChiId";
            this.cmbPhanLoaiChi.Size = new System.Drawing.Size(174, 20);
            this.cmbPhanLoaiChi.StyleController = this.lcMain;
            this.cmbPhanLoaiChi.TabIndex = 9;
            this.cmbPhanLoaiChi.EditValueChanged += new System.EventHandler(this.cmbPhanLoaiChi_EditValueChanged);
            // 
            // phanLoaiChiRowBindingSource
            // 
            this.phanLoaiChiRowBindingSource.DataSource = typeof(phanloaichi);
            // 
            // dateNgayChungTu
            // 
            this.SetBoundFieldName(this.dateNgayChungTu, "NgayChungTu");
            this.SetBoundPropertyName(this.dateNgayChungTu, "EditValue");
            this.dateNgayChungTu.EditValue = null;
            this.dateNgayChungTu.Location = new System.Drawing.Point(529, 178);
            this.dateNgayChungTu.Name = "dateNgayChungTu";
            this.dateNgayChungTu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayChungTu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayChungTu.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateNgayChungTu.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayChungTu.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateNgayChungTu.Size = new System.Drawing.Size(140, 20);
            this.dateNgayChungTu.StyleController = this.lcMain;
            this.dateNgayChungTu.TabIndex = 11;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            this.dxValidationProvider.SetValidationRule(this.dateNgayChungTu, conditionValidationRule1);
            // 
            // txtSoChungTu
            // 
            this.SetBoundFieldName(this.txtSoChungTu, "SoChungTu");
            this.SetBoundPropertyName(this.txtSoChungTu, "EditValue");
            this.txtSoChungTu.Location = new System.Drawing.Point(165, 178);
            this.txtSoChungTu.Name = "txtSoChungTu";
            this.txtSoChungTu.Size = new System.Drawing.Size(209, 20);
            this.txtSoChungTu.StyleController = this.lcMain;
            this.txtSoChungTu.TabIndex = 10;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            this.dxValidationProvider.SetValidationRule(this.txtSoChungTu, conditionValidationRule2);
            // 
            // dateNgayNhap
            // 
            this.SetBoundFieldName(this.dateNgayNhap, "NgayNhap");
            this.SetBoundPropertyName(this.dateNgayNhap, "EditValue");
            this.dateNgayNhap.EditValue = null;
            this.dateNgayNhap.Location = new System.Drawing.Point(165, 111);
            this.dateNgayNhap.Name = "dateNgayNhap";
            this.dateNgayNhap.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayNhap.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayNhap.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateNgayNhap.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayNhap.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateNgayNhap.Size = new System.Drawing.Size(175, 20);
            this.dateNgayNhap.StyleController = this.lcMain;
            this.dateNgayNhap.TabIndex = 8;
            // 
            // txtThanhTien
            // 
            this.SetBoundFieldName(this.txtThanhTien, "ThanhTien");
            this.SetBoundPropertyName(this.txtThanhTien, "EditValue");
            this.txtThanhTien.Location = new System.Drawing.Point(495, 87);
            this.txtThanhTien.Name = "txtThanhTien";
            this.txtThanhTien.Properties.DisplayFormat.FormatString = "n0";
            this.txtThanhTien.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtThanhTien.Properties.Mask.EditMask = "n0";
            this.txtThanhTien.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtThanhTien.Properties.ReadOnly = true;
            this.txtThanhTien.Size = new System.Drawing.Size(174, 20);
            this.txtThanhTien.StyleController = this.lcMain;
            this.txtThanhTien.TabIndex = 7;
            // 
            // txtDonGia
            // 
            this.SetBoundFieldName(this.txtDonGia, "DonGia");
            this.SetBoundPropertyName(this.txtDonGia, "EditValue");
            this.txtDonGia.Location = new System.Drawing.Point(165, 87);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDonGia.Properties.DisplayFormat.FormatString = "n0";
            this.txtDonGia.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtDonGia.Properties.Mask.EditMask = "n0";
            this.txtDonGia.Size = new System.Drawing.Size(175, 20);
            this.txtDonGia.StyleController = this.lcMain;
            this.txtDonGia.TabIndex = 6;
            this.txtDonGia.Leave += new System.EventHandler(this.txtDonGia_Leave);
            // 
            // txtSoLuong
            // 
            this.SetBoundFieldName(this.txtSoLuong, "SoLuong");
            this.SetBoundPropertyName(this.txtSoLuong, "EditValue");
            this.txtSoLuong.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSoLuong.Location = new System.Drawing.Point(495, 63);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSoLuong.Properties.Mask.EditMask = "n2";
            this.txtSoLuong.Size = new System.Drawing.Size(174, 20);
            this.txtSoLuong.StyleController = this.lcMain;
            this.txtSoLuong.TabIndex = 5;
            this.txtSoLuong.Leave += new System.EventHandler(this.txtSoLuong_Leave);
            // 
            // cmbDonViTinh
            // 
            this.SetBoundFieldName(this.cmbDonViTinh, "DonViTinh");
            this.SetBoundPropertyName(this.cmbDonViTinh, "EditValue");
            this.cmbDonViTinh.Location = new System.Drawing.Point(165, 63);
            this.cmbDonViTinh.Name = "cmbDonViTinh";
            this.cmbDonViTinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDonViTinh.Properties.Items.AddRange(new object[] {
            "Cái",
            "Con",
            "Kg",
            "Mét"});
            this.cmbDonViTinh.Size = new System.Drawing.Size(175, 20);
            this.cmbDonViTinh.StyleController = this.lcMain;
            this.cmbDonViTinh.TabIndex = 4;
            // 
            // txtTen
            // 
            this.SetBoundFieldName(this.txtTen, "Ten");
            this.SetBoundPropertyName(this.txtTen, "EditValue");
            this.txtTen.Location = new System.Drawing.Point(165, 33);
            this.txtTen.Name = "txtTen";
            this.txtTen.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtTen.Properties.Appearance.Options.UseFont = true;
            this.txtTen.Size = new System.Drawing.Size(504, 26);
            this.txtTen.StyleController = this.lcMain;
            this.txtTen.TabIndex = 1;
            conditionValidationRule3.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule3.ErrorText = "This value is not valid";
            this.dxValidationProvider.SetValidationRule(this.txtTen, conditionValidationRule3);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlGroup2});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(683, 212);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem5,
            this.layoutControlItem9});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(683, 145);
            this.layoutControlGroup1.Text = "Thông tin Tài sản";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.BackColor = System.Drawing.Color.Transparent;
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseBackColor = true;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem1.Control = this.txtTen;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(659, 30);
            this.layoutControlItem1.Text = "Tên, nhãn hiệu quy cách:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(148, 19);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbDonViTinh;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(330, 24);
            this.layoutControlItem2.Text = "Đơn vị tính:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(148, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtSoLuong;
            this.layoutControlItem3.Location = new System.Drawing.Point(330, 30);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(329, 24);
            this.layoutControlItem3.Text = "Số lượng:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(148, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtDonGia;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 54);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(330, 24);
            this.layoutControlItem4.Text = "Đơn giá:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(148, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.dateNgayNhap;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(330, 24);
            this.layoutControlItem6.Text = "Ngày nhập:";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(148, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtThanhTien;
            this.layoutControlItem5.Location = new System.Drawing.Point(330, 54);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(329, 24);
            this.layoutControlItem5.Text = "Thành tiền:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(148, 13);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.cmbPhanLoaiChi;
            this.layoutControlItem9.Location = new System.Drawing.Point(330, 78);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(329, 24);
            this.layoutControlItem9.Text = "Phân loại Tài sản:";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(148, 13);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 145);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(683, 67);
            this.layoutControlGroup2.Text = "Thông tin chứng từ";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtSoChungTu;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(364, 24);
            this.layoutControlItem7.Text = "Số chứng từ:";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(148, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.dateNgayChungTu;
            this.layoutControlItem8.Location = new System.Drawing.Point(364, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(295, 24);
            this.layoutControlItem8.Text = "Ngày chứng từ:";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(148, 13);
            // 
            // dxValidationProvider
            // 
            this.dxValidationProvider.ValidationMode = DevExpress.XtraEditors.DXErrorProvider.ValidationMode.Auto;
            // 
            // UCEditFormTaiSan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lcMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCEditFormTaiSan";
            this.Size = new System.Drawing.Size(700, 190);
            this.Load += new System.EventHandler(this.UCEditFormTaiSan_Load);
            this.Enter += new System.EventHandler(this.UCEditFormTaiSan_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbPhanLoaiChi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phanLoaiChiRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayChungTu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayChungTu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoChungTu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThanhTien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDonGia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoLuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDonViTinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txtTen;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDonViTinh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SpinEdit txtSoLuong;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.CalcEdit txtDonGia;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit txtThanhTien;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.DateEdit dateNgayNhap;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.DateEdit dateNgayChungTu;
        private DevExpress.XtraEditors.TextEdit txtSoChungTu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.LookUpEdit cmbPhanLoaiChi;
        private System.Windows.Forms.BindingSource phanLoaiChiRowBindingSource;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
    }
}
