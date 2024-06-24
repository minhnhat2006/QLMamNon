using QLMamNon.Dao;

namespace QLMamNon.Forms.ThuChi
{
    partial class FrmBaoCaoHoatDongTaiChinh
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
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaoCaoHoatDongTaiChinh));
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.phanLoaiChiRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPhanLoaiChiId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaPhanLoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienGiai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.dateDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dateTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.lblTaoPhieuThu = new System.Windows.Forms.Label();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciLblTaoPhieuThu = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNgay = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnXemBaoCao = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phanLoaiChiRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciLblTaoPhieuThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.lcMain.Appearance.Control.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lcMain.Appearance.Control.Options.UseFont = true;
            this.lcMain.Controls.Add(this.gcMain);
            this.lcMain.Controls.Add(this.dateDenNgay);
            this.lcMain.Controls.Add(this.dateTuNgay);
            this.lcMain.Controls.Add(this.lblTaoPhieuThu);
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
            this.lcMain.Size = new System.Drawing.Size(292, 373);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "layoutControl1";
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.phanLoaiChiRowBindingSource;
            this.gcMain.Location = new System.Drawing.Point(12, 95);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcMain.Size = new System.Drawing.Size(268, 266);
            this.gcMain.TabIndex = 5;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // phanLoaiChiRowBindingSource
            // 
            this.phanLoaiChiRowBindingSource.DataSource = typeof(phanloaichi);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPhanLoaiChiId,
            this.colMaPhanLoai,
            this.colDienGiai});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colPhanLoaiChiId
            // 
            this.colPhanLoaiChiId.FieldName = "PhanLoaiChiId";
            this.colPhanLoaiChiId.Name = "colPhanLoaiChiId";
            // 
            // colMaPhanLoai
            // 
            this.colMaPhanLoai.Caption = "Mã loại chi";
            this.colMaPhanLoai.FieldName = "MaPhanLoai";
            this.colMaPhanLoai.Name = "colMaPhanLoai";
            this.colMaPhanLoai.Visible = true;
            this.colMaPhanLoai.VisibleIndex = 1;
            this.colMaPhanLoai.Width = 111;
            // 
            // colDienGiai
            // 
            this.colDienGiai.Caption = "Chi tiết loại chi";
            this.colDienGiai.FieldName = "DienGiai";
            this.colDienGiai.Name = "colDienGiai";
            this.colDienGiai.Visible = true;
            this.colDienGiai.VisibleIndex = 2;
            this.colDienGiai.Width = 119;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.EditValue = null;
            this.dateDenNgay.Location = new System.Drawing.Point(208, 65);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateDenNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateDenNgay.Size = new System.Drawing.Size(72, 26);
            this.dateDenNgay.StyleController = this.lcMain;
            this.dateDenNgay.TabIndex = 5;
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.EditValue = null;
            this.dateTuNgay.Location = new System.Drawing.Point(72, 65);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateTuNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTuNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateTuNgay.Size = new System.Drawing.Size(72, 26);
            this.dateTuNgay.StyleController = this.lcMain;
            this.dateTuNgay.TabIndex = 1;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "Vui lòng chọn Ngày";
            this.dxValidationProvider.SetValidationRule(this.dateTuNgay, conditionValidationRule1);
            // 
            // lblTaoPhieuThu
            // 
            this.lblTaoPhieuThu.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
            this.lblTaoPhieuThu.Location = new System.Drawing.Point(12, 12);
            this.lblTaoPhieuThu.Name = "lblTaoPhieuThu";
            this.lblTaoPhieuThu.Size = new System.Drawing.Size(268, 49);
            this.lblTaoPhieuThu.TabIndex = 4;
            this.lblTaoPhieuThu.Text = "BÁO CÁO HOẠT ĐỘNG TÀI CHÍNH";
            this.lblTaoPhieuThu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciLblTaoPhieuThu,
            this.lciNgay,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(292, 373);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciLblTaoPhieuThu
            // 
            this.lciLblTaoPhieuThu.Control = this.lblTaoPhieuThu;
            this.lciLblTaoPhieuThu.Location = new System.Drawing.Point(0, 0);
            this.lciLblTaoPhieuThu.Name = "lciLblTaoPhieuThu";
            this.lciLblTaoPhieuThu.Size = new System.Drawing.Size(272, 53);
            this.lciLblTaoPhieuThu.TextSize = new System.Drawing.Size(0, 0);
            this.lciLblTaoPhieuThu.TextVisible = false;
            // 
            // lciNgay
            // 
            this.lciNgay.Control = this.dateTuNgay;
            this.lciNgay.Location = new System.Drawing.Point(0, 53);
            this.lciNgay.Name = "lciNgay";
            this.lciNgay.Size = new System.Drawing.Size(136, 30);
            this.lciNgay.Text = "Từ ngày:";
            this.lciNgay.TextSize = new System.Drawing.Size(57, 19);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateDenNgay;
            this.layoutControlItem1.Location = new System.Drawing.Point(136, 53);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(136, 30);
            this.layoutControlItem1.Text = "Đến ngày";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(57, 19);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcMain;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 83);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(272, 270);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // dxValidationProvider
            // 
            this.dxValidationProvider.ValidationMode = DevExpress.XtraEditors.DXErrorProvider.ValidationMode.Auto;
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(170, 374);
            this.btnClose.MaximumSize = new System.Drawing.Size(250, 25);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 25);
            this.btnClose.StyleController = this.lcMain;
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnXemBaoCao
            // 
            this.btnXemBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("btnXemBaoCao.Image")));
            this.btnXemBaoCao.Location = new System.Drawing.Point(40, 374);
            this.btnXemBaoCao.MaximumSize = new System.Drawing.Size(250, 25);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.Size = new System.Drawing.Size(112, 25);
            this.btnXemBaoCao.StyleController = this.lcMain;
            this.btnXemBaoCao.TabIndex = 3;
            this.btnXemBaoCao.Text = "Xem báo cáo";
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);
            // 
            // FrmBaoCaoHoatDongTaiChinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 412);
            this.Controls.Add(this.btnXemBaoCao);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lcMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBaoCaoHoatDongTaiChinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo hoạt động tài chính";
            this.Load += new System.EventHandler(this.FrmBaoCaoHoatDongTaiChinh_Load);
            this.Shown += new System.EventHandler(this.FrmBaoCaoHoatDongTaiChinh_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phanLoaiChiRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciLblTaoPhieuThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.Label lblTaoPhieuThu;
        private DevExpress.XtraLayout.LayoutControlItem lciLblTaoPhieuThu;
        private DevExpress.XtraEditors.DateEdit dateTuNgay;
        private DevExpress.XtraLayout.LayoutControlItem lciNgay;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnXemBaoCao;
        private DevExpress.XtraEditors.DateEdit dateDenNgay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.BindingSource phanLoaiChiRowBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colPhanLoaiChiId;
        private DevExpress.XtraGrid.Columns.GridColumn colMaPhanLoai;
        private DevExpress.XtraGrid.Columns.GridColumn colDienGiai;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}