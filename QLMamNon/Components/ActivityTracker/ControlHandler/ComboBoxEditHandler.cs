using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ACG.Core.WinForm.Util;
using DevExpress.XtraEditors;
using ACG.Core.WinForm.ActivityObserver.ControlHandler;

namespace QLMamNon.Components.ActivityTracker.ControlHandler
{
    public class ComboBoxEditHandler : ComboBoxHandler
    {
        protected override bool isControlNeedTrack(Control c)
        {
            return (c is ComboBoxEdit);
        }
    }
}
