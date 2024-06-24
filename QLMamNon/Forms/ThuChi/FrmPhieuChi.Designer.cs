using QLMamNon.Dao;

namespace QLMamNon.Forms.ThuChi
{
    partial class FrmPhieuChi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPhieuChi));
            this.panelMain = new System.Windows.Forms.Panel();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.phieuChiRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhanLoaiChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoiDung = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaPhieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGhiChu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnChinhSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnThem = new DevExpress.XtraEditors.SimpleButton();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phieuChiRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.gcMain);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(792, 573);
            this.panelMain.TabIndex = 0;
            // 
            // gcMain
            // 
            this.gcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcMain.DataSource = this.phieuChiRowBindingSource;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(792, 530);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // phieuChiRowBindingSource
            // 
            this.phieuChiRowBindingSource.AllowNew = true;
            this.phieuChiRowBindingSource.DataSource = typeof(QLMamNon.Dao.phieuchi);
            this.phieuChiRowBindingSource.Sort = "Ngay,MaPhieu";
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSTT,
            this.colNgay,
            this.colPhanLoaiChi,
            this.colNoiDung,
            this.colSoLuong,
            this.colDonGia,
            this.colSoTien,
            this.colMaPhieu,
            this.colGhiChu});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.GroupCount = 1;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gvMain.OptionsBehavior.ReadOnly = true;
            this.gvMain.OptionsEditForm.FormCaptionFormat = "Chỉnh sửa thông tin Lớp học";
            this.gvMain.OptionsEditForm.PopupEditFormWidth = 500;
            this.gvMain.OptionsView.ShowFooter = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colNgay, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gvMain.DoubleClick += new System.EventHandler(this.gvMain_DoubleClick);
            // 
            // colSTT
            // 
            this.colSTT.Caption = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 0;
            this.colSTT.Width = 41;
            // 
            // colNgay
            // 
            this.colNgay.Caption = "Ngày chi";
            this.colNgay.FieldName = "Ngay";
            this.colNgay.Name = "colNgay";
            this.colNgay.Visible = true;
            this.colNgay.VisibleIndex = 1;
            this.colNgay.Width = 85;
            // 
            // colPhanLoaiChi
            // 
            this.colPhanLoaiChi.AppearanceCell.Options.UseTextOptions = true;
            this.colPhanLoaiChi.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colPhanLoaiChi.Caption = "Mã Phân loại Chi";
            this.colPhanLoaiChi.FieldName = "PhanLoaiChi";
            this.colPhanLoaiChi.Name = "colPhanLoaiChi";
            this.colPhanLoaiChi.Visible = true;
            this.colPhanLoaiChi.VisibleIndex = 2;
            this.colPhanLoaiChi.Width = 92;
            // 
            // colNoiDung
            // 
            this.colNoiDung.Caption = "Diễn giải";
            this.colNoiDung.FieldName = "NoiDung";
            this.colNoiDung.Name = "colNoiDung";
            this.colNoiDung.Visible = true;
            this.colNoiDung.VisibleIndex = 3;
            this.colNoiDung.Width = 83;
            // 
            // colSoLuong
            // 
            this.colSoLuong.AppearanceCell.Options.UseTextOptions = true;
            this.colSoLuong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSoLuong.Caption = "Số lượng";
            this.colSoLuong.DisplayFormat.FormatString = "n0";
            this.colSoLuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSoLuong.FieldName = "SoLuong";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Visible = true;
            this.colSoLuong.VisibleIndex = 4;
            this.colSoLuong.Width = 99;
            // 
            // colDonGia
            // 
            this.colDonGia.AppearanceCell.Options.UseTextOptions = true;
            this.colDonGia.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.DisplayFormat.FormatString = "n0";
            this.colDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 5;
            this.colDonGia.Width = 99;
            // 
            // colSoTien
            // 
            this.colSoTien.Caption = "Thành tiền";
            this.colSoTien.DisplayFormat.FormatString = "n0";
            this.colSoTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colSoTien.FieldName = "SoTien";
            this.colSoTien.Name = "colSoTien";
            this.colSoTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SoTien", "{0:n0}")});
            this.colSoTien.Visible = true;
            this.colSoTien.VisibleIndex = 6;
            this.colSoTien.Width = 123;
            // 
            // colMaPhieu
            // 
            this.colMaPhieu.Caption = "Mã phiếu chi";
            this.colMaPhieu.FieldName = "MaPhieu";
            this.colMaPhieu.Name = "colMaPhieu";
            this.colMaPhieu.Visible = true;
            this.colMaPhieu.VisibleIndex = 1;
            this.colMaPhieu.Width = 95;
            // 
            // colGhiChu
            // 
            this.colGhiChu.Caption = "Ghi chú";
            this.colGhiChu.FieldName = "GhiChu";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.Visible = true;
            this.colGhiChu.VisibleIndex = 7;
            this.colGhiChu.Width = 67;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnXoa);
            this.panelControl1.Controls.Add(this.btnChinhSua);
            this.panelControl1.Controls.Add(this.btnThem);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 530);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(792, 43);
            this.panelControl1.TabIndex = 8;
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoa.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnXoa.Appearance.Options.UseFont = true;
            this.btnXoa.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.Image")));
            this.btnXoa.Location = new System.Drawing.Point(681, 10);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(99, 25);
            this.btnXoa.TabIndex = 1;
            this.btnXoa.Text = "Xóa";
            // 
            // btnChinhSua
            // 
            this.btnChinhSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChinhSua.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnChinhSua.Appearance.Options.UseFont = true;
            this.btnChinhSua.Image = ((System.Drawing.Image)(resources.GetObject("btnChinhSua.Image")));
            this.btnChinhSua.Location = new System.Drawing.Point(573, 10);
            this.btnChinhSua.Name = "btnChinhSua";
            this.btnChinhSua.Size = new System.Drawing.Size(99, 25);
            this.btnChinhSua.TabIndex = 0;
            this.btnChinhSua.Text = "Chỉnh sửa";
            // 
            // btnThem
            // 
            this.btnThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThem.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnThem.Appearance.Options.UseFont = true;
            this.btnThem.Image = ((System.Drawing.Image)(resources.GetObject("btnThem.Image")));
            this.btnThem.Location = new System.Drawing.Point(462, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(99, 25);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            // 
            // FrmPhieuChi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelMain);
            this.Name = "FrmPhieuChi";
            this.Text = "Danh sách Phiếu chi";
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phieuChiRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnChinhSua;
        private DevExpress.XtraEditors.SimpleButton btnThem;
        private System.Windows.Forms.BindingSource phieuChiRowBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay;
        private DevExpress.XtraGrid.Columns.GridColumn colSoTien;
        private DevExpress.XtraGrid.Columns.GridColumn colPhanLoaiChi;
        private DevExpress.XtraGrid.Columns.GridColumn colMaPhieu;
        private DevExpress.XtraGrid.Columns.GridColumn colGhiChu;
        private DevExpress.XtraGrid.Columns.GridColumn colNoiDung;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuong;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
    }
}