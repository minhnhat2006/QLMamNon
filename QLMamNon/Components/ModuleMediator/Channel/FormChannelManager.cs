using QLMamNon.Workflow;
using QLMamNon.Workflow.Handler;
using QLMamNon.Facade;

namespace QLMamNon.Components.ModuleMediator.Channel
{
    public class FormChannelManager : IChannelManager
    {
        public void InitSubscribers(object argument)
        {
            ModuleMediatorFacade.Subscribe(WorkFlowActions.InitFormTracker, new InitFormTrackerAction());
            ModuleMediatorFacade.Subscribe(WorkFlowActions.AdjustPopupEditForm, new AdjustPopupEditFormAction());
        }
    }
}
