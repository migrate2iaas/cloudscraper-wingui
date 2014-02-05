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
        public static bool advanced_ = false;
        public static string subscriptionId_ = "";
        public static string certificateThumbprint_ = "";
        public static string containerName_ = string.Empty;
        public static string certificateSelection_ = "";

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
            if (advanced_)
            {
                if (!CertificateUtils.CheckCertificateInstalled(certificateThumbprint_, ref certificateSelection_))
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
            if (logger_.IsDebugEnabled)
                logger_.Debug("Start check storage account.");
            
            string AccountName = storageAccount_;
            string AccountSharedKey = primaryAccessKey_;
            string MessageSignature = "";

            // Set request URI
            Uri requesturi = new Uri("https://" + storageAccount_ + ".blob.core.windows.net/?comp=list");

            // Create HttpWebRequest object
            HttpWebRequest Request = (HttpWebRequest)HttpWebRequest.Create(requesturi.AbsoluteUri);
            Request.Method = "GET";
            Request.ContentLength = 0;
            // Add HTTP headers
            Request.Headers.Add("x-ms-date", DateTime.UtcNow.ToString("R"));
            Request.Headers.Add("x-ms-version", "2009-09-19");

            // Create Signature
            // Verb
            MessageSignature += "GET\n";
            // Content-Encoding
            MessageSignature += "\n";
            // Content-Language
            MessageSignature += "\n";
            // Content-Length
            MessageSignature += "\n";
            // Content-MD5
            MessageSignature += "\n";
            // Content-Type
            MessageSignature += "\n";
            // Date
            MessageSignature += "\n";
            // If-Modified-Since
            MessageSignature += "\n";
            // If-Match
            MessageSignature += "\n";
            // If-None-Match 
            MessageSignature += "\n";
            // If-Unmodified-Since
            MessageSignature += "\n";
            // Range
            MessageSignature += "\n";
            // CanonicalizedHeaders
            MessageSignature += GetCanonicalizedHeaders(Request);
            // CanonicalizedResource
            MessageSignature += GetCanonicalizedResourceVersion2(requesturi, AccountName);
            // Use HMAC-SHA256 to sign the signature
            byte[] SignatureBytes = System.Text.Encoding.UTF8.GetBytes(MessageSignature);
            if (AccountSharedKey.Length == 88)
            {
                System.Security.Cryptography.HMACSHA256 SHA256 = new System.Security.Cryptography.HMACSHA256(Convert.FromBase64String(AccountSharedKey));
                // Create Authorization HTTP header value
                String AuthorizationHeader = "SharedKey " + AccountName + ":" + Convert.ToBase64String(SHA256.ComputeHash(SignatureBytes));
                // Add Authorization HTTP header
                Request.Headers.Add("Authorization", AuthorizationHeader);
            }

            try
            {
                //Send Http request and get response.
                using (HttpWebResponse response = (HttpWebResponse)Request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //If success.
                        if (advanced_ && this.azureDeployVirtualMachineCheckBox.Checked)
                        {
                            DialogResult reslt = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                                Settings.Default.S4AzureTestConnectionTextAdvancedMode, "", "OK", "OK",
                                System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);
                        }
                        else
                        {
                            DialogResult reslt = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                                Settings.Default.S4AzureTestConnectionText, "", "OK", "OK",
                                System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);
                        }

                        this.azureContainerComboBox.Items.Clear();

                        //Read container names from response.  
                        using (XmlTextReader reader = new XmlTextReader(response.GetResponseStream()))
                        {
                            while (reader.Read())
                            {
                                if (reader.Name == "Name")
                                {
                                    this.azureContainerComboBox.Items.Add(reader.ReadElementString("Name"));
                                }
                            }
                        }

                        return true;
                    }

                    return false;
                }
            }
            catch (WebException ex)
            {
                //Show dialog  when auth failed.
                DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                    ex.Status + "\n" +
                    Settings.Default.S4AzureIDKeyInvalid, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);

                this.testButton.Enabled = true;
                this.Cursor = Cursors.Arrow;

                return false;
            }
        }

        private bool CheckStroageAccountInRegion()
        {
            this.testButton.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            string accountName = storageAccount_;
            string accountSharedKey = primaryAccessKey_;

            string subscriptionId = subscriptionId_;
            string thumbprint = certificateThumbprint_;

            X509Certificate2 certificate = CertificateUtils.GetCertificate(thumbprint, CertificateUtils.CertificateStore).certificate;

            if (certificate == null)
            {
                DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                    Settings.Default.S4AzureCertificateInvalid, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
                this.testButton.Enabled = true;
                this.Cursor = Cursors.Arrow;
                return false;
            }

            string uriFormat = "https://management.core.windows.net/{0}/services/storageservices/{1}";
            Uri uri = new Uri(String.Format(uriFormat, subscriptionId, accountName));

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = "GET";
            request.Headers.Add("x-ms-version", "2011-10-01");
            request.ClientCertificates.Add(certificate);
            request.ContentType = "application/xml";

            try
            {
                string location = "";
                // Send Http request and get response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (XmlTextReader reader = new XmlTextReader(response.GetResponseStream()))
                        {
                            while (reader.Read())
                            {
                                if (reader.Name == "Location")
                                {
                                    location = reader.ReadElementString("Location");
                                }
                            }
                        }
                    }
                }

                if (location != region_)
                {
                    DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                        Settings.Default.S4AzureRegionInvalid, "", "OK", "OK",
                        System.Drawing.Image.FromFile("Icons\\WarningDialog.png"), false);

                    return true;
                }
                else
                {
                    DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                        Settings.Default.S4AzureRegionValid, "", "OK", "OK",
                        System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);
                    return true;
                }
            }
            catch (WebException)
            {
                //Show dialog  when auth failed.
                DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                    Settings.Default.S4AzureCertificateInvalid, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);

                this.testButton.Enabled = true;
                this.Cursor = Cursors.Arrow;
                return false;
            }
        }
        
        protected override void TestButtonClick(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Start Test Connection procedure.");
            
            this.testButton.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            //If there are no keys entered.
            if (storageAccount_ == "" || primaryAccessKey_ == "")
            {
                DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                    Settings.Default.S4AzureEnterID, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);

                this.testButton.Enabled = true;
                this.Cursor = Cursors.Arrow;
                return;
            }

            if (!this.CheckStorageAccount())
                return;

            if (advanced_ && this.azureDeployVirtualMachineCheckBox.Checked)
            {
                if (!this.CheckStroageAccountInRegion())
                    return;
            }

            this.testButton.Enabled = true;
            this.Cursor = Cursors.Arrow;
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

        #region Helper method/class

        static ArrayList GetHeaderValues(NameValueCollection headers, string headerName)
        {
            ArrayList list = new ArrayList();
            string[] values = headers.GetValues(headerName);
            if (values != null)
            {
                foreach (string str in values)
                {
                    list.Add(str.TrimStart(new char[0]));
                }
            }
            return list;
        }

        static string GetCanonicalizedHeaders(HttpWebRequest request)
        {
            ArrayList list = new ArrayList();
            StringBuilder sb = new StringBuilder();
            foreach (string str in request.Headers.Keys)
            {
                if (str.ToLowerInvariant().StartsWith("x-ms-", StringComparison.Ordinal))
                {
                    list.Add(str.ToLowerInvariant());
                }
            }
            list.Sort();
            foreach (string str2 in list)
            {
                StringBuilder builder = new StringBuilder(str2);
                string str3 = ":";
                foreach (string str4 in GetHeaderValues(request.Headers, str2))
                {
                    string str5 = str4.Replace("\r\n", string.Empty);
                    builder.Append(str3);
                    builder.Append(str5);
                    str3 = ",";
                }
                sb.Append(builder.ToString());
                sb.Append("\n");
            }
            return sb.ToString();
        }

        static string GetCanonicalizedResourceVersion2(Uri address, string accountName)
        {
            StringBuilder builder = new StringBuilder("/");
            builder.Append(accountName);
            builder.Append(address.AbsolutePath);
            CanonicalizedString str = new CanonicalizedString(builder.ToString());
            NameValueCollection values = HttpUtility.ParseQueryString(address.Query);
            NameValueCollection values2 = new NameValueCollection();
            foreach (string str2 in values.Keys)
            {
                ArrayList list = new ArrayList(values.GetValues(str2));
                list.Sort();
                StringBuilder builder2 = new StringBuilder();
                foreach (object obj2 in list)
                {
                    if (builder2.Length > 0)
                    {
                        builder2.Append(",");
                    }
                    builder2.Append(obj2.ToString());
                }
                values2.Add((str2 == null) ? str2 : str2.ToLowerInvariant(), builder2.ToString());
            }
            ArrayList list2 = new ArrayList(values2.AllKeys);
            list2.Sort();
            foreach (string str3 in list2)
            {
                StringBuilder builder3 = new StringBuilder(string.Empty);
                builder3.Append(str3);
                builder3.Append(":");
                builder3.Append(values2[str3]);
                str.AppendCanonicalizedElement(builder3.ToString());
            }
            return str.Value;
        }

        internal class CanonicalizedString
        {
            // Fields
            private StringBuilder canonicalizedString = new StringBuilder();

            // Methods
            internal CanonicalizedString(string initialElement)
            {
                this.canonicalizedString.Append(initialElement);
            }

            internal void AppendCanonicalizedElement(string element)
            {
                this.canonicalizedString.Append("\n");
                this.canonicalizedString.Append(element);
            }

            // Properties
            internal string Value
            {
                get
                {
                    return this.canonicalizedString.ToString();
                }
            }
        }

        #endregion
    }
}
