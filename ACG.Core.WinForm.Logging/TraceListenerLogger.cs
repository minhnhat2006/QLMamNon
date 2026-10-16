using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ACG.Core.WinForm.Logging
{
    public class TraceListenerLogger : ILogger
    {
        public void Error(string message)
        {
            WriteEntry(message, "error");
        }

        public void Error(Exception ex)
        {
            // ToString() gồm cả InnerException và stack trace của từng lớp lỗi.
            WriteEntry(ex.ToString(), "error");
        }

        public void Warning(string message)
        {
            WriteEntry(message, "warning");
        }

        public void Info(string message)
        {
            WriteEntry(message, "info");
        }

        private void WriteEntry(string message, string type)
        {
            Trace.WriteLine(
                    string.Format("{0} {1} : {2}",
                                  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                  type,
                                  message));
        }
    }
}
