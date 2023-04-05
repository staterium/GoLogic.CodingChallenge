using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>
    ///     An interface describing the operations that can be performed on the product repository.
    /// </summary>
    public interface IProductRepository
    {
        #region Public Members

        /// <summary>
        ///     Deletes all products from the repository.
        /// </summary>
        Task DeleteAllProductsAsync();

        /// <summary>
        ///     Returns a list of all products available in the vending machine.
        /// </summary>
        /// <returns>
        ///     A list of all products available in the vending machine.
        /// </returns>
        Task<List<Product>> GetAllProductsAsync();

        /// <summary>
        ///     Returns a product by its name.
        /// </summary>
        /// <param name="name">
        ///     The name of the product to return.
        /// </param>
        /// <returns>
        ///     The product with the specified name.
        /// </returns>
        Task<Product?> GetProductByNameAsync(string name);

        /// <summary>
        ///     Saves a new product to the repository.
        /// </summary>
        /// <param name="product">
        ///     The product to save.
        /// </param>
        Task SaveNewProductAsync(Product product);

        /// <summary>
        ///     Saves a list of new products to the repository.
        /// </summary>
        /// <param name="products">
        ///     The list of products to save.
        /// </param>
        Task SaveNewProductsAsync(List<Product> products);

        /// <summary>
        ///     Updates an existing product.
        /// </summary>
        /// <param name="product">
        ///     The product to update.
        /// </param>
        Task UpdateProductAsync(Product product);

        #endregion
    }
}