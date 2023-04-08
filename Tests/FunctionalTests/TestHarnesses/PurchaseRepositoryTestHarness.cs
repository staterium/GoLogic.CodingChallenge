using Core.Entities;
using Core.Interfaces;

namespace FunctionalTests.TestHarnesses
{
    /// <summary>
    ///     A test harness that implements the <see cref="IPurchaseRepository" /> interface and facilitates in-memory testing.
    /// </summary>
    public class PurchaseRepositoryTestHarness : IPurchaseRepository
    {
        #region Fields

        private static readonly List<Purchase> _purchases = new();

        #endregion

        #region Interface Implementations

        public Task DeleteAllPurchasesAsync()
        {
            return Task.Run(() => _purchases.Clear());
        }

        public Task<List<Purchase>> GetAllUserPurchasesAsync(User user)
        {
            return Task.FromResult(_purchases.Where(p => p.UserName == user.Name).ToList());
        }

        public Task SaveNewPurchaseAsync(Purchase purchase)
        {
            return Task.Run(() => _purchases.Add(purchase));
        }

        #endregion
    }
}