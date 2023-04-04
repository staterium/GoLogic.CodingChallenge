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
            if (!product.IsAvailable)
                throw new ProductUnavailableException();

            if (user.BalanceAvailable < product.Price)
                throw new InsufficientFundsException();

            product.QuantityAvailable--;
            user.BalanceAvailable -= product.Price;

            return new Purchase { ProductName = product.Name, UserName = user.Name };
        }

        #endregion
    }
}