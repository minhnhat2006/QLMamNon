using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACG.Core.WinForm.ActivityObserver;
using QLMamNon.Facade;

namespace QLMamNon.Components.ActivityTracker
{
    public class LoggingObserver : IActivityObserver
    {
        public void fire(string objectId, object value, ActivityType activityType)
        {
            if (activityType == ActivityType.TextChanged)
            {
                LoggerFacade.Info(string.Format("Text changed {0} {1}", objectId, value));
            }
        }

        public void fire(string objectId, ActivityType activityType)
        {
            throw new NotImplementedException();
        }
    }
}
