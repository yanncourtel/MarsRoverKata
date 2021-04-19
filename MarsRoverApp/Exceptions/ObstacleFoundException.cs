using System;

namespace MarsRoverCore.Exceptions
{
    public class ObstacleFoundException : Exception
    {
        public Coordinates NewCoordinates { get; }

        public ObstacleFoundException(Coordinates newCoordinates)
        {
            NewCoordinates = newCoordinates;
        }
    }
}