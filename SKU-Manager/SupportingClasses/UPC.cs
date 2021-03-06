﻿using System.Collections.Generic;

namespace SKU_Manager.SupportingClasses
{
   /*
    * A class that find the lowest available upc code 
    */
    public static class Upc
    {
        // field that store upc code list
        private static readonly List<double> UpcList = new List<double>();

        /* constructor that add all the used upc code in the list */
        static Upc()
        {
            // connect to database and get the upc
            using (var connection = new System.Data.SqlClient.SqlConnection(Credentials.DesignCon))
            {
                var command = new System.Data.SqlClient.SqlCommand("SELECT UPC_Code_9 FROM master_SKU_Attributes WHERE UPC_Code_9 IS NOT NULL ORDER BY UPC_Code_9", connection);
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                    UpcList.Add(reader.GetDouble(0));
            }
        }

        /* a method that returns the lowest possible upc code */
        public static string GetUpc()
        {
            // local fields for seeking available upc code
            double iterator = 62618300000;

            while (UpcList.Contains(iterator))
                iterator++;

            return iterator.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        /* a method that returns upc code with check digit */
        public static string GetUpc10(string upcCode)
        {
            // it already got check digit, no need to give
            if (upcCode.Length >= 12)
                return upcCode;

            return upcCode + GetCheckNum(upcCode);
        }

        /* a method that generate the check digit for the given upc code */
        private static int GetCheckNum(string upcCode)
        {
            // local fieds for calculation
            int indexOdd = 0;
            int indexEven = 1;
            int[] total = { 0, 0 };    // [0] for odd (and also the combine one), [1] for even

            // odd digits calculation
            while (indexOdd < 11)
            {
                total[0] += upcCode[indexOdd] - '0';
                indexOdd += 2;
            }
            total[0] *= 3;

            // even digits calculation
            while (indexEven < 11)
            {
                total[1] += upcCode[indexEven] - '0';
                indexEven += 2;
            }

            // add together
            total[0] += total[1];

            // return check digit
            return 10 - total[0] % 10;
        }
    }
}
