using System;

namespace Stregsystem.Exceptions
{
    ///<summary>Exception thrown when invalid id has been provided.</summary>
    public class InvalidIdException : Exception
    {
        public InvalidIdException() : base("Invalid id.") { }
    }
}