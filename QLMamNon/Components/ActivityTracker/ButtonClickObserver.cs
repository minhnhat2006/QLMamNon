using ACG.Core.WinForm.ActivityObserver;
using QLMamNon.Facade;

namespace QLMamNon.Components.ActivityTracker
{
    public class ButtonClickObserver : IActivityObserver
    {
        public void fire(string objectId, ActivityType activityType)
        {
            if (activityType == ActivityType.ButtonClick)
            {
                ModuleMediatorFacade.Invoke(null, objectId, null);
            }
        }

        public void fire(string objectId, object value, ActivityType activityType)
        {
            if (activityType == ActivityType.ButtonClick)
            {
                ModuleMediatorFacade.Invoke(null, objectId, value);
            }
        }
    }
}
