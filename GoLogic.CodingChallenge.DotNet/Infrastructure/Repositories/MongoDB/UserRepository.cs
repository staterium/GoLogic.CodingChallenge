﻿using Core.Entities;
using Core.Interfaces;
using Infrastructure.Config;
using Microsoft.Extensions.Options;
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
        public UserRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>("Users");
        }

        #endregion

        #region Interface Implementations

        public Task<User> GetByNameAsync(string name)
        {
            return _usersCollection.Find(user => user.Name == name).FirstOrDefaultAsync();
        }

        public Task SaveNewUserAsync(User user)
        {
            return _usersCollection.InsertOneAsync(user);
        }

        public Task DeleteAllUsersAsync()
        {
            return _usersCollection.DeleteManyAsync(_ => true);
        }

        #endregion
    }
}