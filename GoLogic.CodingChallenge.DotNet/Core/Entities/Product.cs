using Ardalis.GuardClauses;

namespace Core.Entities
{
    /// <summary>
    ///     An entity class describing a product available in the vending machine.
    /// </summary>
    public class Product : EntityBase<Guid>
    {
        #region Properties

        /// <summary>
        ///     The name of the product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The code of the product. The user uses this code to make a selection.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     The quantity of the product available in the vending machine.
        /// </summary>
        public int QuantityAvailable { get; set; }

        /// <summary>
        ///     The price of the product
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Indicates whether the product is available in the vending machine (i.e. quantity available is greater than 0).
        /// </summary>
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