using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;

namespace Core.Services
{
    public class PurchaseService : IPurchaseService
    {
        #region Fields

        private readonly IProductRepository _productRepository;

        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IUserRepository _userRepository;

        #endregion

        #region Constructors

        public PurchaseService(IPurchaseRepository purchaseRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _purchaseRepository = purchaseRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        #endregion

        #region Interface Implementations

        public async Task<Purchase?> PurchaseProductAsync(Product product, User user)
        {
            if (!product.IsAvailable)
                throw new ProductUnavailableException();

            if (user.BalanceAvailable < product.Price)
                throw new InsufficientFundsException();

            product.QuantityAvailable--;
            user.BalanceAvailable -= product.Price;

            var purchase = new Purchase(user.Name, product.Name);

            var tasks = new List<Task>
            {
                _purchaseRepository.SaveNewPurchaseAsync(purchase),
                _userRepository.UpdateUserAsync(user),
                _productRepository.UpdateProductAsync(product)
            };

            await Task.WhenAll(tasks);

            return purchase;
        }

        #endregion
    }
}