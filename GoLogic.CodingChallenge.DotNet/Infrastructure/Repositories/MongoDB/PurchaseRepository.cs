using Core.Entities;
using Core.Interfaces;
using Infrastructure.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

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
        /// <param name="databaseSettings">
        ///     The database settings used to connect to the MongoDB database.
        /// </param>
        public PurchaseRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

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

        #endregion
    }
}