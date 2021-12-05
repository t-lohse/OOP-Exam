using Xunit;
using Stregsystem.Exceptions;
using Stregsystem;

namespace Stregsystem.Tests.UserTests
{
    public class UsernameTests
    {
        [Fact]
        public void UsernameIllegalChar()
        {
            Assert.Throws<InvalidUsernameException>(() => new User("first last", "u$ername", "name@domain.dk", null));
        }
    }
}