using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACG.Core.WinForm.Mediator
{
    public class ModuleMediator : IModuleMediator
    {
        private IDictionary<object, IList<Action>> participants = new Dictionary<object, IList<Action>>();

        public void Subscribe(string channel, Action participant)
        {
            if (!participants.ContainsKey(channel))
            {
                participants.Add(channel, new List<Action>());
            }

            participants[channel].Add(participant);
            participant.Mediator = this;
        }

        public void Broadcast(object from, object message)
        {
            foreach (Action participant in participants.Values)
            {
                participant.Receive(from, message);
            }
        }

        public void Send(object from, string channel, object message)
        {
            foreach (Action participant in participants[channel])
            {
                participant.Receive(from, message);
            }
        }
    }
}
