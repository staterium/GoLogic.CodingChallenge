using WebAPI.Modules.Products.Models;
using WebAPI.Modules.Users.Models;

namespace FunctionalTests.Tests
{
    [Collection("Http Client Collection")]
    public class UserAPI : TestBase
    {
        #region Constructors

        public UserAPI(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        #endregion

        #region Public Members

        [Fact]
        public async Task CanCreateUserAndDepositFunds()
        {
            //arrange 
            var testUser = "TestUser";
            var depositAmount = 15.8m;
            await ResetAndSetUpUserAsync(testUser, depositAmount);

            //act
            await Client.GetAsync($"/user/{testUser}");
            var user = await Client.GetFromJsonAsync<UserDto>($"/user/{testUser}");

            //assert
            user.Name.Should().Be(testUser);
            user.BalanceAvailable.Should().Be(depositAmount);
        }

        [Fact]
        public async Task CanMakeOnePurchaseAndReceiveTheCorrectProductAndTheCorrectChange()
        {
            //arrange 
            var testUser = "TestUser";
            var testProduct = "Snickers";
            var depositAmount = 3.8m;
            await ResetAndSetUpUserAsync(testUser, depositAmount);

            //act
            await MakePurchaseAsync(testProduct, testUser);
            var user = await Client.GetFromJsonAsync<UserDto>($"/user/{testUser}");
            var product = await Client.GetFromJsonAsync<ProductDto>($"/product/{testProduct}");
            var cashOut = await Client.GetFromJsonAsync<CashOutDto>($"/cashout/{testUser}");

            //assert
            user.BalanceAvailable.Should().Be(depositAmount - product.Price);
            product.QuantityAvailable.Should().Be(0);
            cashOut.Purchases.Count.Should().Be(1);
            cashOut.Purchases.First().ProductName.Should().Be(testProduct);
            cashOut.Purchases.First().Quantity.Should().Be(1);
            cashOut.Purchases.First().Price.Should().Be(product.Price);
            cashOut.Purchases.First().Total.Should().Be(product.Price);
            cashOut.ChangeReceived.Should().Be(depositAmount - product.Price);
            cashOut.TotalSpent.Should().Be(product.Price);
        }

        [Fact]
        public async Task CanMakeSeveralPurchasesAndReceiveTheCorrectProductsAndTheCorrectChange()
        {
            //arrange 
            var testUser = "TestUser";
            var testProduct1 = "KitKat";
            var testProduct2 = "Twix";
            var depositAmount = 8.8m;
            await ResetAndSetUpUserAsync(testUser, depositAmount);

            //act
            await MakePurchaseAsync(testProduct1, testUser);
            await MakePurchaseAsync(testProduct1, testUser);
            await MakePurchaseAsync(testProduct2, testUser);
            var user = await Client.GetFromJsonAsync<UserDto>($"/user/{testUser}");
            var product1 = await Client.GetFromJsonAsync<ProductDto>($"/product/{testProduct1}");
            var product2 = await Client.GetFromJsonAsync<ProductDto>($"/product/{testProduct2}");
            var cashOut = await Client.GetFromJsonAsync<CashOutDto>($"/cashout/{testUser}");

            //assert
            user.BalanceAvailable.Should().Be(depositAmount - product1.Price - product1.Price - product2.Price);
            product1.QuantityAvailable.Should().Be(3);
            product2.QuantityAvailable.Should().Be(2);
            cashOut.Purchases.Count.Should().Be(2);

            cashOut.Purchases[0].ProductName.Should().Be(testProduct1);
            cashOut.Purchases[0].Quantity.Should().Be(2);
            cashOut.Purchases[0].Price.Should().Be(product1.Price);
            cashOut.Purchases[0].Total.Should().Be(product1.Price * 2);

            cashOut.Purchases[1].ProductName.Should().Be(testProduct2);
            cashOut.Purchases[1].ProductName.Should().Be(testProduct2);
            cashOut.Purchases[1].Quantity.Should().Be(1);
            cashOut.Purchases[1].Price.Should().Be(product2.Price);
            cashOut.Purchases[1].Total.Should().Be(product2.Price);

            cashOut.ChangeReceived.Should().Be(depositAmount - product1.Price - product1.Price - product2.Price);
            cashOut.TotalSpent.Should().Be(product1.Price + product1.Price + product2.Price);
        }

        [Fact]
        public async Task ReturnsCorrectUserByName()
        {
            //arrange 
            var testUser = "TestUser";
            await ResetAndSetUpUserAsync(testUser);

            //act
            await Client.GetAsync($"/user/{testUser}");
            var user = await Client.GetFromJsonAsync<UserDto>($"/user/{testUser}");

            //assert
            user.Name.Should().Be(testUser);
        }

        #endregion
    }
}