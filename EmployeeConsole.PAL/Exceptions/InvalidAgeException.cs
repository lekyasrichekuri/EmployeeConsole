using System;

namespace EmployeeConsole.PAL.Exceptions
{
    public class InvalidAgeException : Exception
    { 
        public InvalidAgeException(string message) : base(message)
        {

        }
    }
}
