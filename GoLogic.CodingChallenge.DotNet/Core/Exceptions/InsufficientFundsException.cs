namespace Core.Exceptions
{
    public class InsufficientFundsException : InvalidOperationException
    {
        #region Constructors

        public InsufficientFundsException(string message = "Purchase failed: Insufficient funds") : base(message)
        {
        }

        #endregion
    }
}