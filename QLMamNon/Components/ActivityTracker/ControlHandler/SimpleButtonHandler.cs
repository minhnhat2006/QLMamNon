using System;
using System.Windows.Forms;
using ACG.Core.WinForm.ActivityObserver;
using ACG.Core.WinForm.ActivityObserver.ControlHandler;
using ACG.Core.WinForm.Util;
using DevExpress.XtraEditors;

namespace QLMamNon.Components.ActivityTracker.ControlHandler
{
    public class SimpleButtonHandler : ButtonHandler
    {
        public override void HandleControl(System.Windows.Forms.Control control)
        {
            if (isControlNeedTrack(control))
            {
                (control as SimpleButton).Click += new EventHandler(onClick);
            }
        }

        protected override bool isControlNeedTrack(Control c)
        {
            return (c is SimpleButton);
        }

        protected override void onClick(object sender, EventArgs e)
        {
            Control c = sender as Control;
            string controlName = ControlUtil.GetFullNameOfControl(c);

            foreach (object item in ActivityObservers)
            {
                (item as IActivityObserver).fire(controlName, sender, ActivityType.ButtonClick);
            }
        }
    }
}
