using System;
using System.Collections.Generic;
using System.Linq;
using MarsRoverCore.Exceptions;

namespace MarsRoverCore
{
    public class Map : IMap
    {
        private readonly int _width;
        private readonly int _height;
        private readonly List<Coordinates> _obstaclesCoordinatesList;

        public Map(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public Map(int width, int height, List<Coordinates> obstaclesCoordinates)
        {
            _width = width;
            _height = height;
            _obstaclesCoordinatesList = obstaclesCoordinates ?? throw new ArgumentNullException(nameof(obstaclesCoordinates));
        }

        public bool HasObstacleAt(Coordinates coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            return _obstaclesCoordinatesList != null && _obstaclesCoordinatesList.Any(c => c.IsEqual(coordinates));
        }

        public Coordinates NextCoordinateTo(Coordinates coordinates, Direction direction)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            var nextCoordinates = direction switch
            {
                Direction.North => new Coordinates(coordinates.X, coordinates.Y == _height - 1 ? 0 : coordinates.Y + 1),
                Direction.East => new Coordinates(coordinates.X == _width - 1 ? 0 : coordinates.X + 1, coordinates.Y),
                Direction.South => new Coordinates(coordinates.X, coordinates.Y == 0 ? _height - 1 : coordinates.Y - 1),
                Direction.West => new Coordinates(coordinates.X == 0 ? _width - 1 : coordinates.X - 1, coordinates.Y),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

            if (HasObstacleAt(nextCoordinates))
            {
                throw new ObstacleFoundException(nextCoordinates);
            }

            return nextCoordinates;
        }
    }
}