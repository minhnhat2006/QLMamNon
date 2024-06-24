using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ACG.Core.WinForm.ActivityObserver;
using QLMamNon.Components.ActivityTracker;

namespace QLMamNon.Facade
{
    public static class ActivityTrackerFacade
    {
        private static IActivityTracker activityTracker = new DEActivityTracker();

        public static void InitActivityTracker(Form formMain)
        {
            activityTracker.addObserver(formMain.Name, new LoggingObserver());
            activityTracker.InitTracker(formMain);
        }
    }
}
