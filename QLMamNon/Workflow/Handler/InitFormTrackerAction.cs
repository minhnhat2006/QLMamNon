using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACG.Core.WinForm.Mediator;
using QLMamNon.Forms.HocSinh;
using QLMamNon.Facade;
using ACG.Core.WinForm.Util;
using System.Windows.Forms;


namespace QLMamNon.Workflow.Handler
{
    public class InitFormTrackerAction : ACG.Core.WinForm.Mediator.Action
    {
        public override void Receive(object from, object message)
        {
            ActivityTrackerFacade.InitActivityTracker(message as Form);
        }
    }
}
