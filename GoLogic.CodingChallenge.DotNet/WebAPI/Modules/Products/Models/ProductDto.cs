using AutoMapper;

namespace WebAPI.Modules.Products.Models
{
    [AutoMap(typeof(Product))]
    internal class ProductDto
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