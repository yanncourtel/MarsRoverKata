using System;

namespace MarsRoverCore.Commands
{
    public class RotateLeftCommand : Command
    {
        public RotateLeftCommand(Action action) : base(action, CommandRotateLeftKey)
        {
        }
    }
}