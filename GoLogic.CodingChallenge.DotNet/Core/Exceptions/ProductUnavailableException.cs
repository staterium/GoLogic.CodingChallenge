namespace Core.Exceptions
{
    /// <summary>
    ///     This exception is thrown when a user attempts to purchase a product that is out of stock.
    /// </summary>
    public class ProductUnavailableException : InvalidOperationException
    {
        #region Constructors

        public ProductUnavailableException(string message = "Purchase failed: Product is out of stock") : base(message)
        {
        }

        #endregion
    }
}