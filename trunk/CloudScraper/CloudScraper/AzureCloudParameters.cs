using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
using System.Collections.Specialized;
using System.Collections;
using System.Web;
using System.Xml;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

using NLog;
using DotNetPerls;
using CloudScraper.Properties;

using CloudScraper.Azure;

namespace CloudScraper
{
    public class AzureCloudParameters : CloudParametersForm
    {
        public static string storageAccount_ = "";
        public static string primaryAccessKey_ = "";
        public static string region_;
        public static bool isAzure_ = false;
        public static string subscriptionId_ = "";
        public static string certificateThumbprint_ = "";
        public static string containerName_ = string.Empty;
        public static string certificateSelection_ = "";

        public static AzureParams params_ = null;

        private static Logger logger_ = LogManager.GetLogger("AzureCloudParametersForm");

        private readonly AzureAdvancedSettingsPanel pnlAdvancedSettings_;
        
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
                
                //East US by default
                //if (value == "East US")
                //{
                //    this.regionComboBox.SelectedItem = key;
                //}
            }

            if(this.regionComboBox.Items.Count > 0)
                this.regionComboBox.SelectedItem = this.regionComboBox.Items[0];

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

            this.regionLabel.Text = Settings.Default.S4AzureRegionLabelText;
            this.idLabel.Text = Settings.Default.S4AzureIdLabelText;
            this.keyLabel.Text = Settings.Default.S4AzureKeyLabelText;

            this.idTextBox.MaxLength = 24;
            this.keyTextBox.MaxLength = 1024;

            // Create advanced settings panel.
            pnlAdvancedSettings_ = new AzureAdvancedSettingsPanel(region_);
            SetAdvancedPanel(pnlAdvancedSettings_);

            this.SetChooseCloudForm(chooseCloudForm);
        }

        protected override void RegionListBoxChanged(object sender, EventArgs e)
        {
            region_ = this.regionList_[(string)(sender as ComboBox).SelectedItem];
            if (null != pnlAdvancedSettings_)
            {
                pnlAdvancedSettings_.SetRegion(region_);
            }
        }
        
        protected override void TextBoxMouseEnter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                if ((sender as TextBox).Text == "")
                {
                    if ((sender as TextBox) == keyTextBox)
                        this.toolTip.SetToolTip((sender as TextBox), Settings.Default.S4AzureApiKeyToolTip);
                    if ((sender as TextBox) == idTextBox)
                        this.toolTip.SetToolTip((sender as TextBox), Settings.Default.S4AzureUserUIDToolTip);
                }
                else
                {
                    this.toolTip.SetToolTip((sender as TextBox), "");
                }
            }

            if (sender is ComboBox)
            {
                if ((sender as ComboBox) == regionComboBox)
                {
                    this.toolTip.SetToolTip((sender as ComboBox), Settings.Default.S4AzureRegionToolTip);
                }
                else
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
            params_ = pnlAdvancedSettings_.CreateAzureParams(storageAccount_, primaryAccessKey_);
            if (null == params_)
            {
                return;
            }

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
            if (logger_.IsDebugEnabled)
                logger_.Debug("Help button click.");
            
            //Help button url.
            System.Diagnostics.Process.Start(Settings.Default.S4AzureLink);
        }

        private bool CheckStorageAccount()
        {
            Cursor cursor = Cursor;
            
            if (logger_.IsDebugEnabled)
                logger_.Debug("Start check storage account.");

            try
            {
                if (pnlAdvancedSettings_.LoadStorages(storageAccount_, primaryAccessKey_))
                {
                    if (pnlAdvancedSettings_.IsDeployWmChecked())
                    {
                        BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                            Settings.Default.S4AzureTestConnectionTextAdvancedMode, "", "OK", "OK",
                            System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);
                    }
                    else
                    {
                        BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                            Settings.Default.S4AzureTestConnectionText, "", "OK", "OK",
                            System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);
                    }
                    return true;
                }
                return false;
            }
            catch (WebException ex)
            {
                //Show dialog  when auth failed.
                BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                    ex.Status + "\n" +
                    Settings.Default.S4AzureIDKeyInvalid, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);

                return false;
            }
            finally
            {
                testButton.Enabled = true;
                Cursor = cursor;
            }
        }

        private bool CheckStroageAccountInRegion()
        {
            testButton.Enabled = false;
            Cursor cursor = Cursor.Current;
            Cursor = Cursors.WaitCursor;
            bool result = false;
            try
            {
                string location = pnlAdvancedSettings_.GetLocationOfStroageAccount(storageAccount_);
                // TODO: why it is supposed to work this way.
                // Why don't we return false if if the location doesn't match the region?
                result = !string.IsNullOrEmpty(location);
                if (location != region_)
                {
                    BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                        Settings.Default.S4AzureRegionInvalid, "", "OK", "OK",
                        System.Drawing.Image.FromFile("Icons\\WarningDialog.png"), false);
                }
                else
                {
                    BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                        Settings.Default.S4AzureRegionValid, "", "OK", "OK",
                        System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);
                }
            }
            catch (InconsistentAzueDataException ex)
            {
                logger_.LogException(LogLevel.Error, "Check storage account region failed.", ex);
                BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                    Settings.Default.S4AzureRegionCheckFailed, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);                
            }
            catch (AzureCertificateException ex)
            {
                logger_.LogException(LogLevel.Error, "Check storage account region failed.", ex);
                BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                    Settings.Default.S4AzureCertificateInvalid, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
            }
            catch (WebException ex)
            {
                //Show dialog when auth failed.
                logger_.LogException(LogLevel.Error, "Check storage account region failed.", ex);
                BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                    Settings.Default.S4AzureCertificateInvalid, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
            }
            finally
            {
                testButton.Enabled = true;
                Cursor = cursor;
            }
            return result;
        }
        
        protected override void TestButtonClick(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Start Test Connection procedure.");

            Cursor cursor = Cursor;
            testButton.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                //If there are no keys entered.
                if (string.IsNullOrEmpty(storageAccount_) || string.IsNullOrEmpty(primaryAccessKey_))
                {
                    BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                        Settings.Default.S4AzureEnterID, "", "OK", "OK",
                        System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
                    return;
                }
                if (this.azureDeployVirtualMachineCheckBox.CheckState == CheckState.Checked)
                {
                    string error = pnlAdvancedSettings_.CheckAdvancedCredentials();
                    if (!string.IsNullOrEmpty(error))
                    {
                        BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                            error, "", "OK", "OK",
                            System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
                    
                        return;                    
                    }
                }

                if (!this.CheckStorageAccount())
                    return;

                if (pnlAdvancedSettings_.IsDeployWmChecked())
                {
                    this.CheckStroageAccountInRegion();
                }

                pnlAdvancedSettings_.OnCertificateChecked();
            }
            catch (WebException ex)
            {
                logger_.LogException(LogLevel.Error, "Failed to connect to azure server.", ex);
                BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                    Settings.Default.S4AzureCertificateWebException, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
            }
            catch (Exception ex)
            {
                logger_.LogException(LogLevel.Error, "Failed to obtain user data.", ex);
            }
            finally
            {
                testButton.Enabled = true;
                Cursor = cursor;
            }
        }

        //Check enter in Form for activate Next button.
        private void CheckEnter()
        {
            if (storageAccount_ != "" && storageAccount_.Length >= 3 && storageAccount_.Length <= 24 &&
                primaryAccessKey_ != "") 
                //&& primaryAccessKey_.Length == 88)
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
                if (sender is CheckBox)
                {
                    if ((sender as CheckBox) == advancedCheckBox)
                        logger_.Debug("Advanced checked to: " + (sender as CheckBox).Checked.ToString());
                }
            }
        }

        protected override void CloudParametersLoad(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Form loaded.");

            isAzure_ = false;
            base.CloudParametersLoad(sender, e);
        }
    }
}
