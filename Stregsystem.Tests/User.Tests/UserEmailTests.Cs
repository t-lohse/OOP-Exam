using System;
using Xunit;

namespace Stregsystem.Tests
{
    public class UserEmailTests
    {
        [Fact]
        public void EmailMissingAt()
        {
            Assert.Throws<InvalidEmailException>(() => new User("temp", "temp", "email_no_at", null));
        }
    }
}