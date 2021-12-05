using Xunit;
using Stregsystem.Exceptions;


namespace Stregsystem.Tests.ProductTests
{
    public class IdTests
    {
        [Fact]
        public void ZeroId()
        {
            Assert.Throws<InvalidIdException>(() =>
                new Product(0, "name", -18.9f));
            
        }
    }
}