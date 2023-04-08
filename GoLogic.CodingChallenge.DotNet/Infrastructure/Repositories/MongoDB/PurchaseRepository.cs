namespace Infrastructure.Repositories.MongoDB
{
    /// <summary>
    ///     A repository that uses MongoDB to store purchases.
    /// </summary>
    public class PurchaseRepository : IPurchaseRepository
    {
        #region Fields

        private readonly IMongoCollection<Purchase> _purchasesCollection;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a new instance of the <see cref="PurchaseRepository" /> class.
        /// </summary>
        /// <param name="config">
        ///     The configuration that contains the connection string and database name.
        /// </param>
        public PurchaseRepository(IConfiguration config)
        {
            var mongoClient = new MongoClient(config["ConnectionString"]);
            var mongoDatabase = mongoClient.GetDatabase(config["DatabaseName"]);

            _purchasesCollection = mongoDatabase.GetCollection<Purchase>("Purchases");
        }

        #endregion

        #region Interface Implementations

        public Task<List<Purchase>> GetAllUserPurchasesAsync(User user)
        {
            return _purchasesCollection.Find(purchase => purchase.UserName == user.Name).ToListAsync();
        }

        public Task SaveNewPurchaseAsync(Purchase purchase)
        {
            return _purchasesCollection.InsertOneAsync(purchase);
        }

        public Task DeleteAllPurchasesAsync()
        {
            return _purchasesCollection.DeleteManyAsync(_ => true);
        }

        #endregion
    }
}