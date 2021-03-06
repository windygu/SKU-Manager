﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace SKU_Manager.SupportingClasses.Photo
{
   /*
    * A class that add image with different name
    */
    public class ImageReplace
    {
        // fields for searching image, sku, and upc
        private const string START_DIR = @"Z:\Public\Product Media Content";
        private readonly List<string> skuList = new List<string>();

        // fields for getting progress
        public int Progress { get; private set; }
        public int Total => skuList.Count;

        /* method that add existing sku image with upc code */
        public void AddUpc(string sku, string upc)
        {
            // local supporting fields
            string prefix = sku.Substring(0, sku.IndexOf('-'));
            string targetDirectory = START_DIR;
            int i = 1;    // for naming file

            // check if directory exists
            if (Directory.Exists(START_DIR + '/' + prefix))
                targetDirectory += '/' + prefix;
            else // no image for this design, return it
                return;

            // start copying a upc code image
            foreach (string image in Directory.GetFiles(targetDirectory, "*.jpg").Where(image => image.Contains(sku)))
            {
                File.Copy(image, targetDirectory + '/' + upc + '_' + i + ".jpg", true);
                i++;
            }
        }

        /* update all the current existing sku image with a upc code image */
        public void AddGlobalUpc()
        {
            // clear the list first
            skuList.Clear();
            Progress = 0;

            // get all the sku from database
            using (SqlConnection connection = new SqlConnection(Credentials.DesignCon))
            {
                SqlCommand command = new SqlCommand("SELECT SKU_Ashlin FROM master_SKU_Attributes WHERE Active = 'True'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    skuList.Add(reader.GetString(0));
            }

            // start adding image
            foreach (string sku in skuList)
            {
                // get the upc code
                string[] upcCode = GetUpc(sku);

                // if no upc code assign yet -> skip it
                if (upcCode[0] == "" || upcCode[1] == "") continue;

                // add image for 9 and 10 digit upc
                AddUpc(sku, upcCode[0]);
                AddUpc(sku, upcCode[1]);

                Progress++;
            }
        }

        /* return upc code for the given sku */
        private static string[] GetUpc(string sku)
        {
            // local field for storing data
            DataTable table = new DataTable();
            string[] upcCode = new string[2];

            using (SqlConnection connection = new SqlConnection(Credentials.DesignCon))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT UPC_Code_9, UPC_Code_10 FROM master_SKU_Attributes WHERE SKU_Ashlin = \'" + sku + '\'', connection);
                connection.Open();
                adapter.Fill(table);
            }

            upcCode[0] = table.Rows[0][0].ToString();
            upcCode[1] = table.Rows[0][1].ToString();

            return upcCode;
        }
    }
}
