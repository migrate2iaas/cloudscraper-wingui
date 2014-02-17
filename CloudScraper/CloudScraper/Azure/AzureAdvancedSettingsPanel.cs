using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Xml;

using DotNetPerls;
using CloudScraper.Properties;
using NLog;

namespace CloudScraper.Azure
{
    public partial class AzureAdvancedSettingsPanel : UserControl
    {
        #region Constants

        private const string EmptySelection = "None";

        #endregion Constants

        #region Data members

        private static Logger logger_ = LogManager.GetLogger("AzureAdvancedParametersPanel");
        
        private readonly ToolTipContainer toolTipContainer_;
        private readonly ControlDebugPrintoutContainer debugPrintOutContainer_;

        private string region_ = string.Empty;
        private string certSelection_ = string.Empty;

        private string container_ = string.Empty; // comboContainer.Text
        private string id_ = string.Empty; // textSubscriptionId.Text
        private string thumbprint_ = string.Empty; // textThumbprint.Text
        private string affinityTag_ = string.Empty; // comboAffinity.Text
        private string subnets_ = string.Empty; // comboSubnets.Text

        private enum EnumVnType
        {
            None,
            AffinityGroup,
            VirtualNetwork
        }

        private readonly Dictionary<string, List<string>> tagsToSubnets_ = new Dictionary<string,List<string>>();
        private readonly Dictionary<string, EnumVnType> tagsToVnType_ = new Dictionary<string, EnumVnType>();
        private readonly Dictionary<string, string> tagsToNames_ = new Dictionary<string, string>();

        #endregion Data members

        #region Constructors

        public AzureAdvancedSettingsPanel(string region) : this()
        {
            region_ = region;
        }
        
        protected AzureAdvancedSettingsPanel( )
        {
            InitializeComponent();

            // Tags text:
            tagContainer.Text = Settings.Default.S4AzureContainerNameLabelText;
            tagID.Text = Settings.Default.S4AzureSubscriptionIdText;
            tagThumbprint.Text = Settings.Default.S4AzureCertificateThumbprintText;
            tagAffinityGroup.Text = Settings.Default.S4AzureAffinityText;

            // Tool tip text:
            toolTipContainer_ = new ToolTipContainer(toolTip);
            toolTipContainer_.Create(comboContainer, Settings.Default.S4AzureContainerToolTip, null);
            toolTipContainer_.Create(textSubscriptionId, Settings.Default.S4AzureSubscriptionIdToolTip, null);
            toolTipContainer_.Create(textThumbprint, Settings.Default.S4AzureThumbprintToolTip, null);
            toolTipContainer_.Create(comboAffinity, Settings.Default.S4AzureAffinityToolTip, null);
            toolTipContainer_.Create(comboSubnets, Settings.Default.S4AzureAffinityToolTip, null);
            toolTipContainer_.Create(btnCreateCertificate, Settings.Default.S4AzureCreateThumbprintButtonToolTip, Settings.Default.S4AzureCreateThumbprintButtonToolTip);
            toolTipContainer_.Create(chkDeployVm, Settings.Default.S4AzureDeployWmToolTip, Settings.Default.S4AzureDeployWmToolTip);

            // Register debug print-out:
            debugPrintOutContainer_ = new ControlDebugPrintoutContainer(logger_);
            debugPrintOutContainer_.Create(comboContainer, "Container: ");
            debugPrintOutContainer_.Create(textSubscriptionId, "Subscription ID: ");
            debugPrintOutContainer_.Create(textThumbprint, "Thumbprint: ");
            debugPrintOutContainer_.Create(comboAffinity, "Affinity combobox: ");
            debugPrintOutContainer_.Create(btnCreateCertificate, "Button create certificate: ");
            debugPrintOutContainer_.Create(chkDeployVm, "Deploy virtual machine: : ");

            // Disable virtual machine deployment section.
            pnlVirtualMachineSettingsHolder.Enabled = false;
        }

        #endregion Constructors

        #region Public methods

        public void SetRegion(string region)
        {
            region_ = region;
        }

        public AzureParams CreateAzureParams(string storageAcc, string primaryAccessKey)
        {
            if (!string.IsNullOrEmpty(thumbprint_))
            {
                if (!CertificateUtils.CheckCertificateInstalled(thumbprint_, ref certSelection_))
                {
                    return null;
                }

                EnumVnType type = EnumVnType.None;
                string affinity = string.Empty;
                string virtualNetwork = string.Empty;
                string subnet = string.Empty;
                if (!string.IsNullOrEmpty(affinityTag_) && EmptySelection != affinityTag_)
                {
                    // getting original name and type for this tag
                    string affinity_name = tagsToNames_[affinityTag_];
                    tagsToVnType_.TryGetValue(affinityTag_, out type);
                    switch (type)
                    {
                        case EnumVnType.AffinityGroup:
                            {
                                affinity = affinity_name;
                                break;
                            }
                        case EnumVnType.VirtualNetwork:
                            {
                                virtualNetwork = affinity_name;
                                if (!string.IsNullOrEmpty(subnets_) && EmptySelection != subnets_)
                                {
                                    subnet = subnets_;
                                }
                                break;
                            }
                        case EnumVnType.None:
                        default:
                            {
                                break;
                            }
                    }
                }

                return new AzureParams(storageAcc,
                    primaryAccessKey,
                    region_,
                    certSelection_,
                    id_,
                    thumbprint_,
                    virtualNetwork,
                    subnet,
                    affinity,
                    container_);
            }
            else
            {
                return new AzureParams(storageAcc,
                    primaryAccessKey,
                    region_,
                    certSelection_,
                    id_,
                    thumbprint_,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    container_);

            }
        }

        public string GetLocationOfStroageAccount(string accountName)
        {
            return MyGetLocationOfStroageAccount(accountName);
        }

        public void OnCertificateChecked()
        {
            if (!chkDeployVm.Checked)
            {
                return;
            }

            //1. Load affinity groups.
            List<AffinityGroupInfo> groups = MyRequestAffinityGroups();
            MyClearAffinity();
            MyClearSubnets();
            tagsToSubnets_.Clear();
            tagsToVnType_.Clear();
            tagsToNames_.Clear();

            comboAffinity.TextChanged -= comboAffinity_TextChanged;
            comboAffinity.DropDownStyle = ComboBoxStyle.DropDown;

            comboAffinity.Items.Add(EmptySelection);
            List<string> empty = new List<string>();
            empty.Add(EmptySelection);
            tagsToNames_.Add(EmptySelection, EmptySelection);
            tagsToSubnets_.Add(EmptySelection, empty);
            tagsToVnType_.Add(EmptySelection, EnumVnType.None);

            foreach (AffinityGroupInfo group in groups)
            {
                string tag = string.Format("{0} (affinity group)", group.Name);
                comboAffinity.Items.Add(tag);
                tagsToNames_.Add(tag, group.Name);
                tagsToSubnets_.Add(tag, empty);
                tagsToVnType_.Add(tag, EnumVnType.AffinityGroup);
            }
            
            //2. Load virtual networks.
            List<VirtualNetworksInfo> networks = MyRequestVirtualNetworks();
            foreach (VirtualNetworksInfo network in networks)
            {
                string tag = string.Format("{0} (virtual network)", network.Name);
                tagsToNames_.Add(tag, network.Name);
                comboAffinity.Items.Add(tag);
                List<string> subnets = new List<string>();
                subnets.Add(EmptySelection);
                foreach(VirtualNetworksInfo subnet in network.Subnets)
                {
                    subnets.Add(subnet.Name);
                }
                tagsToSubnets_.Add(tag, subnets);
                tagsToVnType_.Add(tag, EnumVnType.VirtualNetwork);
            }

            comboAffinity.SelectedIndex = 0;
            comboAffinity.TextChanged += comboAffinity_TextChanged;
        }

        public string CheckCredentials()
        {
            // Do the client-side check.
            // Subscription ID should be GUID
            // Thumbprint should have exactly 40 symbols
            
            try
            {
                Guid guid = new Guid(id_);
            }
            catch (FormatException)
            {
                return Settings.Default.S4AzureSubscriptionIDError;
            }            

            if (null == thumbprint_ || 40 != thumbprint_.Length)
            {
                return Settings.Default.S4AzureThumbprintIllFormed;
            }

            return null;
        }

        public bool LoadStorages(string storageAcc, string primaryKey)
        {
            string accountName = storageAcc;
            string accountSharedKey = primaryKey;
            string messageSignature = "";

            // Set request URI
            Uri requesturi = new Uri("https://" + accountName + ".blob.core.windows.net/?comp=list");

            // Create HttpWebRequest object
            HttpWebRequest Request = (HttpWebRequest)HttpWebRequest.Create(requesturi.AbsoluteUri);
            Request.Method = "GET";
            Request.ContentLength = 0;
            // Add HTTP headers
            Request.Headers.Add("x-ms-date", DateTime.UtcNow.ToString("R"));
            Request.Headers.Add("x-ms-version", "2009-09-19");

            // Create Signature
            // Verb
            messageSignature += "GET\n";
            // Content-Encoding
            messageSignature += "\n";
            // Content-Language
            messageSignature += "\n";
            // Content-Length
            messageSignature += "\n";
            // Content-MD5
            messageSignature += "\n";
            // Content-Type
            messageSignature += "\n";
            // Date
            messageSignature += "\n";
            // If-Modified-Since
            messageSignature += "\n";
            // If-Match
            messageSignature += "\n";
            // If-None-Match 
            messageSignature += "\n";
            // If-Unmodified-Since
            messageSignature += "\n";
            // Range
            messageSignature += "\n";
            // CanonicalizedHeaders
            messageSignature += GetCanonicalizedHeaders(Request);
            // CanonicalizedResource
            messageSignature += GetCanonicalizedResourceVersion2(requesturi, accountName);
            // Use HMAC-SHA256 to sign the signature
            byte[] SignatureBytes = System.Text.Encoding.UTF8.GetBytes(messageSignature);
            if (accountSharedKey.Length == 88)
            {
                System.Security.Cryptography.HMACSHA256 SHA256 = new System.Security.Cryptography.HMACSHA256(Convert.FromBase64String(accountSharedKey));
                // Create Authorization HTTP header value
                String AuthorizationHeader = "SharedKey " + accountName + ":" + Convert.ToBase64String(SHA256.ComputeHash(SignatureBytes));
                // Add Authorization HTTP header
                Request.Headers.Add("Authorization", AuthorizationHeader);
            }

            //Send Http request and get response.
            using (HttpWebResponse response = (HttpWebResponse)Request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MyClearContainer();

                    //Read container names from response.  
                    using (XmlTextReader reader = new XmlTextReader(response.GetResponseStream()))
                    {
                        try
                        {
                            comboContainer.TextChanged -= comboContainer_TextChanged;
                            while (reader.Read())
                            {
                                if (reader.Name == "Name")
                                {
                                    comboContainer.Items.Add(reader.ReadElementString("Name"));
                                }
                            }

                            if (0 != comboContainer.Items.Count)
                            {
                                comboContainer.DropDownStyle = ComboBoxStyle.DropDown;
                            }
                        }
                        finally
                        {
                            comboContainer.TextChanged += comboContainer_TextChanged;
                        }
                    }
                    return true;
                }
                return false;
            }
        }

        public bool IsDeployWmChecked()
        {
            return chkDeployVm.Checked;
        }

        #endregion Public methods

        #region Private methods

        private List<AffinityGroupInfo> MyRequestAffinityGroups()
        {
            string uri = String.Format("https://management.core.windows.net/{0}/affinitygroups", id_);

            List<AffinityGroupInfo> groups = new List<AffinityGroupInfo>();
            using (HttpWebResponse response = AzureHttpRequest.DoRequest("2011-10-01", thumbprint_, uri))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(response.GetResponseStream());
                    foreach (XmlNode node in doc.DocumentElement)
                    {
                        string name = node["Name"].InnerText;
                        groups.Add(new AffinityGroupInfo(name));
                    }
                }
            }
            return groups;
        }

        private List<VirtualNetworksInfo> MyRequestVirtualNetworks()
        {
            string uri = String.Format("https://management.core.windows.net/{0}/services/networking/virtualnetwork", id_);

            List<VirtualNetworksInfo> networks = new List<VirtualNetworksInfo>();
            using (HttpWebResponse response = AzureHttpRequest.DoRequest("2012-03-01", thumbprint_, uri))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(response.GetResponseStream());
                    foreach (XmlNode node in doc.DocumentElement)
                    {
                        string name = node["Name"].InnerText;
                        VirtualNetworksInfo vni = new VirtualNetworksInfo(name);
                        networks.Add(vni);
                        try
                        {
                            foreach (XmlNode subnet in node["Subnets"])
                            {
                                vni.AddSubnet(new VirtualNetworksInfo(subnet["Name"].InnerText));
                            }
                        }
                        catch (Exception ex)
                        {
                            logger_.Error(string.Format("Failed to get subnets. Message: {0}. Stack tarace: {1}",
                                ex.Message,
                                ex.StackTrace));
                        }
                    }
                }
            }

            return networks;
        }

        private string MyGetLocationOfStroageAccount(string accountName)
        {
            string subscriptionId = id_;
            string thumbprint = thumbprint_;

            X509Certificate2 certificate = CertificateUtils.GetCertificate(thumbprint, CertificateUtils.CertificateStore).certificate;

            if (certificate == null)
            {
                throw new AzureCertificateException("Failed to obtain certificate.");
            }

            string uriFormat = "https://management.core.windows.net/{0}/services/storageservices/{1}";
            Uri uri = new Uri(String.Format(uriFormat, subscriptionId, accountName));

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = "GET";
            request.Headers.Add("x-ms-version", "2011-10-01");
            request.ClientCertificates.Add(certificate);
            request.ContentType = "application/xml";

            string location = string.Empty;
            // Send Http request and get response
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (XmlTextReader reader = new XmlTextReader(response.GetResponseStream()))
                    {
                        while (reader.Read())
                        {
                            // If the storage accoun is assign to an affinity group - no location is provided.
                            // Look up for the affinity group here.
                            // Then fetch location from the affinity group.
                            if (reader.Name == "Location")
                            {
                                location = reader.ReadElementString("Location");
                            }
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(location))
            {
                throw new InconsistentAzueDataException("Failed to obtain the storage location.");
            }

            return location;
        }

        private void MyClearSubscriptionId()
        {
            MyClearTextBox(textSubscriptionId, textSubscriptionId_TextChanged);
            id_ = string.Empty;
        }

        private void MyClearThumbprint()
        {
            MyClearTextBox(textThumbprint, textThumbprint_TextChanged);
            thumbprint_ = string.Empty;
        }

        private void MyClearContainer()
        {
            MyClearComboBox(comboContainer, comboContainer_TextChanged);
            container_ = string.Empty;
        }

        private void MyClearAffinity()
        {
            MyClearComboBox(comboAffinity, comboAffinity_TextChanged);
            affinityTag_ = string.Empty;
        }

        private void MyClearSubnets()
        {
            MyClearComboBox(comboSubnets, comboSubnets_TextChanged);
            subnets_ = string.Empty;
        }

        private void MyClearTextBox(TextBox ctrl, EventHandler textChangedHandler)
        {
            ctrl.TextChanged -= textChangedHandler;
            ctrl.Text = string.Empty;
            ctrl.TextChanged += textChangedHandler;
        }

        private void MyClearComboBox(ComboBox ctrl, EventHandler textChangedHandler)
        {
            ctrl.TextChanged -= textChangedHandler;
            ctrl.Items.Clear();
            ctrl.Text = string.Empty;
            ctrl.DropDownStyle = ComboBoxStyle.Simple;
            ctrl.TextChanged += textChangedHandler;
        }

        #endregion Private methods

        #region Event handlers

        private void azureDeployVirtualMachineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            pnlVirtualMachineSettingsHolder.Enabled = chkDeployVm.Checked;
        }

        private void comboContainer_TextChanged(object sender, EventArgs e)
        {
            container_ = comboContainer.Text;
        }

        private void comboAffinity_TextChanged(object sender, EventArgs e)
        {
            affinityTag_ = string.Empty;
            if (EmptySelection != comboAffinity.Text)
            {
                affinityTag_ = comboAffinity.Text;
            }
            MyClearSubnets();

            comboSubnets.TextChanged -= comboSubnets_TextChanged;
            List<string> subnets = null;
            if (tagsToSubnets_.TryGetValue(comboAffinity.Text, out subnets) && null != subnets && 0 != subnets.Count)
            {
                comboSubnets.DropDownStyle = ComboBoxStyle.DropDown;
                foreach (string subnet in subnets)
                {
                    comboSubnets.Items.Add(subnet);
                }
                comboSubnets.SelectedIndex = 0;
            }
            comboSubnets.TextChanged += comboSubnets_TextChanged;
        }

        private void comboSubnets_TextChanged(object sender, EventArgs e)
        {
            subnets_ = (EmptySelection == comboSubnets.Text) ? string.Empty : comboSubnets.Text;
        }

        private void textSubscriptionId_TextChanged(object sender, EventArgs e)
        {
            id_ = textSubscriptionId.Text;
        }

        private void textThumbprint_TextChanged(object sender, EventArgs e)
        {
            thumbprint_ = textThumbprint.Text;
        }        
        
        private void btnCreateCertificate_Click(object sender, EventArgs e)
        {
            MakeCertLauncher launcher = new MakeCertLauncher(logger_, delegate(string thumbprint)
                {
                    if (!string.IsNullOrEmpty(thumbprint))
                    {
                        DialogResult reslt = BetterDialog.ShowDialog(Settings.Default.S4AzureCertificateHeader,
                        Settings.Default.S4AzureCertificateCreateSuccess, "", "OK", "OK",
                        System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);
                    }
                    else
                    {
                        DialogResult reslt = BetterDialog.ShowDialog(Settings.Default.S4AzureCertificateHeader,
                        Settings.Default.S4AzureCertificateCreateError, "", "OK", "OK",
                        System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
                        return;
                    }                    
                    
                    this.BeginInvoke(new Action(() => { textThumbprint.Text = thumbprint; }));
                    // Open browser with Windows Azure, for user to upload created certificate.
                    Process.Start("https://manage.windowsazure.com/#Workspaces/AdminTasks/ListManagementCertificates");

                    BetterDialog.ShowDialog(Settings.Default.S4AzureCertificateHeader,
                        Settings.Default.S4AzureCertificateUploadWait, "", "OK", "OK",
                        System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);            
                    
                    // Why does it throw 'call UI control' from another thread exception?
                    // System.Windows.Forms.MessageBox.Show(this, Settings.Default.S4AzureCertificateUploadWait, Settings.Default.S4AzureCertificateHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                });

            if (!launcher.Start())
            {
                BetterDialog.ShowDialog(Settings.Default.S4AzureCertificateHeader,
                    Settings.Default.S4AzureCertificateCreateError, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);            
            }
        }

        #endregion Event handlers

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

        #endregion Helper method/class
    }
}
