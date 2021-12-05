using System;

namespace Stregsystem.Exceptions
{
    ///<summary>Exception thrown when invalid email has been inputted.</summary>
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base("Invalid email address") { }
    }
}
