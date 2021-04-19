using System;

namespace MarsRoverCore
{
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinates(int x, int y)
        {
            ValidateCoordinate(x);
            ValidateCoordinate(y);

            X = x;
            Y = y;
        }

        private static void ValidateCoordinate(int coordinate)
        {
            if (coordinate < 0)
            {
                throw new ArgumentException(nameof(coordinate));
            }
        }

        public override string ToString()
        {
            return $"{X}:{Y}";
        }

        public bool IsEqual(Coordinates coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            return coordinates.X == X && coordinates.Y == Y;
        }
    }
}
