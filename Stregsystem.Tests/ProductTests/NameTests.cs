using Xunit;
using Stregsystem.Exceptions;


namespace Stregsystem.Tests.ProductTests
{
    public class NameTests
    {
        [Fact]
        public void EmptyName()
        {
            Assert.Throws<InvalidNameException>(() =>
                new Product(1, "", -18.9f));
        }
        
        [Fact]
        public void WhitespaceName()
        {
            Assert.Throws<InvalidNameException>(() =>
                new Product(1, "    ", -18.9f));
        }
    }
}