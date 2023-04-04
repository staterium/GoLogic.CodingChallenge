namespace Core.Exceptions
{
    /// <summary>
    ///     This exception is thrown when the user attempts to purchase a product but does not have sufficient funds.
    /// </summary>
    public class InsufficientFundsException : InvalidOperationException
    {
        #region Constructors

        public InsufficientFundsException(string message = "Purchase failed: Insufficient funds") : base(message)
        {
        }

        #endregion
    }
}