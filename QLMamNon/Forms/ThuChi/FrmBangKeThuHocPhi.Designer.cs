using QLMamNon.Dao;

namespace QLMamNon.Forms.ThuChi
{
    partial class FrmBangKeThuHocPhi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBangKeThuHocPhi));
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.dateDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.lblTaoPhieuThu = new System.Windows.Forms.Label();
            this.cmLop = new DevExpress.XtraEditors.LookUpEdit();
            this.lopRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciLblTaoPhieuThu = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnXemBaoCao = new DevExpress.XtraEditors.SimpleButton();
            this.dateTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmLop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciLblTaoPhieuThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.lcMain.Appearance.Control.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lcMain.Appearance.Control.Options.UseFont = true;
            this.lcMain.Controls.Add(this.dateTuNgay);
            this.lcMain.Controls.Add(this.dateDenNgay);
            this.lcMain.Controls.Add(this.lblTaoPhieuThu);
            this.lcMain.Controls.Add(this.cmLop);
            this.lcMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.lcMain.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lcMain.Location = new System.Drawing.Point(0, 0);
            this.lcMain.Name = "lcMain";
            this.lcMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(547, 189, 250, 350);
            this.lcMain.OptionsPrint.AppearanceGroupCaption.BackColor = System.Drawing.Color.LightGray;
            this.lcMain.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("Tahoma", 20F);
            this.lcMain.OptionsPrint.AppearanceGroupCaption.Options.UseBackColor = true;
            this.lcMain.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.lcMain.OptionsPrint.AppearanceGroupCaption.Options.UseTextOptions = true;
            this.lcMain.OptionsPrint.AppearanceGroupCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcMain.OptionsPrint.AppearanceGroupCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcMain.OptionsPrint.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 20F);
            this.lcMain.OptionsPrint.AppearanceItemCaption.Options.UseFont = true;
            this.lcMain.OptionsPrint.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcMain.OptionsPrint.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lcMain.OptionsPrint.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcMain.Root = this.layoutControlGroup1;
            this.lcMain.Size = new System.Drawing.Size(292, 100);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "layoutControl1";
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.EditValue = null;
            this.dateDenNgay.Location = new System.Drawing.Point(199, 36);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateDenNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateDenNgay.Size = new System.Drawing.Size(64, 26);
            this.dateDenNgay.StyleController = this.lcMain;
            this.dateDenNgay.TabIndex = 2;
            // 
            // lblTaoPhieuThu
            // 
            this.lblTaoPhieuThu.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold);
            this.lblTaoPhieuThu.Location = new System.Drawing.Point(12, 12);
            this.lblTaoPhieuThu.Name = "lblTaoPhieuThu";
            this.lblTaoPhieuThu.Size = new System.Drawing.Size(251, 20);
            this.lblTaoPhieuThu.TabIndex = 4;
            this.lblTaoPhieuThu.Text = "BẢNG KÊ THU HỌC PHÍ";
            this.lblTaoPhieuThu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmLop
            // 
            this.cmLop.Location = new System.Drawing.Point(72, 66);
            this.cmLop.Name = "cmLop";
            this.cmLop.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmLop.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmLop.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 37, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cmLop.Properties.DataSource = this.lopRowBindingSource;
            this.cmLop.Properties.DisplayMember = "Name";
            this.cmLop.Properties.NullText = "[Chọn lớp]";
            this.cmLop.Properties.ShowHeader = false;
            this.cmLop.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmLop.Properties.ValueMember = "LopId";
            this.cmLop.Size = new System.Drawing.Size(191, 26);
            this.cmLop.StyleController = this.lcMain;
            this.cmLop.TabIndex = 3;
            // 
            // lopRowBindingSource
            // 
            this.lopRowBindingSource.DataSource = typeof(lop);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciLblTaoPhieuThu,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(275, 104);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciLblTaoPhieuThu
            // 
            this.lciLblTaoPhieuThu.Control = this.lblTaoPhieuThu;
            this.lciLblTaoPhieuThu.Location = new System.Drawing.Point(0, 0);
            this.lciLblTaoPhieuThu.Name = "lciLblTaoPhieuThu";
            this.lciLblTaoPhieuThu.Size = new System.Drawing.Size(255, 24);
            this.lciLblTaoPhieuThu.TextSize = new System.Drawing.Size(0, 0);
            this.lciLblTaoPhieuThu.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateDenNgay;
            this.layoutControlItem1.Location = new System.Drawing.Point(127, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(128, 30);
            this.layoutControlItem1.Text = "Đến ngày";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(57, 19);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmLop;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 54);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(255, 30);
            this.layoutControlItem2.Text = "Lớp";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(57, 19);
            // 
            // dxValidationProvider
            // 
            this.dxValidationProvider.ValidationMode = DevExpress.XtraEditors.DXErrorProvider.ValidationMode.Auto;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(177, 99);
            this.btnClose.MaximumSize = new System.Drawing.Size(250, 25);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(71, 25);
            this.btnClose.StyleController = this.lcMain;
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnXemBaoCao
            // 
            this.btnXemBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXemBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("btnXemBaoCao.Image")));
            this.btnXemBaoCao.Location = new System.Drawing.Point(45, 99);
            this.btnXemBaoCao.MaximumSize = new System.Drawing.Size(250, 25);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.Size = new System.Drawing.Size(115, 25);
            this.btnXemBaoCao.StyleController = this.lcMain;
            this.btnXemBaoCao.TabIndex = 4;
            this.btnXemBaoCao.Text = "Xem báo cáo";
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.EditValue = null;
            this.dateTuNgay.Location = new System.Drawing.Point(72, 36);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Size = new System.Drawing.Size(63, 26);
            this.dateTuNgay.StyleController = this.lcMain;
            this.dateTuNgay.TabIndex = 1;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.dateTuNgay;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(127, 30);
            this.layoutControlItem3.Text = "Từ ngày";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(57, 19);
            // 
            // FrmBangKeThuHocPhi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 131);
            this.Controls.Add(this.btnXemBaoCao);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBangKeThuHocPhi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bảng kê thu học phí";
            this.Load += new System.EventHandler(this.FrmBaoCaoHoatDongTaiChinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmLop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciLblTaoPhieuThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.Label lblTaoPhieuThu;
        private DevExpress.XtraLayout.LayoutControlItem lciLblTaoPhieuThu;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnXemBaoCao;
        private DevExpress.XtraEditors.DateEdit dateDenNgay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LookUpEdit cmLop;
        private System.Windows.Forms.BindingSource lopRowBindingSource;
        private DevExpress.XtraEditors.DateEdit dateTuNgay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}