using System;

namespace Stregsystem.Exceptions
{
    ///<summary>Exception thrown when invalid username has been inputted.</summary>
    public class InvalidUsernameException : Exception
    {
        public string Username { get;  }
        public InvalidUsernameException(string u) : base("Invalid Username") => Username = u;
    }
}
