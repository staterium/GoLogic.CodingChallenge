using AutoMapper;
using WebAPI.Modules.Products.Models;

namespace WebAPI.Modules.Products
{
    internal class ProductsModule : IModule
    {
        #region Interface Implementations

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/products", GetProductsAsync());
            endpoints.MapGet("/product/{name}", GetProductByNameAsync());

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            builder.AddTransient<IProductRepository, ProductRepository>();
            return builder;
        }

        #endregion

        #region Private Members

        private static Func<string, IProductRepository, IMapper, Task<IResult>> GetProductByNameAsync()
        {
            return async (name, productRepository, mapper) =>
            {
                var product = await productRepository.GetProductByNameAsync(name);
                var result = mapper.Map<ProductDto>(product);

                return product == null ? Results.NotFound() : Results.Ok(result);
            };
        }


        private static Func<IProductRepository, IMapper, Task<IResult>> GetProductsAsync()
        {
            return async (productRepository, mapper) =>
            {
                var products = await productRepository.GetAllProductsAsync();
                var result = mapper.Map<List<ProductDto>>(products);

                return Results.Ok(result);
            };
        }

        #endregion
    }
}