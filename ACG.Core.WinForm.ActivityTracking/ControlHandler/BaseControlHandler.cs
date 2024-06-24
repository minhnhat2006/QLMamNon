using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace ACG.Core.WinForm.ActivityObserver.ControlHandler
{
    public abstract class BaseControlHandler
    {
        public Queue ActivityObservers { get; set; }

        public abstract void HandleControl(Control control);
    }
}
