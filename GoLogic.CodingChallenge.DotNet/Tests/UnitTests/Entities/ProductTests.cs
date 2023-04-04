using Core.Entities;

namespace UnitTests.Entities
{
    public class ProductTests
    {
        #region Public Members

        [Fact]
        public void Quantity_Cannot_BeLessThanZero()
        {
            //assert
            Assert.Throws<ArgumentException>(() => new Product("TestProduct", "A1", -1));
        }

        #endregion
    }
}