namespace Core.Exceptions
{
    public class ProductUnavailableException : InvalidOperationException
    {
        #region Constructors

        public ProductUnavailableException(string message = "Product is not available") : base(message)
        {
        }

        #endregion
    }
}