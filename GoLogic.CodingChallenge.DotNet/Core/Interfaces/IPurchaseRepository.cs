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
        ///     Deletes all purchases from the repository.
        /// </summary>
        Task DeleteAllPurchasesAsync();

        /// <summary>
        ///     Returns a list of all purchases made by the specified user.
        /// </summary>
        /// <param name="user">
        ///     The user whose purchases are to be returned.
        /// </param>
        /// <returns>
        ///     A list of all purchases made by the specified user.
        /// </returns>
        Task<List<Purchase>> GetAllUserPurchasesAsync(User user);

        /// <summary>
        ///     Saves a new purchase to the repository.
        /// </summary>
        /// <param name="purchase">
        ///     The purchase to save.
        /// </param>
        Task SaveNewPurchaseAsync(Purchase purchase);

        #endregion
    }
}