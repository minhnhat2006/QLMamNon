﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLMamNon.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2023-04-13")]
        public global::System.DateTime AppLannchDate {
            get {
                return ((global::System.DateTime)(this["AppLannchDate"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Thực phẩm, Phụ phí")]
        public string DefaultMaPhanLoaiChi {
            get {
                return ((string)(this["DefaultMaPhanLoaiChi"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://rest.esms.vn/MainService.svc/json/SendMultipleMessage_V4_get?Phone={0}&Con" +
            "tent={1}&ApiKey=9BB708A18DC3EE2D63879D0D2E55CC&SecretKey=27AD94C5BC577605E8D26BC" +
            "DBF086F&SmsType=4&IsUnicode=0")]
        public string SMSWSSendMessageUrl {
            get {
                return ((string)(this["SMSWSSendMessageUrl"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("6000")]
        public long TienAnSang {
            get {
                return ((long)(this["TienAnSang"]));
            }
            set {
                this["TienAnSang"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("6000")]
        public long TienAnToi {
            get {
                return ((long)(this["TienAnToi"]));
            }
            set {
                this["TienAnToi"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20000")]
        public long TienAnChinh {
            get {
                return ((long)(this["TienAnChinh"]));
            }
            set {
                this["TienAnChinh"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2015")]
        public int StartYearForDropDown {
            get {
                return ((int)(this["StartYearForDropDown"]));
            }
            set {
                this["StartYearForDropDown"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public long SoTienTonDauKy {
            get {
                return ((long)(this["SoTienTonDauKy"]));
            }
            set {
                this["SoTienTonDauKy"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2005")]
        public int NamSinhStart {
            get {
                return ((int)(this["NamSinhStart"]));
            }
            set {
                this["NamSinhStart"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public int SpeedSMSType {
            get {
                return ((int)(this["SpeedSMSType"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://api.speedsms.vn/index.php")]
        public string SpeedSMSRootUrl {
            get {
                return ((string)(this["SpeedSMSRootUrl"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("vVkic2h4PsnEnTRDArJiPxkAoGx7Fsvl")]
        public string SpeedSMSAccessToken {
            get {
                return ((string)(this["SpeedSMSAccessToken"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("MNHongMinh")]
        public string SpeedSMSSender {
            get {
                return ((string)(this["SpeedSMSSender"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string PhanLoaiThuByHocSinh {
            get {
                return ((string)(this["PhanLoaiThuByHocSinh"]));
            }
            set {
                this["PhanLoaiThuByHocSinh"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ShowGiayBaoNopTienDieuHoa {
            get {
                return ((bool)(this["ShowGiayBaoNopTienDieuHoa"]));
            }
            set {
                this["ShowGiayBaoNopTienDieuHoa"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ShowGiayBaoNopTienNote {
            get {
                return ((bool)(this["ShowGiayBaoNopTienNote"]));
            }
            set {
                this["ShowGiayBaoNopTienNote"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string POSPrinterName {
            get {
                return ((string)(this["POSPrinterName"]));
            }
            set {
                this["POSPrinterName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("12345")]
        public long LastMaPhieuThu {
            get {
                return ((long)(this["LastMaPhieuThu"]));
            }
            set {
                this["LastMaPhieuThu"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Setting {
            get {
                return ((string)(this["Setting"]));
            }
            set {
                this["Setting"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("001100{0}81{1}0008aa{2}{3}")]
        public string PDUSendMessageTemplate {
            get {
                return ((string)(this["PDUSendMessageTemplate"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IsMNHongMinh {
            get {
                return ((bool)(this["IsMNHongMinh"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("server=127.0.0.1;user id=root;password=root;persistsecurityinfo=True;database=qlm" +
            "amnon;characterset=utf8;allowzerodatetime=True;convertzerodatetime=True")]
        public string qlmamnonConnectionString1 {
            get {
                return ((string)(this["qlmamnonConnectionString1"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("TRƯỜNG MN HỒNG MINH")]
        public string MNHongMinh {
            get {
                return ((string)(this["MNHongMinh"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("TRUNG TÂM ANH NGỮ DOMYTY")]
        public string Domity {
            get {
                return ((string)(this["Domity"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Số 40-42 Nguyễn Trung Trực, Sơn Trà, Đà Nẵng")]
        public string MNHongMinhAddress {
            get {
                return ((string)(this["MNHongMinhAddress"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Số 105A Nguyễn Trung Trực, Sơn Trà, Đà Nẵng")]
        public string DomityAddress {
            get {
                return ((string)(this["DomityAddress"]));
            }
        }
    }
}
