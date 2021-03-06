﻿namespace SKU_Manager.AdminModules.UpdateInventory.InventoryTable
{
    /*
     * A class for storing giant tiger inventory values in order to generate csv file
     */
    public class GiantTigerInventoryValues
    {
        public string HostSku { get; set; }
        public string VendorSku { get; set; }
        public string Upc { get; set; }
        public string HostItemDescription { get; set; }
        public int QtyOnHand { get; set; }
        public double UnitCost { get; set; }
        public bool PurchaseOrder { get; set; }
        public bool Discontinue { get; set; }

        // additional field for convenience
        public string BpItemNumber { get; set; }
        public int ReorderQuantity { get; set; }

        /* first constructor that takes no argument */
        public GiantTigerInventoryValues()
        {
            HostSku = "";
            VendorSku = "";
            Upc = "";
            HostItemDescription = "";
            QtyOnHand = 0;
            UnitCost = 0;
            PurchaseOrder = false;
            Discontinue = false;

            BpItemNumber = "";
            ReorderQuantity = 0;
        }

        /* second constructor that accept all parameters as argument */
        public GiantTigerInventoryValues(string hostSku, string vendorSku, string upc, string hostItemDescription, int qtyOnHand, double unitCost, bool purchaseOrder, bool discontinue,
                                         string bpItemNumber, int reorderQty)
        {
            HostSku = hostSku;
            VendorSku = vendorSku;
            Upc = upc;
            HostItemDescription = hostItemDescription;
            QtyOnHand = qtyOnHand;
            UnitCost = unitCost;
            PurchaseOrder = purchaseOrder;
            Discontinue = discontinue;

            BpItemNumber = bpItemNumber;
            ReorderQuantity = reorderQty;
        }
    }
}
