using QLMamNon.Dao;

namespace QLMamNon.Forms.ThuChi
{
    partial class FrmBaoCaoTinhHinhThuChi
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
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaoCaoTinhHinhThuChi));
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.txtTon = new DevExpress.XtraEditors.CalcEdit();
            this.chkTon = new DevExpress.XtraEditors.CheckEdit();
            this.gcThu = new DevExpress.XtraGrid.GridControl();
            this.phanLoaiThuRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvThu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
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
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnXemBaoCao = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phanLoaiThuRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.lcMain.Appearance.Control.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lcMain.Appearance.Control.Options.UseFont = true;
            this.lcMain.Controls.Add(this.txtTon);
            this.lcMain.Controls.Add(this.chkTon);
            this.lcMain.Controls.Add(this.gcThu);
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
            this.lcMain.Size = new System.Drawing.Size(292, 433);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "layoutControl1";
            // 
            // txtTon
            // 
            this.txtTon.Enabled = false;
            this.txtTon.Location = new System.Drawing.Point(135, 88);
            this.txtTon.Name = "txtTon";
            this.txtTon.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTon.Properties.DisplayFormat.FormatString = "n0";
            this.txtTon.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTon.Properties.Mask.EditMask = "n0";
            this.txtTon.Size = new System.Drawing.Size(145, 26);
            this.txtTon.StyleController = this.lcMain;
            this.txtTon.TabIndex = 4;
            // 
            // chkTon
            // 
            this.chkTon.Location = new System.Drawing.Point(12, 88);
            this.chkTon.Name = "chkTon";
            this.chkTon.Properties.Caption = "Tồn tháng trước";
            this.chkTon.Size = new System.Drawing.Size(119, 23);
            this.chkTon.StyleController = this.lcMain;
            this.chkTon.TabIndex = 3;
            this.chkTon.CheckedChanged += new System.EventHandler(this.chkTon_CheckedChanged);
            // 
            // gcThu
            // 
            this.gcThu.DataSource = this.phanLoaiThuRowBindingSource;
            this.gcThu.Location = new System.Drawing.Point(12, 118);
            this.gcThu.MainView = this.gvThu;
            this.gcThu.Name = "gcThu";
            this.gcThu.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.gcThu.Size = new System.Drawing.Size(268, 114);
            this.gcThu.TabIndex = 5;
            this.gcThu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvThu});
            // 
            // phanLoaiThuRowBindingSource
            // 
            this.phanLoaiThuRowBindingSource.DataSource = typeof(phanloaithu);
            // 
            // gvThu
            // 
            this.gvThu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3});
            this.gvThu.GridControl = this.gcThu;
            this.gvThu.Name = "gvThu";
            this.gvThu.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvThu.OptionsSelection.MultiSelect = true;
            this.gvThu.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvThu.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.FieldName = "PhanLoaiThuId";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Phân loại thu";
            this.gridColumn3.FieldName = "DienGiai";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 119;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.phanLoaiChiRowBindingSource;
            this.gcMain.Location = new System.Drawing.Point(12, 236);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcMain.Size = new System.Drawing.Size(268, 185);
            this.gcMain.TabIndex = 6;
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
            this.dateDenNgay.Location = new System.Drawing.Point(208, 58);
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
            this.dateDenNgay.TabIndex = 2;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "Vui lòng chọn Ngày";
            this.dxValidationProvider.SetValidationRule(this.dateDenNgay, conditionValidationRule1);
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.EditValue = null;
            this.dateTuNgay.Location = new System.Drawing.Point(72, 58);
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
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "Vui lòng chọn Ngày";
            this.dxValidationProvider.SetValidationRule(this.dateTuNgay, conditionValidationRule2);
            // 
            // lblTaoPhieuThu
            // 
            this.lblTaoPhieuThu.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold);
            this.lblTaoPhieuThu.Location = new System.Drawing.Point(12, 12);
            this.lblTaoPhieuThu.Name = "lblTaoPhieuThu";
            this.lblTaoPhieuThu.Size = new System.Drawing.Size(268, 42);
            this.lblTaoPhieuThu.TabIndex = 4;
            this.lblTaoPhieuThu.Text = "BÁO CÁO TÌNH HÌNH THU CHI";
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
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(292, 433);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciLblTaoPhieuThu
            // 
            this.lciLblTaoPhieuThu.Control = this.lblTaoPhieuThu;
            this.lciLblTaoPhieuThu.Location = new System.Drawing.Point(0, 0);
            this.lciLblTaoPhieuThu.Name = "lciLblTaoPhieuThu";
            this.lciLblTaoPhieuThu.Size = new System.Drawing.Size(272, 46);
            this.lciLblTaoPhieuThu.TextSize = new System.Drawing.Size(0, 0);
            this.lciLblTaoPhieuThu.TextVisible = false;
            // 
            // lciNgay
            // 
            this.lciNgay.Control = this.dateTuNgay;
            this.lciNgay.Location = new System.Drawing.Point(0, 46);
            this.lciNgay.Name = "lciNgay";
            this.lciNgay.Size = new System.Drawing.Size(136, 30);
            this.lciNgay.Text = "Từ ngày:";
            this.lciNgay.TextSize = new System.Drawing.Size(57, 19);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateDenNgay;
            this.layoutControlItem1.Location = new System.Drawing.Point(136, 46);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(136, 30);
            this.layoutControlItem1.Text = "Đến ngày";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(57, 19);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcMain;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 224);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(272, 189);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcThu;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 106);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(272, 118);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 76);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(272, 30);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtTon;
            this.layoutControlItem5.Location = new System.Drawing.Point(123, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(149, 30);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkTon;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(123, 27);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(123, 30);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
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
            this.btnClose.Location = new System.Drawing.Point(177, 439);
            this.btnClose.MaximumSize = new System.Drawing.Size(250, 25);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(71, 25);
            this.btnClose.StyleController = this.lcMain;
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnXemBaoCao
            // 
            this.btnXemBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXemBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("btnXemBaoCao.Image")));
            this.btnXemBaoCao.Location = new System.Drawing.Point(45, 439);
            this.btnXemBaoCao.MaximumSize = new System.Drawing.Size(250, 25);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.Size = new System.Drawing.Size(115, 25);
            this.btnXemBaoCao.StyleController = this.lcMain;
            this.btnXemBaoCao.TabIndex = 7;
            this.btnXemBaoCao.Text = "Xem báo cáo";
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);
            // 
            // FrmBaoCaoTinhHinhThuChi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 471);
            this.Controls.Add(this.btnXemBaoCao);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBaoCaoTinhHinhThuChi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo tình hình thu chi";
            this.Load += new System.EventHandler(this.FrmBaoCaoTinhHinhThuChi_Load);
            this.Shown += new System.EventHandler(this.FrmBaoCaoTinhHinhThuChi_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phanLoaiThuRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn colPhanLoaiChiId;
        private DevExpress.XtraGrid.Columns.GridColumn colMaPhanLoai;
        private DevExpress.XtraGrid.Columns.GridColumn colDienGiai;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.BindingSource phanLoaiChiRowBindingSource;
        private DevExpress.XtraGrid.GridControl gcThu;
        private DevExpress.XtraGrid.Views.Grid.GridView gvThu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.BindingSource phanLoaiThuRowBindingSource;
        private DevExpress.XtraEditors.CheckEdit chkTon;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.CalcEdit txtTon;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}