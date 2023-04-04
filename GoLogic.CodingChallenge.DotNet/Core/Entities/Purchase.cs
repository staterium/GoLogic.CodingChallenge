namespace Core.Entities
{
    /// <summary>
    ///     An entity class describing a purchase made by a user. This class is used to store the purchase history of a user.
    ///     The quantity of the product purchased is not stored because the user can only purchase one product at a time.
    /// </summary>
    public class Purchase
    {
        #region Properties

        /// <summary>
        ///     The name of the user who made the purchase.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     The name of the product purchased.
        /// </summary>
        public string ProductName { get; set; }

        #endregion
    }
}