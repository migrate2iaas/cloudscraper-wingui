using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using CloudScraper.Properties;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.EC2;
using Amazon.EC2.Model;
using System.Configuration;
using System.Collections.Specialized;
using NLog;
using DotNetPerls;

namespace CloudScraper
{
    public class AmazonCloudParameters : CloudParametersForm
    {
        public static string awsId_ = "";
        public static string awsKey_ = "";
        public static string region_;

        public static AmazonParams userInput_;

        public static bool isAmazon_ = false;

        private static Logger logger_ = LogManager.GetLogger("AmazonCloudParametersForm");

        private readonly AmazonAdvansedSettingsPanel pnlAdvancedSettings_;

        public AmazonCloudParameters(ChooseCloudForm chooseCloudForm)
        {
            isAmazon_ = false;

            //Move regions strings from settings file to regionComboBox.
            foreach (string str in Settings.Default.Regions)
            {
                string key = str.Split(new char[] { Settings.Default.Separator }, 2)[1];
                string value = str.Split(new char[] { Settings.Default.Separator }, 2)[0];
                this.regionList_.Add(key, value);

                this.regionComboBox.Items.Add(key);
                
                //if (value == "us-east-1")
                //{
                //    this.regionComboBox.SelectedItem = key;
                //}
            }

            if (this.regionComboBox.Items.Count > 0)
                this.regionComboBox.SelectedItem = this.regionComboBox.Items[0];

            //Set basic UI strings in Form. 
            this.helpButton.Image = new Bitmap(System.Drawing.Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.toolTip.SetToolTip(this.helpButton, Settings.Default.HelpButtonToolTip);
            this.nextButton.Enabled = false;
            this.Text = Settings.Default.S4Header;
            this.backButton.Text = Settings.Default.S4BackButtonText;
            this.toolTip.SetToolTip(this.backButton, Settings.Default.S4BackButtonToolTip);
            this.nextButton.Text = Settings.Default.S4NextButtonText;
            this.toolTip.SetToolTip(this.nextButton, Settings.Default.S4NextButtonToolTip);
            this.testButton.Text = Settings.Default.S4TestButtonText;
            this.toolTip.SetToolTip(this.testButton, Settings.Default.S4TestButtonToolTip);
            this.regionLabel.Text = Settings.Default.S4AmazonRegionLabelText;
            this.idLabel.Text = Settings.Default.S4awsIdLabelText;
            this.keyLabel.Text = Settings.Default.S4awsKeyLabelText;
            this.bucketLabel.Text = Settings.Default.S4AmazonBucketLabelText;
            this.folderKeyLabel.Text = Settings.Default.S4AmazonFolderKeyLabelText;
            this.typeLabel.Text = Settings.Default.S4AmazonTypeLabelText;
            this.zoneLabel.Text = Settings.Default.S4AmazonZoneLabelText;
            this.groupLabel.Text = Settings.Default.S4AmazonGroupLabelText;

            //Move server types strings from settings file to serverTypeComboBox.
            SortedDictionary<string, string> serverTypeList = new SortedDictionary<string, string>();
            foreach (string str in Settings.Default.ServerTypes)
            {
                string key = str.Split(new char[] { Settings.Default.Separator }, 2)[1];
                string value = str.Split(new char[] { Settings.Default.Separator }, 2)[0];
                serverTypeList.Add(key, value);
            }
            serverTypeList_ = new SortedDictionary<string, string>(serverTypeList); // TODO: do we need this data member

            // Set advanced setting panel
            pnlAdvancedSettings_ = new AmazonAdvansedSettingsPanel(region_, serverTypeList);
            SetAdvancedPanel(pnlAdvancedSettings_);
            pnlAdvancedSettings_.OnContentVerified += new EventHandler<AmazonAdvansedSettingsPanel.ContentVerificationArgs>(OnAdvancedSettingsContentVerified);

            this.SetChooseCloudForm(chooseCloudForm);
        }

        protected override void IDChanged(object sender, EventArgs e)
        {
            awsId_ = (sender as TextBox).Text;
            this.nextButton.Enabled = CheckNextEnabled();
        }

        protected override void KeyChanged(object sender, EventArgs e)
        {
            awsKey_ = (sender as TextBox).Text;
            this.nextButton.Enabled = CheckNextEnabled();
        }

        private void OnAdvancedSettingsContentVerified(object sender, AmazonAdvansedSettingsPanel.ContentVerificationArgs e)
        {
            this.nextButton.Enabled = CheckNextEnabled();
        }

        protected bool CheckNextEnabled()
        {
            return pnlAdvancedSettings_.IsContentValid() &&
                awsId_ != "" && awsId_.Length == 20 &&
                awsKey_ != "" && awsKey_.Length == 40;
        }

        protected override void RegionListBoxChanged(object sender, EventArgs e)
        {
            region_ = this.regionList_[(string)(sender as ComboBox).SelectedItem];
            if (null != pnlAdvancedSettings_)
            {
                pnlAdvancedSettings_.ChangeRegion(region_);
            }
        }

        protected override void BackButtonClick(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Return to the ChooseCloudForm.");
            
            isAmazon_ = false;
            base.BackButtonClick(sender, e);
        }

        protected override void NextButtonClick(object sender, EventArgs e)
        {
            pnlAdvancedSettings_.VerifyBucket();

            if (logger_.IsDebugEnabled)
                logger_.Debug("Next to the ImagesPathForm.");
            
            isAmazon_ = true;
            userInput_ = pnlAdvancedSettings_.GetUserInput(AmazonCloudParameters.awsId_);
            
            this.Hide();

            if (this.imagesPathForm_ == null)
            {
                this.imagesPathForm_ = new ImagesPathForm(this);
            }

            imagesPathForm_.ShowDialog();
        }

        protected override void HelpButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Settings.Default.S4AmazonLink);
        }
        
        protected override void TestButtonClick(object sender, EventArgs e)
        {
            try
            {
                this.testButton.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                //If there are no keys entered.
                if (string.IsNullOrEmpty(awsId_) || string.IsNullOrEmpty(awsKey_))
                {
                    DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                        Settings.Default.S4EnterAWS, "", "OK", "OK",
                        System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);

                    return;
                }

                try
                {
                    pnlAdvancedSettings_.LoadEC2UserSettingsFromServer(awsId_, awsKey_);
                }
                catch (AmazonEC2Exception amazonEC2Exception)
                {
                    //Show dialog  when auth failed.
                    DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                        amazonEC2Exception.ErrorCode + "\n" +
                        Settings.Default.S4IDKeyInvalid, "", "OK", "OK",
                        System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);

                    return;
                }

                //If not advanced mode, show done message and return.
                if (!pnlAdvancedSettings_.IsAdvancedMode())
                {
                    DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                        Settings.Default.S4TestConnectionText, "", "OK", "OK",
                        System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);

                    return;
                }
                
                // we are in the advanced mode.
                try
                {
                    pnlAdvancedSettings_.LoadS3UserSettingsFromServer(awsId_, awsKey_);

                    //Show done message.
                    DialogResult result2 = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                        Settings.Default.S4TestConnectionText, "", "OK", "OK",
                        System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);
                }
                catch (AmazonAdvansedSettingsPanel.RegionMismatchException)
                {
                    DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                        Settings.Default.S4BucketLocated, "", "OK", "OK",
                        System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);
                    
                    return;
                }
                catch (AmazonS3Exception ex)
                {
                    if (ex.ErrorCode == "NoSuchBucket")
                    {
                        //If no such bucket.
                        DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                            Settings.Default.S4NoBucketExists, "", "OK", "OK",
                            System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);
                    }
                    else
                    {
                        //If bucket exist but locked.
                        DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                            Settings.Default.S4CannotAccessBucketText, "", "OK", "OK",
                            System.Drawing.Image.FromFile("Icons\\WarningDialog.png"), false);
                    }

                    return;
                }
            }
            finally
            {
                this.testButton.Enabled = true;
                this.Cursor = Cursors.Arrow;
            }
        }

        protected override void CloudParametersLoad(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Form loaded.");
            
            isAmazon_ = false;
            base.CloudParametersLoad(sender, e);
        }

        protected override void OnLeaveEnter(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
            {
                if (sender is ComboBox)
                {
                    if ((sender as ComboBox) == regionComboBox)
                        logger_.Debug("Region select: " + (sender as ComboBox).Text);
                }
                if (sender is TextBox)
                {
                    //if ((sender as TextBox) == keyTextBox)
                    //    logger_.Debug("Key enter: " + (sender as TextBox).Text);
                    if ((sender as TextBox) == idTextBox)
                        logger_.Debug("Id enter: " + (sender as TextBox).Text);
                }
            }
        }

        protected override void TextBoxMouseEnter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                if ((sender as TextBox).Text == "")
                {
                    if ((sender as TextBox) == keyTextBox)
                        this.toolTip.SetToolTip((sender as TextBox), Settings.Default.S4AmazonAccessKeyToolTip);
                    if ((sender as TextBox) == idTextBox)
                        this.toolTip.SetToolTip((sender as TextBox), Settings.Default.S4AmazonAccessIDToolTip);
                }
                else
                {
                    
                    this.toolTip.SetToolTip((sender as TextBox), "");
                }
            }

            if ((sender is ComboBox))
            {
                // for regionComboBox
                this.toolTip.SetToolTip((sender as ComboBox), "");
            }
        }
    }
}
