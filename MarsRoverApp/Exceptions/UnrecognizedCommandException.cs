using System;

namespace MarsRoverCore.Exceptions
{
    public class UnrecognizedCommandException : Exception
    {
        public UnrecognizedCommandException(char commandKey) : base($"The command {commandKey} is not recognized")
        {
        }
    }
}