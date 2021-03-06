﻿namespace SKU_Manager.AdminModules.DirectUpdate
{
    partial class UpdateHts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateHts));
            this.dataGridViewCA = new System.Windows.Forms.DataGridView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.udpateButton = new System.Windows.Forms.Button();
            this.dataGridViewUS = new System.Windows.Forms.DataGridView();
            this.caHtsLabel = new System.Windows.Forms.Label();
            this.usHtsLabel = new System.Windows.Forms.Label();
            this.backgroundWorkerUpdate = new System.ComponentModel.BackgroundWorker();
            this.currencyLabel = new System.Windows.Forms.Label();
            this.dataGridViewCurrency = new System.Windows.Forms.DataGridView();
            this.listview = new System.Windows.Forms.ListView();
            this.currency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastestCurrencyLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCurrency)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCA
            // 
            this.dataGridViewCA.AllowUserToOrderColumns = true;
            this.dataGridViewCA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dataGridViewCA.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewCA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewCA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCA.GridColor = System.Drawing.Color.Purple;
            this.dataGridViewCA.Location = new System.Drawing.Point(0, 45);
            this.dataGridViewCA.Name = "dataGridViewCA";
            this.dataGridViewCA.Size = new System.Drawing.Size(655, 283);
            this.dataGridViewCA.TabIndex = 2;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.ForeColor = System.Drawing.Color.Thistle;
            this.progressBar.Location = new System.Drawing.Point(-8, 609);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1345, 150);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 8;
            // 
            // udpateButton
            // 
            this.udpateButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.udpateButton.BackColor = System.Drawing.Color.Purple;
            this.udpateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udpateButton.ForeColor = System.Drawing.Color.White;
            this.udpateButton.Image = ((System.Drawing.Image)(resources.GetObject("udpateButton.Image")));
            this.udpateButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.udpateButton.Location = new System.Drawing.Point(584, 644);
            this.udpateButton.Name = "udpateButton";
            this.udpateButton.Size = new System.Drawing.Size(184, 80);
            this.udpateButton.TabIndex = 9;
            this.udpateButton.Text = "Update HTS";
            this.udpateButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.udpateButton.UseVisualStyleBackColor = false;
            this.udpateButton.Click += new System.EventHandler(this.modifyButton_Click);
            // 
            // dataGridViewUS
            // 
            this.dataGridViewUS.AllowUserToOrderColumns = true;
            this.dataGridViewUS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dataGridViewUS.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewUS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewUS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUS.GridColor = System.Drawing.Color.Purple;
            this.dataGridViewUS.Location = new System.Drawing.Point(682, 45);
            this.dataGridViewUS.Name = "dataGridViewUS";
            this.dataGridViewUS.Size = new System.Drawing.Size(655, 283);
            this.dataGridViewUS.TabIndex = 3;
            // 
            // caHtsLabel
            // 
            this.caHtsLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.caHtsLabel.AutoSize = true;
            this.caHtsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.caHtsLabel.ForeColor = System.Drawing.Color.Purple;
            this.caHtsLabel.Location = new System.Drawing.Point(255, 13);
            this.caHtsLabel.Name = "caHtsLabel";
            this.caHtsLabel.Size = new System.Drawing.Size(132, 24);
            this.caHtsLabel.TabIndex = 0;
            this.caHtsLabel.Text = "HTS CA Table";
            // 
            // usHtsLabel
            // 
            this.usHtsLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.usHtsLabel.AutoSize = true;
            this.usHtsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usHtsLabel.ForeColor = System.Drawing.Color.Purple;
            this.usHtsLabel.Location = new System.Drawing.Point(938, 13);
            this.usHtsLabel.Name = "usHtsLabel";
            this.usHtsLabel.Size = new System.Drawing.Size(131, 24);
            this.usHtsLabel.TabIndex = 1;
            this.usHtsLabel.Text = "HTS US Table";
            // 
            // backgroundWorkerUpdate
            // 
            this.backgroundWorkerUpdate.WorkerReportsProgress = true;
            this.backgroundWorkerUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerUpdate_DoWork);
            this.backgroundWorkerUpdate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerUpdate_ProgressChanged);
            // 
            // currencyLabel
            // 
            this.currencyLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.currencyLabel.AutoSize = true;
            this.currencyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currencyLabel.ForeColor = System.Drawing.Color.Purple;
            this.currencyLabel.Location = new System.Drawing.Point(-4, 346);
            this.currencyLabel.Name = "currencyLabel";
            this.currencyLabel.Size = new System.Drawing.Size(140, 24);
            this.currencyLabel.TabIndex = 4;
            this.currencyLabel.Text = "Currency Table";
            // 
            // dataGridViewCurrency
            // 
            this.dataGridViewCurrency.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dataGridViewCurrency.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewCurrency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewCurrency.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCurrency.GridColor = System.Drawing.Color.Purple;
            this.dataGridViewCurrency.Location = new System.Drawing.Point(178, 346);
            this.dataGridViewCurrency.Name = "dataGridViewCurrency";
            this.dataGridViewCurrency.Size = new System.Drawing.Size(477, 257);
            this.dataGridViewCurrency.TabIndex = 5;
            // 
            // listview
            // 
            this.listview.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.listview.BackColor = System.Drawing.Color.White;
            this.listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.currency,
            this.value});
            this.listview.ForeColor = System.Drawing.Color.Purple;
            this.listview.GridLines = true;
            this.listview.Location = new System.Drawing.Point(682, 346);
            this.listview.Name = "listview";
            this.listview.Size = new System.Drawing.Size(477, 257);
            this.listview.TabIndex = 6;
            this.listview.UseCompatibleStateImageBehavior = false;
            this.listview.View = System.Windows.Forms.View.Details;
            this.listview.Visible = false;
            // 
            // currency
            // 
            this.currency.Text = "Currency";
            this.currency.Width = 193;
            // 
            // value
            // 
            this.value.Text = "Value";
            this.value.Width = 257;
            // 
            // lastestCurrencyLabel
            // 
            this.lastestCurrencyLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lastestCurrencyLabel.AutoSize = true;
            this.lastestCurrencyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastestCurrencyLabel.ForeColor = System.Drawing.Color.Purple;
            this.lastestCurrencyLabel.Location = new System.Drawing.Point(1179, 346);
            this.lastestCurrencyLabel.Name = "lastestCurrencyLabel";
            this.lastestCurrencyLabel.Size = new System.Drawing.Size(149, 24);
            this.lastestCurrencyLabel.TabIndex = 7;
            this.lastestCurrencyLabel.Text = "Lastest Currency";
            this.lastestCurrencyLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // UpdateHts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1330, 751);
            this.Controls.Add(this.lastestCurrencyLabel);
            this.Controls.Add(this.listview);
            this.Controls.Add(this.dataGridViewCurrency);
            this.Controls.Add(this.currencyLabel);
            this.Controls.Add(this.usHtsLabel);
            this.Controls.Add(this.caHtsLabel);
            this.Controls.Add(this.dataGridViewUS);
            this.Controls.Add(this.dataGridViewCA);
            this.Controls.Add(this.udpateButton);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateHts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update HTS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.UpdateHTS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCurrency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewCA;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button udpateButton;
        private System.Windows.Forms.DataGridView dataGridViewUS;
        private System.Windows.Forms.Label caHtsLabel;
        private System.Windows.Forms.Label usHtsLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorkerUpdate;
        private System.Windows.Forms.Label currencyLabel;
        private System.Windows.Forms.DataGridView dataGridViewCurrency;
        private System.Windows.Forms.ListView listview;
        private System.Windows.Forms.Label lastestCurrencyLabel;
        private System.Windows.Forms.ColumnHeader currency;
        private System.Windows.Forms.ColumnHeader value;
    }
}