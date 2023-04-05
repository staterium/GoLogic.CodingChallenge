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

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            builder.AddTransient<IPurchaseRepository, PurchaseRepository>();
            builder.AddTransient<IPurchaseService, PurchaseService>();

            return builder;
        }

        #endregion

        #region Private Members

        private static Func<PurchaseDto, IUserRepository, IProductRepository, IPurchaseService, Task<IResult>> DoPurchaseAsync()
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

        #endregion
    }
}