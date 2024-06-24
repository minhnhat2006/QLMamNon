using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ACG.Core.WinForm.Util;
using System.Collections;
using ACG.Core.WinForm.ActivityObserver.ControlHandler;
using DevExpress.XtraEditors;

namespace QLMamNon.Components.ActivityTracker.ControlHandler
{
    public class TextEditHandler : TextBoxHandler
    {
        protected override bool isControlNeedTrack(Control c)
        {
            return (c is TextEdit);
        }
    }
}
