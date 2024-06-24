using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACG.Core.WinForm.ActivityObserver
{
    public interface IActivityTracker
    {
        void InitTracker(Form form);

        void addObserver(string key, IActivityObserver observer);
    }
}
