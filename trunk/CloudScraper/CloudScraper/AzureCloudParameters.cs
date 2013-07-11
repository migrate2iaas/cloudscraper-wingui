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

using System.Collections.Specialized;
using System.Collections;
using System.Web;
using System.Xml;

// for certificate
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;


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

            this.regionLabel.Text = Settings.Default.S4AzureRegionLabelText;
            this.idLabel.Text = Settings.Default.S4AzureIdLabelText;
            this.keyLabel.Text = Settings.Default.S4AzureKeyLabelText;
            this.typeLabel.Text = Settings.Default.S4AzureSubscriptionIdText;
            this.bucketLabel.Text = Settings.Default.S4AzureContainerNameLabelText;
            this.zoneLabel.Text = Settings.Default.S4AzureCertificateThumbprintText;
            this.idTextBox.MaxLength = 24;
            this.keyTextBox.MaxLength = 1024;
            //this.advancedCheckBox.Text = Settings.Default.S4ehDirectUploadCheckBoxText;

            //this.toolTip.SetToolTip(this.advancedCheckBox, Settings.Default.S4EHDirectUploadCheckBoxToolTip);
            //this.toolTip.SetToolTip(this.deduplcationCheckBox, Settings.Default.S4EHDeduplicationCheckBoxToolTip);
            //this.toolTip.SetToolTip(this.drivesDataGridView, Settings.Default.S4EHDrivesListBoxToolTip);

            //this.bucketLabel.Visible = false;
            this.folderKeyLabel.Visible = false;
            this.typeLabel.Visible = true;
            this.zoneLabel.Visible = true;
            this.groupLabel.Visible = false;
            this.bucketTextBox.Visible = false;
            this.folderKeyBox.Visible = false;
            this.serverTypeComboBox.Visible = false;
            this.zoneComboBox.Visible = true;
            this.zoneComboBox.MaxLength = 40;
            this.groupComboBox.Visible = false;
            this.drivesDataGridView.Visible = false;
            this.deduplcationCheckBox.Visible = false;
            this.drivesListLabel.Visible = false;
            this.selectAllCheckBox.Visible = false;
            this.advancedCheckBox.Visible = true;

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
            if (advanced_)
            {
                X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                // Try to open the store.
                try
                {
                    certStore.Open(OpenFlags.ReadOnly);
                }
                catch (Exception ex)
                {
                    if (ex is CryptographicException)
                    {
                        Console.WriteLine("Error: The store is unreadable.");
                    }
                    else if (ex is SecurityException)
                    {
                        Console.WriteLine("Error: You don't have the required permission.");
                    }
                    else if (ex is ArgumentException)
                    {
                        Console.WriteLine("Error: Invalid values in the store.");
                    }
                    else
                    {
                        throw;
                    }
                }


                // Find the certificate that matches the thumbprint.
                X509Certificate2Collection certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, 
                    certificateThumbprint_, false);
                certStore.Close();

                // Check to see if our certificate was added to the collection. If no, throw an error, if yes, create a certificate using it.
                if (0 == certCollection.Count)
                {
                    throw new Exception("Error: No certificate found containing thumbprint " + certificateThumbprint_);
                }

                // Create an X509Certificate2 object using our matching certificate.
                X509Certificate2 certificate = certCollection[0];

                
                //certificateThumbprint_;
                //subscriptionId_;
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
            System.Diagnostics.Process.Start(Settings.Default.S4EHLink);
        }

        protected override void AdvancedChecked(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                advanced_ = true;
                this.bucketLabel.Enabled = true;
                this.azureContainerComboBox.Enabled = true;
                this.azureDeployVirtualMachineCheckBox.Enabled = true;
                this.CheckEnter();
            }
            else
            {
                advanced_ = false;
                this.bucketLabel.Enabled = false;
                this.azureContainerComboBox.Enabled = false;
                this.azureDeployVirtualMachineCheckBox.Enabled = false;
                this.azureDeployVirtualMachineCheckBox.Checked = false;
                this.CheckEnter();
            }
        }

        protected override void AzureDeployVirtualMachineChecked(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                this.typeLabel.Enabled = true;
                this.azureSubscriptionId.Enabled = true;
                this.zoneLabel.Enabled = true;
                this.zoneComboBox.Enabled = true;
                this.azureCreateNewCertificateButton.Enabled = true;
                this.CheckEnter();
            }
            else
            {
                this.typeLabel.Enabled = false;
                this.azureSubscriptionId.Enabled = false;
                this.zoneLabel.Enabled = false;
                this.zoneComboBox.Enabled = false;
                this.azureCreateNewCertificateButton.Enabled = false;
                this.CheckEnter();
            }
        }

        protected override void AzureCreateNewCertificateButtonClick(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Certificate File (*.cer)|*.cer";
            saveFileDialog.DefaultExt = "." + "cer";
            string path = "";

            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
            }

            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "makecert.exe";
            info.Arguments = "-sky exchange -r -n \"CN=" + path + "\" -pe -a sha1 -len 2048 -ss My \"" + path + "\"";
            info.UseShellExecute = true;
            info.UserName = System.Diagnostics.Process.GetCurrentProcess().StartInfo.UserName;
            info.Password = System.Diagnostics.Process.GetCurrentProcess().StartInfo.Password;
            process.StartInfo = info;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Exited += new EventHandler(this.ProcessExited);
            process.EnableRaisingEvents = true;
            process.Start();
        }

        void ProcessExited(object sender, EventArgs e)
        {
            Process.Start("https://manage.windowsazure.com/#Workspaces/AdminTasks/ListManagementCertificates");
            try
            {
                X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection fcollection = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                //X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(fcollection, "Test Certificate Select", "Select a certificate from the following list to get information on that certificate", X509SelectionFlag.MultiSelection);
                //Console.WriteLine("Number of certificates: {0}{1}", scollection.Count, Environment.NewLine);
                //foreach (X509Certificate2 x509 in scollection)
                //{
                //    byte[] rawdata = x509.RawData;
                //    Console.WriteLine("Content Type: {0}{1}", X509Certificate2.GetCertContentType(rawdata), Environment.NewLine);
                //    Console.WriteLine("Friendly Name: {0}{1}", x509.FriendlyName, Environment.NewLine);
                //    Console.WriteLine("Certificate Verified?: {0}{1}", x509.Verify(), Environment.NewLine);
                //    Console.WriteLine("Simple Name: {0}{1}", x509.GetNameInfo(X509NameType.SimpleName, true), Environment.NewLine);
                //    Console.WriteLine("Signature Algorithm: {0}{1}", x509.SignatureAlgorithm.FriendlyName, Environment.NewLine);
                //    Console.WriteLine("Private Key: {0}{1}", x509.PrivateKey.ToXmlString(false), Environment.NewLine);
                //    Console.WriteLine("Public Key: {0}{1}", x509.PublicKey.Key.ToXmlString(false), Environment.NewLine);
                //    Console.WriteLine("Certificate Archived?: {0}{1}", x509.Archived, Environment.NewLine);
                //    Console.WriteLine("Length of Raw Data: {0}{1}", x509.RawData.Length, Environment.NewLine);
                //    X509Certificate2UI.DisplayCertificate(x509);
                //    x509.Reset();
                //}
                store.Close();
            }
            catch (CryptographicException)
            {
                Console.WriteLine("Information could not be written out for this certificate.");
            }
        }

        protected override void AzureSubscriptionIdTextChanged(object sender, EventArgs e)
        {
            subscriptionId_ = (sender as TextBox).Text;
            this.CheckEnter();
        }

        protected override void ZoneComboBoxTextChanged(object sender, EventArgs e)
        {
            certificateThumbprint_ = (sender as ComboBox).Text;
            this.CheckEnter();
        }

        protected override void TestButtonClick(object sender, EventArgs e)
        {
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

            string AccountName = storageAccount_;
            string AccountSharedKey = primaryAccessKey_;
            string MessageSignature = "";
            Console.WriteLine("Please input the name of the container press <ENTER>. Its blobs info will be listed:");
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
            System.Security.Cryptography.HMACSHA256 SHA256 = new System.Security.Cryptography.HMACSHA256(Convert.FromBase64String(AccountSharedKey));
            // Create Authorization HTTP header value
            String AuthorizationHeader = "SharedKey " + AccountName + ":" + Convert.ToBase64String(SHA256.ComputeHash(SignatureBytes));
            // Add Authorization HTTP header
            Request.Headers.Add("Authorization", AuthorizationHeader);

            try
            {
                // Send Http request and get response
                using (HttpWebResponse response = (HttpWebResponse)Request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // If success
                        DialogResult reslt = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                            Settings.Default.S4AzureTestConnectionText, "", "OK", "OK",
                            System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);

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
                    }
                }
            }
            catch (WebException ex)
            {
                //Console.WriteLine("An error occured. Status code:" + ((HttpWebResponse)ex.Response).StatusCode);
                //Console.WriteLine("Error information:");
                using (Stream stream = ex.Response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        string s = sr.ReadToEnd();
                    }
                }
                //Show dialog  when auth failed.

                DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                    ex.Status + "\n" +
                    Settings.Default.S4EHIDKeyInvalid, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);

                this.testButton.Enabled = true;
                this.Cursor = Cursors.Arrow;
            }

            this.testButton.Enabled = true;
            this.Cursor = Cursors.Arrow;
        }
        
        //Check enter in Form for activate Next button.
        private void CheckEnter()
        {
            if (storageAccount_ != "" && storageAccount_.Length >= 3 && storageAccount_.Length <= 24 &&
                primaryAccessKey_ != "" && primaryAccessKey_.Length == 88)
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
