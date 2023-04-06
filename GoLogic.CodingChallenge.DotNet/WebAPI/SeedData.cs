namespace WebAPI
{
    public static class SeedData
    {
        #region Fields

        public static readonly Product Product1 = new("KitKat", "A1", 1.2m, 5);
        public static readonly Product Product2 = new("Mars", "A2", 1.5m, 2);
        public static readonly Product Product3 = new("Twix", "A3", 1.8m, 3);
        public static readonly Product Product4 = new("Snickers", "B1", 2.1m, 1);
        public static readonly Product Product5 = new("Milky Way", "B2", 2.9m, 4);
        public static readonly Product Product6 = new("M&Ms", "B3", 2.7m, 0);

        #endregion

        #region Public Members

        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var seedTasks = new List<Task>
            {
                ClearUsersAsync(serviceProvider), ClearPurchasesAsync(serviceProvider), ClearAndSeedProductsAsync(serviceProvider)
            };

            try
            {
                await Task.WhenAll(seedTasks);
            }
            // Only happens when the MongoDB container hasn't started yet or is unreachable.
            // Swallow exception so the API can still start, and we can see the error when making API calls.
            catch (TimeoutException)
            {
            }
        }

        #endregion

        #region Private Members

        private static async Task ClearAndSeedProductsAsync(IServiceProvider serviceProvider)
        {
            var productRepository = serviceProvider.GetRequiredService<IProductRepository>();
            await productRepository.DeleteAllProductsAsync();

            var productList = new List<Product>
            {
                Product1,
                Product2,
                Product3,
                Product4,
                Product5,
                Product6
            };

            await productRepository.SaveNewProductsAsync(productList);
        }

        private static async Task ClearPurchasesAsync(IServiceProvider serviceProvider)
        {
            var purchaseRepository = serviceProvider.GetRequiredService<IPurchaseRepository>();
            await purchaseRepository.DeleteAllPurchasesAsync();
        }

        private static async Task ClearUsersAsync(IServiceProvider serviceProvider)
        {
            var userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            await userRepository.DeleteAllUsersAsync();
        }

        #endregion
    }
}