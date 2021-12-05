using System;

namespace Stregsystem.Exceptions
{   
    ///<summary>Exception thrown when invalid name (full name) has been inputted.</summary>
    public class InvalidNameException : Exception
    {
        public InvalidNameException() : base("Full name is required.") { }
    }
}
