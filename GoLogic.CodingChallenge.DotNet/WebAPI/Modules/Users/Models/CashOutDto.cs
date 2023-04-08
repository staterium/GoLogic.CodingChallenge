using WebAPI.Modules.Purchases.Models;

namespace WebAPI.Modules.Users.Models
{
    /// <summary>
    ///     Represents the data transfer object for a cash out operation.
    /// </summary>
    public class CashOutDto
    {
        #region Properties

        public string UserName { get; set; }

        public decimal TotalSpent { get; set; }

        public decimal ChangeReceived { get; set; }

        public List<GroupedPurchaseDto> Purchases { get; set; }

        #endregion
    }
}