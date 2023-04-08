using System.Net;
using WebAPI.Modules.Products.Models;
using WebAPI.Modules.Purchases.Models;
using WebAPI.Modules.Users.Models;

namespace FunctionalTests.Tests
{
    [Collection("Http Client Collection")]
    public class PurchaseAPI : TestBase
    {
        #region Constructors

        public PurchaseAPI(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        #endregion

        #region Public Members

        [Fact]
        public async Task PerformsCorrectPurchaseWhenUserHasSufficientFundsAndProductIsAvailable()
        {
            //arrange 
            var testUser = "TestUser";
            var testProduct = "Mars";
            var depositAmount = 15.8m;
            await ResetAndSetUpUserAsync(testUser, depositAmount);

            //act
            var productToBuy = await Client.GetFromJsonAsync<ProductDto>($"/product/{testProduct}");
            await Client.PostAsJsonAsync("/purchase", new MakePurchaseDto { ProductName = testProduct, UserName = testUser });
            var user = await Client.GetFromJsonAsync<UserDto>($"/user/{testUser}");
            var purchases = await Client.GetFromJsonAsync<List<GroupedPurchaseDto>>($"/purchases/{testUser}");
            var products = await Client.GetFromJsonAsync<List<ProductDto>>("/products");

            //assert
            productToBuy.Name.Should().Be(testProduct);
            productToBuy.QuantityAvailable.Should().Be(2);
            user.Name.Should().Be(testUser);
            user.BalanceAvailable.Should().Be(depositAmount - productToBuy.Price);
            purchases.Count.Should().Be(1);
            purchases.First().ProductName.Should().Be(testProduct);
            purchases.First().Quantity.Should().Be(1);
            purchases.First().Price.Should().Be(productToBuy.Price);
            purchases.First().Total.Should().Be(productToBuy.Price);
            products.First(f => f.Name == testProduct).QuantityAvailable.Should().Be(productToBuy.QuantityAvailable - 1);
        }

        [Fact]
        public async Task ReturnsInsufficientFundsErrorWhenUserHasInsufficientFunds()
        {
            //arrange 
            var testUser = "TestUser";
            var testProduct = "Snickers";
            var depositAmount = 1.8m;
            await ResetAndSetUpUserAsync(testUser, depositAmount);

            //act
            var response = await Client.PostAsJsonAsync("/purchase", new MakePurchaseDto { ProductName = testProduct, UserName = testUser });
            var errorMessage = await response.Content.ReadAsStringAsync();
            var user = await Client.GetFromJsonAsync<UserDto>($"/user/{testUser}");
            var product = await Client.GetFromJsonAsync<ProductDto>($"/product/{testProduct}");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            errorMessage.Should().Be("\"Purchase failed: Insufficient funds\"");
            user.BalanceAvailable.Should().Be(depositAmount);
            product.QuantityAvailable.Should().Be(1);
        }

        [Fact]
        public async Task ReturnsProductUnavailableErrorWhenProductIsUnavailable()
        {
            //arrange 
            var testUser = "TestUser";
            var testProduct = "M&Ms";
            var depositAmount = 3.8m;
            await ResetAndSetUpUserAsync(testUser, depositAmount);

            //act
            var response = await Client.PostAsJsonAsync("/purchase", new MakePurchaseDto { ProductName = testProduct, UserName = testUser });
            var errorMessage = await response.Content.ReadAsStringAsync();
            var user = await Client.GetFromJsonAsync<UserDto>($"/user/{testUser}");
            var product = await Client.GetFromJsonAsync<ProductDto>($"/product/{testProduct}");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            errorMessage.Should().Be("\"Purchase failed: Product is out of stock\"");
            user.BalanceAvailable.Should().Be(depositAmount);
            product.QuantityAvailable.Should().Be(0);
        }

        #endregion
    }
}