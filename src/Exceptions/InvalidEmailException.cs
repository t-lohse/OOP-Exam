using System;

namespace Stregsystem
{
    ///<summary>Exception thrown when invalid email has been inputted.</summary>
    class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base("Invalid email address") { }
    }
}
