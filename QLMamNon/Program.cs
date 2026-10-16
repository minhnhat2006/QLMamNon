using System;
using System.Threading;
using System.Windows.Forms;
using QLMamNon.Forms;
using System.Drawing;
using System.Globalization;
using QLMamNon.Facade;

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

            // Áp dụng cùng culture cho các thread nền (BackgroundWorker, Task.Run) để đọc/ghi số và ngày
            // giống hệt UI thread.
            CultureInfo.DefaultThreadCurrentCulture = newCulture;
            CultureInfo.DefaultThreadCurrentUICulture = newCulture;

            // Lưới an toàn cuối cùng: ghi log và báo lỗi thay vì để ứng dụng hiện hộp thoại crash.
            // Phải đăng ký trước khi tạo bất kỳ form nào.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new Font("Times New Roman", 12);
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultMenuFont = new Font("Times New Roman", 12);
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultPrintFont = new Font("Times New Roman", 12);

            Application.Run(new FrmMain());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            LoggerFacade.Error(e.Exception, "Program");
            MessageBox.Show(String.Format("Xuất hiện lỗi không mong muốn. Thao tác vừa rồi có thể chưa được thực hiện.\n\n{0}", e.Exception.Message),
                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Lỗi trên thread nền: tiến trình vẫn sẽ dừng, nhưng ít nhất lỗi được ghi vào log.
            Exception ex = e.ExceptionObject as Exception;

            if (ex != null)
            {
                LoggerFacade.Error(ex, "Program");
            }
        }
    }
}
