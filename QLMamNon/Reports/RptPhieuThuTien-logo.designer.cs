namespace QLMamNon.Reports
{
    partial class RptPhieuThuTien
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptPhieuThuTien));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.HoTenHS = new DevExpress.XtraReports.Parameters.Parameter();
            this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FieldCaption = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DataField = new DevExpress.XtraReports.UI.XRControlStyle();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.coTienNo = new DevExpress.XtraReports.UI.FormattingRule();
            this.NgayNop = new DevExpress.XtraReports.Parameters.Parameter();
            this.ConLai = new DevExpress.XtraReports.Parameters.Parameter();
            this.TongCong = new DevExpress.XtraReports.UI.CalculatedField();
            this.ThuaThangTruoc = new DevExpress.XtraReports.UI.CalculatedField();
            this.PhaiNop = new DevExpress.XtraReports.UI.CalculatedField();
            this.DieuHoaNangKhieu = new DevExpress.XtraReports.UI.CalculatedField();
            this.Lop = new DevExpress.XtraReports.Parameters.Parameter();
            this.SoTien = new DevExpress.XtraReports.Parameters.Parameter();
            this.NguoiThu = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.lblTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNgayThangNam = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHoTen = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblLop = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSoTienNop = new DevExpress.XtraReports.UI.XRLabel();
            this.lblConLai = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNguoiThu = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 0F;
            this.Detail.MultiColumn.ColumnCount = 2;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // HoTenHS
            // 
            this.HoTenHS.Description = "Ho Ten HS";
            this.HoTenHS.Name = "HoTenHS";
            this.HoTenHS.Visible = false;
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.White;
            this.Title.BorderColor = System.Drawing.SystemColors.ControlText;
            this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Title.BorderWidth = 1F;
            this.Title.Font = new System.Drawing.Font("Times New Roman", 21F);
            this.Title.ForeColor = System.Drawing.Color.Black;
            this.Title.Name = "Title";
            // 
            // FieldCaption
            // 
            this.FieldCaption.BackColor = System.Drawing.Color.White;
            this.FieldCaption.BorderColor = System.Drawing.SystemColors.ControlText;
            this.FieldCaption.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.FieldCaption.BorderWidth = 1F;
            this.FieldCaption.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.FieldCaption.ForeColor = System.Drawing.Color.Black;
            this.FieldCaption.Name = "FieldCaption";
            // 
            // PageInfo
            // 
            this.PageInfo.BackColor = System.Drawing.Color.White;
            this.PageInfo.BorderColor = System.Drawing.SystemColors.ControlText;
            this.PageInfo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.PageInfo.BorderWidth = 1F;
            this.PageInfo.Font = new System.Drawing.Font("Arial", 8F);
            this.PageInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PageInfo.Name = "PageInfo";
            // 
            // DataField
            // 
            this.DataField.BackColor = System.Drawing.Color.White;
            this.DataField.BorderColor = System.Drawing.SystemColors.ControlText;
            this.DataField.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.DataField.BorderWidth = 1F;
            this.DataField.Font = new System.Drawing.Font("Arial", 9F);
            this.DataField.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DataField.Name = "DataField";
            this.DataField.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 0F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 0F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // coTienNo
            // 
            this.coTienNo.Condition = "[TienNo] > 0";
            // 
            // 
            // 
            this.coTienNo.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.coTienNo.Name = "coTienNo";
            // 
            // NgayNop
            // 
            this.NgayNop.Name = "NgayNop";
            this.NgayNop.Type = typeof(System.DateTime);
            this.NgayNop.Visible = false;
            // 
            // ConLai
            // 
            this.ConLai.Description = "Con Lai";
            this.ConLai.Name = "ConLai";
            this.ConLai.Type = typeof(long);
            this.ConLai.ValueInfo = "0";
            this.ConLai.Visible = false;
            // 
            // TongCong
            // 
            this.TongCong.Expression = "[SoTienAnSang]+[SoTienKhoanThuChinh]+[SoTienAnToi]+[SoTienDieuHoa]+[SoTienNoThang" +
    "Truoc] + [SoTienNangKhieu]+[SoTienDoDung]";
            this.TongCong.FieldType = DevExpress.XtraReports.UI.FieldType.Double;
            this.TongCong.Name = "TongCong";
            // 
            // ThuaThangTruoc
            // 
            this.ThuaThangTruoc.Expression = "[SoTienAnSangThuaThangTruoc]+[SoTienAnTruaThuaThangTruoc]+[SoTienAnToiThuaThangTr" +
    "uoc]";
            this.ThuaThangTruoc.FieldType = DevExpress.XtraReports.UI.FieldType.Double;
            this.ThuaThangTruoc.Name = "ThuaThangTruoc";
            // 
            // PhaiNop
            // 
            this.PhaiNop.Expression = "[TongCong]-[ThuaThangTruoc]";
            this.PhaiNop.FieldType = DevExpress.XtraReports.UI.FieldType.Double;
            this.PhaiNop.Name = "PhaiNop";
            // 
            // DieuHoaNangKhieu
            // 
            this.DieuHoaNangKhieu.Expression = "Iif([Parameters.ShowDieuHoa],  [SoTienDieuHoa] + [SoTienNangKhieu], [SoTienNangKh" +
    "ieu])";
            this.DieuHoaNangKhieu.FieldType = DevExpress.XtraReports.UI.FieldType.Double;
            this.DieuHoaNangKhieu.Name = "DieuHoaNangKhieu";
            // 
            // Lop
            // 
            this.Lop.Description = "Lop";
            this.Lop.Name = "Lop";
            this.Lop.Visible = false;
            // 
            // SoTien
            // 
            this.SoTien.Description = "So Tien Nop";
            this.SoTien.Name = "SoTien";
            this.SoTien.Type = typeof(long);
            this.SoTien.ValueInfo = "0";
            this.SoTien.Visible = false;
            // 
            // NguoiThu
            // 
            this.NguoiThu.Description = "Nguoi Thu";
            this.NguoiThu.Name = "NguoiThu";
            this.NguoiThu.Visible = false;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.lblTitle,
            this.xrLabel1,
            this.lblNgayThangNam,
            this.lblHoTen,
            this.xrLabel2,
            this.lblLop,
            this.lblSoTienNop,
            this.lblConLai,
            this.lblNguoiThu});
            this.ReportHeader.HeightF = 500F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 121F);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTitle.SizeF = new System.Drawing.SizeF(280F, 20F);
            this.lblTitle.StylePriority.UseFont = false;
            this.lblTitle.StylePriority.UseTextAlignment = false;
            this.lblTitle.Text = "BIÊN LAI THU HỌC PHÍ";
            this.lblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 61F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(280F, 20F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "TRƯỜNG MN HỒNG MINH";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblNgayThangNam
            // 
            this.lblNgayThangNam.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.NgayNop, "Text", "Ngày {0:dd} tháng {0:MM} năm {0:yyyy}")});
            this.lblNgayThangNam.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblNgayThangNam.LocationFloat = new DevExpress.Utils.PointFloat(0F, 171F);
            this.lblNgayThangNam.Name = "lblNgayThangNam";
            this.lblNgayThangNam.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100F);
            this.lblNgayThangNam.SizeF = new System.Drawing.SizeF(280F, 20F);
            this.lblNgayThangNam.StylePriority.UseFont = false;
            this.lblNgayThangNam.StylePriority.UsePadding = false;
            this.lblNgayThangNam.StylePriority.UseTextAlignment = false;
            this.lblNgayThangNam.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblHoTen
            // 
            this.lblHoTen.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.HoTenHS, "Text", "Tên HS: {0}")});
            this.lblHoTen.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblHoTen.LocationFloat = new DevExpress.Utils.PointFloat(0F, 226F);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100F);
            this.lblHoTen.SizeF = new System.Drawing.SizeF(280F, 20F);
            this.lblHoTen.StylePriority.UseFont = false;
            this.lblHoTen.StylePriority.UsePadding = false;
            this.lblHoTen.StylePriority.UseTextAlignment = false;
            this.lblHoTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 81F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(280F, 20F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Số 40-42 Nguyễn Trung Trực, Sơn Trà, Đà Nẵng";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblLop
            // 
            this.lblLop.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.Lop, "Text", "Lớp: {0}")});
            this.lblLop.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblLop.LocationFloat = new DevExpress.Utils.PointFloat(0F, 281F);
            this.lblLop.Name = "lblLop";
            this.lblLop.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100F);
            this.lblLop.SizeF = new System.Drawing.SizeF(280F, 20F);
            this.lblLop.StylePriority.UseFont = false;
            this.lblLop.StylePriority.UsePadding = false;
            this.lblLop.StylePriority.UseTextAlignment = false;
            this.lblLop.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblSoTienNop
            // 
            this.lblSoTienNop.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.SoTien, "Text", "Số tiền nộp: {0:n0} đồng")});
            this.lblSoTienNop.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblSoTienNop.LocationFloat = new DevExpress.Utils.PointFloat(0F, 336F);
            this.lblSoTienNop.Name = "lblSoTienNop";
            this.lblSoTienNop.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100F);
            this.lblSoTienNop.SizeF = new System.Drawing.SizeF(280F, 20F);
            this.lblSoTienNop.StylePriority.UseFont = false;
            this.lblSoTienNop.StylePriority.UsePadding = false;
            this.lblSoTienNop.StylePriority.UseTextAlignment = false;
            this.lblSoTienNop.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblConLai
            // 
            this.lblConLai.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.ConLai, "Text", "Còn lại: {0:n0}")});
            this.lblConLai.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblConLai.LocationFloat = new DevExpress.Utils.PointFloat(0F, 391F);
            this.lblConLai.Name = "lblConLai";
            this.lblConLai.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100F);
            this.lblConLai.SizeF = new System.Drawing.SizeF(280F, 20F);
            this.lblConLai.StylePriority.UseFont = false;
            this.lblConLai.StylePriority.UsePadding = false;
            this.lblConLai.StylePriority.UseTextAlignment = false;
            this.lblConLai.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblNguoiThu
            // 
            this.lblNguoiThu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.NguoiThu, "Text", "Người thu: {0}")});
            this.lblNguoiThu.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblNguoiThu.LocationFloat = new DevExpress.Utils.PointFloat(0F, 446F);
            this.lblNguoiThu.Name = "lblNguoiThu";
            this.lblNguoiThu.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100F);
            this.lblNguoiThu.SizeF = new System.Drawing.SizeF(280F, 20F);
            this.lblNguoiThu.StylePriority.UseFont = false;
            this.lblNguoiThu.StylePriority.UsePadding = false;
            this.lblNguoiThu.StylePriority.UseTextAlignment = false;
            this.lblNguoiThu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10, 100F);
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(280F, 60F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox1.StylePriority.UsePadding = false;
            // 
            // RptPhieuThuTien
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1,
            this.ReportHeader});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.TongCong,
            this.ThuaThangTruoc,
            this.PhaiNop,
            this.DieuHoaNangKhieu});
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.coTienNo});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 450;
            this.PageWidth = 280;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.NgayNop,
            this.ConLai,
            this.HoTenHS,
            this.Lop,
            this.SoTien,
            this.NguoiThu});
            this.ShowPrintMarginsWarning = false;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.FieldCaption,
            this.PageInfo,
            this.DataField});
            this.Version = "14.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.report_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRControlStyle Title;
        private DevExpress.XtraReports.UI.XRControlStyle FieldCaption;
        private DevExpress.XtraReports.UI.XRControlStyle PageInfo;
        private DevExpress.XtraReports.UI.XRControlStyle DataField;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private DevExpress.XtraReports.UI.FormattingRule coTienNo;
        public DevExpress.XtraReports.Parameters.Parameter NgayNop;
        public DevExpress.XtraReports.Parameters.Parameter ConLai;
        public DevExpress.XtraReports.Parameters.Parameter HoTenHS;
        private DevExpress.XtraReports.UI.CalculatedField TongCong;
        private DevExpress.XtraReports.UI.CalculatedField ThuaThangTruoc;
        private DevExpress.XtraReports.UI.CalculatedField PhaiNop;
        private DevExpress.XtraReports.UI.CalculatedField DieuHoaNangKhieu;
        public DevExpress.XtraReports.Parameters.Parameter Lop;
        public DevExpress.XtraReports.Parameters.Parameter SoTien;
        public DevExpress.XtraReports.Parameters.Parameter NguoiThu;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel lblTitle;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel lblNgayThangNam;
        private DevExpress.XtraReports.UI.XRLabel lblHoTen;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel lblLop;
        private DevExpress.XtraReports.UI.XRLabel lblSoTienNop;
        private DevExpress.XtraReports.UI.XRLabel lblConLai;
        private DevExpress.XtraReports.UI.XRLabel lblNguoiThu;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
    }
}
