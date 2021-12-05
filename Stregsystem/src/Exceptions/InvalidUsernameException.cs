using System;

namespace Stregsystem.Exceptions
{
    ///<summary>Exception thrown when invalid username has been inputted.</summary>
    public class InvalidUsernameException : Exception
    {
        public InvalidUsernameException() : base("Invalid Username") { }
    }
}
