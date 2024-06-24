using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLMamNon.Components.Command.QLMNDao
{
    public abstract class QLMNDaoCommand
    {
        protected QLMNDaoCommand nextExecutedCommand;

        public CommandParameter CommandParameter { get; set; }

        public QLMNDaoCommand(QLMNDaoCommand nextExecutedCommand)
        {
            this.nextExecutedCommand = nextExecutedCommand;
            this.CommandParameter = new CommandParameter();
        }

        public QLMNDaoCommand()
        {
            this.CommandParameter = new CommandParameter();
        }

        public virtual void Execute()
        {
            this.onExecute();

            if (this.nextExecutedCommand != null)
            {
                this.nextExecutedCommand.Execute();
            }
        }

        protected abstract void onExecute();
    }
}
