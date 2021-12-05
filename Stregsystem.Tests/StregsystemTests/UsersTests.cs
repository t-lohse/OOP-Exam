using Xunit;
using System.Collections.Generic;
using Stregsystem.Exceptions;

namespace Stregsystem.Tests.StregsystemTests
{
    public class UsersTests
    {
        [Fact]
        public void SearchNonExistingUser()
        {
            var sts = new Stregsystem(productPath: "products_test.csv");
            Assert.Throws<InvalidUsernameException>(() => sts.GetUserByUsername("non-existing"));
        }
        
        [Fact]
        public void SearchExistingUser()
        {
            var sts = new Stregsystem(productPath: "products_test.csv");
            Assert.Equal(sts.GetUserByUsername("ndavo"), new User("Nancy Davolio", "ndavo", "ndavo@sample.stregsystem.dk", 1, 40000));
        }
        
        [Fact]
        public void SearchUsersWithNegativeMoney()
        {
            var sts = new Stregsystem(productPath: "products_test.csv");
            List<User> expected = new List<User>()
            {
                new User("Robert King", "rking", "rking@sample.stregsystem.dk", 7, -1000),
            };
            Assert.Equal(expected, sts.GetUsers(x => x.Balance < 0));
        }
    }
}