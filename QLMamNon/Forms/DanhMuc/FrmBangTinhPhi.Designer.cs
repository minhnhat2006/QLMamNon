using QLMamNon.Dao;

namespace QLMamNon.Forms.DanhMuc
{
    partial class FrmBangTinhPhi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBangTinhPhi));
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuyBo = new DevExpress.XtraEditors.SimpleButton();
            this.btnChinhSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnThem = new DevExpress.XtraEditors.SimpleButton();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bangTinhPhiRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colKhoanThuHangNamId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKhoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoNgayNghiMin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoNgayNghiMax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bangTinhPhiRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelControl1);
            this.panelMain.Controls.Add(this.gcMain);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(792, 733);
            this.panelMain.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnLuu);
            this.panelControl1.Controls.Add(this.btnHuyBo);
            this.panelControl1.Controls.Add(this.btnChinhSua);
            this.panelControl1.Controls.Add(this.btnXoa);
            this.panelControl1.Controls.Add(this.btnThem);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 690);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(792, 43);
            this.panelControl1.TabIndex = 8;
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.Location = new System.Drawing.Point(681, 10);
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
            this.btnHuyBo.Location = new System.Drawing.Point(574, 10);
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
            this.btnChinhSua.Location = new System.Drawing.Point(467, 10);
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
            this.btnXoa.Location = new System.Drawing.Point(360, 10);
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
            this.btnThem.Location = new System.Drawing.Point(253, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(99, 25);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            // 
            // gcMain
            // 
            this.gcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcMain.DataSource = this.bangTinhPhiRowBindingSource;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(792, 684);
            this.gcMain.TabIndex = 6;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bangTinhPhiRowBindingSource
            // 
            this.bangTinhPhiRowBindingSource.DataSource = typeof(bangtinhphi);
            this.bangTinhPhiRowBindingSource.Sort = "KhoiId,KhoanThuId";
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colKhoanThuHangNamId,
            this.colKhoi,
            this.colName,
            this.colSoNgayNghiMin,
            this.colSoNgayNghiMax,
            this.colSoTien});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.GroupCount = 2;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gvMain.OptionsEditForm.FormCaptionFormat = "Chỉnh sửa thông tin Khoản thu hằng năm";
            this.gvMain.OptionsEditForm.PopupEditFormWidth = 500;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colKhoi, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvMain.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvMain_CustomColumnDisplayText);
            // 
            // colKhoanThuHangNamId
            // 
            this.colKhoanThuHangNamId.Caption = "STT";
            this.colKhoanThuHangNamId.Name = "colKhoanThuHangNamId";
            this.colKhoanThuHangNamId.Visible = true;
            this.colKhoanThuHangNamId.VisibleIndex = 0;
            this.colKhoanThuHangNamId.Width = 62;
            // 
            // colKhoi
            // 
            this.colKhoi.AppearanceCell.Options.UseTextOptions = true;
            this.colKhoi.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colKhoi.Caption = "Khối";
            this.colKhoi.FieldName = "KhoiId";
            this.colKhoi.Name = "colKhoi";
            this.colKhoi.Visible = true;
            this.colKhoi.VisibleIndex = 1;
            this.colKhoi.Width = 79;
            // 
            // colName
            // 
            this.colName.AppearanceCell.Options.UseTextOptions = true;
            this.colName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colName.Caption = "Tên Khoản thu";
            this.colName.FieldName = "KhoanThuId";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 105;
            // 
            // colSoNgayNghiMin
            // 
            this.colSoNgayNghiMin.Caption = "Số ngày nghỉ tối thiểu";
            this.colSoNgayNghiMin.FieldName = "SoNgayNghiMin";
            this.colSoNgayNghiMin.Name = "colSoNgayNghiMin";
            this.colSoNgayNghiMin.Visible = true;
            this.colSoNgayNghiMin.VisibleIndex = 1;
            this.colSoNgayNghiMin.Width = 148;
            // 
            // colSoNgayNghiMax
            // 
            this.colSoNgayNghiMax.Caption = "Số ngày nghỉ tối đa";
            this.colSoNgayNghiMax.FieldName = "SoNgayNghiMax";
            this.colSoNgayNghiMax.Name = "colSoNgayNghiMax";
            this.colSoNgayNghiMax.Visible = true;
            this.colSoNgayNghiMax.VisibleIndex = 2;
            this.colSoNgayNghiMax.Width = 159;
            // 
            // colSoTien
            // 
            this.colSoTien.Caption = "Số tiền";
            this.colSoTien.DisplayFormat.FormatString = "c0";
            this.colSoTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colSoTien.FieldName = "SoTien";
            this.colSoTien.Name = "colSoTien";
            this.colSoTien.Visible = true;
            this.colSoTien.VisibleIndex = 3;
            this.colSoTien.Width = 235;
            // 
            // FrmBangTinhPhi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 733);
            this.Controls.Add(this.panelMain);
            this.Name = "FrmBangTinhPhi";
            this.Text = "Bảng tính phí";
            this.Load += new System.EventHandler(this.FrmBangTinhPhi_Load);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bangTinhPhiRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colKhoanThuHangNamId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colSoTien;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnHuyBo;
        private DevExpress.XtraEditors.SimpleButton btnChinhSua;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraEditors.SimpleButton btnThem;
        private DevExpress.XtraGrid.Columns.GridColumn colKhoi;
        private System.Windows.Forms.BindingSource bangTinhPhiRowBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colSoNgayNghiMin;
        private DevExpress.XtraGrid.Columns.GridColumn colSoNgayNghiMax;
    }
}