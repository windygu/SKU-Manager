﻿using System;

namespace SKU_Manager.AdminModules.UpdateInventory.InventoryTable
{
    /*
     * A class for storing shop.ca inventory values in order to generate txt file
     */
    public class ShopCaInventoryValues
    {
        // identification
        public string SupplierId = "ashlin_bpg";
        public string StoreName = "nishis_boutique";

        // must have fields
        public string Sku { get; set; }
        public int Quantity { get; set; }

        // minor fields
        public bool PurchaseOrder { get; set; }
        public bool Discontinued { get; set; }
        public DateTime RestockDate { get; set; }
        public int ReorderQuantity { get; set; }

        // additional field for convenience
        public string BpItemNumber { get; set; }

        /* first constructor that takes no argument */
        public ShopCaInventoryValues()
        {
            Sku = "";
            Quantity = 0;

            PurchaseOrder = false;
            Discontinued = false;
            RestockDate = DateTime.Today;
            ReorderQuantity = 0;

            BpItemNumber = "";
        }

        /* second constructor that accept all parameters as argument */
        public ShopCaInventoryValues(string sku, int quantity, bool purchaseOrder, bool discontinued, DateTime restockDate, int reorderQuantity, string bpItemNumber)
        {
            Sku = sku;
            Quantity = quantity;

            PurchaseOrder = purchaseOrder;
            Discontinued = discontinued;
            RestockDate = restockDate;
            ReorderQuantity = reorderQuantity;

            BpItemNumber = bpItemNumber;
        }
    }
}
