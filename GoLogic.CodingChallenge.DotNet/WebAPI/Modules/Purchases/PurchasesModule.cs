using Core.Services;
using WebAPI.Modules.Purchases.Models;

namespace WebAPI.Modules.Purchases
{
    /// <summary>
    ///     A module that contains all the functionality related to purchases (endpoints, services etc).
    /// </summary>
    internal class PurchasesModule : IModule
    {
        #region Interface Implementations

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/purchase", DoPurchaseAsync());
            endpoints.MapGet("/purchases/{username}", GetPurchasesAsync());

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            builder.AddTransient<IPurchaseRepository, PurchaseRepository>();
            builder.AddTransient<IPurchaseService, PurchaseService>();

            return builder;
        }

        #endregion

        #region Public Members

        public static List<GroupedPurchaseDto> GetPurchasesGrouped(List<Purchase> purchases, List<Product> products)
        {
            var purchasesDto = purchases.Select(
                s => new ListPurchasesDto { Price = products.First(f => f.Name == s.ProductName).Price, ProductName = s.ProductName, Quantity = 1 });

            var purchasesGrouped = purchasesDto.GroupBy(g => new { g.ProductName, g.Price })
                .ToList()
                .Select(
                    s => new GroupedPurchaseDto
                    {
                        ProductName = s.Key.ProductName, Price = s.Key.Price, Quantity = s.Sum(g => g.Quantity), Total = s.Sum(g => g.Total)
                    })
                .ToList();

            return purchasesGrouped;
        }

        #endregion

        #region Private Members

        private static Func<MakePurchaseDto, IUserRepository, IProductRepository, IPurchaseService, Task<IResult>> DoPurchaseAsync()
        {
            return async (purchaseDto, userRepository, productRepository, purchaseService) =>
            {
                var user = await userRepository.GetByNameAsync(purchaseDto.UserName);

                if (user == null)
                    return Results.NotFound("No such user found");

                var product = await productRepository.GetProductByNameAsync(purchaseDto.ProductName);

                if (product == null)
                    return Results.NotFound("No such product found");

                try
                {
                    var purchase = await purchaseService.PurchaseProductAsync(product, user);

                    return purchase == null ? Results.BadRequest("Purchase failed") : Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            };
        }

        private static Func<string, IUserRepository, IProductRepository, IPurchaseRepository, Task<IResult>> GetPurchasesAsync()
        {
            return async (userName, userRepository, productRepository, purchaseRepository) =>
            {
                var user = await userRepository.GetByNameAsync(userName);

                if (user == null)
                    return Results.NotFound("No such user found");

                var products = await productRepository.GetAllProductsAsync();
                var purchases = await purchaseRepository.GetAllUserPurchasesAsync(user);

                var purchasesGrouped = GetPurchasesGrouped(purchases, products);

                return Results.Ok(purchasesGrouped);
            };
        }

        #endregion
    }
}