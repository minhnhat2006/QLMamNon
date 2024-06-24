using System;
using System.Threading;
using System.Windows.Forms;
using QLMamNon.Forms;
using System.Drawing;
using System.Globalization;

namespace QLMamNon
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var newCulture = new CultureInfo("vi-VN");
            newCulture.DateTimeFormat = new DateTimeFormatInfo();
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            newCulture.NumberFormat = new NumberFormatInfo();
            newCulture.NumberFormat.NumberGroupSeparator = ".";
            newCulture.NumberFormat.NumberDecimalSeparator = ",";
            newCulture.NumberFormat.CurrencySymbol = "";// "VNĐ "
            newCulture.NumberFormat.CurrencyGroupSeparator = ".";
            newCulture.NumberFormat.CurrencyDecimalSeparator = ",";
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new Font("Times New Roman", 12);
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultMenuFont = new Font("Times New Roman", 12);
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultPrintFont = new Font("Times New Roman", 12);

            Application.Run(new FrmMain());
        }
    }
}
