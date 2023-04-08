namespace WebAPI.Modules.Purchases.Models
{
    /// <summary>
    ///     A DTO that describes all a user's purchases, grouped by product
    /// </summary>
    public class GroupedPurchaseDto
    {
        #region Properties

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }

        #endregion
    }
}