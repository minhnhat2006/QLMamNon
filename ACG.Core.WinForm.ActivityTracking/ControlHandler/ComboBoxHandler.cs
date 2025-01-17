﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ACG.Core.WinForm.Util;

namespace ACG.Core.WinForm.ActivityObserver.ControlHandler
{
    public class ComboBoxHandler : BaseControlHandler
    {
        public override void HandleControl(System.Windows.Forms.Control control)
        {
            if (isControlNeedTrack(control))
            {
                control.TextChanged += new EventHandler(onTextChanged);
            }
        }

        protected virtual bool isControlNeedTrack(Control c)
        {
            return (c is ComboBox);
        }

        protected virtual void onTextChanged(object sender, EventArgs e)
        {
            Control c = sender as Control;
            string controlName = ControlUtil.GetFullNameOfControl(c);

            foreach (object item in ActivityObservers)
            {
                (item as IActivityObserver).fire(controlName, c.Text, ActivityType.TextChanged);
            }
        }
    }
}
