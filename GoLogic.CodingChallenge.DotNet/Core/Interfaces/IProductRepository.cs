using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        #region Public Members

        public IList<Product> GetAllProducts();

        public Product GetProductByName(string name);

        public void SaveNewProduct(Product product);

        #endregion
    }
}