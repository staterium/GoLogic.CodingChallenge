using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;

namespace Core.Services
{
    public class PurchaseService : IPurchaseService
    {
        #region Interface Implementations

        public Purchase PurchaseProduct(Product product, User user)
        {
            if (product.IsAvailable)
                return new Purchase { ProductName = product.Name, UserName = user.Name };

            throw new ProductUnavailableException("");
        }

        #endregion
    }
}