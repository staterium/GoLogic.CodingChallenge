using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Services;
using FakeItEasy;
using FluentAssertions;

namespace UnitTests.Services
{
    public class PurchaseServiceTests
    {
        #region Public Members

        [Fact]
        public async Task PurchaseProduct_CallsSaveNewPurchase_OnSuccess()
        {
            //arrange
            var purchaseService = SetupPurchaseService(out _, out _, out var fakePurchaseRepository);
            var user = GetTestUser(15m);
            var product = GetTestProduct(2);

            //act
            var purchase = await purchaseService.PurchaseProductAsync(product, user);

            //assert
            A.CallTo(() => fakePurchaseRepository.SaveNewPurchaseAsync(purchase)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task PurchaseProduct_CallsUpdateProduct_OnSuccess()
        {
            //arrange
            var purchaseService = SetupPurchaseService(out _, out var fakeProductRepository, out _);
            var user = GetTestUser(15m);
            var product = GetTestProduct(2);

            //act
            await purchaseService.PurchaseProductAsync(product, user);

            //assert
            A.CallTo(() => fakeProductRepository.UpdateProductAsync(product)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task PurchaseProduct_CallsUpdateUser_OnSuccess()
        {
            //arrange
            var purchaseService = SetupPurchaseService(out var fakeUserRepository, out _, out _);
            var user = GetTestUser(15m);
            var product = GetTestProduct(2);

            //act
            await purchaseService.PurchaseProductAsync(product, user);

            //assert
            A.CallTo(() => fakeUserRepository.UpdateUserAsync(user)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task PurchaseProduct_DecreasesStockLevel_ByOne()
        {
            //arrange
            var purchaseService = SetupPurchaseService(out _, out _, out _);
            var user = GetTestUser(15m);
            var product = GetTestProduct(2);

            //act
            var purchase = await purchaseService.PurchaseProductAsync(product, user);

            //assert
            purchase.Should().NotBeNull();
            product.QuantityAvailable.Should().Be(1);
        }

        [Fact]
        public async Task PurchaseProduct_DecreasesUserBalance_ByPriceOfProduct()
        {
            //arrange
            var purchaseService = SetupPurchaseService(out _, out _, out _);
            var user = GetTestUser(15m);
            var product = GetTestProduct(2);

            //act
            var purchase = await purchaseService.PurchaseProductAsync(product, user);

            //assert
            user.BalanceAvailable.Should().Be(10m);
        }

        [Fact]
        public async Task PurchaseProduct_ReturnsPurchaseEntity_WhenProductQuantityIsGreaterThanZero()
        {
            //arrange
            var purchaseService = SetupPurchaseService(out _, out _, out _);
            var user = GetTestUser(15m);
            var product = GetTestProduct(2);

            //act
            var purchase = await purchaseService.PurchaseProductAsync(product, user);

            //assert
            purchase.Should().NotBeNull();
            purchase.ProductName.Should().Be(product.Name);
            purchase.UserName.Should().Be(user.Name);
        }

        [Fact]
        public async Task PurchaseProduct_ThrowsException_WhenProductQuantityIsLessThanZero()
        {
            //arrange
            var purchaseService = SetupPurchaseService(out _, out _, out _);
            var user = GetTestUser(15m);
            var product = GetTestProduct(0);

            //act
            product.QuantityAvailable = -1;

            //assert
            await Assert.ThrowsAsync<ProductUnavailableException>(async () => await purchaseService.PurchaseProductAsync(product, user));
        }

        [Fact]
        public async Task PurchaseProduct_ThrowsException_WhenProductQuantityIsZero()
        {
            //arrange
            var purchaseService = SetupPurchaseService(out _, out _, out _);
            var user = GetTestUser(15m);
            var product = GetTestProduct(0);

            //assert
            await Assert.ThrowsAsync<ProductUnavailableException>(async () => await purchaseService.PurchaseProductAsync(product, user));
        }

        [Fact]
        public async Task PurchaseProduct_ThrowsException_WhenUserBalanceIsLessThanPriceOfProduct()
        {
            //arrange
            var purchaseService = SetupPurchaseService(out _, out _, out _);
            var user = GetTestUser(1m);
            var product = GetTestProduct(5);

            //assert
            await Assert.ThrowsAsync<InsufficientFundsException>(async () => await purchaseService.PurchaseProductAsync(product, user));
        }

        #endregion

        #region Private Members

        private static Product GetTestProduct(int quantityAvailable)
        {
            return new Product("TestProduct", "A1", 5m, quantityAvailable);
        }

        private static User GetTestUser(decimal balanceAvailable)
        {
            return new User("TestUser", balanceAvailable);
        }

        private static IPurchaseService SetupPurchaseService(out IUserRepository fakeUserRepository,
            out IProductRepository fakeProductRepository,
            out IPurchaseRepository fakePurchaseRepository)
        {
            fakeUserRepository = A.Fake<IUserRepository>();
            fakeProductRepository = A.Fake<IProductRepository>();
            fakePurchaseRepository = A.Fake<IPurchaseRepository>();

            return new PurchaseService(fakePurchaseRepository, fakeUserRepository, fakeProductRepository);
        }

        #endregion
    }
}