﻿namespace Infrastructure.Repositories.MongoDB
{
    /// <summary>
    ///     A repository that uses MongoDB to store products.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        #region Fields

        private readonly IMongoCollection<Product> _productsCollection;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a new instance of the <see cref="ProductRepository" /> class.
        /// </summary>
        /// <param name="config">
        ///     The configuration that contains the connection string and database name.
        /// </param>
        public ProductRepository(IConfiguration config)
        {
            var mongoClient = new MongoClient(config["ConnectionString"]);
            var mongoDatabase = mongoClient.GetDatabase(config["DatabaseName"]);

            _productsCollection = mongoDatabase.GetCollection<Product>("Products");
        }

        #endregion

        #region Interface Implementations

        public Task<List<Product>> GetAllProductsAsync()
        {
            return _productsCollection.Find(_ => true).ToListAsync();
        }

        public Task<Product?> GetProductByNameAsync(string name)
        {
            return _productsCollection.Find(product => product.Name == name).FirstOrDefaultAsync()!;
        }

        public Task SaveNewProductAsync(Product product)
        {
            return _productsCollection.InsertOneAsync(product);
        }

        public Task SaveNewProductsAsync(List<Product> products)
        {
            return _productsCollection.InsertManyAsync(products);
        }

        public Task UpdateProductAsync(Product product)
        {
            var filter = Builders<Product>.Filter.Eq("Id", product.Id);
            return _productsCollection.ReplaceOneAsync(filter, product);
        }

        public Task DeleteAllProductsAsync()
        {
            return _productsCollection.DeleteManyAsync(_ => true);
        }

        #endregion
    }
}