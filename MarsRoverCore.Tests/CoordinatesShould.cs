using FluentAssertions;
using System;
using Xunit;

namespace MarsRoverCore.Tests
{
    public class CoordinatesShould
    {
        [Fact]
        public void Have_An_X_That_Is_A_Positive_Integer()
        {
            Action act = () => new Coordinates(-1, 0);
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Have_A_Y_That_Is_A_Positive_Integer()
        {
            Action act = () => new Coordinates(0, -1);
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Display_Its_X_And_Y()
        {
            Coordinates coordinates = new Coordinates(0, 0);
            coordinates.ToString().Should().Be("0:0");
        }

        [Fact]
        public void Compare_With_Another_Coordinate()
        {
            Coordinates coordinates = new Coordinates(0, 0);
            coordinates.IsEqual(new Coordinates(0, 0)).Should().Be(true);
        }

        [Fact]
        public void Compare_With_Null_Coordinates_Throws_Exception()
        {
            Coordinates coordinates = new Coordinates(0, 0);
            Action act = (() => coordinates.IsEqual(null));
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
