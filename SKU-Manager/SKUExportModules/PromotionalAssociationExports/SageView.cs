﻿using SKU_Manager.SKUExportModules.Tables.PromotionalAssociationTables;
using SKU_Manager.SupportingClasses;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace SKU_Manager.SKUExportModules.PromotionalAssociationExports
{
    /* 
     * An application module for sku export that view the export sheet for sage
     */
    public partial class SageView : Form
    {
        // field for storing data
        private DataTable table;

        // supporting fields
        private readonly double usd = Currency.Usd;
        private int timeLeft;
        private bool done;  // default set to false

        // initialize BestbuyExportTable object
        private readonly SageExportTable sageTable = new SageExportTable();

        /* constructor that initialize graphic components */
        public SageView()
        {
            InitializeComponent();

            // set up timer
            timeLeft = 4;
            timer.Start();

            // set progress
            progressLabel.Text = 0 + " / " + sageTable.Total;

            // call background worker adding data on data grid view
            if (!backgroundWorkerTable.IsBusy)
                backgroundWorkerTable.RunWorkerAsync();
        }

        /* background worker that get the sage export table and send it to data grid view */
        private void backgroundWorkerTable_DoWork(object sender, DoWorkEventArgs e)
        {
            // send table to table field
            table = sageTable.GetTable();
        }
        private void backgroundWorkerTable_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridView.DataSource = table;

            // stop the loading promopt
            timer.Stop();
            loadingLabel.Visible = false;
            progressLabel.Visible = false;

            done = true;
            currencyButton.Enabled = true;

            // set first column to freeze
            dataGridView.Columns[0].Frozen = true;
        }

        /* the event for timer that make the visual of loading promopt */
        private void timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;

            // set progress
            progressLabel.Text = sageTable.Progress + " / " + sageTable.Total;

            if (timeLeft <= 0)
            {
                loadingLabel.Text = "Please Wait";
                timeLeft = 4;
            }
            else
                loadingLabel.Text += '.';
        }

        /* currency button click that switch the currency of the price */
        private void currencyButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (currencyButton.Text == @"=> USD")
            {
                // change currency in each row
                foreach (DataRow row in table.Rows)
                {
                    for (int i = 25; i <= 96; i++)
                        row[i] = Convert.ToDouble(row[i]) * usd;
                }

                // set currency to USD
                Currency.SageCurrency = "USD";
                currencyButton.Text = @"=> CAD";
            }
            else
            {
                // change currency in each row
                foreach (DataRow row in table.Rows)
                {
                    for (int i = 25; i <= 96; i++)
                        row[i] = Convert.ToDouble(row[i]) / usd;
                }

                // set currency to CAD
                Currency.SageCurrency = "CAD";
                currencyButton.Text = @"=> USD";
            }

            Cursor.Current = Cursors.Default;
        }

        /* the event for exit button click */
        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /* save the data when the form is closing */
        private void SageView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (done)
                Properties.Settings.Default.SageTable = table;
        }
    }
}
