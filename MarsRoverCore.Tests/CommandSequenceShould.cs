using FluentAssertions;
using MarsRoverCore.Commands;
using MarsRoverCore.Exceptions;
using System;
using System.Collections.Generic;
using Xunit;

namespace MarsRoverCore.Tests
{
    public class CommandSequenceShould
    {
        [Fact]
        public void Execute_A_Series_Of_Commands_From_Available_Command()
        {
            //Arrange
            var commandExecuted = false;
            var availableCommands = new List<Command>
            {
                new MoveCommand(() => commandExecuted = true)
            };
            var commandSequence = new CommandSequence("M", availableCommands);

            //Act
            commandSequence.Execute();

            //Assert
            commandExecuted.Should().BeTrue();
        }

        [Fact]
        public void Be_Invalid_When_The_Available_Commands_Are_Not_Set()
        {
            Action act = () => new CommandSequence("M", null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Be_Invalid_When_There_Is_No_Available_Commands()
        {
            Action act = () => new CommandSequence("M", new List<Command>());
            act.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        [InlineData("   ")]
        public void Be_Invalid_If_The_Command_Sequence_Keys_Are_Not_In_The_Good_Format(string sequence)
        {
            //Arrange
            var availableCommands = new List<Command>
            {
                new MoveCommand(() => { })
            };

            Action act = () => new CommandSequence(sequence, availableCommands);
            act.Should().Throw<CommandSequenceFormatException>();
        }

        [Fact]
        public void Be_Invalid_If_The_Command_Sequence_Keys_Does_Not_Match_Any_Of_The_Available_Commands()
        {
            //Arrange
            var availableCommands = new List<Command>
            {
                new MoveCommand(() => { })
            };

            Action act = () => new CommandSequence("abc", availableCommands);
            act.Should().Throw<UnrecognizedCommandException>();
        }
    }
}
