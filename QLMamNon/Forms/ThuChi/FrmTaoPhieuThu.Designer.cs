using QLMamNon.Dao;

namespace QLMamNon.Forms.ThuChi
{
    partial class FrmTaoPhieuThu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTaoPhieuThu));
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.cmbHocSinh = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.hocSinhRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colHoTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLopDangHoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgaySinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnChonMayIn = new DevExpress.XtraEditors.SimpleButton();
            this.btnIn = new DevExpress.XtraEditors.SimpleButton();
            this.cmbPhanLoaiThu = new DevExpress.XtraEditors.LookUpEdit();
            this.phanLoaiThuRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtGhiChu = new DevExpress.XtraEditors.MemoEdit();
            this.btnLuuTao = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.txtSoTien = new DevExpress.XtraEditors.CalcEdit();
            this.txtMaPhieu = new DevExpress.XtraEditors.TextEdit();
            this.dateNgay = new DevExpress.XtraEditors.DateEdit();
            this.lblTaoPhieuThu = new System.Windows.Forms.Label();
            this.txtConLai = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciLblTaoPhieuThu = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNgay = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciTxtMaPhieu = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciTxtSoTien = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciTxtConLai = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCmbHocSinh = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHocSinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hocSinhRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPhanLoaiThu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phanLoaiThuRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGhiChu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaPhieu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConLai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciLblTaoPhieuThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTxtMaPhieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTxtSoTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTxtConLai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCmbHocSinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.lcMain.Appearance.Control.Font = new System.Drawing.Font("Tahoma", 20F);
            this.lcMain.Appearance.Control.Options.UseFont = true;
            this.lcMain.Controls.Add(this.cmbHocSinh);
            this.lcMain.Controls.Add(this.btnChonMayIn);
            this.lcMain.Controls.Add(this.btnIn);
            this.lcMain.Controls.Add(this.cmbPhanLoaiThu);
            this.lcMain.Controls.Add(this.txtGhiChu);
            this.lcMain.Controls.Add(this.btnLuuTao);
            this.lcMain.Controls.Add(this.btnLuu);
            this.lcMain.Controls.Add(this.txtSoTien);
            this.lcMain.Controls.Add(this.txtMaPhieu);
            this.lcMain.Controls.Add(this.dateNgay);
            this.lcMain.Controls.Add(this.lblTaoPhieuThu);
            this.lcMain.Controls.Add(this.txtConLai);
            this.lcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcMain.Font = new System.Drawing.Font("Tahoma", 20F);
            this.lcMain.Location = new System.Drawing.Point(0, 0);
            this.lcMain.Name = "lcMain";
            this.lcMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2477, 242, 250, 350);
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
            this.lcMain.Size = new System.Drawing.Size(444, 336);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "layoutControl1";
            // 
            // cmbHocSinh
            // 
            this.cmbHocSinh.EditValue = "";
            this.cmbHocSinh.Location = new System.Drawing.Point(180, 124);
            this.cmbHocSinh.Name = "cmbHocSinh";
            this.cmbHocSinh.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmbHocSinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHocSinh.Properties.DataSource = this.hocSinhRowBindingSource;
            this.cmbHocSinh.Properties.DisplayMember = "HoTen";
            this.cmbHocSinh.Properties.NullText = "";
            this.cmbHocSinh.Properties.UseReadOnlyAppearance = false;
            this.cmbHocSinh.Properties.ValueMember = "HocSinhId";
            this.cmbHocSinh.Properties.View = this.searchLookUpEdit1View;
            this.cmbHocSinh.Size = new System.Drawing.Size(448, 40);
            this.cmbHocSinh.StyleController = this.lcMain;
            this.cmbHocSinh.TabIndex = 16;
            this.cmbHocSinh.EditValueChanged += new System.EventHandler(this.cmbHocSinh_EditValueChanged);
            // 
            // hocSinhRowBindingSource
            // 
            this.hocSinhRowBindingSource.DataSource = typeof(hocsinh);
            this.hocSinhRowBindingSource.Sort = "LopDangHoc";
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colHoTen,
            this.colLopDangHoc,
            this.colNgaySinh});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colHoTen
            // 
            this.colHoTen.Caption = "Họ tên";
            this.colHoTen.FieldName = "HoTen";
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.Visible = true;
            this.colHoTen.VisibleIndex = 0;
            this.colHoTen.Width = 209;
            // 
            // colLopDangHoc
            // 
            this.colLopDangHoc.Caption = "Lớp";
            this.colLopDangHoc.FieldName = "LopDangHoc";
            this.colLopDangHoc.Name = "colLopDangHoc";
            this.colLopDangHoc.Visible = true;
            this.colLopDangHoc.VisibleIndex = 1;
            this.colLopDangHoc.Width = 60;
            // 
            // colNgaySinh
            // 
            this.colNgaySinh.Caption = "Ngày sinh";
            this.colNgaySinh.DisplayFormat.FormatString = "d/M/yyyy";
            this.colNgaySinh.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgaySinh.FieldName = "NgaySinh";
            this.colNgaySinh.Name = "colNgaySinh";
            this.colNgaySinh.Visible = true;
            this.colNgaySinh.VisibleIndex = 2;
            this.colNgaySinh.Width = 80;
            // 
            // btnChonMayIn
            // 
            this.btnChonMayIn.Image = ((System.Drawing.Image)(resources.GetObject("btnChonMayIn.Image")));
            this.btnChonMayIn.Location = new System.Drawing.Point(505, 340);
            this.btnChonMayIn.Name = "btnChonMayIn";
            this.btnChonMayIn.Size = new System.Drawing.Size(123, 40);
            this.btnChonMayIn.StyleController = this.lcMain;
            this.btnChonMayIn.TabIndex = 15;
            this.btnChonMayIn.Text = "Máy in";
            this.btnChonMayIn.Click += new System.EventHandler(this.btnChonMayIn_Click);
            // 
            // btnIn
            // 
            this.btnIn.Image = ((System.Drawing.Image)(resources.GetObject("btnIn.Image")));
            this.btnIn.Location = new System.Drawing.Point(126, 340);
            this.btnIn.MaximumSize = new System.Drawing.Size(150, 0);
            this.btnIn.MinimumSize = new System.Drawing.Size(100, 0);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(100, 40);
            this.btnIn.StyleController = this.lcMain;
            this.btnIn.TabIndex = 14;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // cmbPhanLoaiThu
            // 
            this.cmbPhanLoaiThu.Location = new System.Drawing.Point(180, 36);
            this.cmbPhanLoaiThu.Name = "cmbPhanLoaiThu";
            this.cmbPhanLoaiThu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPhanLoaiThu.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DienGiai", "Dien Giai", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cmbPhanLoaiThu.Properties.DataSource = this.phanLoaiThuRowBindingSource;
            this.cmbPhanLoaiThu.Properties.DisplayMember = "DienGiai";
            this.cmbPhanLoaiThu.Properties.NullText = "";
            this.cmbPhanLoaiThu.Properties.ShowHeader = false;
            this.cmbPhanLoaiThu.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbPhanLoaiThu.Properties.ValueMember = "PhanLoaiThuId";
            this.cmbPhanLoaiThu.Size = new System.Drawing.Size(448, 40);
            this.cmbPhanLoaiThu.StyleController = this.lcMain;
            this.cmbPhanLoaiThu.TabIndex = 13;
            this.cmbPhanLoaiThu.EditValueChanged += new System.EventHandler(this.cmbPhanLoaiThu_EditValueChanged);
            // 
            // phanLoaiThuRowBindingSource
            // 
            this.phanLoaiThuRowBindingSource.DataSource = typeof(phanloaithu);
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(180, 300);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(448, 36);
            this.txtGhiChu.StyleController = this.lcMain;
            this.txtGhiChu.TabIndex = 12;
            // 
            // btnLuuTao
            // 
            this.btnLuuTao.Image = ((System.Drawing.Image)(resources.GetObject("btnLuuTao.Image")));
            this.btnLuuTao.Location = new System.Drawing.Point(230, 340);
            this.btnLuuTao.MaximumSize = new System.Drawing.Size(300, 0);
            this.btnLuuTao.MinimumSize = new System.Drawing.Size(250, 0);
            this.btnLuuTao.Name = "btnLuuTao";
            this.btnLuuTao.Size = new System.Drawing.Size(271, 40);
            this.btnLuuTao.StyleController = this.lcMain;
            this.btnLuuTao.TabIndex = 10;
            this.btnLuuTao.Text = "Lưu, In và Tạo mới";
            this.btnLuuTao.Click += new System.EventHandler(this.btnLuuTao_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.Location = new System.Drawing.Point(22, 340);
            this.btnLuu.MaximumSize = new System.Drawing.Size(150, 0);
            this.btnLuu.MinimumSize = new System.Drawing.Size(100, 0);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 40);
            this.btnLuu.StyleController = this.lcMain;
            this.btnLuu.TabIndex = 9;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // txtSoTien
            // 
            this.txtSoTien.Location = new System.Drawing.Point(180, 212);
            this.txtSoTien.Name = "txtSoTien";
            this.txtSoTien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSoTien.Properties.DisplayFormat.FormatString = "n0";
            this.txtSoTien.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtSoTien.Properties.Mask.EditMask = "n0";
            this.txtSoTien.Size = new System.Drawing.Size(448, 40);
            this.txtSoTien.StyleController = this.lcMain;
            this.txtSoTien.TabIndex = 7;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.Greater;
            conditionValidationRule1.ErrorText = "Vui lòng nhập Số tiền";
            conditionValidationRule1.Value1 = ((long)(0));
            conditionValidationRule1.Value2 = "0";
            conditionValidationRule1.Values.Add("0");
            this.dxValidationProvider.SetValidationRule(this.txtSoTien, conditionValidationRule1);
            this.txtSoTien.EditValueChanged += new System.EventHandler(this.txtSoTien_EditValueChanged);
            // 
            // txtMaPhieu
            // 
            this.txtMaPhieu.Location = new System.Drawing.Point(180, 168);
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.Properties.ReadOnly = true;
            this.txtMaPhieu.Size = new System.Drawing.Size(448, 40);
            this.txtMaPhieu.StyleController = this.lcMain;
            this.txtMaPhieu.TabIndex = 6;
            // 
            // dateNgay
            // 
            this.dateNgay.EditValue = null;
            this.dateNgay.Location = new System.Drawing.Point(180, 80);
            this.dateNgay.Name = "dateNgay";
            this.dateNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgay.Size = new System.Drawing.Size(448, 40);
            this.dateNgay.StyleController = this.lcMain;
            this.dateNgay.TabIndex = 5;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "Vui lòng chọn Ngày";
            this.dxValidationProvider.SetValidationRule(this.dateNgay, conditionValidationRule2);
            // 
            // lblTaoPhieuThu
            // 
            this.lblTaoPhieuThu.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold);
            this.lblTaoPhieuThu.Location = new System.Drawing.Point(12, 12);
            this.lblTaoPhieuThu.Name = "lblTaoPhieuThu";
            this.lblTaoPhieuThu.Size = new System.Drawing.Size(616, 20);
            this.lblTaoPhieuThu.TabIndex = 4;
            this.lblTaoPhieuThu.Text = "TẠO/CẬP NHẬT PHIẾU THU";
            this.lblTaoPhieuThu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtConLai
            // 
            this.txtConLai.Location = new System.Drawing.Point(180, 256);
            this.txtConLai.Name = "txtConLai";
            this.txtConLai.Properties.ReadOnly = true;
            this.txtConLai.Size = new System.Drawing.Size(448, 40);
            this.txtConLai.StyleController = this.lcMain;
            this.txtConLai.TabIndex = 8;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 20F);
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciLblTaoPhieuThu,
            this.lciNgay,
            this.lciTxtMaPhieu,
            this.lciTxtSoTien,
            this.lciTxtConLai,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.lciCmbHocSinh});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(640, 392);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciLblTaoPhieuThu
            // 
            this.lciLblTaoPhieuThu.Control = this.lblTaoPhieuThu;
            this.lciLblTaoPhieuThu.Location = new System.Drawing.Point(0, 0);
            this.lciLblTaoPhieuThu.Name = "lciLblTaoPhieuThu";
            this.lciLblTaoPhieuThu.Size = new System.Drawing.Size(620, 24);
            this.lciLblTaoPhieuThu.TextSize = new System.Drawing.Size(0, 0);
            this.lciLblTaoPhieuThu.TextVisible = false;
            // 
            // lciNgay
            // 
            this.lciNgay.Control = this.dateNgay;
            this.lciNgay.Location = new System.Drawing.Point(0, 68);
            this.lciNgay.Name = "lciNgay";
            this.lciNgay.Size = new System.Drawing.Size(620, 44);
            this.lciNgay.Text = "Ngày thu:";
            this.lciNgay.TextSize = new System.Drawing.Size(165, 33);
            // 
            // lciTxtMaPhieu
            // 
            this.lciTxtMaPhieu.Control = this.txtMaPhieu;
            this.lciTxtMaPhieu.Location = new System.Drawing.Point(0, 156);
            this.lciTxtMaPhieu.Name = "lciTxtMaPhieu";
            this.lciTxtMaPhieu.Size = new System.Drawing.Size(620, 44);
            this.lciTxtMaPhieu.Text = "Số biên lai:";
            this.lciTxtMaPhieu.TextSize = new System.Drawing.Size(165, 33);
            // 
            // lciTxtSoTien
            // 
            this.lciTxtSoTien.Control = this.txtSoTien;
            this.lciTxtSoTien.Location = new System.Drawing.Point(0, 200);
            this.lciTxtSoTien.Name = "lciTxtSoTien";
            this.lciTxtSoTien.Size = new System.Drawing.Size(620, 44);
            this.lciTxtSoTien.Text = "Số tiền nộp:";
            this.lciTxtSoTien.TextSize = new System.Drawing.Size(165, 33);
            // 
            // lciTxtConLai
            // 
            this.lciTxtConLai.Control = this.txtConLai;
            this.lciTxtConLai.Location = new System.Drawing.Point(0, 244);
            this.lciTxtConLai.Name = "lciTxtConLai";
            this.lciTxtConLai.Size = new System.Drawing.Size(620, 44);
            this.lciTxtConLai.Text = "Còn lại:";
            this.lciTxtConLai.TextSize = new System.Drawing.Size(165, 33);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnLuu;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.layoutControlItem1.Location = new System.Drawing.Point(10, 328);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(104, 44);
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Right;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnLuuTao;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.layoutControlItem2.Location = new System.Drawing.Point(218, 328);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(275, 44);
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Right;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 328);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 44);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtGhiChu;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 288);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(620, 40);
            this.layoutControlItem3.Text = "Ghi chú:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(165, 33);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbPhanLoaiThu;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(620, 44);
            this.layoutControlItem4.Text = "Phân loại thu";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(165, 33);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnIn;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem5.Location = new System.Drawing.Point(114, 328);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(104, 44);
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Right;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnChonMayIn;
            this.layoutControlItem6.Location = new System.Drawing.Point(493, 328);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(127, 44);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // lciCmbHocSinh
            // 
            this.lciCmbHocSinh.Control = this.cmbHocSinh;
            this.lciCmbHocSinh.Location = new System.Drawing.Point(0, 112);
            this.lciCmbHocSinh.Name = "lciCmbHocSinh";
            this.lciCmbHocSinh.Size = new System.Drawing.Size(620, 44);
            this.lciCmbHocSinh.Text = "Cho Học sinh:";
            this.lciCmbHocSinh.TextSize = new System.Drawing.Size(165, 33);
            this.lciCmbHocSinh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // dxValidationProvider
            // 
            this.dxValidationProvider.ValidationMode = DevExpress.XtraEditors.DXErrorProvider.ValidationMode.Auto;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // FrmTaoPhieuThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 336);
            this.Controls.Add(this.lcMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTaoPhieuThu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo/Cập nhật Phiếu thu";
            this.Activated += new System.EventHandler(this.FrmTaoPhieuThu_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmTaoPhieuThu_FormClosed);
            this.Load += new System.EventHandler(this.FrmTaoPhieuThu_Load);
            this.Enter += new System.EventHandler(this.FrmTaoPhieuThu_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbHocSinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hocSinhRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPhanLoaiThu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phanLoaiThuRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGhiChu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaPhieu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConLai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciLblTaoPhieuThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTxtMaPhieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTxtSoTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTxtConLai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCmbHocSinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.Label lblTaoPhieuThu;
        private DevExpress.XtraLayout.LayoutControlItem lciLblTaoPhieuThu;
        private DevExpress.XtraEditors.DateEdit dateNgay;
        private DevExpress.XtraLayout.LayoutControlItem lciNgay;
        private DevExpress.XtraEditors.TextEdit txtMaPhieu;
        private DevExpress.XtraLayout.LayoutControlItem lciTxtMaPhieu;
        private DevExpress.XtraEditors.CalcEdit txtSoTien;
        private DevExpress.XtraLayout.LayoutControlItem lciTxtSoTien;
        private DevExpress.XtraLayout.LayoutControlItem lciTxtConLai;
        private DevExpress.XtraEditors.SimpleButton btnLuuTao;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
        private System.Windows.Forms.BindingSource hocSinhRowBindingSource;
        private DevExpress.XtraEditors.TextEdit txtConLai;
        private DevExpress.XtraEditors.MemoEdit txtGhiChu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LookUpEdit cmbPhanLoaiThu;
        private System.Windows.Forms.BindingSource phanLoaiThuRowBindingSource;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton btnIn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private System.Windows.Forms.PrintDialog printDialog1;
        private DevExpress.XtraEditors.SimpleButton btnChonMayIn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SearchLookUpEdit cmbHocSinh;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem lciCmbHocSinh;
        private DevExpress.XtraGrid.Columns.GridColumn colHoTen;
        private DevExpress.XtraGrid.Columns.GridColumn colLopDangHoc;
        private DevExpress.XtraGrid.Columns.GridColumn colNgaySinh;
    }
}