using QLMamNon.Dao;

namespace QLMamNon.UserControls
{
    partial class UCEditFormKhoanThuHangNam
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmbKhoi = new DevExpress.XtraEditors.LookUpEdit();
            this.khoiRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.chkIsBatBuoc = new DevExpress.XtraEditors.CheckEdit();
            this.txtSoTien = new DevExpress.XtraEditors.CalcEdit();
            this.txtTenKhoanThu = new DevExpress.XtraEditors.LookUpEdit();
            this.khoanThuRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbNam = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKhoi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.khoiRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsBatBuoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenKhoanThu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.khoanThuRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNam.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.SetBoundPropertyName(this.layoutControl1, "");
            this.layoutControl1.Controls.Add(this.cmbKhoi);
            this.layoutControl1.Controls.Add(this.chkIsBatBuoc);
            this.layoutControl1.Controls.Add(this.txtSoTien);
            this.layoutControl1.Controls.Add(this.txtTenKhoanThu);
            this.layoutControl1.Controls.Add(this.cmbNam);
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
            this.layoutControl1.Size = new System.Drawing.Size(400, 120);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmbKhoi
            // 
            this.SetBoundFieldName(this.cmbKhoi, "KhoiId");
            this.SetBoundPropertyName(this.cmbKhoi, "EditValue");
            this.cmbKhoi.Location = new System.Drawing.Point(113, 42);
            this.cmbKhoi.Name = "cmbKhoi";
            this.cmbKhoi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKhoi.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Khối", 80, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cmbKhoi.Properties.DataSource = this.khoiRowBindingSource;
            this.cmbKhoi.Properties.DisplayMember = "Name";
            this.cmbKhoi.Properties.NullText = "";
            this.cmbKhoi.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            this.cmbKhoi.Properties.ShowHeader = false;
            this.cmbKhoi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbKhoi.Properties.ValueMember = "KhoiId";
            this.cmbKhoi.Size = new System.Drawing.Size(259, 20);
            this.cmbKhoi.StyleController = this.layoutControl1;
            this.cmbKhoi.TabIndex = 1;
            // 
            // khoiRowBindingSource
            // 
            this.khoiRowBindingSource.DataSource = typeof(khoi);
            // 
            // chkIsBatBuoc
            // 
            this.SetBoundFieldName(this.chkIsBatBuoc, "IsBatBuoc");
            this.SetBoundPropertyName(this.chkIsBatBuoc, "EditValue");
            this.chkIsBatBuoc.Location = new System.Drawing.Point(113, 120);
            this.chkIsBatBuoc.Name = "chkIsBatBuoc";
            this.chkIsBatBuoc.Properties.Caption = "";
            this.chkIsBatBuoc.Size = new System.Drawing.Size(259, 19);
            this.chkIsBatBuoc.StyleController = this.layoutControl1;
            this.chkIsBatBuoc.TabIndex = 4;
            // 
            // txtSoTien
            // 
            this.SetBoundFieldName(this.txtSoTien, "SoTien");
            this.SetBoundPropertyName(this.txtSoTien, "EditValue");
            this.txtSoTien.Location = new System.Drawing.Point(113, 90);
            this.txtSoTien.Name = "txtSoTien";
            this.txtSoTien.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtSoTien.Properties.Appearance.Options.UseFont = true;
            this.txtSoTien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSoTien.Properties.DisplayFormat.FormatString = "c0";
            this.txtSoTien.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtSoTien.Properties.EditFormat.FormatString = "c0";
            this.txtSoTien.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtSoTien.Properties.Mask.EditMask = "c0";
            this.txtSoTien.Size = new System.Drawing.Size(259, 26);
            this.txtSoTien.StyleController = this.layoutControl1;
            this.txtSoTien.TabIndex = 3;
            // 
            // txtTenKhoanThu
            // 
            this.SetBoundFieldName(this.txtTenKhoanThu, "KhoanThuId");
            this.SetBoundPropertyName(this.txtTenKhoanThu, "EditValue");
            this.txtTenKhoanThu.Location = new System.Drawing.Point(113, 12);
            this.txtTenKhoanThu.Name = "txtTenKhoanThu";
            this.txtTenKhoanThu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtTenKhoanThu.Properties.Appearance.Options.UseFont = true;
            this.txtTenKhoanThu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTenKhoanThu.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Ten", 80, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.txtTenKhoanThu.Properties.DataSource = this.khoanThuRowBindingSource;
            this.txtTenKhoanThu.Properties.DisplayMember = "Ten";
            this.txtTenKhoanThu.Properties.NullText = "";
            this.txtTenKhoanThu.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            this.txtTenKhoanThu.Properties.ShowHeader = false;
            this.txtTenKhoanThu.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtTenKhoanThu.Properties.ValueMember = "KhoanThuId";
            this.txtTenKhoanThu.Size = new System.Drawing.Size(259, 26);
            this.txtTenKhoanThu.StyleController = this.layoutControl1;
            this.txtTenKhoanThu.TabIndex = 0;
            // 
            // khoanThuRowBindingSource
            // 
            this.khoanThuRowBindingSource.DataSource = typeof(khoanthu);
            // 
            // cmbNam
            // 
            this.SetBoundFieldName(this.cmbNam, "NgayTinh");
            this.SetBoundPropertyName(this.cmbNam, "EditValue");
            this.cmbNam.EditValue = null;
            this.cmbNam.Location = new System.Drawing.Point(113, 66);
            this.cmbNam.Name = "cmbNam";
            this.cmbNam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNam.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNam.Properties.Mask.EditMask = "";
            this.cmbNam.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.cmbNam.Size = new System.Drawing.Size(259, 20);
            this.cmbNam.StyleController = this.layoutControl1;
            this.cmbNam.TabIndex = 2;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem5,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(384, 151);
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
            this.layoutControlItem1.Control = this.txtTenKhoanThu;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(364, 30);
            this.layoutControlItem1.Text = "Tên Khoản thu:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(98, 19);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlItem5.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem5.Control = this.txtSoTien;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(364, 30);
            this.layoutControlItem5.Text = "Số tiền:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(98, 19);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkIsBatBuoc;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 108);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(364, 23);
            this.layoutControlItem2.Text = "Khoản thu bắt buộc:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(98, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cmbKhoi;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(364, 24);
            this.layoutControlItem3.Text = "Thu cho Khối:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(98, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbNam;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 54);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(364, 24);
            this.layoutControlItem4.Text = "Ngày bắt đầu:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(98, 13);
            // 
            // dxValidationProvider
            // 
            this.dxValidationProvider.ValidationMode = DevExpress.XtraEditors.DXErrorProvider.ValidationMode.Auto;
            // 
            // UCEditFormKhoanThuHangNam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCEditFormKhoanThuHangNam";
            this.Size = new System.Drawing.Size(400, 120);
            this.Load += new System.EventHandler(this.UCEditFormKhoanThuHangNam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbKhoi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.khoiRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsBatBuoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenKhoanThu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.khoanThuRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNam.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
        private DevExpress.XtraEditors.CheckEdit chkIsBatBuoc;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CalcEdit txtSoTien;
        private DevExpress.XtraEditors.LookUpEdit cmbKhoi;
        private DevExpress.XtraEditors.LookUpEdit txtTenKhoanThu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private System.Windows.Forms.BindingSource khoanThuRowBindingSource;
        private System.Windows.Forms.BindingSource khoiRowBindingSource;
        private DevExpress.XtraEditors.DateEdit cmbNam;
    }
}
