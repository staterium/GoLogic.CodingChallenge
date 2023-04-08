namespace WebAPI.Modules.Users.Models
{
    /// <summary>
    ///     Represents the data transfer object for a deposit funds operation.
    /// </summary>
    public class DepositFundsDto
    {
        #region Properties

        public string UserName { get; set; }

        public decimal DepositAmount { get; set; }

        #endregion
    }
}