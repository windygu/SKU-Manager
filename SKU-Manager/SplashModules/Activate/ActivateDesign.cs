﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using SKU_Manager.ActiveInactiveList;
using System.Drawing;

namespace SKU_Manager.SplashModules.Activate
{
   /*
    * An application module that activate a design
    */
    public partial class ActivateDesign : Form
    {
        // fields for storing design data
        private string designCode;
        private string productFamily;
        private string designServiceFlag;
        private string internalName;
        private string shortDescription;
        private string extendedDescription;
        private string designOnlineEnglish;
        private string designOnlineFrench;
        private bool giftbox;

        // fields for combobox
        private readonly ArrayList designCodeList = new ArrayList();

        /* constructor that initialize graphic components */
        public ActivateDesign()
        {
            InitializeComponent();
            designCodeList.Add("");

            // call background worker for adding items to combobox
            if (!backgroundWorkerCombobox.IsBusy)
                backgroundWorkerCombobox.RunWorkerAsync();
        }

        #region Combobox Generation
        /* the backgound workder for adding items to comboBoxes */
        private void backgroundWorkerCombobox_DoWork(object sender, DoWorkEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Credentials.DesignCon))
            {
                SqlCommand command = new SqlCommand("SELECT Design_Service_Code FROM master_Design_Attributes WHERE Active = 'False' AND " +
                                                    "Design_Service_Family_Code IN (SELECT Design_Service_Family_Code FROM ref_Families WHERE Active = 'True') " +
                                                    "ORDER BY Design_Service_Code", connection);    // for selecting data
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();    // for reading data
                while (reader.Read())
                    designCodeList.Add(reader.GetString(0));
            }
        }
        private void backgroundWorkerCombobox_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            designCodeCombobox.DataSource = designCodeList;
        }
        #endregion

        #region Combobox Event
        /* the event when user change an item in combobox */
        private void designCodeCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            // change the information on the controls
            if (designCodeCombobox.SelectedItem.ToString() != "")
            {
                activateDesignButton.Enabled = true;
                onlineButton.Enabled = true;

                // set designCode field from the selected item 
                designCode = designCodeCombobox.SelectedItem.ToString();

                // call background worker for showing information of the selected item in combobox
                if (!backgroundWorkerInfo.IsBusy)
                    backgroundWorkerInfo.RunWorkerAsync();
            }
            else
            {
                // set the text to nothing
                productFamilyTextbox.Text = string.Empty;
                brandTextbox.Text = string.Empty;
                designServiceFlagTextbox.Text = string.Empty;
                internalNameTextbox.Text = string.Empty;
                shortDescriptionTextbox.Text = string.Empty;
                extendedDescriptionTextbox.Text = string.Empty;
                giftCheckbox.Checked = false;

                activateDesignButton.Enabled = false;
                onlineButton.Enabled = false;
            }
        }
        private void backgroundWorkerInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            // local fields for storing data
            DataTable table = new DataTable();

            // store data to the table
            using (SqlConnection connection = new SqlConnection(Credentials.DesignCon))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Design_Service_Family_Code, Design_Service_Flag, Design_Service_Fashion_Name_Ashlin, Short_Description, Extended_Description, Design_Online, Design_Online_FR, GiftBox " 
                                                          + "FROM master_Design_Attributes WHERE Design_Service_Code = \'" + designCode + '\'', connection);
                connection.Open();
                adapter.Fill(table);
            }

            // assign data to the fields
            productFamily = table.Rows[0][0].ToString();
            designServiceFlag = table.Rows[0][1].ToString();
            internalName = table.Rows[0][2].ToString();
            shortDescription = table.Rows[0][3].ToString();
            extendedDescription = table.Rows[0][4].ToString();
            designOnlineEnglish = table.Rows[0][5].ToString();
            designOnlineFrench = table.Rows[0][6].ToString();
            giftbox = Convert.ToBoolean(table.Rows[0][7]);
        }
        private void backgroundWorkerInfo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            productFamilyTextbox.Text = productFamily;
            brandTextbox.Text = @"Ashlin®";
            designServiceFlagTextbox.Text = designServiceFlag;
            internalNameTextbox.Text = internalName;
            shortDescriptionTextbox.Text = shortDescription;
            extendedDescriptionTextbox.Text = extendedDescription;
            giftCheckbox.Checked = giftbox;
        }
        #endregion

        #region Activate Button
        /* the event when activate design button is clicked */
        private void activateDesignButton_Click(object sender, EventArgs e)
        {
            // initiliaze designCode
            designCode = designCodeCombobox.SelectedItem.ToString();

            // call background worker, the update button will only be activated if vaild color has been selected, so no need to check
            if (!backgroundWorkerActivate.IsBusy)
                backgroundWorkerActivate.RunWorkerAsync();
        }
        private void backgroundWorkerActivate_DoWork(object sender, DoWorkEventArgs e)
        {
            // simulate progress 1% ~ 60%
            for (int i = 1; i <= 60; i++)
            {
                Thread.Sleep(25);
                backgroundWorkerActivate.ReportProgress(i);
            }

            // connect to database and activate the color
            try
            {
                using (SqlConnection connection = new SqlConnection(Credentials.DesignCon))
                {
                    SqlCommand command = new SqlCommand("UPDATE master_Design_Attributes SET Active = 'True', Date_Activated = \'" + DateTime.Now.ToString("yyyy-MM-dd") + "\' "
                                                      + "WHERE Design_Service_Code = \'" + designCode + '\'', connection);
                    connection.Open();
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
                backgroundWorkerActivate.ReportProgress(i);
            }
        }
        private void backgroundWorkerActivate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
        #endregion

        #region Active and Inactive
        /* the event for active and inactive list button that open the table of active design list */
        private void activeListButton_Click(object sender, EventArgs e)
        {
            new ActiveDesignList().ShowDialog(this);
        }
        private void inactiveListButton_Click(object sender, EventArgs e)
        {
            new InactiveDesignList().ShowDialog(this);
        }
        #endregion

        /* online button clicks that shows design online description */
        private void onlineButton_Click(object sender, EventArgs e)
        {
            Online online = new Online("Design Online Description", designOnlineEnglish, designOnlineFrench, Color.FromArgb(231, 222, 67), Color.Black , false);
            online.ShowDialog(this);
        }
    }
}
