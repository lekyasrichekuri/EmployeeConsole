using System;

namespace EmployeeConsole.PL.Exceptions
{
    public class InvalidAgeException : Exception
    { 
        public InvalidAgeException(string message) : base(message)
        {

        }
    }
}
