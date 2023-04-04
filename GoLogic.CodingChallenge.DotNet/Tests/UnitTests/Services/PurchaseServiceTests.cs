using Core.Entities;
using Core.Exceptions;
using Core.Services;
using FluentAssertions;

namespace UnitTests.Services
{
    public class PurchaseServiceTests
    {
        #region Public Members

        [Fact]
        public void PurchaseProduct_ReturnsPurchaseEntity_WhenProductQuantityIsGreaterThanZero()
        {
            //arrange
            var user = new User("TestUser");
            var purchaseService = new PurchaseService();
            var product = new Product("TestProduct", "A1", 2);

            //act
            var purchase = purchaseService.PurchaseProduct(product, user);

            //assert
            purchase.Should().NotBeNull();
            purchase.ProductName.Should().Be(product.Name);
            purchase.UserName.Should().Be(user.Name);
        }

        [Fact]
        public void PurchaseProduct_ThrowsException_WhenProductQuantityIsLessThanZero()
        {
            //arrange
            var user = new User("TestUser");
            var purchaseService = new PurchaseService();
            var product = new Product("TestProduct", "A1", 0);

            //act
            product.QuantityAvailable = -1;

            //assert
            Assert.Throws<ProductUnavailableException>(() => purchaseService.PurchaseProduct(product, user));
        }

        [Fact]
        public void PurchaseProduct_ThrowsException_WhenProductQuantityIsZero()
        {
            //arrange
            var user = new User("TestUser");
            var purchaseService = new PurchaseService();
            var product = new Product("TestProduct", "A1", 0);

            //assert
            Assert.Throws<ProductUnavailableException>(() => purchaseService.PurchaseProduct(product, user));
        }

        #endregion
    }
}