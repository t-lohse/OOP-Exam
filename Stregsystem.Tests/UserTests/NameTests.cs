using Xunit;
using Stregsystem.Exceptions;


namespace Stregsystem.Tests.UserTests
{
    public class NameTests
    {
        [Fact]
        public void MissingFullName()
        {
            Assert.Throws<InvalidNameException>(() => new User("first", "username", "name@domain.dk", null));
        }

        [Fact]
        public void NameOk()
        {
            Assert.NotNull(new User("first last", "username", "user@domain.dk", null));
        }
    }
}