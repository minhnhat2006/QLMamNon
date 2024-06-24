using System;
using System.Windows.Forms;
using ACG.Core.WinForm.Util;

namespace ACG.Core.WinForm.ActivityObserver.ControlHandler
{
    public class ButtonHandler : BaseControlHandler
    {
        public override void HandleControl(System.Windows.Forms.Control control)
        {
            if (isControlNeedTrack(control))
            {
                (control as Button).Click += new EventHandler(onClick);
            }
        }

        protected virtual bool isControlNeedTrack(Control c)
        {
            return (c is Button);
        }

        protected virtual void onClick(object sender, EventArgs e)
        {
            Control c = sender as Control;
            string controlName = ControlUtil.GetFullNameOfControl(c);

            foreach (object item in ActivityObservers)
            {
                (item as IActivityObserver).fire(controlName, ActivityType.ButtonClick);
            }
        }
    }
}
