using System;

namespace MarsRoverCore.Commands
{
    public class MoveCommand : Command
    {
        public MoveCommand(Action action) : base(action, CommandMoveKey)
        {
        }
    }
}