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
        public void PurchaseProduct_DecreasesStockLevel_ByOne()
        {
            //arrange
            var purchaseService = new PurchaseService();
            var user = GetTestUser(15m);
            var product = GetTestProduct(2);

            //act
            var purchase = purchaseService.PurchaseProduct(product, user);

            //assert
            purchase.Should().NotBeNull();
            product.QuantityAvailable.Should().Be(1);
        }

        [Fact]
        public void PurchaseProduct_DecreasesUserBalance_ByPriceOfProduct()
        {
            //arrange
            var purchaseService = new PurchaseService();
            var user = GetTestUser(15m);
            var product = GetTestProduct(2);

            //act
            var purchase = purchaseService.PurchaseProduct(product, user);

            //assert
            user.BalanceAvailable.Should().Be(10m);
        }

        [Fact]
        public void PurchaseProduct_ReturnsPurchaseEntity_WhenProductQuantityIsGreaterThanZero()
        {
            //arrange
            var purchaseService = new PurchaseService();
            var user = GetTestUser(15m);
            var product = GetTestProduct(2);

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
            var purchaseService = new PurchaseService();
            var user = GetTestUser(15m);
            var product = GetTestProduct(0);

            //act
            product.QuantityAvailable = -1;

            //assert
            Assert.Throws<ProductUnavailableException>(() => purchaseService.PurchaseProduct(product, user));
        }

        [Fact]
        public void PurchaseProduct_ThrowsException_WhenProductQuantityIsZero()
        {
            //arrange
            var purchaseService = new PurchaseService();
            var user = GetTestUser(15m);
            var product = GetTestProduct(0);

            //assert
            Assert.Throws<ProductUnavailableException>(() => purchaseService.PurchaseProduct(product, user));
        }

        [Fact]
        public void PurchaseProduct_ThrowsException_WhenUserBalanceIsLessThanPriceOfProduct()
        {
            //arrange
            var purchaseService = new PurchaseService();
            var user = GetTestUser(1m);
            var product = GetTestProduct(5);

            //assert
            Assert.Throws<InsufficientFundsException>(() => purchaseService.PurchaseProduct(product, user));
        }

        #endregion

        #region Private Members

        private static Product GetTestProduct(int quantityAvailable)
        {
            return new Product("TestProduct", "A1", 5m, quantityAvailable);
        }

        private User GetTestUser(decimal balanceAvailable)
        {
            return new User("TestUser", balanceAvailable);
        }

        #endregion
    }
}