using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>
    ///     An interface describing the purchase service.
    /// </summary>
    public interface IPurchaseService
    {
        #region Public Members

        /// <summary>
        ///     Purchases a product for a user.
        /// </summary>
        /// <param name="product">
        ///     The product to purchase.
        /// </param>
        /// <param name="user">
        ///     The user who is purchasing the product.
        /// </param>
        /// <returns>
        ///     A purchase entity describing the purchase.
        /// </returns>
        /// <exception cref="Exceptions.InsufficientFundsException" />
        /// <exception cref="Exceptions.ProductUnavailableException" />
        public Purchase PurchaseProduct(Product product, User user);

        #endregion
    }
}