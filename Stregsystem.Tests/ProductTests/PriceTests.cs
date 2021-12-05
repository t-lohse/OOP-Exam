using Xunit;

namespace Stregsystem.Tests.ProductTests
{
    public class PriceTests
    {
        [Fact]
        public void NegativePrice()
        {
            Assert.Equal(0f, new global::Stregsystem.Product(1, "name", -18.9f).Price);
        }
        
        [Fact]
        public void ReAssignPrice()
        {
            var actual = new Product(1, "name", 200f);
            float expected = 20f;
            actual.Price = expected;
            Assert.Equal(expected, actual.Price);
        }
    }
}