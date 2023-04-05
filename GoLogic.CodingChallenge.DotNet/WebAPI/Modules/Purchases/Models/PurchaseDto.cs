namespace WebAPI.Modules.Purchases.Models
{
    [AutoMap(typeof(Purchase))]
    internal class PurchaseDto
    {
        #region Properties

        public string UserName { get; set; }

        public string ProductName { get; set; }

        #endregion
    }
}