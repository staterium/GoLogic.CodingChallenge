namespace WebAPI.Modules.Products.Models
{
    /// <summary>
    ///     A data transfer object for the product entity.
    /// </summary>
    [AutoMap(typeof(Product))]
    public class ProductDto
    {
        #region Properties

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int QuantityAvailable { get; set; }

        public decimal Price { get; set; }

        #endregion
    }
}