using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACG.Core.WinForm.Logging
{
    public interface ILogger
    {
        void Error(string message);

        void Error(Exception ex);

        void Warning(string message);

        void Info(string message);
    }
}
