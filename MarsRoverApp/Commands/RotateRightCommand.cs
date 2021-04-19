using System;

namespace MarsRoverCore.Commands
{
    public class RotateRightCommand : Command
    {
        public RotateRightCommand(Action action) : base(action, CommandRotateRightKey)
        {
        }
    }
}