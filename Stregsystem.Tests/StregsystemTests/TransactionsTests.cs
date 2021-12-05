using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;
using Stregsystem.Exceptions;

namespace Stregsystem.Tests.StregsystemTests
{
    public class TransactionsTests
    {
        [Fact]
        public void GetTransactionsFromUser()
        {
            User u = new User("First Last", "test", "username@domain.dk", 12);
            var sts = new Stregsystem("test_log.csv", productPath: "products_test.csv");
            /*
            List<Transaction> expected = new List<Transaction>()
            {
                new InsertCashTransaction(u, DateTime.ParseExact("2021-12-05 15:27:50",
                    "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), 800f),
                new BuyTransaction(u, DateTime.ParseExact("2021-12-05 15:27:51",
                    "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), sts.GetProductById(4)),
                new BuyTransaction(u, DateTime.ParseExact("2021-12-05 15:27:52",
                    "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), sts.GetProductById(4)),
                new BuyTransaction(u, DateTime.ParseExact("2021-12-05 15:27:53",
                    "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), sts.GetProductById(4)),
                new InsertCashTransaction(u, DateTime.ParseExact("2021-12-05 15:27:54",
                    "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), 800f),
            };
            */
            Assert.True(CompareTransactions(new List<uint>() {5, 4, 3}, sts.GetTransactions(u, 3)));
        }
        
        [Fact]
        public void BuyProductWithInsuficientCredit()
        {
            User u = new User("First Last", "test", "username@domain.dk", 12);
            var sts = new Stregsystem("test_log2.csv", productPath: "products_test.csv");
            Assert.Throws<InsufficientCreditsException>(() => sts.BuyProduct(u, sts.GetProductById(4)));
        }
        
        [Fact]
        public void BuyProduct()
        {
            User u = new User("First Last", "test", "username@domain.dk", 12, 10000);
            var sts = new Stregsystem("test_log2.csv", productPath: "products_test.csv");
            sts.BuyProduct(u, sts.GetProductById(4));
            Assert.True(u.Balance < 10000);
        }
        
        [Fact]
        public void AddMoney()
        {
            User u = new User("First Last", "test", "username@domain.dk", 12);
            var sts = new Stregsystem("test_log2.csv", productPath: "products_test.csv");
            sts.AddCreditsToAccount(u, 100.0f);
            Assert.True(u.Balance == 100.0f);
        }
        
        private bool CompareTransactions(List<uint> expected, List<Transaction> actual) =>
            expected.All(x => actual.Any(y => y.Id == x)) &&
            actual.All(x => expected.Any(y => y == x.Id));
    }
}