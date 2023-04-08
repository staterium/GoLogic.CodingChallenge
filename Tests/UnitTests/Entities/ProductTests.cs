using Core.Entities;

namespace UnitTests.Entities
{
    public class ProductTests
    {
        #region Public Members

        [Fact]
        public void Price_Cannot_BeZeroOrLexx()
        {
            //assert
            Assert.Throws<ArgumentException>(() => new Product("TestProduct", "A1", 0m, 15));
            Assert.Throws<ArgumentException>(() => new Product("TestProduct", "A1", -1m, 15));
        }

        [Fact]
        public void Quantity_Cannot_BeLessThanZero()
        {
            //assert
            Assert.Throws<ArgumentException>(() => new Product("TestProduct", "A1", 5m, -1));
        }

        #endregion
    }
}