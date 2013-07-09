using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;
using CloudScraper.Properties;
using System.Drawing;
using System.IO;
using NLog;
using DotNetPerls;

namespace CloudScraper
{
    public class AzureCloudParameters : CloudParametersForm
    {
        public static string storageAccount_;
        public static string primaryAccessKey_;
        public static string region_;
        public static bool isAzure_ = false;

        private static Logger logger_ = LogManager.GetLogger("AzureCloudParametersForm");
        
        public AzureCloudParameters(ChooseCloudForm chooseCloudForm)
        {
            isAzure_ = false;

            //Move regions strings from settings file to regionComboBox.
            foreach (string str in Settings.Default.AzureRegions)
            {
                string key = str.Split(new char[] { Settings.Default.Separator }, 2)[1];
                string value = str.Split(new char[] { Settings.Default.Separator }, 2)[0];
                this.regionList_.Add(key, value);

                this.regionComboBox.Items.Add(key);
                if (value == "East US")
                {
                    this.regionComboBox.SelectedItem = key;
                }
            }

            //Set basic UI strings in Form. 
            this.helpButton.Image = new Bitmap(System.Drawing.Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.toolTip.SetToolTip(this.helpButton, Settings.Default.HelpButtonToolTip);
            this.nextButton.Enabled = false;
            this.Text = Settings.Default.S4AzureHeader;
            this.backButton.Text = Settings.Default.S4BackButtonText;
            this.toolTip.SetToolTip(this.backButton, Settings.Default.S4BackButtonToolTip);
            this.nextButton.Text = Settings.Default.S4NextButtonText;
            this.toolTip.SetToolTip(this.nextButton, Settings.Default.S4NextButtonToolTip);
            this.testButton.Text = Settings.Default.S4TestButtonText;
            this.toolTip.SetToolTip(this.testButton, Settings.Default.S4TestButtonToolTip);

            this.idTextBox.MaxLength = 40;

            this.regionLabel.Text = Settings.Default.S4AzureRegionLabelText;
            this.idLabel.Text = Settings.Default.S4AzureIdLabelText;
            this.keyLabel.Text = Settings.Default.S4AzureKeyLabelText;
            //this.advancedCheckBox.Text = Settings.Default.S4ehDirectUploadCheckBoxText;

            //this.toolTip.SetToolTip(this.advancedCheckBox, Settings.Default.S4EHDirectUploadCheckBoxToolTip);
            //this.toolTip.SetToolTip(this.deduplcationCheckBox, Settings.Default.S4EHDeduplicationCheckBoxToolTip);
            //this.toolTip.SetToolTip(this.drivesDataGridView, Settings.Default.S4EHDrivesListBoxToolTip);

            this.bucketLabel.Visible = false;
            this.folderKeyLabel.Visible = false;
            this.typeLabel.Visible = false;
            this.zoneLabel.Visible = false;
            this.groupLabel.Visible = false;
            this.bucketTextBox.Visible = false;
            this.folderKeyBox.Visible = false;
            this.serverTypeComboBox.Visible = false;
            this.zoneComboBox.Visible = false;
            this.groupComboBox.Visible = false;

            this.SetChooseCloudForm(chooseCloudForm);
        }

        protected override void RegionListBoxChanged(object sender, EventArgs e)
        {
            region_ = this.regionList_[(string)(sender as ComboBox).SelectedItem];
        }
        
        protected override void TextBoxMouseEnter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                if ((sender as TextBox).Text == "")
                {
                    if ((sender as TextBox) == keyTextBox)
                        this.toolTip.SetToolTip((sender as TextBox), Settings.Default.S4EHApiKeyToolTip);
                    if ((sender as TextBox) == idTextBox)
                        this.toolTip.SetToolTip((sender as TextBox), Settings.Default.S4EHUserUIDToolTip);
                }
                else
                {
                    this.toolTip.SetToolTip((sender as TextBox), "");
                }
            }

            if ((sender is ComboBox))
            {
                if ((sender as ComboBox).Text == "")
                {
                    this.toolTip.SetToolTip((sender as ComboBox), "Test");
                }
                else
                {
                    this.toolTip.SetToolTip((sender as ComboBox), "");
                }
            }
        }

        protected override void IDChanged(object sender, EventArgs e)
        {
            storageAccount_ = (sender as TextBox).Text;
            this.CheckEnter();
        }

        protected override void KeyChanged(object sender, EventArgs e)
        {
            primaryAccessKey_ = (sender as TextBox).Text;
            this.CheckEnter();
        }

        protected override void BackButtonClick(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Return to the ChooseCloudForm.");

            isAzure_ = false;
            base.BackButtonClick(sender, e);
        }

        protected override void NextButtonClick(object sender, EventArgs e)
        {
            isAzure_ = true;
            this.Hide();

            if (logger_.IsDebugEnabled)
                logger_.Debug("Next to the ImagesPathForm.");

            if (this.imagesPathForm_ == null)
            {
                this.imagesPathForm_ = new ImagesPathForm(this);
            }

            imagesPathForm_.ShowDialog();
            
        }

        protected override void HelpButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Settings.Default.S4EHLink);
        }
        
        //Check enter in Form for activate Next button.
        private void CheckEnter()
        {
            if (storageAccount_ != "" && storageAccount_.Length >= 3 && storageAccount_.Length <= 24 &&
                primaryAccessKey_ != "" && primaryAccessKey_.Length == 40)
            {

                foreach (char ch in storageAccount_)
                {
                    if (!(char.IsLower(ch) || char.IsDigit(ch)))
                    {
                        this.nextButton.Enabled = false;
                        return;
                    }
                }
                this.nextButton.Enabled = true;
            }
            else
            {
                this.nextButton.Enabled = false;
            }
        }
    }
}
