﻿using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SKU_Manager.SKUExportModules.Tables.ActiveAttributeTables;

namespace SKU_Manager.SKUExportModules.ActiveAttributeExports
{
    /* 
     * An application module for sku export that view the export sheet for stock quantity
     */
    public partial class StockQuantityView : Form
    {
        // field for storing data
        private DataTable table;

        // supporting fields
        private int timeLeft;
        private bool complete;  // default set to false

        // initialize BestbuyExportTable object
        private readonly StockExportTable stockTable = new StockExportTable();

        /* constructor that initialize graphic components */
        public StockQuantityView()
        {
            InitializeComponent();

            // set up timer
            timeLeft = 4;
            timer.Start();

            // set progress
            progressLabel.Text = 0 + " / " + stockTable.Total;

            // call background worker adding data on data grid view
            if (!backgroundWorkerTable.IsBusy)
                backgroundWorkerTable.RunWorkerAsync();
        }

        /* background worker that get the stock quantity export table */
        private void backgroundWorkerTable_DoWork(object sender, DoWorkEventArgs e)
        {
            // send table to table field
            table = stockTable.GetTable();
        }
        private void backgroundWorkerTable_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridView.DataSource = table;

            // stop the loading promopt
            timer.Stop();
            loadingLabel.Visible = false;
            progressLabel.Visible = false;

            complete = true;
        }

        /* the event for timer that make the visual of loading promopt */
        private void timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;

            // set progress
            progressLabel.Text = stockTable.Progress + " / " + stockTable.Total;

            if (timeLeft <= 0)
            {
                loadingLabel.Text = "Please Wait";
                timeLeft = 4;
            }
            else
                loadingLabel.Text += '.';
        }

        /* the event for exit button click */
        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /* save the data when the form is closing */
        private void StockQuantityView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (complete)
                Properties.Settings.Default.StockQuantityTable = table;
        }
    }
}
