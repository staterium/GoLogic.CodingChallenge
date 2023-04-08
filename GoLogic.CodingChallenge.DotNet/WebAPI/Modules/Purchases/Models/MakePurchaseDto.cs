namespace WebAPI.Modules.Purchases.Models
{
    /// <summary>
    ///     A data transfer object that describes a purchase.
    /// </summary>
    public class MakePurchaseDto
    {
        #region Properties

        public string UserName { get; set; }

        public string ProductName { get; set; }

        #endregion
    }
}