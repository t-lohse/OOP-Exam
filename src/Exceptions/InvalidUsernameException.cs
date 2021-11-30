using System;

namespace Stregsystem
{
    ///<summary>Exception thrown when invalid username has been inputted.</summary>
    class InvalidUsernameException : Exception
    {
        public InvalidUsernameException() : base("Invalid Username") { }
    }
}
