using Xunit;
using Stregsystem.Exceptions;


namespace Stregsystem.Tests.UserTests
{
    public class EmailTests
    {
        [Fact]
        public void EmailMissingAt()
        {
            Assert.Throws<InvalidEmailException>(() => new User("temp", "temp", "email_no_at", null));
        }

        [Fact]
        public void EmailMissingDomain()
        {
            Assert.Throws<InvalidEmailException>(() =>
                new User("temp", "temp", "no_domain@", null));
        }
        
        [Fact]
        public void EmailMissingWrongFormatDomain()
        {
            Assert.Throws<InvalidEmailException>(() =>
                new User("temp", "temp", "no_domain@_domain.dk", null));
        }
        
        [Fact]
        public void EmailMissingWrongLetter()
        {
            Assert.Throws<InvalidEmailException>(() =>
                new User("temp", "temp", "no_do$main@domain.dk", null));
        }
    }
}