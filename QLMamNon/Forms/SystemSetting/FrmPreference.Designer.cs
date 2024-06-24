using QLMamNon.Dao;

namespace QLMamNon.Forms.SystemSetting
{
    partial class FrmPreference
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPreference));
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.chkGiayBaoNopTienDieuHoa = new DevExpress.XtraEditors.CheckEdit();
            this.btnAddPhanLoaiThu = new DevExpress.XtraEditors.SimpleButton();
            this.cmbPhanLoaiThu = new DevExpress.XtraEditors.LookUpEdit();
            this.phanLoaiThuRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtThuByHS = new DevExpress.XtraEditors.TextEdit();
            this.txtTTHSSearchYearFrom = new DevExpress.XtraEditors.CalcEdit();
            this.txtYearForDropDown = new DevExpress.XtraEditors.CalcEdit();
            this.txtSoTienTonDauKy = new DevExpress.XtraEditors.CalcEdit();
            this.txtSoTienAnChinh = new DevExpress.XtraEditors.CalcEdit();
            this.txtSoTienAnToi = new DevExpress.XtraEditors.CalcEdit();
            this.txtSoTienAnSang = new DevExpress.XtraEditors.CalcEdit();
            this.txtConnectionString = new DevExpress.XtraEditors.TextEdit();
            this.dateAppLaunchDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciSoTienAnToi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.chkGiayBaoNopTienGhiChu = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkGiayBaoNopTienDieuHoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPhanLoaiThu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phanLoaiThuRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThuByHS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTTHSSearchYearFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYearForDropDown.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTienTonDauKy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTienAnChinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTienAnToi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTienAnSang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConnectionString.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAppLaunchDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAppLaunchDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSoTienAnToi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGiayBaoNopTienGhiChu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.lcMain.Appearance.Control.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lcMain.Appearance.Control.Options.UseFont = true;
            this.lcMain.Controls.Add(this.chkGiayBaoNopTienGhiChu);
            this.lcMain.Controls.Add(this.chkGiayBaoNopTienDieuHoa);
            this.lcMain.Controls.Add(this.btnAddPhanLoaiThu);
            this.lcMain.Controls.Add(this.cmbPhanLoaiThu);
            this.lcMain.Controls.Add(this.txtThuByHS);
            this.lcMain.Controls.Add(this.txtTTHSSearchYearFrom);
            this.lcMain.Controls.Add(this.txtYearForDropDown);
            this.lcMain.Controls.Add(this.txtSoTienTonDauKy);
            this.lcMain.Controls.Add(this.txtSoTienAnChinh);
            this.lcMain.Controls.Add(this.txtSoTienAnToi);
            this.lcMain.Controls.Add(this.txtSoTienAnSang);
            this.lcMain.Controls.Add(this.txtConnectionString);
            this.lcMain.Controls.Add(this.dateAppLaunchDate);
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
            this.lcMain.Size = new System.Drawing.Size(569, 283);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "layoutControl1";
            // 
            // chkGiayBaoNopTienDieuHoa
            // 
            this.chkGiayBaoNopTienDieuHoa.Location = new System.Drawing.Point(159, 209);
            this.chkGiayBaoNopTienDieuHoa.Name = "chkGiayBaoNopTienDieuHoa";
            this.chkGiayBaoNopTienDieuHoa.Properties.Caption = "Hiển thị tiền Điều hòa trên Giấy báo nộp tiền";
            this.chkGiayBaoNopTienDieuHoa.Size = new System.Drawing.Size(369, 23);
            this.chkGiayBaoNopTienDieuHoa.StyleController = this.lcMain;
            this.chkGiayBaoNopTienDieuHoa.TabIndex = 14;
            // 
            // btnAddPhanLoaiThu
            // 
            this.btnAddPhanLoaiThu.Location = new System.Drawing.Point(459, 105);
            this.btnAddPhanLoaiThu.Name = "btnAddPhanLoaiThu";
            this.btnAddPhanLoaiThu.Size = new System.Drawing.Size(68, 26);
            this.btnAddPhanLoaiThu.StyleController = this.lcMain;
            this.btnAddPhanLoaiThu.TabIndex = 13;
            this.btnAddPhanLoaiThu.Text = "Thêm";
            this.btnAddPhanLoaiThu.Click += new System.EventHandler(this.btnAddPhanLoaiThu_Click);
            // 
            // cmbPhanLoaiThu
            // 
            this.cmbPhanLoaiThu.Location = new System.Drawing.Point(344, 105);
            this.cmbPhanLoaiThu.Name = "cmbPhanLoaiThu";
            this.cmbPhanLoaiThu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPhanLoaiThu.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PhanLoaiThuId", "Phan Loai Thu Id", 5, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DienGiai", "Dien Giai", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cmbPhanLoaiThu.Properties.DataSource = this.phanLoaiThuRowBindingSource;
            this.cmbPhanLoaiThu.Properties.DisplayMember = "DienGiai";
            this.cmbPhanLoaiThu.Properties.NullText = "[Chọn Phân loại thu]";
            this.cmbPhanLoaiThu.Properties.ShowHeader = false;
            this.cmbPhanLoaiThu.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbPhanLoaiThu.Properties.ValueMember = "PhanLoaiThuId";
            this.cmbPhanLoaiThu.Size = new System.Drawing.Size(111, 26);
            this.cmbPhanLoaiThu.StyleController = this.lcMain;
            this.cmbPhanLoaiThu.TabIndex = 12;
            // 
            // phanLoaiThuRowBindingSource
            // 
            this.phanLoaiThuRowBindingSource.DataSource = typeof(phanloaithu);
            // 
            // txtThuByHS
            // 
            this.txtThuByHS.Location = new System.Drawing.Point(160, 105);
            this.txtThuByHS.Name = "txtThuByHS";
            this.txtThuByHS.Size = new System.Drawing.Size(180, 26);
            this.txtThuByHS.StyleController = this.lcMain;
            this.txtThuByHS.TabIndex = 11;
            // 
            // txtTTHSSearchYearFrom
            // 
            this.txtTTHSSearchYearFrom.Location = new System.Drawing.Point(413, 179);
            this.txtTTHSSearchYearFrom.Name = "txtTTHSSearchYearFrom";
            this.txtTTHSSearchYearFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTTHSSearchYearFrom.Size = new System.Drawing.Size(115, 26);
            this.txtTTHSSearchYearFrom.StyleController = this.lcMain;
            this.txtTTHSSearchYearFrom.TabIndex = 10;
            // 
            // txtYearForDropDown
            // 
            this.txtYearForDropDown.Location = new System.Drawing.Point(159, 179);
            this.txtYearForDropDown.Name = "txtYearForDropDown";
            this.txtYearForDropDown.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtYearForDropDown.Size = new System.Drawing.Size(115, 26);
            this.txtYearForDropDown.StyleController = this.lcMain;
            this.txtYearForDropDown.TabIndex = 9;
            // 
            // txtSoTienTonDauKy
            // 
            this.txtSoTienTonDauKy.Location = new System.Drawing.Point(413, 74);
            this.txtSoTienTonDauKy.Name = "txtSoTienTonDauKy";
            this.txtSoTienTonDauKy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSoTienTonDauKy.Size = new System.Drawing.Size(115, 26);
            this.txtSoTienTonDauKy.StyleController = this.lcMain;
            this.txtSoTienTonDauKy.TabIndex = 8;
            // 
            // txtSoTienAnChinh
            // 
            this.txtSoTienAnChinh.Location = new System.Drawing.Point(159, 74);
            this.txtSoTienAnChinh.Name = "txtSoTienAnChinh";
            this.txtSoTienAnChinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSoTienAnChinh.Size = new System.Drawing.Size(115, 26);
            this.txtSoTienAnChinh.StyleController = this.lcMain;
            this.txtSoTienAnChinh.TabIndex = 7;
            // 
            // txtSoTienAnToi
            // 
            this.txtSoTienAnToi.Location = new System.Drawing.Point(413, 44);
            this.txtSoTienAnToi.Name = "txtSoTienAnToi";
            this.txtSoTienAnToi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSoTienAnToi.Size = new System.Drawing.Size(115, 26);
            this.txtSoTienAnToi.StyleController = this.lcMain;
            this.txtSoTienAnToi.TabIndex = 6;
            // 
            // txtSoTienAnSang
            // 
            this.txtSoTienAnSang.Location = new System.Drawing.Point(159, 44);
            this.txtSoTienAnSang.Name = "txtSoTienAnSang";
            this.txtSoTienAnSang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSoTienAnSang.Size = new System.Drawing.Size(115, 26);
            this.txtSoTienAnSang.StyleController = this.lcMain;
            this.txtSoTienAnSang.TabIndex = 5;
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(159, -59);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Properties.ReadOnly = true;
            this.txtConnectionString.Size = new System.Drawing.Size(369, 26);
            this.txtConnectionString.StyleController = this.lcMain;
            this.txtConnectionString.TabIndex = 1;
            // 
            // dateAppLaunchDate
            // 
            this.dateAppLaunchDate.EditValue = null;
            this.dateAppLaunchDate.Location = new System.Drawing.Point(159, 14);
            this.dateAppLaunchDate.Name = "dateAppLaunchDate";
            this.dateAppLaunchDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.dateAppLaunchDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateAppLaunchDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateAppLaunchDate.Properties.Mask.EditMask = "";
            this.dateAppLaunchDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateAppLaunchDate.Properties.ReadOnly = true;
            this.dateAppLaunchDate.Size = new System.Drawing.Size(369, 26);
            this.dateAppLaunchDate.StyleController = this.lcMain;
            this.dateAppLaunchDate.TabIndex = 2;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, -102);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(552, 385);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.lciSoTienAnToi,
            this.layoutControlItem5,
            this.layoutControlGroup5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 73);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(532, 165);
            this.layoutControlGroup2.Text = "Dữ liệu thu chi";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateAppLaunchDate;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(508, 30);
            this.layoutControlItem2.Text = "Ngày bắt đầu";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(132, 19);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtSoTienAnSang;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(254, 30);
            this.layoutControlItem3.Text = "Số tiền/suất sáng";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(132, 19);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtSoTienAnChinh;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 60);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(254, 30);
            this.layoutControlItem4.Text = "Số tiền/suất chính";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(132, 19);
            // 
            // lciSoTienAnToi
            // 
            this.lciSoTienAnToi.Control = this.txtSoTienAnToi;
            this.lciSoTienAnToi.Location = new System.Drawing.Point(254, 30);
            this.lciSoTienAnToi.Name = "lciSoTienAnToi";
            this.lciSoTienAnToi.Size = new System.Drawing.Size(254, 30);
            this.lciSoTienAnToi.Text = "Số tiến/suất tối";
            this.lciSoTienAnToi.TextSize = new System.Drawing.Size(132, 19);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtSoTienTonDauKy;
            this.layoutControlItem5.Location = new System.Drawing.Point(254, 60);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(254, 30);
            this.layoutControlItem5.Text = "Số tiền tồn đầu kỳ";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(132, 19);
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10,
            this.layoutControlItem9,
            this.layoutControlItem8});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 90);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(508, 32);
            this.layoutControlGroup5.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnAddPhanLoaiThu;
            this.layoutControlItem10.Location = new System.Drawing.Point(434, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(72, 30);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.cmbPhanLoaiThu;
            this.layoutControlItem9.Location = new System.Drawing.Point(319, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(115, 30);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtThuByHS;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(319, 30);
            this.layoutControlItem8.Text = "Loại thu theo học sinh";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(132, 19);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(532, 73);
            this.layoutControlGroup3.Text = "Kết nối cơ sở dữ liệu";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtConnectionString;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(508, 30);
            this.layoutControlItem1.Text = "Connection String";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(132, 19);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem11,
            this.layoutControlItem12});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 238);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(532, 127);
            this.layoutControlGroup4.Text = "Cấu hình ưa thích";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtYearForDropDown;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(254, 30);
            this.layoutControlItem6.Text = "Dropdown từ năm";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(132, 19);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtTTHSSearchYearFrom;
            this.layoutControlItem7.Location = new System.Drawing.Point(254, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(254, 30);
            this.layoutControlItem7.Text = "Tìm năm sinh HS từ";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(132, 19);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.chkGiayBaoNopTienDieuHoa;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(508, 27);
            this.layoutControlItem11.Text = "Chọn";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(132, 19);
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
            this.btnClose.Location = new System.Drawing.Point(455, 289);
            this.btnClose.MaximumSize = new System.Drawing.Size(250, 25);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 25);
            this.btnClose.StyleController = this.lcMain;
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(345, 289);
            this.btnSave.MaximumSize = new System.Drawing.Size(250, 25);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 25);
            this.btnSave.StyleController = this.lcMain;
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnXemBaoCao_Click);
            // 
            // chkGiayBaoNopTienGhiChu
            // 
            this.chkGiayBaoNopTienGhiChu.Location = new System.Drawing.Point(159, 236);
            this.chkGiayBaoNopTienGhiChu.Name = "chkGiayBaoNopTienGhiChu";
            this.chkGiayBaoNopTienGhiChu.Properties.Caption = "Hiển thị ghi chú trên Giấy báo nộp tiền";
            this.chkGiayBaoNopTienGhiChu.Size = new System.Drawing.Size(369, 23);
            this.chkGiayBaoNopTienGhiChu.StyleController = this.lcMain;
            this.chkGiayBaoNopTienGhiChu.TabIndex = 15;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.chkGiayBaoNopTienGhiChu;
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 57);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(508, 27);
            this.layoutControlItem12.Text = " ";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(132, 19);
            // 
            // FrmPreference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 321);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPreference";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thiết lập số liệu phần mềm";
            this.Load += new System.EventHandler(this.FrmBaoCaoHoatDongTaiChinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkGiayBaoNopTienDieuHoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPhanLoaiThu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phanLoaiThuRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThuByHS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTTHSSearchYearFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYearForDropDown.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTienTonDauKy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTienAnChinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTienAnToi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTienAnSang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConnectionString.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAppLaunchDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAppLaunchDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSoTienAnToi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGiayBaoNopTienGhiChu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit txtConnectionString;
        private DevExpress.XtraEditors.DateEdit dateAppLaunchDate;
        private DevExpress.XtraEditors.CalcEdit txtSoTienAnSang;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.CalcEdit txtSoTienAnToi;
        private DevExpress.XtraLayout.LayoutControlItem lciSoTienAnToi;
        private DevExpress.XtraEditors.CalcEdit txtSoTienAnChinh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.CalcEdit txtSoTienTonDauKy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.CalcEdit txtYearForDropDown;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.CalcEdit txtTTHSSearchYearFrom;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.TextEdit txtThuByHS;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.SimpleButton btnAddPhanLoaiThu;
        private DevExpress.XtraEditors.LookUpEdit cmbPhanLoaiThu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private System.Windows.Forms.BindingSource phanLoaiThuRowBindingSource;
        private DevExpress.XtraEditors.CheckEdit chkGiayBaoNopTienDieuHoa;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.CheckEdit chkGiayBaoNopTienGhiChu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
    }
}