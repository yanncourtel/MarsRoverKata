using System;

namespace MarsRoverCore.Commands
{
    public abstract class Command
    {
        protected const char CommandRotateRightKey = 'R';
        protected const char CommandRotateLeftKey = 'L';
        protected const char CommandMoveKey = 'M';
        protected const char CommandMoveBackwardsKey = 'B';

        protected Command(Action action, char identifier)
        {
            Action = action;
            Identifier = identifier;
        }

        public char Identifier { get; set; }

        public Action Action { get; set; }

        public void ExecuteCommand()
        {
            Action.Invoke();
        }
    }
}