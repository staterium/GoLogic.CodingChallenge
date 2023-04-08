namespace WebAPI.Modules.Purchases.Models
{
    /// <summary>
    ///     A data transfer object that describes a purchase.
    /// </summary>
    public class ListPurchasesDto
    {
        #region Properties

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Total => Price * Quantity;

        #endregion
    }
}