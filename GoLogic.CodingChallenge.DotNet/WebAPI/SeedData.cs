using Core.Entities;
using Core.Interfaces;

namespace WebAPI
{
    public static class SeedData
    {
        #region Fields

        public static readonly Product Product1 = new("KitKat", "A1", 1.2m, 5);
        public static readonly Product Product2 = new("Mars", "A2", 1.5m, 2);
        public static readonly Product Product3 = new("Twix", "A3", 1.8m, 3);
        public static readonly Product Product4 = new("Snickers", "B1", 2.1m, 1);
        public static readonly Product Product5 = new("Milky Way", "B2", 2.4m, 4);
        public static readonly Product Product6 = new("M&Ms", "B3", 2.7m, 0);

        #endregion

        #region Public Members

        public static void Initialize(IServiceProvider serviceProvider)
        {
            ClearUsers(serviceProvider);
            ClearPurchases(serviceProvider);
            ClearAndSeedProducts(serviceProvider);
        }

        #endregion

        #region Private Members

        private static void ClearAndSeedProducts(IServiceProvider serviceProvider)
        {
            var productRepository = serviceProvider.GetRequiredService<IProductRepository>();
            productRepository.DeleteAllProductsAsync().Wait();

            var productList = new List<Product>
            {
                Product1,
                Product2,
                Product3,
                Product4,
                Product5,
                Product6
            };

            productRepository.SaveNewProductsAsync(productList).Wait();
        }

        private static void ClearPurchases(IServiceProvider serviceProvider)
        {
            var purchaseRepository = serviceProvider.GetRequiredService<IPurchaseRepository>();
            purchaseRepository.DeleteAllPurchasesAsync().Wait();
        }

        private static void ClearUsers(IServiceProvider serviceProvider)
        {
            var userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            userRepository.DeleteAllUsersAsync().Wait();
        }

        #endregion
    }
}