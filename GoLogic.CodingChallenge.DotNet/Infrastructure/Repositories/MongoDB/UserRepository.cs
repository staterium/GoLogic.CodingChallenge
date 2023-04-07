using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Repositories.MongoDB
{
    /// <summary>
    ///     A repository that uses MongoDB to store users.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        #region Fields

        private readonly IMongoCollection<User> _usersCollection;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        /// <param name="databaseSettings">
        ///     The database settings used to connect to the MongoDB database.
        /// </param>
        public UserRepository(IConfiguration config)
        {
            var mongoClient = new MongoClient(config["ConnectionString"]);
            var mongoDatabase = mongoClient.GetDatabase(config["DatabaseName"]);

            _usersCollection = mongoDatabase.GetCollection<User>("Users");
        }

        #endregion

        #region Interface Implementations

        public Task<User?> GetByNameAsync(string name)
        {
            return _usersCollection.Find(user => user.Name == name).FirstOrDefaultAsync()!;
        }

        public Task SaveNewUserAsync(User user)
        {
            return _usersCollection.InsertOneAsync(user);
        }

        public Task UpdateUserAsync(User user)
        {
            var filter = Builders<User>.Filter.Eq("Id", user.Id);
            return _usersCollection.ReplaceOneAsync(filter, user);
        }

        public Task DeleteAllUsersAsync()
        {
            return _usersCollection.DeleteManyAsync(_ => true);
        }

        #endregion
    }
}