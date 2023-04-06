﻿namespace WebAPI.Modules.Purchases.Models
{
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