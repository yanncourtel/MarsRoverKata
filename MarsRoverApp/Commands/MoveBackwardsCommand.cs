using System;

namespace MarsRoverCore.Commands
{
    public class MoveBackwardsCommand : Command
    {
        public MoveBackwardsCommand(Action action) : base(action, CommandMoveBackwardsKey)
        {
        }
    }
}