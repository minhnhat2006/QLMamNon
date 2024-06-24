using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACG.Core.WinForm.ActivityObserver
{
    public interface IActivityObserver
    {
        void fire(string objectId, ActivityType activityType);

        void fire(string objectId, object value, ActivityType activityType);
    }

    public enum ActivityType
    {
        TextChanged, ButtonClick
    }
}
