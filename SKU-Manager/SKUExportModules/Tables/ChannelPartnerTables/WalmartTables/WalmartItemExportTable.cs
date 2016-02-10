﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SKU_Manager.SKUExportModules.Tables.ChannelPartnerTables.WalmartTables
{
    /*
     * A class that return walmart item export table
     */
    class WalmartItemExportTable : WalmartExportTable
    {
        /* constructor that initialize fields */
        public WalmartItemExportTable()
        {
            mainTable = new DataTable();
            connection = new SqlConnection(Properties.Settings.Default.Designcs);
            skuList = getSKU();
        }

        /* the real thing -> return the table !!! */
        public override DataTable getTable()
        {
            // reset table just in case
            mainTable.Reset();

            // add column to table
            addColumn(mainTable, "UPC/GTIN");                         // 1
            addColumn(mainTable, "UPC CK Digit");                     // 2
            addColumn(mainTable, "Supplier Stock Number");            // 3
            addColumn(mainTable, "Item Description 1");               // 4
            addColumn(mainTable, "French Item Description 1");
            addColumn(mainTable, "Shelf 1 / Color");
            addColumn(mainTable, "French Shelf 1 / Color");
            addColumn(mainTable, "Shelf 2 / Size");
            addColumn(mainTable, "French Shelf 2 / Size");
            addColumn(mainTable, "Unit Size UOM");
            addColumn(mainTable, "Unit Size/Sell Qty");
            addColumn(mainTable, "Item Description 2");
            addColumn(mainTable, "French Item Description 2");
            addColumn(mainTable, "UPC Description");
            addColumn(mainTable, "French UPC Description");
            addColumn(mainTable, "Signing Desc");
            addColumn(mainTable, "French Signing Desc");
            addColumn(mainTable, "Brand");
            addColumn(mainTable, "Shop/Ticket Description");
            addColumn(mainTable, "French Shop/Ticket Description");
            addColumn(mainTable, "Plu Number");
            addColumn(mainTable, "Case UPC Supplier Pack");
            addColumn(mainTable, "Supplier Pack Qty");
            addColumn(mainTable, "Supplier Pack Length");
            addColumn(mainTable, "Supplier Pack Width");
            addColumn(mainTable, "Supplier Pack Height");
            addColumn(mainTable, "Supplier Pack Weight");
            addColumn(mainTable, "Supplier Min Order Qty");
            addColumn(mainTable, "Warehouse Pack UPC Number");
            addColumn(mainTable, "Whse Pack Qty");
            addColumn(mainTable, "Whse Pack Length");
            addColumn(mainTable, "Whse Pack Width");
            addColumn(mainTable, "Whse Pack Height");
            addColumn(mainTable, "Whse Pack Weight");
            addColumn(mainTable, "Whse Max Order Qty");
            addColumn(mainTable, "Special Handling Instructions");
            addColumn(mainTable, "Pallet Ti");
            addColumn(mainTable, "pallet Hi");
            addColumn(mainTable, "Pallet Rount Pct");
            addColumn(mainTable, "Whse Area");
            addColumn(mainTable, "Marshal ID");
            addColumn(mainTable, "Conveyable");
            addColumn(mainTable, "Master Carton Ind");
            addColumn(mainTable, "Crush Factor");
            addColumn(mainTable, "Whse Rotation");
            addColumn(mainTable, "unit Cost");
            addColumn(mainTable, "Base Unit Retail");
            addColumn(mainTable, "Supplier Pack Cost");
            addColumn(mainTable, "Mfgr Pre Price");
            addColumn(mainTable, "Mfgr Suggested Price");
            addColumn(mainTable, "Item Opp");
            addColumn(mainTable, "Whse Pacck Calc Method");
            addColumn(mainTable, "Department");
            addColumn(mainTable, "Supplier Number");
            addColumn(mainTable, "item Type");
            addColumn(mainTable, "Sub type");
            addColumn(mainTable, "Subclass");
            addColumn(mainTable, "Fineline");
            addColumn(mainTable, "Shelf Number");
            addColumn(mainTable, "Product Number");
            addColumn(mainTable, "Projected yearly Sales Qty");
            addColumn(mainTable, "Send to Store Date");
            addColumn(mainTable, "Item Effective Date");
            addColumn(mainTable, "Item Expiration Date");
            addColumn(mainTable, "Performance Rating");
            addColumn(mainTable, "Corporate Orderbook");
            addColumn(mainTable, "eCommerce Orderbook");
            addColumn(mainTable, "Variety Pack Ind");
            addColumn(mainTable, "intangible Ind");
            addColumn(mainTable, "Country of Origin");
            addColumn(mainTable, "Place of Manufacture");
            addColumn(mainTable, "Factory ID");
            addColumn(mainTable, "Whse Aligment");
            addColumn(mainTable, "Warehouses Stocked");
            addColumn(mainTable, "Wal-Mart");
            addColumn(mainTable, "Supercenter");
            addColumn(mainTable, "Neighborhood Market/Amigo");
            addColumn(mainTable, "Online");
            addColumn(mainTable, "Send Traits");
            addColumn(mainTable, "Omit Traits");
            addColumn(mainTable, "replaces Item");
            addColumn(mainTable, "Change Reason Code");
            addColumn(mainTable, "Comment");
            addColumn(mainTable, "Item Length");
            addColumn(mainTable, "Item Width");
            addColumn(mainTable, "Item Height");
            addColumn(mainTable, "Item Weight");
            addColumn(mainTable, "Guaranteed sales");
            addColumn(mainTable, "Eletronic Article Serveillance Ind");
            addColumn(mainTable, "Temp sensitive Ind");
            addColumn(mainTable, "Modular Batch Print");
            addColumn(mainTable, "Retail Unit Measurement");
            addColumn(mainTable, "Item Scannable Ind");
            addColumn(mainTable, "Scalable at Register Ind");
            addColumn(mainTable, "Backroom Scale Ind");
            addColumn(mainTable, "Sold by Weight/Repl by Unit");
            addColumn(mainTable, "Shelf Life Days");
            addColumn(mainTable, "Min Whse Life Qty");
            addColumn(mainTable, "Variance Days");
            addColumn(mainTable, "Ideal Temp Lo");
            addColumn(mainTable, "Ideal Temp Hi");
            addColumn(mainTable, "Acceptable Temp Lo");
            addColumn(mainTable, "Acceptable Temp hi");
            addColumn(mainTable, "Vnpk Netwgt");
            addColumn(mainTable, "Acctg Dept Nbr");
            addColumn(mainTable, "Supplier Pack Weight Format");
            addColumn(mainTable, "Variable Comp Ind");
            addColumn(mainTable, "Season Code");
            addColumn(mainTable, "Season Year");
            addColumn(mainTable, "Hazmat Ind");
            addColumn(mainTable, "Consideration Code");

            // local field for inserting data to table
            DataRow row;
            double[] price = getPrice();

            // start loading data
            mainTable.BeginLoadData();

            // add data to each row 
            foreach (string sku in skuList)
            {
                ArrayList list = getData(sku);

                row = mainTable.NewRow();

                row[0] = list[0];                                            // upc/gtin
                row[2] = sku;                                                    // supplier stock number
                row[3] = list[9];                // item description 1
                row[4] = list[10];            // french item description 1
                row[5] = list[11];            // shelf 1 / color
                row[6] = list[12];            // french shelf 1 / color
                row[9] = "Ea";          // unit size uom
                row[10] = 00001.0000;   // unit size / sell qty
                row[11] = "Online Only";    // item description 2
                row[12] = "En ligne seulement";  // french item description 2
                row[13] = "Leather Goods";      // UPC Description
                row[14] = "Ashlin Cuir";        // French UPC description
                row[15] = "Ashlin® " + list[3];      // signing desc
                row[16] = "Ashlin® " + list[4];      // french signing desc
                row[17] = 312999;           // brand 
                row[45] = Convert.ToDouble(list[1]) * price[0];               // unit cost
                row[46] = Convert.ToDouble(list[1]) * price[1];           // base unit retail
                row[83] = list[5];           // item length
                row[84] = list[6];           // item width
                row[85] = list[7];           // item height
                row[86] = list[8];           // item weight

                mainTable.Rows.Add(row);  
                progress++;
            }

            // finish loading data
            mainTable.EndLoadData();

            return mainTable;
        }

        /* method that get the data from given sku */
        protected override ArrayList getData(string sku)
        {
            // local fields for storing data
            ArrayList list = new ArrayList();
            DataTable table = new DataTable();

            // allocate elements from sku
            string color = sku.Substring(sku.LastIndexOf('-') + 1);
            string design = sku.Substring(0, sku.IndexOf('-'));

            // start grabbing data              
            // [0] upc/gtin, [1] for all related to price
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT UPC_Code_10, Base_Price FROM master_SKU_Attributes WHERE SKU_Ashlin = \'" + sku + "\';", connection);
            connection.Open();
            adapter.Fill(table);
            for (int i = 0; i <= 1; i++)
            {
                list.Add(table.Rows[0][i]);
            }
            table.Reset();
            // [2] for further looking, [3] signing desc, [4] french signgin desc, [5] item length, [6] item width, [7] item height, [8] item weight
            adapter = new SqlDataAdapter("SELECT Design_Service_Family_Code, Short_Description, Short_Description_FR, Depth_cm, Width_cm, Height_cm, Weight_grams FROM master_Design_Attributes WHERE Design_Service_Code = \'" + design + "\';", connection);
            adapter.Fill(table);
            for (int i = 0; i < 7; i++)
            {
                list.Add(table.Rows[0][i]);
            }
            // [9] item description 1, [10] french item description 1
            adapter = new SqlDataAdapter("SELECT Design_Service_Family_Description, Design_Service_Family_Description_FR FROM ref_Families WHERE Design_Service_Family_Code = \'" + table.Rows[0][0] + "\';", connection);
            table.Reset();
            adapter.Fill(table);
            for (int i = 0; i <= 1; i++)
            {
                list.Add(table.Rows[0][i]);
            }
            table.Reset();
            // [11] shelf 1 / color, [12] french shelf 1 / color
            adapter = new SqlDataAdapter("SELECT Colour_Description_Short, Colour_Description_Short_FR FROM ref_Colours WHERE Colour_Code = \'" + color + "\';", connection);
            adapter.Fill(table);
            for (int i = 0; i <= 1; i++)
            {
                list.Add(table.Rows[0][i]);
            }
            table.Reset();
            connection.Close();

            return list;
        }

        /* a method that return the discount matrix -> [0] wholesale, [1] multiplier*/
        private double[] getPrice()
        {
            double[] list = new double[2];

            // [0] wholesale
            SqlCommand command = new SqlCommand("SELECT [Wholesale_Net] FROM ref_discount_matrix;", connection);  
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            list[0] = reader.GetDouble(0);
            reader.Close();
            // [1] multiplier
            command = new SqlCommand("SELECT [MSRP Multiplier] FROM ref_msrp_multiplier;", connection);     
            reader = command.ExecuteReader();
            reader.Read();
            list[1] = reader.GetDouble(0);
            connection.Close();
            
            return list;
        }
    }
}
