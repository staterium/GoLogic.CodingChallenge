using Ardalis.GuardClauses;

namespace Core.Entities
{
    public class Product
    {
        #region Properties

        public string Name { get; set; }

        public string Code { get; set; }

        public int QuantityAvailable { get; set; }

        public bool IsAvailable => QuantityAvailable > 0;

        #endregion

        #region Constructors

        public Product(string name, string code, int quantityAvailable)
        {
            Guard.Against.Negative(quantityAvailable);

            Name = name;
            QuantityAvailable = quantityAvailable;
            Code = code;
        }

        #endregion
    }
}