using Ardalis.GuardClauses;

namespace Core.Entities
{
    public class Product
    {
        #region Properties

        public string Name { get; set; }

        public string Code { get; set; }

        public int QuantityAvailable { get; set; }

        public decimal Price { get; set; }

        public bool IsAvailable => QuantityAvailable > 0;

        #endregion

        #region Constructors

        public Product(string name, string code, decimal price, int quantityAvailable)
        {
            Guard.Against.Negative(quantityAvailable);
            Guard.Against.NegativeOrZero(price);

            Name = name;
            QuantityAvailable = quantityAvailable;
            Code = code;
            Price = price;
        }

        #endregion
    }
}