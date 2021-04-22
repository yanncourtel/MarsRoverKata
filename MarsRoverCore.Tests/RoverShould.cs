using FluentAssertions;
using MarsRoverCore.Exceptions;
using System;
using System.Collections.Generic;
using Xunit;

namespace MarsRoverCore.Tests
{
    public class RoverShould
    {
        private Rover _rover;

        public RoverShould()
        {
            _rover = new Rover(0, 0, Direction.North, new Map(10, 10));
        }

        [Fact]
        public void Have_An_Initial_Position()
        {
            _rover = new Rover(1, 1, Direction.North, new Map(10, 10));
            string currentPosition = _rover.ReportPosition();
            currentPosition.Should().Be("1:1:N");
        }

        [Fact]
        public void Have_A_Map()
        {
            Action act = () => _rover = new Rover(0, 0, Direction.North, null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throw_Exception_If_Commands_Are_Empty()
        {
            Action act = () => _rover.ExecuteCommand("");
            act.Should().Throw<CommandSequenceFormatException>();
        }

        [Fact]
        public void Throw_Exception_If_Commands_Are_Null()
        {
            Action act = () => _rover.ExecuteCommand(null);
            act.Should().Throw<CommandSequenceFormatException>();
        }

        [Fact]
        public void Throw_Exception_If_Commands_Are_Whitespace()
        {
            Action act = () => _rover.ExecuteCommand("  ");
            act.Should().Throw<CommandSequenceFormatException>();
        }

        [Fact]
        public void Throw_Exception_If_Command_Is_Unknown()
        {
            Action act = () => _rover.ExecuteCommand("ABC");
            act.Should().Throw<UnrecognizedCommandException>().Which.Message.Should().Contain("A");
        }

        [Fact]
        public void Abort_Sequence_And_Throw_Exception_If_Command_Sequence_Contains_Unknown_Command()
        {
            Action act = () => _rover.ExecuteCommand("MMABC");

            act.Should().Throw<UnrecognizedCommandException>();
            _rover.ReportPosition().Should().Be("0:0:N");
        }

        [Theory]
        [InlineData("R", "0:0:E")]
        public void Rotate_When_Given_R_Command(string commands, string expectedPosition)
        {
            var executionResult = _rover.ExecuteCommand(commands);
            executionResult.Position.Should().Be(expectedPosition);
        }

        [Theory]
        [InlineData("R", "0:0:E")]
        [InlineData("RR", "0:0:S")]
        [InlineData("RRR", "0:0:W")]
        [InlineData("RRL", "0:0:E")]
        [InlineData("RRRRRL", "0:0:N")]
        [InlineData("L", "0:0:W")]
        [InlineData("LL", "0:0:S")]
        [InlineData("LLLLLR", "0:0:N")]
        public void Rotate_When_Given_R_Or_L_Command(string commands, string expectedPosition)
        {
            var executionResult = _rover.ExecuteCommand(commands);
            executionResult.Position.Should().Be(expectedPosition);
        }

        [Theory]
        [InlineData("M", "0:1:N")]
        [InlineData("MM", "0:2:N")]
        [InlineData("RM", "1:0:E")]
        [InlineData("RMLLM", "0:0:W")]
        [InlineData("MMRRMM", "0:0:S")]
        [InlineData("RMMLM", "2:1:N")]
        public void Move_Towards_Direction_When_Given_M_Commands(string commands, string expectedPosition)
        {
            var executionResult = _rover.ExecuteCommand(commands);
            executionResult.Position.Should().Be(expectedPosition);
        }

        [Theory]
        [InlineData("MMMMMMMMMM", "0:0:N")]
        [InlineData("RMMMMMMMMMM", "0:0:E")]
        [InlineData("RRM", "0:9:S")]
        [InlineData("LM", "9:0:W")]
        public void Move_Position_At_The_EndOfGrid_Wraps_Around_The_Edges(string commands, string expectedPosition)
        {
            var executionResult = _rover.ExecuteCommand(commands);
            executionResult.Position.Should().Be(expectedPosition);
        }

        [Theory]
        [InlineData("B", "0:9:N")]
        [InlineData("BB", "0:8:N")]
        [InlineData("RB", "9:0:E")]
        [InlineData("RBLLB", "0:0:W")]
        [InlineData("BBRRBB", "0:0:S")]
        [InlineData("RBBLM", "8:1:N")]
        public void Move_Backwards_Direction_When_Given_B_Commands(string commands, string expectedPosition)
        {
            var executionResult = _rover.ExecuteCommand(commands);
            executionResult.Position.Should().Be(expectedPosition);
        }

        [Fact]
        public void Throw_Exception_If_List_Obstacles_Is_Null()
        {
            Action act = () => _rover = new Rover(0, 0, Direction.North, new Map(10, 10, null));
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("MR", "O:0:1:N", 0, 1)]
        [InlineData("B", "O:0:9:N", 0, 9)]
        public void Report_Previous_Position_When_Encountering_An_Obstacle(string commands, string expectedPosition, int obstacleX, int obstacleY)
        {
            var obstacles = new List<Coordinates>
            {
                new Coordinates(obstacleX, obstacleY)
            };
            var map = new Map(10, 10, obstacles);
            _rover = new Rover(0, 0, Direction.North, map);

            var executionResult = _rover.ExecuteCommand(commands);

            executionResult.ObstacleFound.Should().Be(true);
            executionResult.Position.Should().Be(expectedPosition);
        }

        [Theory]
        [InlineData("MR", 0, 1, "RM", "1:0:E")]
        public void Be_Able_To_Move_After_Encountering_An_Obstacle(string commands, int obstacleX, int obstacleY, string secondCommands, string expectedPosition)
        {
            var obstacles = new List<Coordinates>
            {
                new Coordinates(obstacleX, obstacleY)
            };
            var map = new Map(10, 10, obstacles);
            _rover = new Rover(0, 0, Direction.North, map);

            _ = _rover.ExecuteCommand(commands);

            var executionResult = _rover.ExecuteCommand(secondCommands);
            executionResult.Position.Should().Be(expectedPosition);
        }
    }
}
