using Core.Entities;

namespace Core.Interfaces
{
    public interface IPurchaseRepository
    {
        #region Public Members

        public IList<Purchase> GetAllUserPurchases(User user);

        public void SaveNewPurchase(Purchase purchase);

        #endregion
    }
}