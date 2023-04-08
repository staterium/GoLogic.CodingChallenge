using Core.Entities;
using Core.Interfaces;

namespace FunctionalTests.TestHarnesses
{
    /// <summary>
    ///     A test harness that implements the <see cref="IProductRepository" /> interface and facilitates in-memory testing.
    /// </summary>
    internal class ProductRepositoryTestHarness : IProductRepository
    {
        #region Fields

        private readonly List<Product> _products = new();

        #endregion

        #region Interface Implementations

        public Task DeleteAllProductsAsync()
        {
            return Task.Run(() => _products.Clear());
        }

        public Task<List<Product>> GetAllProductsAsync()
        {
            return Task.FromResult(_products.ToList());
        }

        public Task<Product?> GetProductByNameAsync(string name)
        {
            return Task.FromResult(_products.FirstOrDefault(p => p.Name == name));
        }

        public Task SaveNewProductAsync(Product product)
        {
            return Task.Run(() => _products.Add(product));
        }

        public Task SaveNewProductsAsync(List<Product> products)
        {
            return Task.Run(() => _products.AddRange(products));
        }

        public Task UpdateProductAsync(Product product)
        {
            return Task.Run(
                () =>
                {
                    var index = _products.FindIndex(p => p.Name == product.Name);
                    _products[index] = product;
                });
        }

        #endregion
    }
}