﻿using SKU_Manager.SupportingClasses;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SKU_Manager.SplashModules.Add
{
    /*
     * A spalsh add module that edit the online description
     */
    public partial class Online : Form
    {
        // fields for storing description
        public string English;
        public string French;

        /* constructor that initialize graphic components and the title of the online description belongs to */
        public Online(string title, string english, string french, Color color)
        {
            InitializeComponent();

            // set the title and color
            Text = title;
            editButton.BackColor = color;

            // set fields
            englishTextbox.Text = english;
            frenchTextbox.Text = french;
        }

        #region Translate
        /* translate button clicks that translate the given english text to french */
        private void translateButton_Click(object sender, EventArgs e)
        {
            // call background worker
            if (!backgroundWorkerTranslate.IsBusy)
                backgroundWorkerTranslate.RunWorkerAsync();
        }
        private void backgroundWorkerTranslate_DoWork(object sender, DoWorkEventArgs e)
        {
            // initialize translate object 
            Translate translate = new Translate();

            // translate and get the french
            translate.nowTranslate(englishTextbox.Text);
            French = translate.getFrench();
        }
        private void backgroundWorkerTranslate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            frenchTextbox.Text = French;
        }
        #endregion

        /* edit button clicks that set the online description for the client */
        private void editButton_Click(object sender, EventArgs e)
        {
            // get the online description
            English = englishTextbox.Text;
            French = frenchTextbox.Text;

            // set ok result
            DialogResult = DialogResult.OK;
        }
    }
}