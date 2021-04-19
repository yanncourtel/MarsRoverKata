using MarsRoverCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MarsRoverCore.Tests")]
namespace MarsRoverCore.Commands
{
    internal class CommandSequence
    {
        private readonly string _sequence;
        private readonly List<Command> _availableCommands;

        internal CommandSequence(string sequence, List<Command> availableCommands)
        {
            _availableCommands = availableCommands ?? throw new ArgumentNullException(nameof(availableCommands));

            if (!availableCommands.Any())
            {
                throw new ArgumentException("The list of available commands cannot be empty");
            }

            ValidationCommandSequence(sequence);

            _sequence = sequence;
        }

        internal void Execute()
        {
            foreach (var command in _sequence.Select(commandKey => _availableCommands.FirstOrDefault(command => command.Identifier == commandKey)))
            {
                command?.ExecuteCommand();
            }
        }

        private void ValidationCommandSequence(string commands)
        {
            if (string.IsNullOrWhiteSpace(commands))
            {
                throw new CommandSequenceFormatException();
            }

            foreach (var commandKey in commands.Where(commandKey => _availableCommands.All(command => command.Identifier != commandKey)))
            {
                throw new UnrecognizedCommandException(commandKey);
            }
        }
    }
}
