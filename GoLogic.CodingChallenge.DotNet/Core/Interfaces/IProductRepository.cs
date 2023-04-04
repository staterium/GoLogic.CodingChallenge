﻿using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>
    ///     An interface describing the operations that can be performed on the product repository.
    /// </summary>
    public interface IProductRepository
    {
        #region Public Members

        /// <summary>
        ///     Returns a list of all products available in the vending machine.
        /// </summary>
        /// <returns>
        ///     A list of all products available in the vending machine.
        /// </returns>
        public Task<List<Product>> GetAllProductsAsync();

        /// <summary>
        ///     Returns a product by its name.
        /// </summary>
        /// <param name="name">
        ///     The name of the product to return.
        /// </param>
        /// <returns>
        ///     The product with the specified name.
        /// </returns>
        public Task<Product> GetProductByNameAsync(string name);

        /// <summary>
        ///     Saves a new product to the repository.
        /// </summary>
        /// <param name="product">
        ///     The product to save.
        /// </param>
        public Task SaveNewProductAsync(Product product);

        #endregion
    }
}