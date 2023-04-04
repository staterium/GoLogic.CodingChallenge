namespace Core.Exceptions
{
    public class ProductUnavailableException : InvalidOperationException
    {
        #region Constructors

        public ProductUnavailableException(string message = "Purchase failed: Product is out of stock") : base(message)
        {
        }

        #endregion
    }
}