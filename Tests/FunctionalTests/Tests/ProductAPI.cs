using WebAPI.Modules.Products.Models;

namespace FunctionalTests.Tests
{
    [Collection("Http Client Collection")]
    public class ProductAPI : TestBase
    {
        #region Constructors

        public ProductAPI(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        #endregion

        #region Public Members

        [Fact]
        public async Task Returns6ProductsAfterReset()
        {
            //act
            await ResetAsync();
            var products = await Client.GetFromJsonAsync<List<ProductDto>>("/products");

            //assert
            products.Count.Should().Be(6);
        }

        [Fact]
        public async Task ReturnsCorrectProductByName()
        {
            //act
            await ResetAsync();
            var product = await Client.GetFromJsonAsync<ProductDto>("/product/KitKat");

            //assert
            product.Name.Should().Be("KitKat");
            product.Price.Should().Be(1.20m);
            product.QuantityAvailable.Should().Be(5);
        }

        #endregion
    }
}