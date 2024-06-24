using System;
using System.Collections;
using System.Windows.Forms;
using ACG.Core.WinForm.Util;
using System.Collections.Generic;
using ACG.Core.WinForm.ActivityObserver.ControlHandler;

namespace ACG.Core.WinForm.ActivityObserver
{
    public abstract class BaseActivityTracker : IActivityTracker
    {
        private IList<string> controlNames = new List<string>();

        private IDictionary<string, Queue> queues = new Dictionary<string, Queue>();

        protected IList<BaseControlHandler> controlHandlers = new List<BaseControlHandler>();

        public void InitTracker(Form form)
        {
            this.initControlHandlers();
            this.initTracker(form as Control);
        }

        protected virtual void initControlHandlers()
        {
            controlHandlers.Add(new TextBoxHandler());
            controlHandlers.Add(new ComboBoxHandler());
            controlHandlers.Add(new DateTimePickerHandler());
            controlHandlers.Add(new ButtonHandler());
        }

        protected void initTracker(Control control)
        {
            if (control == null)
            {
                return;
            }

            foreach (Control c in control.Controls)
            {
                string controlName = ControlUtil.GetFullNameOfControl(c);

                if (controlNames.Contains(controlName))
                {
                    continue;
                }

                controlNames.Add(controlName);

                Form form = ControlUtil.GetForm(c);
                Queue queue = queues[form.Name];

                foreach (BaseControlHandler controlHandler in controlHandlers)
                {
                    controlHandler.ActivityObservers = queue;
                    controlHandler.HandleControl(c);
                }

                this.initTracker(c);
            }
        }

        public void addObserver(string key, IActivityObserver observer)
        {
            getQueue(key).Enqueue(observer);
        }

        private Queue getQueue(string key)
        {
            if (!queues.ContainsKey(key))
            {
                queues.Add(key, new Queue());
            }

            return queues[key];
        }
    }
}
