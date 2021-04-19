using FluentAssertions;
using System;
using Xunit;

namespace MarsRoverCore.Tests
{
    public class DirectionShould
    {
        [Theory]
        [InlineData(Direction.North, Direction.East)]
        [InlineData(Direction.East, Direction.South)]
        [InlineData(Direction.South, Direction.West)]
        [InlineData(Direction.West, Direction.North)]
        public void Give_Next_Direction_To_The_Right(Direction direction, Direction expectedDirection)
        {
            direction.Right().Should().Be(expectedDirection);
        }

        [Theory]
        [InlineData(Direction.North, Direction.West)]
        [InlineData(Direction.West, Direction.South)]
        [InlineData(Direction.South, Direction.East)]
        [InlineData(Direction.East, Direction.North)]
        public void Give_Next_Direction_To_The_Left(Direction direction, Direction expectedDirection)
        {
            direction.Left().Should().Be(expectedDirection);
        }

        [Theory]
        [InlineData(Direction.North, 'N')]
        [InlineData(Direction.West, 'W')]
        [InlineData(Direction.South, 'S')]
        [InlineData(Direction.East, 'E')]
        public void Convert_To_Char(Direction direction, char expectedChar)
        {
            direction.ToChar().Should().Be(expectedChar);
        }

        [Fact]
        public void Throw_Exception_When_Converting_To_Char_Outside_Of_Direction_Value()
        {
            Action act = () => ((Direction)5).ToChar();
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(Direction.North, Direction.South)]
        [InlineData(Direction.South, Direction.North)]
        [InlineData(Direction.East, Direction.West)]
        [InlineData(Direction.West, Direction.East)]
        public void Give_Opposite_Direction(Direction direction, Direction expectedDirection)
        {
            direction.GetOppositeDirection().Should().Be(expectedDirection);
        }

        [Fact]
        public void Throw_Exception_When_Giving_Opposite_Direction_Outside_Of_Direction_Value()
        {
            Action act = () => ((Direction)5).GetOppositeDirection();
            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
