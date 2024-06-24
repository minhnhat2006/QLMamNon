using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACG.Core.WinForm.Mediator
{
    public interface IModuleMediator
    {
        void Subscribe(string channel, Action participant);

        void Broadcast(object form, object message);

        void Send(object form, string channel, object message);
    }
}
