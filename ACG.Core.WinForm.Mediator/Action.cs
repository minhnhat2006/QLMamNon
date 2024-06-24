using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACG.Core.WinForm.Mediator
{
    public abstract class Action
    {
        public string Id { get; set; }

        public IModuleMediator Mediator { get; set; }

        public abstract void Receive(object from, object message);
    }
}
