using Core.Entities;

namespace Core.Interfaces
{
    public interface IPurchaseService
    {
        #region Public Members

        public Purchase PurchaseProduct(Product product, User user);

        #endregion
    }
}