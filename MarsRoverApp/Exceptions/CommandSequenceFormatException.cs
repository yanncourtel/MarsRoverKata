using System;

namespace MarsRoverCore.Exceptions
{
    public class CommandSequenceFormatException : Exception
    {
        public CommandSequenceFormatException() : base("The sequence of commands has an invalid format. (Possibly null or empty sequence)")
        {

        }
    }
}