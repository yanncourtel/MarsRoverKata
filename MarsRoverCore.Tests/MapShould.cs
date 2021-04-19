using FluentAssertions;
using MarsRoverCore.Exceptions;
using System;
using System.Collections.Generic;
using Xunit;

namespace MarsRoverCore.Tests
{
    public class MapShould
    {
        [Fact]
        public void Tell_Whether_There_Is_An_Obstacle_At_Coordinates()
        {
            var obstacleCoordinate = new Coordinates(1, 1);
            var map = new Map(10, 10, new List<Coordinates> { obstacleCoordinate });
            map.HasObstacleAt(obstacleCoordinate).Should().BeTrue();
        }

        [Fact]
        public void Throw_Exception_When_Coordinates_Are_Not_Set()
        {
            var obstacleCoordinate = new Coordinates(1, 1);
            var map = new Map(10, 10, new List<Coordinates> { obstacleCoordinate });
            Action actual = () => map.HasObstacleAt(null);
            actual.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(Direction.East, 0, 0, 1, 0)]
        [InlineData(Direction.North, 0, 0, 0, 1)]
        [InlineData(Direction.West, 0, 0, 9, 0)]
        [InlineData(Direction.South, 0, 0, 0, 9)]
        public void Show_Next_Coordinate_To(Direction directionTo, int initialX, int initialY, int expectedX, int expectedY)
        {
            var map = new Map(10, 10);
            var nextCoordinate = map.NextCoordinateTo(new Coordinates(initialX, initialY), directionTo);

            nextCoordinate.X.Should().Be(expectedX);
            nextCoordinate.Y.Should().Be(expectedY);
        }

        [Fact]
        public void Throw_Exception_When_Showing_Next_Coordinate_And_No_Coordinates_Are_Set()
        {
            var map = new Map(10, 10);
            Action actual = () => map.NextCoordinateTo(null, Direction.North);
            actual.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throw_Exception_When_Encoutering_An_Obstacle_While_Showing_Next_Coordinates_To()
        {
            var obstacleCoordinate = new Coordinates(0, 1);
            var map = new Map(10, 10, new List<Coordinates> { obstacleCoordinate });
            Action act = () => map.NextCoordinateTo(new Coordinates(0, 0), Direction.North);

            act.Should().Throw<ObstacleFoundException>().Which.NewCoordinates.Should().BeEquivalentTo(obstacleCoordinate);
        }
    }
}
