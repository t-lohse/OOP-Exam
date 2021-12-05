using System;
using Xunit;
using Stregsystem.Exceptions;
namespace Stregsystem.Tests.TransactionTests
{
    public class ExecuteTests
    {
        [Fact]
        public void InsertNegativeMoney()
        {
            var user = new User("first last", "username", "name@domain.dk", null);
            var expected = user.Balance;
            var trans = new InsertCashTransaction(user, DateTime.Now, -80f);
            trans.Execute();
            Assert.Equal(expected, user.Balance);
        }
        [Fact]
        public void AttemptPurchaseWithInsufficientMoney()
        {
            var user = new User("first last", "username", "name@domain.dk", null);
            var prod = new Product(10, "product", 100);
            var trans = new BuyTransaction(user, DateTime.Now, prod);
            Assert.Throws<InsufficientCreditsException>(trans.Execute);
            try
            {
                trans.Execute();
            }
            catch (InsufficientCreditsException e)
            {
                Assert.Equal(prod, e.Product);
                Assert.Equal(user, e.User);
            }
        }
        [Fact]
        public void SuccessfulPurchase()
        {
            var user = new User("first last", "username", "name@domain.dk", null, 200);
            var prod = new Product(10, "product", 100);
            var trans = new BuyTransaction(user, DateTime.Now, prod);
            trans.Execute();
            Assert.True(true);
            
        }
    }
}