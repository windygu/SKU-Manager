﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SKU_Manager.SupportingClasses;

namespace SKU_Manager.SplashModules.Add
{
    /*
     * An application module to add new family to SKU database
     */
    public partial class AddFamily : Form
    {
        // fields for storing adding color data
        private string familyCode;
        private string shortEnglishDescription;
        private string shortFrenchDescription;
        private string generalKeywords;
        private string[] amazonKeywords = new string[5];
        private string[] amazonCaNode = new string[2];
        private string[] amazonComNode = new string[2];
        private string sageCategory;
        private string sageTheme;
        private string esp;
        private string promoMarketing;
        private string uducat;
        private string distributorCentral;
        private string caHts;
        private string usHts;
        private string caDuty;
        private string usDuty;
        private bool active = true;    // default is set to true

        // fields for comboBoxes
        private ArrayList canadianHtsList = new ArrayList();
        private ArrayList usHtsList = new ArrayList();
        private ArrayList sageCategoryList = new ArrayList();
        private ArrayList sageThemeList = new ArrayList();
        private ArrayList espList = new ArrayList();
        private ArrayList promoMarketingList = new ArrayList();
        private ArrayList uducatList = new ArrayList();
        private ArrayList distributorCentralList = new ArrayList();

        // connection string to the database
        private string connectionString = Properties.Settings.Default.Designcs;

        /* constructor that initialize graphic component */
        public AddFamily()
        {
            InitializeComponent();

            // call background worker
            if (!backgroundWorkerCombobox.IsBusy)
            {
                backgroundWorkerCombobox.RunWorkerAsync();
            }
        }

        /* the backgound workder for adding items to comboBoxes */
        private void backgroundWorkerCombobox_DoWork(object sender, DoWorkEventArgs e)
        {
            // local fields for comboBoxes
            SqlCommand command;
            SqlDataReader reader;

            // make comboBox for Canadian HTS
            SqlConnection connection = new SqlConnection(connectionString);
            command = new SqlCommand("SELECT HTS_CA FROM HTS_CA;", connection);
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                canadianHtsList.Add(reader.GetValue(0));
            }
            reader.Close();

            // make comboBox for US HTS
            command = new SqlCommand("SELECT HTS_US FROM HTS_US;", connection);  
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                usHtsList.Add(reader.GetValue(0));
            }
            reader.Close();

            // make comboBox for SAGE [CATEGORY]
            command = new SqlCommand("SELECT Design_Service_Family_Category_Sage FROM list_online_product_categories WHERE Design_Service_Family_Category_Sage is not NULL;", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                sageCategoryList.Add(reader.GetValue(0));
            }
            reader.Close();

            // make comboBox for SAGE [THEME]
            command = new SqlCommand("SELECT Design_Service_Family_Themes_Sage FROM list_online_product_categories WHERE Design_Service_Family_Themes_Sage is not NULL;", connection); 
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                sageThemeList.Add(reader.GetValue(0));
            }
            reader.Close();

            // make comboBox for ESP
            command = new SqlCommand("SELECT Design_Service_Family_Category_ESP FROM list_online_product_categories WHERE Design_Service_Family_Category_ESP is not NULL;", connection);  
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                espList.Add(reader.GetValue(0));
            }
            reader.Close();

            // make comboBox for promo marketing
            command = new SqlCommand("SELECT Design_Service_Family_Category_PromoMarketing FROM list_online_product_categories WHERE Design_Service_Family_Category_PromoMarketing is not NULL;", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                promoMarketingList.Add(reader.GetValue(0));
            }
            reader.Close();

            // make comboBox for UDUCAT
            command = new SqlCommand("SELECT Design_Service_Family_Category_UDUCAT FROM list_online_product_categories WHERE Design_Service_Family_Category_UDUCAT is not NULL;", connection);   
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                uducatList.Add(reader.GetValue(0));
            }
            reader.Close();

            // make comboBox for distributor central
            command = new SqlCommand("SELECT Design_Service_Family_Category_DistributorCentral FROM list_online_product_categories WHERE Design_Service_Family_Category_DistributorCentral is not NULL;", connection);   
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                distributorCentralList.Add(reader.GetValue(0));
            }
            connection.Close();
        }
        private void backgroundWorkerCombobox_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            canadianHtsCombobox.DataSource = canadianHtsList;
            usHtsCombobox.DataSource = usHtsList;
            sageCategoryCombobox.DataSource = sageCategoryList;
            sageThemeCombobox.DataSource = sageThemeList;
            espCombobox.DataSource = espList;
            promoMarketingCombobox.DataSource = promoMarketingList;
            uducatCombobox.DataSource = uducatList;
            distributorCentralCombobox.DataSource = distributorCentralList;

        }

        /* the event for translate button that translate English to French */
        private void translateButton_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerTranslate.IsBusy)
            {
                backgroundWorkerTranslate.RunWorkerAsync();
            }
        }
        private void backgroundWorkerTranslate_DoWork(object sender, DoWorkEventArgs e)
        {
            // if the user does not enter the description
            if (shortEnglishDescriptionTextbox.Text == "")
            {
                MessageBox.Show("You haven't put the description yet", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // initialize a Translation class for translation
            Translate translate = new Translate();

            // down to business, this is for short description
            translate.nowTranslate(shortEnglishDescriptionTextbox.Text);
            shortFrenchDescription = translate.getFrench();
        }
        private void backgroundWorkerTranslate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // show result to textbox
            shortFrenchDescriptionTextbox.Text = shortFrenchDescription;
        }

        /* the event for showing the corresponding duty form the item selected in combobox */
        private void canadianHtsCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            // connect to database to get the info about this material code
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT CA_Duty FROM HTS_CA WHERE HTS_CA = \'" + canadianHtsCombobox.SelectedItem.ToString() + "\';", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                caDutyTextbox.Text = reader.GetDecimal(0).ToString();
            }
        }
        private void usHtsCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            // connect to database to get the info about this material code
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT US_Duty FROM HTS_US WHERE HTS_US = \'" + usHtsCombobox.SelectedItem.ToString() + "\';", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                usDutyTextbox.Text = reader.GetDecimal(0).ToString();
            }
        }

        /* the event for sage categroy add and remove button click */
        private void addSageCategoryButton_Click(object sender, EventArgs e)
        {
            // add selected item to the textbox
            if (sageCategoryTextbox.Text.Contains(sageCategoryCombobox.SelectedItem.ToString())) 
            {
                return;
            }
            else if (sageCategoryTextbox.Text == "")
            {
                sageCategoryTextbox.Text = sageCategoryCombobox.SelectedItem.ToString();
            }
            else
            {
                sageCategoryTextbox.Text += "; " + sageCategoryCombobox.SelectedItem.ToString();
            }
        }
        private void removeSageCategoryButton_Click(object sender, EventArgs e)
        {
            if (sageCategoryTextbox.Text.Contains(";"))
            {
                int index = sageCategoryTextbox.Text.LastIndexOf(';');
                sageCategoryTextbox.Text = sageCategoryTextbox.Text.Substring(0, index);
            } else
            {
                sageCategoryTextbox.Text = "";
            }
        }

        /* the event for sage theme add and remove button click */
        private void addSageThemeButton_Click(object sender, EventArgs e)
        {
            // add selected item to the textbox
            if (sageThemeTextbox.Text.Contains(sageThemeCombobox.SelectedItem.ToString()))
            {
                return;
            }
            else if (sageThemeTextbox.Text == "")
            {
                sageThemeTextbox.Text = sageThemeCombobox.SelectedItem.ToString();
            }
            else
            {
                sageThemeTextbox.Text += "; " + sageThemeCombobox.SelectedItem.ToString();
            }
        }
        private void removeSageThemeButton_Click(object sender, EventArgs e)
        {
            if (sageThemeTextbox.Text.Contains(";"))
            {
                int index = sageThemeTextbox.Text.LastIndexOf(';');
                sageThemeTextbox.Text = sageThemeTextbox.Text.Substring(0, index);
            }
            else
            {
                sageThemeTextbox.Text = "";
            }
        }

        /* the event for ESP add and remove button click */
        private void addEspButton_Click(object sender, EventArgs e)
        {
            // add selected item to the textbox
            if (espTextbox.Text.Contains(espCombobox.SelectedItem.ToString()))
            {
                return;
            }
            else if (espTextbox.Text == "")
            {
                espTextbox.Text = espCombobox.SelectedItem.ToString();
            }
            else
            {
                espTextbox.Text += "; " + espCombobox.SelectedItem.ToString();
            }
        }
        private void removeEspButton_Click(object sender, EventArgs e)
        {
            if (espTextbox.Text.Contains(";"))
            {
                int index = espTextbox.Text.LastIndexOf(';');
                espTextbox.Text = espTextbox.Text.Substring(0, index);
            }
            else
            {
                espTextbox.Text = "";
            }
        }

        /* the event for promo marketing add and remove button click */
        private void addPromoMarketingButton_Click(object sender, EventArgs e)
        {
            // add selected item to the textbox
            if (promoMarketingTextbox.Text.Contains(promoMarketingCombobox.SelectedItem.ToString()))
            {
                return;
            }
            else if (promoMarketingTextbox.Text == "")
            {
                promoMarketingTextbox.Text = promoMarketingCombobox.SelectedItem.ToString();
            }
            else
            {
                promoMarketingTextbox.Text += "; " + promoMarketingCombobox.SelectedItem.ToString();
            }
        }
        private void removingPromoMarketingButton_Click(object sender, EventArgs e)
        {
            if (promoMarketingTextbox.Text.Contains(";"))
            {
                int index = promoMarketingTextbox.Text.LastIndexOf(';');
                promoMarketingTextbox.Text = promoMarketingTextbox.Text.Substring(0, index);
            }
            else
            {
                promoMarketingTextbox.Text = "";
            }
        }

        /* the event for UDUCAT add and remove button click */
        private void addUducatButton_Click(object sender, EventArgs e)
        {
            // add selected item to the textbox
            if (uducatTextbox.Text.Contains(uducatCombobox.SelectedItem.ToString()))
            {
                return;
            }
            else if (uducatTextbox.Text == "")
            {
                uducatTextbox.Text = uducatCombobox.SelectedItem.ToString();
            }
            else
            {
                uducatTextbox.Text += "; " + uducatCombobox.SelectedItem.ToString();
            }
        }
        private void removeUducatButton_Click(object sender, EventArgs e)
        {
            if (uducatTextbox.Text.Contains(";"))
            {
                int index = uducatTextbox.Text.LastIndexOf(';');
                uducatTextbox.Text = uducatTextbox.Text.Substring(0, index);
            }
            else
            {
                uducatTextbox.Text = "";
            }
        }

        /* the event for distributor central add and remove button click */
        private void addDistributorCentralButton_Click(object sender, EventArgs e)
        {
            // add selected item to the textbox
            if (distributorCentralTextbox.Text.Contains(distributorCentralCombobox.SelectedItem.ToString()))
            {
                return;
            }
            else if (distributorCentralTextbox.Text == "")
            {
                distributorCentralTextbox.Text = distributorCentralCombobox.SelectedItem.ToString();
            }
            else
            {
                distributorCentralTextbox.Text += "; " + distributorCentralCombobox.SelectedItem.ToString();
            }
        }
        private void removeDistributorCentralButton_Click(object sender, EventArgs e)
        {
            if (distributorCentralTextbox.Text.Contains(";"))
            {
                int index = distributorCentralTextbox.Text.LastIndexOf(';');
                distributorCentralTextbox.Text = distributorCentralTextbox.Text.Substring(0, index);
            }
            else
            {
                distributorCentralTextbox.Text = "";
            }
        }

        /* the event for active and inactive family button that active and inactive the family */
        private void activeFamilyButton_Click(object sender, EventArgs e)
        {
            active = true;    // make active true

            // set buttons enability
            activeFamilyButton.Enabled = false;
            inactiveFamilyButton.Enabled = true;

            this.AutoScrollPosition = new Point(762, 841);
        }
        private void inactiveFamilyButton_Click(object sender, EventArgs e)
        {
            active = false;    // make active false

            // set buttons enability
            inactiveFamilyButton.Enabled = false;
            activeFamilyButton.Enabled = true;

            this.AutoScrollPosition = new Point(762, 841);
        }

        /* the event for add product family button */
        private void addProductFamilyButton_Click(object sender, EventArgs e)
        {
            // initialize some of the fields
            caHts = canadianHtsCombobox.SelectedItem.ToString();
            usHts = usHtsCombobox.SelectedItem.ToString();

            if (!backgroundWorkerAddFamily.IsBusy)
            {
                backgroundWorkerAddFamily.RunWorkerAsync();
            }
        }
        private void backgroundWorkerAddFamily_DoWork(object sender, DoWorkEventArgs e)
        {
            // simulate progress 1% ~ 30%
            for (int i = 1; i <= 30; i++)
            {
                Thread.Sleep(25);
                backgroundWorkerAddFamily.ReportProgress(i);
            }

            // get data from user input
            familyCode = productFamilyCodeTextbox.Text;
            shortEnglishDescription = shortEnglishDescriptionTextbox.Text.Replace("'", "''");
            shortFrenchDescription = shortFrenchDescriptionTextbox.Text.Replace("'", "''");
            generalKeywords = generalKeywordsTextBox.Text.Replace("'", "''");
            amazonKeywords[0] = amazonKeywordsTextbox1.Text.Replace("'", "''");
            amazonKeywords[1] = amazonKeywordsTextbox2.Text.Replace("'", "''");
            amazonKeywords[2] = amazonKeywordsTextbox3.Text.Replace("'", "''");
            amazonKeywords[3] = amazonKeywordsTextbox4.Text.Replace("'", "''");
            amazonKeywords[4] = amazonKeywordsTextbox5.Text.Replace("'", "''");
            amazonCaNode[0] = amazonCaTextbox1.Text;
            amazonCaNode[1] = amazonCaTextbox2.Text;
            amazonComNode[0] = amazonComTextbox1.Text;
            amazonComNode[1] = amazonComTextbox2.Text;
            sageCategory = sageCategoryTextbox.Text.Replace("'", "''");
            sageTheme = sageThemeTextbox.Text.Replace("'", "''");
            esp = espTextbox.Text.Replace("'", "''");
            promoMarketing = promoMarketingTextbox.Text.Replace("'", "''");
            uducat = uducatTextbox.Text.Replace("'", "''");
            distributorCentral = distributorCentralTextbox.Text.Replace("'", "''");
            caDuty = caDutyTextbox.Text;
            usDuty = usDutyTextbox.Text;

            // simulate progress 30% ~ 60%
            for (int i = 30; i <= 60; i++)
            {
                Thread.Sleep(25);
                backgroundWorkerAddFamily.ReportProgress(i);
            }

            // connect to database and insert new row
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO ref_Families (Design_Service_Family_Code, Design_Service_Family_Description, Design_Service_Family_Description_FR, Design_Service_Family_KeyWords_General, Design_Service_Family_Category_Sage, Design_Service_Family_Themes_Sage, Design_Service_Family_Category_ESP, Design_Service_Family_Category_PromoMarketing, Design_Service_Family_Category_UDUCAT, Design_Service_Family_Category_DistributorCentral, Active, Date_Added, KeyWords_Amazon_1, KeyWords_Amazon_2, KeyWords_Amazon_3, KeyWords_Amazon_4, KeyWords_Amazon_5, Amazon_Browse_Nodes_1_CDA, Amazon_Browse_Nodes_2_CDA, Amazon_Browse_Nodes_1_USA, Amazon_Browse_Nodes_2_USA, HTS_CA, HTS_US, CA_Duty, US_Duty) " +
                                                    "VALUES (\'" + familyCode + "\', \'" + shortEnglishDescription + "\', \'" + shortFrenchDescription + "\', \'" + generalKeywords + "\', \'" + sageCategory + "\', \'" + sageTheme + "\', \'" + esp + "\', \'" + promoMarketing + "\', \'" + uducat + "\', \'" + distributorCentral + "\', \'" + active.ToString() + "\', \'" + DateTime.Now.ToString() + "\', \'" + amazonKeywords[0] + "\', \'" + amazonKeywords[1] + "\', \'" + amazonKeywords[2] + "\', \'" + amazonKeywords[3] + "\', \'" + amazonKeywords[4] + "\', \'" + amazonCaNode[0] + "\', \'" + amazonCaNode[1] + "\', \'" + amazonComNode[0] + "\', \'" + amazonComNode[1] + "\', \'" + caHts + "\', \'" + usHts + "\', " + caDuty + ", " + usDuty + ");", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }

            // simulate progress 60% ~ 100%
            for (int i = 60; i <= 100; i++)
            {
                Thread.Sleep(25);
                backgroundWorkerAddFamily.ReportProgress(i);
            }
        }
        private void backgroundWorkerAddFamily_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
    }
}
