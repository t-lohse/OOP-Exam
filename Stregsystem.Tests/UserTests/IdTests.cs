using Xunit;

namespace Stregsystem.Tests.UserTests
{
    public class IdTests
    {
        [Fact]
        public void NullId()
        {
            var actual = new User("first last", "username", "name@domain.dk", null);
            Assert.Equal(1u, actual.Id);
        }
        
        [Fact]
        public void NullAfterExplicitId()
        {
            new User("first last", "username", "name@domain.dk", 900);
            var actual = new User("first last", "username", "name@domain.dk", null);
            Assert.Equal(901u, actual.Id);
        }
        
        [Fact]
        public void LowToHigh()
        {
            new User("first last", "username", "name@domain.dk", 900);
            var actual = new User("first last", "username", "name@domain.dk", 400);
            Assert.Equal(400u, actual.Id);
        }
    }
}