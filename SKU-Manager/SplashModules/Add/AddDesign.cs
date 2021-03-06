﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SKU_Manager.SupportingClasses;
using System.Collections.Generic;
using System.Globalization;

namespace SKU_Manager.SplashModules.Add
{
    /*
     * An application module to add new design to SKU database
     */
    public partial class AddDesign : Form
    {
        // fields for storing adding design data
        private string designServiceCode;
        private string productFamily;
        private string designServiceFlag;
        private string internalName;
        private string shortEnglishDescription = "";
        private string shortFrenchDescription = "";
        private string extendedEnglishDescription = "";
        private string extendedFrenchDescription = "";
        private string trendShortEnglishDescription = "";
        private string trendShortFrenchDescription = "";
        private string trendExtendedEnglishDescription = "";
        private string trendExtendedFrenchDescription = "";
        private string designOnlineEnglish = "";
        private string designOnlineFrench = "";
        private readonly string[] boolean = new string[8];    // [0] for monogrammed, [1] for imprinted, [2] for strap, [3] for detachable, [4] for zipped, [5] for shipped flat, [6] for shipped folded, [7] for displayed website, [8] for gift box
        private readonly int[] integer = new int[10];         // corresponding to the field above
        private string imprintHeight;
        private string imprintWidth;
        private string productHeight;
        private string productWidth;
        private string productDepth;
        private string country;
        private struct BagWallet
        {
            public double ShoulderDropLength;
            public double HandleStrapDropLength;
            public string NotableStrapGeneralFeatures;
            public bool ProtectiveFeet;
            public string Closure;
            public bool InnerPocket;
            public bool OutsidePocket;
            public string SizeDifferentiation;
            public bool DustBag;
            public bool AuthenticityCard;
            public int BillCompartment;
            public int CardSlot;
        }
        private BagWallet bw = new BagWallet { NotableStrapGeneralFeatures = "1 long", Closure = "Zippered", SizeDifferentiation = "Medium", DustBag = true, AuthenticityCard = true };
        private string weight;
        private string numberComponents;
        private string shippableHeight;
        private string shippableWidth;
        private string shippableDepth;
        private string shippableWeight;
        private string tsc;
        private string theBay;
        private string bestbuy;
        private string shopca;
        private string amazon;
        private string sears;
        private string staples;
        private string walmart;
        private readonly string[] englishOption = new string[5];
        private readonly string[] frenchOption = new string[5];
        private bool active = true;    // default set to true

        // supporting boolean flag -> set to default
        private bool isFlat;
        private bool isFolded;

        // field for lists
        private readonly ArrayList productFamilyList = new ArrayList();
        private readonly HashSet<string> designCodeList = new HashSet<string>();
        private readonly HashSet<string> internalNameList = new HashSet<string>();

        /* constructor that initialize graphic component */
        public AddDesign()
        {
            InitializeComponent();

            // call background worker
            if (!backgroundWorkerCombobox.IsBusy)
                backgroundWorkerCombobox.RunWorkerAsync();
        }

        #region Combobox Generation
        /* the backgound workder for adding items to comboBoxes */
        private void backgroundWorkerCombobox_DoWork(object sender, DoWorkEventArgs e)
        {
            // make comboBox for Product Family
            using (SqlConnection connection = new SqlConnection(Credentials.DesignCon))
            {
                SqlCommand command = new SqlCommand("SELECT Design_Service_Family_Description FROM ref_Families WHERE Design_Service_Family_Description != '' ORDER BY Design_Service_Family_Description", connection);   
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    productFamilyList.Add(reader.GetString(0));
                reader.Close();

                // additional addition for design service code and ashlin internal name checking
                command.CommandText = "SELECT Design_Service_Code FROM master_Design_Attributes";
                reader = command.ExecuteReader();
                while (reader.Read())
                    designCodeList.Add(reader.GetString(0));
                reader.Close();

                command.CommandText = "SELECT Design_Service_Fashion_Name_Ashlin FROM master_Design_Attributes";
                reader = command.ExecuteReader();
                while (reader.Read())
                    internalNameList.Add(reader.GetString(0));
            }
        }
        private void backgroundWorkerCombobox_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            productFamilyCombobox.DataSource = productFamilyList;
        }
        #endregion

        /* the event for design service code textbox that will check if there is duplicate or not */
        private void designServiceCodeTextbox_TextChanged(object sender, EventArgs e)
        {
            if (designCodeList.Contains(designServiceCodeTextbox.Text))
            {
                duplicateLabel1.Visible = true;
                designServiceCodeTextbox.BackColor = Color.Red;
            }
            else
            {
                duplicateLabel1.Visible = false;
                designServiceCodeTextbox.BackColor = SystemColors.Window;
            }
        }

        /* the event of internal ashlin name textbox that detect whether the user input has duplicate */
        private void internalNameTextbox_TextChanged(object sender, EventArgs e)
        {
            if (internalNameTextbox.Text != "" && internalNameList.Contains(internalNameTextbox.Text))
            {
                internalNameTextbox.BackColor = Color.Red;
                duplicateLabel2.Visible = true;
            }
            else
            {
                internalNameTextbox.BackColor = SystemColors.Window;
                duplicateLabel2.Visible = false;
            }
        }

        /* the event for design service flag combobox change that change the enability of some textboxes */
        private void designServiceFlagCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (designServiceFlagCombobox.SelectedIndex == 0)
            {
                tscTextbox.Enabled = true;
                theBayTextbox.Enabled = true;
                bestbuyTextbox.Enabled = true;
                shopcaTextbox.Enabled = true;
                amazonTextbox.Enabled = true;
                searsTextbox.Enabled = true;
                staplesTextbox.Enabled = true;
                walmartTextbox.Enabled = true;
                monogrammedCombobox.Enabled = true;
                imprintedCombobox.Enabled = true;
                imprintHeightTextbox.Enabled = true;
                imprintWidthTextbox.Enabled = true;
                productHeightTextbox.Enabled = true;
                productWidthTextbox.Enabled = true;
                productDepthTextbox.Enabled = true;
                weightTextBox.Enabled = true;
                countryCombobox.Enabled = true;
                bagWalletDetailButton.Enabled = true;
                numberComponentTextbox.Enabled = true;
                strapCombobox.Enabled = true;
                detachableCombobox.Enabled = true;
                zippedCombobox.Enabled = true;
                shippedFlatCombobox.Enabled = true;
                shippedFoldedCombobox.Enabled = true;
            }
            else
            {
                tscTextbox.Enabled = false;
                theBayTextbox.Enabled = false;
                bestbuyTextbox.Enabled = false;
                shopcaTextbox.Enabled = false;
                amazonTextbox.Enabled = false;
                searsTextbox.Enabled = false;
                staplesTextbox.Enabled = false;
                walmartTextbox.Enabled = false;
                monogrammedCombobox.Enabled = false;
                imprintedCombobox.Enabled = false;
                imprintHeightTextbox.Enabled = false;
                imprintWidthTextbox.Enabled = false;
                productHeightTextbox.Enabled = false;
                productWidthTextbox.Enabled = false;
                productDepthTextbox.Enabled = false;
                weightTextBox.Enabled = false;
                countryCombobox.Enabled = false;
                bagWalletDetailButton.Enabled = false;
                numberComponentTextbox.Enabled = false;
                strapCombobox.Enabled = false;
                detachableCombobox.Enabled = false;
                zippedCombobox.Enabled = false;
                shippedFlatCombobox.Enabled = false;
                shippedFoldedCombobox.Enabled = false;
            }
        }

        /* the event for product family combobox change that determine the active and website flag for the design */
        private void productFamilyCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Credentials.DesignCon))
            {
                SqlCommand command = new SqlCommand("SELECT Active FROM ref_Families WHERE Design_Service_Family_Description = \'" + productFamilyCombobox.SelectedItem + '\'', connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                // the case of inactive family -> set design to inactive and website to false
                if (!reader.GetBoolean(0))
                {
                    activeDesignButton.Enabled = false;
                    inactiveDesignButton.Enabled = false;
                    displayedOnWebsiteCombobox.Enabled = false;
                    displayedOnWebsiteCombobox.SelectedIndex = 1;
                    active = false;
                }
                else
                {
                    activeDesignButton.Enabled = true;
                    inactiveDesignButton.Enabled = true;
                    displayedOnWebsiteCombobox.Enabled = true;
                }
            }
        }

        #region Translate Button 1 Event
        /* the event for the first translate button that translate English to French */
        private void translateButton1_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerTranslate1.IsBusy)
                backgroundWorkerTranslate1.RunWorkerAsync();
        }
        private void backgroundWorkerTranslate1_DoWork(object sender, DoWorkEventArgs e)
        {
            // if the user does not enter the description
            if (shortEnglishDescriptionTextbox.Text == "" && extendedEnglishDescriptionTextbox.Text == "" && trendShortEnglishTextbox.Text == "" && trendExtendedEnglishTextbox.Text == "")
            {
                MessageBox.Show("You haven't put the description yet", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // down to business, this is for short description
            if (shortEnglishDescriptionTextbox.Text != "")
                shortFrenchDescription = Translate.NowTranslate(shortEnglishDescriptionTextbox.Text);

            // this is for extended description
            if (extendedEnglishDescriptionTextbox.Text != "")
                extendedFrenchDescription = Translate.NowTranslate(extendedEnglishDescriptionTextbox.Text);

            // this is for trend short description
            if (trendShortEnglishTextbox.Text != "")
                trendShortFrenchDescription = Translate.NowTranslate(trendShortEnglishTextbox.Text);

            // this is for trend extended description
            if (trendExtendedEnglishTextbox.Text != "")
                trendExtendedFrenchDescription = Translate.NowTranslate(trendExtendedEnglishTextbox.Text);
        }
        private void backgroundWorkerTranslate1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // show result to textbox
            if (Translate.Error)
            {
                MessageBox.Show("Error: " + Translate.ErrorMessage, "Translate Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            shortFrenchDescriptionTextbox.Text = shortFrenchDescription;

            if (Translate.Error)
            {
                MessageBox.Show("Error: " + Translate.ErrorMessage, "Translate Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            extendedFrenchDescriptionTextbox.Text = extendedFrenchDescription;

            if (Translate.Error)
            {
                MessageBox.Show("Error: " + Translate.ErrorMessage, "Translate Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            trendShortFrenchTextbox.Text = trendShortFrenchDescription;

            if (Translate.Error)
                MessageBox.Show("Error: " + Translate.ErrorMessage, "Translate Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                trendExtendedFrenchTextbox.Text = trendExtendedFrenchDescription;
        }
        #endregion

        /* online button clicks that allow user to edit design online description */
        private void onlineButton_Click(object sender, EventArgs e)
        {
            Online online = new Online("Design Online Description", designOnlineEnglish, designOnlineFrench, Color.FromArgb(78, 95, 190));
            online.ShowDialog(this);

            // set color online 
            if (online.DialogResult != DialogResult.OK) return;

            designOnlineEnglish = online.English;
            designOnlineFrench = online.French;
        }

        /* bag detail button clicks that allow user to edit more fields for design */
        private void bagWalletDetailButton_Click(object sender, EventArgs e)
        {
            BagWalletDetail detail = new BagWalletDetail(bw.ShoulderDropLength, bw.HandleStrapDropLength, bw.NotableStrapGeneralFeatures, bw.ProtectiveFeet, bw.Closure, bw.InnerPocket, bw.OutsidePocket, bw.SizeDifferentiation, 
                                                         bw.DustBag, bw.AuthenticityCard, bw.BillCompartment, bw.CardSlot, Color.FromArgb(78, 95, 190), Color.White);
            detail.ShowDialog(this);

            // set color online 
            if (detail.DialogResult != DialogResult.OK) return;

            // allocate data
            bw.ShoulderDropLength = detail.ShoulderDropLength;
            bw.HandleStrapDropLength = detail.HandleStrapDropLength;
            bw.NotableStrapGeneralFeatures = detail.NotableStrapGeneralFeatures;
            bw.ProtectiveFeet = detail.ProtectiveFeet;
            bw.Closure = detail.Closure;
            bw.InnerPocket = detail.InnerPocket;
            bw.OutsidePocket = detail.OutsidePocket;
            bw.SizeDifferentiation = detail.SizeDifferentiation;
            bw.DustBag = detail.DustBag;
            bw.AuthenticityCard = detail.AuthenticityCard;
            bw.BillCompartment = detail.BillCompartment;
            bw.CardSlot = detail.CardSlot;
        }

        #region Translate Button 2 Event
        /* the event for the second translate button that translate English to French */
        private void translateButton2_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerTranslate2.IsBusy)
                backgroundWorkerTranslate2.RunWorkerAsync();
        }
        private void backgroundWorkerTranslate2_DoWork(object sender, DoWorkEventArgs e)
        {
            // if the user does not enter the description
            if (option1EnglishTextbox.Text == "" && option2EnglishTextbox.Text == "" && option3EnglishTextbox.Text == "" && option4EnglishTextbox.Text == "" && option5EnglishTextbox.Text == "")
            {
                MessageBox.Show("You haven't put the description yet", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // down to business, this is for the first option
            if (option1EnglishTextbox.Text != "")
                frenchOption[0] = Translate.NowTranslate(option1EnglishTextbox.Text);

            // this is for the second option
            if (option2EnglishTextbox.Text != "")
                frenchOption[1] = Translate.NowTranslate(option2EnglishTextbox.Text);

            // this is for the third option
            if (option3EnglishTextbox.Text != "")
                frenchOption[2] = Translate.NowTranslate(option3EnglishTextbox.Text);

            // this is for the fourth option
            if (option4EnglishTextbox.Text != "")
                frenchOption[3] = Translate.NowTranslate(option4EnglishTextbox.Text);

            // this is for the fifth option
            if (option5EnglishTextbox.Text != "")
                frenchOption[4] = Translate.NowTranslate(option5EnglishTextbox.Text);
        }
        private void backgroundWorkerTranslate2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // show result to textbox
            if (Translate.Error)
            {
                MessageBox.Show("Error: " + Translate.ErrorMessage, "Translate Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            option1FrenchTextbox.Text = frenchOption[0];

            if (Translate.Error)
            {
                MessageBox.Show("Error: " + Translate.ErrorMessage, "Translate Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            option2FrenchTextbox.Text = frenchOption[1];

            if (Translate.Error)
            {
                MessageBox.Show("Error: " + Translate.ErrorMessage, "Translate Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            option3FrenchTextbox.Text = frenchOption[2];

            if (Translate.Error)
            {
                MessageBox.Show("Error: " + Translate.ErrorMessage, "Translate Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            option4FrenchTextbox.Text = frenchOption[3];

            if (Translate.Error)
                MessageBox.Show("Error: " + Translate.ErrorMessage, "Translate Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                option5FrenchTextbox.Text = frenchOption[4];
        }
        #endregion

        /* a method that turn boolean from the user input to 1 or 0 */
        private void CalculateTrueAndFalse()
        {
            // get the selected true or false from comboboxes
            boolean[0] = monogrammedCombobox.SelectedItem.ToString();
            boolean[1] = imprintedCombobox.SelectedItem.ToString();
            boolean[2] = strapCombobox.SelectedItem.ToString();
            boolean[3] = detachableCombobox.SelectedItem.ToString();
            boolean[4] = zippedCombobox.SelectedItem.ToString();
            boolean[5] = shippedFlatCombobox.SelectedItem.ToString();
            boolean[6] = shippedFoldedCombobox.SelectedItem.ToString();
            boolean[7] = displayedOnWebsiteCombobox.SelectedItem.ToString();

            // loop through to determine 1 or 0
            for (int i = 0; i < 8; i++)
                integer[i] = boolean[i] == "True" ? 1 : 0;

            // special cases for active and gift box
            integer[8] = active ? 1 : 0;
            integer[9] = giftCheckbox.Checked ? 1 : 0;
        }

        #region Add Design Button Event
        /* the event for add design button */
        private void addDesignButton_Click(object sender, EventArgs e)
        {
            if (designServiceCodeTextbox.Text == "")
            {
                designServiceCodeTextbox.BackColor = Color.Red;
                MessageBox.Show("Please provide the Design Service Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // get data from user input
            productFamily = productFamilyCombobox.SelectedItem.ToString();
            designServiceFlag = designServiceFlagCombobox.SelectedItem.ToString();
            country = countryCombobox.SelectedItem.ToString();

            CalculateTrueAndFalse();

            if (!backgroundWorkerAddDesign.IsBusy)
                backgroundWorkerAddDesign.RunWorkerAsync();
        }
        private void backgroundWorkerAddDesign_DoWork(object sender, DoWorkEventArgs e)
        {
            // simulate progress 1% ~ 30%
            for (int i = 1; i <= 30; i++)
            {
                Thread.Sleep(25);
                backgroundWorkerAddDesign.ReportProgress(i);
            }

            // get data from user input
            designServiceCode = designServiceCodeTextbox.Text;
            internalName = internalNameTextbox.Text.Replace("'", "''");
            shortEnglishDescription = shortEnglishDescriptionTextbox.Text.Replace("'", "''");
            shortFrenchDescription = shortFrenchDescriptionTextbox.Text.Replace("'", "''");
            extendedEnglishDescription = extendedEnglishDescriptionTextbox.Text.Replace("'", "''");
            extendedFrenchDescription = extendedFrenchDescriptionTextbox.Text.Replace("'", "''");
            trendShortEnglishDescription = trendShortEnglishTextbox.Text.Replace("'", "''");
            trendShortFrenchDescription = trendShortFrenchTextbox.Text.Replace("'", "''");
            trendExtendedEnglishDescription = trendExtendedEnglishTextbox.Text.Replace("'", "''");
            trendExtendedFrenchDescription = trendExtendedFrenchTextbox.Text.Replace("'", "''"); 
            imprintHeight = imprintHeightTextbox.Text;
            if (imprintHeight == "") imprintHeight = "NULL";
            imprintWidth = imprintWidthTextbox.Text;
            if (imprintWidth == "") imprintWidth = "NULL";
            productHeight = productHeightTextbox.Text;
            if (productHeight == "") productHeight = "NULL";
            productWidth = productWidthTextbox.Text;
            if (productWidth == "") productWidth = "NULL";
            productDepth = productDepthTextbox.Text;
            if (productDepth == "") productDepth = "NULL";
            weight = weightTextBox.Text;
            if (weight == "") weight = "NULL";
            numberComponents = numberComponentTextbox.Text;
            if (numberComponents == "") numberComponents = "NULL";
            shippableHeight = shippableHeightTextbox.Text;
            if (shippableHeight == "") shippableHeight = "NULL";
            shippableWidth = productWidthTextbox.Text;
            if (shippableWidth == "") shippableWidth = "NULL";
            shippableDepth = shippableDepthTextbox.Text;
            if (shippableDepth == "") shippableDepth = "NULL";
            shippableWeight = shippableWeightTextbox.Text;
            if (shippableWeight == "") shippableWeight = "NULL";
            tsc = tscTextbox.Text.Replace("'", "''");
            theBay = theBayTextbox.Text.Replace("'", "''");
            bestbuy = bestbuyTextbox.Text.Replace("'", "''");
            shopca = shopcaTextbox.Text.Replace("'", "''");
            amazon = amazonTextbox.Text.Replace("'", "''");
            sears = searsTextbox.Text.Replace("'", "''");
            staples = staplesTextbox.Text.Replace("'", "''");
            walmart = walmartTextbox.Text.Replace("'", "''");
            englishOption[0] = option1EnglishTextbox.Text.Replace("'", "''");
            englishOption[1] = option2EnglishTextbox.Text.Replace("'", "''");
            englishOption[2] = option3EnglishTextbox.Text.Replace("'", "''");
            englishOption[3] = option4EnglishTextbox.Text.Replace("'", "''");
            englishOption[4] = option5EnglishTextbox.Text.Replace("'", "''");
            frenchOption[0] = option1FrenchTextbox.Text.Replace("'", "''");
            frenchOption[1] = option2FrenchTextbox.Text.Replace("'", "''");
            frenchOption[2] = option3FrenchTextbox.Text.Replace("'", "''");
            frenchOption[3] = option4FrenchTextbox.Text.Replace("'", "''");
            frenchOption[4] = option5FrenchTextbox.Text.Replace("'", "''");
            productFamily = productFamily.Replace("'", "''");

            // addition field (I don't really know what this field is for ==! )
            string designUrl = "https://www.ashlinbpg.com/index.php/" + designServiceCode + "/html";

            // simulate progress 30% ~ 60%
            for (int i = 30; i <= 60; i++)
            {
                Thread.Sleep(25);
                backgroundWorkerAddDesign.ReportProgress(i);
            }

            // connect to database and insert new row
            try
            {
                using (SqlConnection connection = new SqlConnection(Credentials.DesignCon))
                {
                    // this is for searching family code for product family
                    SqlCommand command = new SqlCommand("SELECT Design_Service_Family_Code FROM ref_Families WHERE Design_service_Family_Description = \'" + productFamily + '\'', connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    // this is the real thing
                    command = new SqlCommand("INSERT INTO master_Design_Attributes (Design_Service_Code,GiftBox,Brand,Design_Service_Flag,Design_Service_Family_Code,Design_Service_Fashion_Name_Ashlin,Design_Service_Fashion_Name_TSC_CA,Design_Service_Fashion_Name_THE_BAY,Design_Service_Fashion_Name_BESTBUY_CA,Design_Service_Fashion_Name_SHOP_CA,Design_Service_Fashion_Name_AMAZON_CA,Design_Service_Fashion_Name_AMAZON_COM,Design_Service_Fashion_Name_SEARS_CA,Design_Service_Fashion_Name_STAPLES_CA,Design_Service_Fashion_Name_WALMART, Short_Description,Short_Description_FR,Extended_Description,Extended_Description_FR,Trend_Short_Description,Trend_Short_Description_FR,Trend_Extended_Description,Trend_Extended_Description_FR,Design_Online,Design_Online_FR,Imprintable,Imprint_Height_cm,Imprint_Width_cm,Width_cm,Height_cm,Depth_cm, Weight_grams,Flat_Shippable,Fold_Shippable,Shippable_Width_cm,Shippable_Height_cm,Shippable_Depth_cm,Shippable_Weight_grams,Components,Strap,Detachable_Strap,Zippered_Enclosure,Option_1,Option_1_FR,Option_2,Option_2_FR,Option_3,Option_3_FR,Option_4,Option_4_FR,Option_5,Option_5_FR,Website_Flag,Active,Date_Added,Design_URL,Monogram,Country,ShoulderDropLength,HandleStrapDropLength,NotableStrapGeneralFeatures,ProtectiveFeet,Closure,InnerPocket,OutsidePocket,SizeDifferentiation,Dust_Bag,Authenticity_Card,Bill_Compartment,Card_Slot) "
                                           + "VALUES (\'" + designServiceCode + "\'," + integer[9] + ",\'Ashlin®\',\'" + designServiceFlag + "\',\'" + reader.GetString(0) + "\',\'" + internalName + "\',\'" + tsc + "\',\'" + theBay + "\',\'" + bestbuy + "\',\'" + shopca + "\',\'" + amazon + "\',\'" + amazon + "\',\'" + sears + "\',\'" + staples + "\',\'" + walmart + "\',\'" + shortEnglishDescription + "\',\'" + shortFrenchDescription + "\',\'" + extendedEnglishDescription + "\',\'" + extendedFrenchDescription + "\',\'" + trendShortEnglishDescription + "\',\'" + trendShortFrenchDescription + "\',\'" + trendExtendedEnglishDescription + "\',\'" + trendExtendedFrenchDescription + "\',\'" + designOnlineEnglish.Replace("'", "''") + "\',\'" + designOnlineFrench.Replace("'", "''") + "\'," + integer[1] + ',' + imprintHeight + ',' + imprintWidth + ',' + productWidth + ", " + productHeight + ',' + productDepth + ',' + weight + ',' + integer[5] + ',' + integer[6] + ',' + shippableWidth + ',' + shippableHeight + ',' + shippableDepth + ',' + shippableWeight + ',' + numberComponents + ',' + integer[2] + ',' + integer[3] + ',' + integer[4] + ",\'" + englishOption[0] + "\',\'" + frenchOption[0] + "\',\'" + englishOption[1] + "\',\'" 
                                           + frenchOption[1] + "\',\'" + englishOption[2] + "\',\'" + frenchOption[2] + "\',\'" + englishOption[3] + "\',\'" + frenchOption[3] + "\',\'" + englishOption[4] + "\',\'" + frenchOption[4] + "\'," + integer[7] + "," + integer[8] + ",\'" + DateTime.Today.ToString("yyyy-MM-dd") + "\',\'" + designUrl + "\'," + integer[0] + ",\'" + country + "\'," + bw.ShoulderDropLength + ',' + bw.HandleStrapDropLength + ",\'" + bw.NotableStrapGeneralFeatures + "\',\'" + bw.ProtectiveFeet + "\',\'" + bw.Closure + "\',\'" + bw.InnerPocket + "\',\'" + bw.OutsidePocket + "\',\'" + bw.SizeDifferentiation + "\',\'" + bw.DustBag + "\',\'" + bw.AuthenticityCard + "\'," + bw.BillCompartment + ',' + bw.CardSlot + ')', connection);
                    reader.Close();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error happen during database updating:\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // simulate progress 60% ~ 100%
            for (int i = 60; i <= 100; i++)
            {
                Thread.Sleep(25);
                backgroundWorkerAddDesign.ReportProgress(i);
            }
        }
        private void backgroundWorkerAddDesign_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
        #endregion

        #region Active and Inactive Buttons Event
        /* the event for active and inactive buttons click */
        private void activeDesignButton_Click(object sender, EventArgs e)
        {
            active = true;    // make active true

            // set buttons enability
            activeDesignButton.Enabled = false;
            inactiveDesignButton.Enabled = true;

            // set website flag enability
            displayedOnWebsiteCombobox.Enabled = true;

            AutoScrollPosition = new Point(HorizontalScroll.Value, VerticalScroll.Value);
        }
        private void inactiveDesignButton_Click(object sender, EventArgs e)
        {
            active = false;    // make active false

            // set buttons enability
            inactiveDesignButton.Enabled = false;
            activeDesignButton.Enabled = true;

            // set website flag enability
            displayedOnWebsiteCombobox.SelectedIndex = 1;
            displayedOnWebsiteCombobox.Enabled = false;

            AutoScrollPosition = new Point(HorizontalScroll.Value, VerticalScroll.Value);
        }
        #endregion

        /* the event for imprinted combobox selected item changed that change imprint dimensions enability*/
        private void imprintedCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (imprintedCombobox.SelectedItem.ToString() == "False")
            {
                imprintHeightTextbox.Enabled = false;
                imprintWidthTextbox.Enabled = false;
                imprintHeightTextbox.Text = string.Empty;
                imprintWidthTextbox.Text = string.Empty;
            }
            else
            {
                imprintHeightTextbox.Enabled = true;
                imprintWidthTextbox.Enabled = true;
            }
        }

        #region Imprint Dimensions Textboxes Keypress Event
        /* the key press event for imprint dimensions textbox that only allow number */
        private void imprintHeightTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
                e.Handled = true;
        }
        private void imprintWidthTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
                e.Handled = true;
        }
        #endregion

        #region Shipped Falt and Shipped Folded Comboboxes Event
        /* the event for shipped properties comboboxes selected value changed */
        private void shippedFlatCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (shippedFlatCombobox.SelectedItem.ToString() == "True")
            {
                shippedFoldedCombobox.Enabled = false;
                isFlat = true;

                // calculate flat value for shippable width
                if (shippableWidthTextbox.Text != "")
                    shippableWidthTextbox.Text = (Convert.ToDouble(shippableWidthTextbox.Text)*1.2).ToString();

                // calculate flat value for shippable depth
                if (shippableWidthTextbox.Text != "")
                    shippableDepthTextbox.Text = (Convert.ToDouble(shippableDepthTextbox.Text)*0.3).ToString();
            }
            else
            {
                shippedFoldedCombobox.Enabled = true;
                isFlat = false;

                shippableWidthTextbox.Text = productWidthTextbox.Text;
                shippableDepthTextbox.Text = productDepthTextbox.Text;
            }
        }
        private void shippedFoldedCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (shippedFoldedCombobox.SelectedItem.ToString() == "True")
            {
                shippedFlatCombobox.Enabled = false;
                shippableHeightTextbox.Enabled = true;
                shippableWidthTextbox.Enabled = true;
                shippableDepthTextbox.Enabled = true;
                isFolded = true;

                shippableHeightTextbox.Text = string.Empty;
                shippableWidthTextbox.Text = string.Empty;
                shippableDepthTextbox.Text = string.Empty;
                shippableWeightTextbox.Text = string.Empty;
            }
            else
            {
                shippedFlatCombobox.Enabled = true;
                shippableHeightTextbox.Enabled = false;
                shippableWidthTextbox.Enabled = false;
                shippableDepthTextbox.Enabled = false;
                isFolded = false;

                shippableWeightTextbox.Text = string.Empty;
                shippableHeightTextbox.Text = productHeightTextbox.Text;
                shippableWidthTextbox.Text = productWidthTextbox.Text;
                shippableDepthTextbox.Text = productDepthTextbox.Text;
            }
        }
        #endregion

        #region Product Dimensions Textboxes Text Change Event 
        /* the event for product dimension textboxes' text changed that show the correspond value in shippable dimension textboxes */
        private void productHeightTextbox_TextChanged(object sender, EventArgs e)
        {
            if (!isFolded) // normal situation
                shippableHeightTextbox.Text = productHeightTextbox.Text;
        }
        private void productWidthTextbox_TextChanged(object sender, EventArgs e)
        {
            if (!isFlat && !isFolded) // normal situation
                shippableWidthTextbox.Text = productWidthTextbox.Text;
            else if (isFlat && productWidthTextbox.Text != "") // is flat, calculate the flat value of width
                shippableWidthTextbox.Text = (Convert.ToDouble(productWidthTextbox.Text)*1.2).ToString(CultureInfo.InvariantCulture);
        }
        private void productDepthTextbox_TextChanged(object sender, EventArgs e)
        {
            if (!isFlat && !isFolded) // normal situation
                shippableDepthTextbox.Text = productDepthTextbox.Text;
            else if (isFlat && productDepthTextbox.Text != "") // is flat, calculate the flat value of depth
                shippableDepthTextbox.Text = (Convert.ToDouble(productDepthTextbox.Text)*0.3).ToString(CultureInfo.InvariantCulture);
        }
        #endregion

        #region Product Dimensions Textboxes Keypress Event 
        /* the restriction for dimension textboxes that only allow numbers */
        private void productHeightTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
                e.Handled = true;
        }
        private void productWidthTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
                e.Handled = true;
        }
        private void productDepthTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
                e.Handled = true;
        }
        #endregion

        #region Shippable Dimensions Textboxes Text Change Event 
        /* the event when shippable dimensions textboxes' text change that calculate the shippable weight if all fields are filled */
        private void shippableHeightTextbox_TextChanged(object sender, EventArgs e)
        {
            if (shippableHeightTextbox.Text != "" && shippableWidthTextbox.Text != "" &&
                shippableDepthTextbox.Text != "")
                shippableWeightTextbox.Text = Math.Round(Convert.ToDouble(shippableHeightTextbox.Text)*Convert.ToDouble(shippableWidthTextbox.Text)*Convert.ToDouble(shippableDepthTextbox.Text)/6, 2).ToString(CultureInfo.InvariantCulture);
            else
                shippableWeightTextbox.Text = string.Empty;
        }
        private void shippableWidthTextbox_TextChanged(object sender, EventArgs e)
        {
            if (shippableHeightTextbox.Text != "" && shippableWidthTextbox.Text != "" &&
                shippableDepthTextbox.Text != "")
                shippableWeightTextbox.Text = Math.Round(Convert.ToDouble(shippableHeightTextbox.Text)*Convert.ToDouble(shippableWidthTextbox.Text)*Convert.ToDouble(shippableDepthTextbox.Text)/6, 2).ToString(CultureInfo.InvariantCulture);
            else
                shippableWeightTextbox.Text = string.Empty;
        }
        private void shippableDepthTextbox_TextChanged(object sender, EventArgs e)
        {
            if (shippableHeightTextbox.Text != "" && shippableWidthTextbox.Text != "" && shippableDepthTextbox.Text != "")
                shippableWeightTextbox.Text = Math.Round(Convert.ToDouble(shippableHeightTextbox.Text)*Convert.ToDouble(shippableWidthTextbox.Text)*Convert.ToDouble(shippableDepthTextbox.Text)/ 6, 2).ToString(CultureInfo.InvariantCulture);
            else
                shippableWeightTextbox.Text = string.Empty;
        }
        #endregion

        #region Shippable Dimension Textboxes Keypress Event
        /* the restriction for shippable dimension textboxes that only allow numbers */
        private void shippableHeightTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
                e.Handled = true;
        }
        private void shippableWidthTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
                e.Handled = true;
        }
        private void shippableDepthTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
                e.Handled = true;
        }
        #endregion
    }
}
