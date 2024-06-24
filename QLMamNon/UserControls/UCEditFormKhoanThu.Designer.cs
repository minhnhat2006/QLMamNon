namespace QLMamNon.UserControls
{
    partial class UCEditFormKhoanThu
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkIsBatBuoc = new DevExpress.XtraEditors.CheckEdit();
            this.txtTenhoanThu = new DevExpress.XtraEditors.TextEdit();
            this.txtSoTien = new DevExpress.XtraEditors.CalcEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsBatBuoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenhoanThu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.SetBoundPropertyName(this.layoutControl1, "");
            this.layoutControl1.Controls.Add(this.chkIsBatBuoc);
            this.layoutControl1.Controls.Add(this.txtTenhoanThu);
            this.layoutControl1.Controls.Add(this.txtSoTien);
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
            this.layoutControl1.Size = new System.Drawing.Size(400, 80);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkIsBatBuoc
            // 
            this.SetBoundFieldName(this.chkIsBatBuoc, "IsBatBuoc");
            this.SetBoundPropertyName(this.chkIsBatBuoc, "EditValue");
            this.chkIsBatBuoc.Location = new System.Drawing.Point(113, 72);
            this.chkIsBatBuoc.Name = "chkIsBatBuoc";
            this.chkIsBatBuoc.Properties.Caption = "";
            this.chkIsBatBuoc.Size = new System.Drawing.Size(259, 19);
            this.chkIsBatBuoc.StyleController = this.layoutControl1;
            this.chkIsBatBuoc.TabIndex = 5;
            // 
            // txtTenhoanThu
            // 
            this.SetBoundFieldName(this.txtTenhoanThu, "Ten");
            this.SetBoundPropertyName(this.txtTenhoanThu, "EditValue");
            this.txtTenhoanThu.Location = new System.Drawing.Point(113, 12);
            this.txtTenhoanThu.Name = "txtTenhoanThu";
            this.txtTenhoanThu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtTenhoanThu.Properties.Appearance.Options.UseFont = true;
            this.txtTenhoanThu.Size = new System.Drawing.Size(259, 26);
            this.txtTenhoanThu.StyleController = this.layoutControl1;
            this.txtTenhoanThu.TabIndex = 0;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            this.dxValidationProvider.SetValidationRule(this.txtTenhoanThu, conditionValidationRule1);
            // 
            // txtSoTien
            // 
            this.SetBoundPropertyName(this.txtSoTien, "");
            this.txtSoTien.Location = new System.Drawing.Point(113, 42);
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
            this.txtSoTien.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem5,
            this.layoutControlItem2});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(384, 103);
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
            this.layoutControlItem1.Control = this.txtTenhoanThu;
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
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(364, 30);
            this.layoutControlItem5.Text = "Số tiền:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(98, 19);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkIsBatBuoc;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 60);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(364, 23);
            this.layoutControlItem2.Text = "Khoản thu bắt buộc:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(98, 13);
            // 
            // dxValidationProvider
            // 
            this.dxValidationProvider.ValidationMode = DevExpress.XtraEditors.DXErrorProvider.ValidationMode.Auto;
            // 
            // UCEditFormKhoanThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCEditFormKhoanThu";
            this.Size = new System.Drawing.Size(400, 80);
            this.Load += new System.EventHandler(this.UCEditFormKhoanThu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkIsBatBuoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenhoanThu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txtTenhoanThu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
        private DevExpress.XtraEditors.CheckEdit chkIsBatBuoc;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CalcEdit txtSoTien;
    }
}
