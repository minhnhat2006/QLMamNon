using QLMamNon.Dao;

namespace QLMamNon.UserControls
{
    partial class UCEditFormXepLop
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
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.dateNgayVaoLop = new DevExpress.XtraEditors.DateEdit();
            this.txtLop = new DevExpress.XtraEditors.TextEdit();
            this.dateNgaySinh = new DevExpress.XtraEditors.DateEdit();
            this.txtHoTen = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.phanLoaiChiRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayVaoLop.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayVaoLop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgaySinh.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgaySinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoTen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phanLoaiChiRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.SetBoundPropertyName(this.lcMain, "");
            this.lcMain.Controls.Add(this.dateNgayVaoLop);
            this.lcMain.Controls.Add(this.txtLop);
            this.lcMain.Controls.Add(this.dateNgaySinh);
            this.lcMain.Controls.Add(this.txtHoTen);
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
            this.lcMain.Size = new System.Drawing.Size(500, 120);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "layoutControl1";
            // 
            // dateNgayVaoLop
            // 
            this.SetBoundFieldName(this.dateNgayVaoLop, "NgayVaoLop");
            this.SetBoundPropertyName(this.dateNgayVaoLop, "EditValue");
            this.dateNgayVaoLop.EditValue = null;
            this.dateNgayVaoLop.Location = new System.Drawing.Point(310, 100);
            this.dateNgayVaoLop.Name = "dateNgayVaoLop";
            this.dateNgayVaoLop.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayVaoLop.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayVaoLop.Size = new System.Drawing.Size(159, 20);
            this.dateNgayVaoLop.StyleController = this.lcMain;
            this.dateNgayVaoLop.TabIndex = 13;
            // 
            // txtLop
            // 
            this.SetBoundFieldName(this.txtLop, "LopDangHoc");
            this.SetBoundPropertyName(this.txtLop, "EditValue");
            this.txtLop.Location = new System.Drawing.Point(80, 100);
            this.txtLop.Name = "txtLop";
            this.txtLop.Properties.ReadOnly = true;
            this.txtLop.Size = new System.Drawing.Size(160, 20);
            this.txtLop.StyleController = this.lcMain;
            this.txtLop.TabIndex = 12;
            // 
            // dateNgaySinh
            // 
            this.SetBoundFieldName(this.dateNgaySinh, "NgaySinh");
            this.SetBoundPropertyName(this.dateNgaySinh, "EditValue");
            this.dateNgaySinh.EditValue = null;
            this.dateNgaySinh.Location = new System.Drawing.Point(327, 33);
            this.dateNgaySinh.Name = "dateNgaySinh";
            this.dateNgaySinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgaySinh.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgaySinh.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateNgaySinh.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgaySinh.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateNgaySinh.Properties.ReadOnly = true;
            this.dateNgaySinh.Size = new System.Drawing.Size(142, 20);
            this.dateNgaySinh.StyleController = this.lcMain;
            this.dateNgaySinh.TabIndex = 11;
            // 
            // txtHoTen
            // 
            this.SetBoundFieldName(this.txtHoTen, "HoTen");
            this.txtHoTen.Location = new System.Drawing.Point(80, 33);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Properties.ReadOnly = true;
            this.txtHoTen.Size = new System.Drawing.Size(177, 20);
            this.txtHoTen.StyleController = this.lcMain;
            this.txtHoTen.TabIndex = 10;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(483, 134);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(483, 67);
            this.layoutControlGroup2.Text = "Thông tin học sinh";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtHoTen;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(247, 24);
            this.layoutControlItem7.Text = "Họ tên";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(63, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.dateNgaySinh;
            this.layoutControlItem8.Location = new System.Drawing.Point(247, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(212, 24);
            this.layoutControlItem8.Text = "Ngày sinh";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(63, 13);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10,
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 67);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(483, 67);
            this.layoutControlGroup3.Text = "Thông tin xếp lớp";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.txtLop;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(230, 24);
            this.layoutControlItem10.Text = "Lớp:";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(63, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateNgayVaoLop;
            this.layoutControlItem1.Location = new System.Drawing.Point(230, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(229, 24);
            this.layoutControlItem1.Text = "Ngày vào lớp";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(63, 13);
            // 
            // phanLoaiChiRowBindingSource
            // 
            this.phanLoaiChiRowBindingSource.DataSource = typeof(phanloaichi);
            // 
            // dxValidationProvider
            // 
            this.dxValidationProvider.ValidationMode = DevExpress.XtraEditors.DXErrorProvider.ValidationMode.Auto;
            // 
            // UCEditFormXepLop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lcMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCEditFormXepLop";
            this.Size = new System.Drawing.Size(500, 120);
            this.Load += new System.EventHandler(this.UCEditFormXepLop_Load);
            this.Enter += new System.EventHandler(this.UCEditFormXepLop_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayVaoLop.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayVaoLop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgaySinh.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgaySinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoTen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phanLoaiChiRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
        private System.Windows.Forms.BindingSource phanLoaiChiRowBindingSource;
        private DevExpress.XtraEditors.TextEdit txtLop;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraEditors.DateEdit dateNgaySinh;
        private DevExpress.XtraEditors.TextEdit txtHoTen;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.DateEdit dateNgayVaoLop;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
