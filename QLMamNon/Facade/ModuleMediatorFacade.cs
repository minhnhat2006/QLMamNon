using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACG.Core.WinForm.Mediator;

namespace QLMamNon.Facade
{
    public class ModuleMediatorFacade
    {
        private static IModuleMediator moduleMediator = new ACG.Core.WinForm.Mediator.ModuleMediator();

        public static void Subscribe(string channel, ACG.Core.WinForm.Mediator.Action participant)
        {
            moduleMediator.Subscribe(channel, participant);
        }

        public static void Invoke(object from, string channel, object message)
        {
            moduleMediator.Send(from, channel, message);
        }
    }
}
