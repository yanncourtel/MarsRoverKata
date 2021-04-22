using MarsRoverCore.Commands;
using MarsRoverCore.Exceptions;
using System;
using System.Collections.Generic;

namespace MarsRoverCore
{
    public class Rover
    {
        private readonly IMap _map;
        private readonly List<Command> _availableCommands;
        private Coordinates _currentCoordinates;
        private Direction _currentDirection;

        public Rover(int initialX, int initialY, Direction initialDirection, IMap map)
        {
            _map = map ?? throw new ArgumentNullException(nameof(map));
            _currentCoordinates = new Coordinates(initialX, initialY);
            _currentDirection = initialDirection;
            _availableCommands = new List<Command>
            {
                new MoveCommand(() => MoveToNewCoordinates(_map.NextCoordinateTo(_currentCoordinates, _currentDirection))),
                new MoveBackwardsCommand(() => MoveToNewCoordinates(_map.NextCoordinateTo(_currentCoordinates, _currentDirection.GetOppositeDirection()))),
                new RotateLeftCommand(() => _currentDirection = _currentDirection.Left()),
                new RotateRightCommand(() => _currentDirection = _currentDirection.Right())
            };
        }

        public ExecutionResult ExecuteCommand(string commands)
        {
            try
            {
                var commandSequence = new CommandSequence(commands, _availableCommands);
                commandSequence.Execute();
            }
            catch (ObstacleFoundException e)
            {
                return new ExecutionResult(ReportObstacleAt(e.NewCoordinates), true);
            }

            return new ExecutionResult(ReportPosition());
        }

        public string ReportPosition()
        {
            return $"{_currentCoordinates}" + ":" + _currentDirection.ToChar();
        }

        public string ReportObstacleAt(Coordinates obstacleCoordinates)
        {
            return $"O:{obstacleCoordinates}" + ":" + _currentDirection.ToChar();
        }

        private void MoveToNewCoordinates(Coordinates newCoordinates)
        {
            _currentCoordinates = newCoordinates;
        }
    }
}