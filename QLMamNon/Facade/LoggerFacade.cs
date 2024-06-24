using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ACG.Core.WinForm.Logging;

namespace QLMamNon.Facade
{
    public static class LoggerFacade
    {
        private static ILogger logger = new TraceListenerLogger();

        public static void Error(string message, string module)
        {
            logger.Error(string.Format("{0}-{1}", module, message));
        }

        public static void Error(Exception ex, string module)
        {
            logger.Error(ex);
        }

        public static void Warning(string message, string module)
        {
            logger.Warning(string.Format("{0}-{1}", module, message));
        }

        public static void Info(string message, string module)
        {
            logger.Info(string.Format("{0}-{1}", module, message));
        }

        public static void Info(string message)
        {
            logger.Info(message);
        }
    }
}
