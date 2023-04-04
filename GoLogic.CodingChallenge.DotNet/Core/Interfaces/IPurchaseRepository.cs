using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>
    ///     An interface describing the operations that can be performed on the user repository.
    /// </summary>
    public interface IPurchaseRepository
    {
        #region Public Members

        /// <summary>
        ///     Returns a list of all purchases made by the specified user.
        /// </summary>
        /// <param name="user">
        ///     The user whose purchases are to be returned.
        /// </param>
        /// <returns>
        ///     A list of all purchases made by the specified user.
        /// </returns>
        public IList<Purchase> GetAllUserPurchases(User user);

        /// <summary>
        ///     Saves a new purchase to the repository.
        /// </summary>
        /// <param name="purchase">
        ///     The purchase to save.
        /// </param>
        public void SaveNewPurchase(Purchase purchase);

        #endregion
    }
}