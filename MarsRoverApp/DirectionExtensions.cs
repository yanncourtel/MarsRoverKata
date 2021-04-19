using System;

namespace MarsRoverCore
{
    public static class DirectionExtensions
    {
        internal const char DirectionNorthChar = 'N';
        internal const char DirectionSouthChar = 'S';
        internal const char DirectionEastChar = 'E';
        internal const char DirectionWestChar = 'W';

        public static Direction Right(this Direction direction)
        {
            var indexDirection = (int)direction;

            indexDirection++;

            if (indexDirection > Enum.GetValues(typeof(Direction)).Length - 1)
            {
                indexDirection = 0;
            }

            return (Direction)indexDirection;
        }

        public static Direction Left(this Direction direction)
        {
            var indexDirection = (int)direction;

            indexDirection--;

            if (indexDirection < 0)
            {
                indexDirection = Enum.GetValues(typeof(Direction)).Length - 1;
            }

            return (Direction)indexDirection;
        }

        public static char ToChar(this Direction direction)
        {
            return direction switch
            {
                Direction.North => DirectionNorthChar,
                Direction.East => DirectionEastChar,
                Direction.South => DirectionSouthChar,
                Direction.West => DirectionWestChar,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static Direction GetOppositeDirection(this Direction direction)
        {
            return direction switch
            {
                Direction.North => Direction.South,
                Direction.East => Direction.West,
                Direction.South => Direction.North,
                Direction.West => Direction.East,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}