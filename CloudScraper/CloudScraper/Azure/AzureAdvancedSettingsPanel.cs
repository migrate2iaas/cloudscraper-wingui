using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using DotNetPerls;
using CloudScraper.Properties;
using NLog;

namespace CloudScraper
{
    public partial class AzureAdvancedSettingsPanel : UserControl
    {
        #region Data members

        private static Logger logger_ = LogManager.GetLogger("AzureAdvancedParametersPanel");
        
        private readonly ToolTipContainer toolTipContainer_;
        private readonly ControlDebugPrintoutContainer debugPrintOutContainer_;

        private string region_ = string.Empty;

        private string container_ = string.Empty; // comboContainer.Text
        private string id_ = string.Empty; // textSubscriptionId.Text
        private string thumbprint_ = string.Empty; // textThumbprint.Text
        private string affinity_ = string.Empty; // comboAffinity.Text

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
            comboContainer.Text = Settings.Default.S4AzureContainerNameLabelText;
            textSubscriptionId.Text = Settings.Default.S4AzureSubscriptionIdText;
            textThumbprint.Text = Settings.Default.S4AzureCertificateThumbprintText;
            comboAffinity.Text = Settings.Default.S4AzureAffinityText;

            // Tool tip text:
            toolTipContainer_ = new ToolTipContainer(toolTip);
            toolTipContainer_.Create(comboContainer, Settings.Default.S4AzureContainerToolTip, null);
            toolTipContainer_.Create(textSubscriptionId, Settings.Default.S4AzureSubscriptionIdToolTip, null);
            toolTipContainer_.Create(textThumbprint, Settings.Default.S4AzureThumbprintToolTip, null);
            toolTipContainer_.Create(comboAffinity, Settings.Default.S4AzureAffinityToolTip, null);
            toolTipContainer_.Create(btnCreateCertificate, Settings.Default.S4AzureCreateThumbprintButtonToolTip, null);

            // Register debug print-out:
            debugPrintOutContainer_ = new ControlDebugPrintoutContainer(logger_);
            debugPrintOutContainer_.Create(comboContainer, "Container: ");
            debugPrintOutContainer_.Create(textSubscriptionId, "Subscription ID: ");
            debugPrintOutContainer_.Create(textThumbprint, "Thumbprint: ");
            debugPrintOutContainer_.Create(comboAffinity, "Affinity combobox: ");
            debugPrintOutContainer_.Create(btnCreateCertificate, "Button create certificate: ");
            debugPrintOutContainer_.Create(chkDeployVm, "Deploy virtual machine: : ");
        }

        #endregion Constructors

        #region Private methods

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
            affinity_ = string.Empty;
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
            affinity_ = comboAffinity.Text;
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
            if (logger_.IsDebugEnabled)
                logger_.Debug("Create Certificate button click.");

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Certificate File (*.cer)|*.cer";
            saveFileDialog.DefaultExt = "." + "cer";
            string certificatePath = string.Empty;
            string certificateName = string.Empty;

            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                certificatePath = saveFileDialog.FileName;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }

            certificateName = certificatePath.Substring(saveFileDialog.FileName.LastIndexOf('\\') + 1);

            // NOTE: we should alter the argeuments of makecert to create cert in the local machine location in order to run from service context
            // or impersonate alternatively the service process when accessing certs

            //Creating certificate process.
            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "makecert.exe";
            info.Arguments = "-sky exchange -r -n \"CN=" + certificateName + "\" -pe -a sha1 -len 2048 -ss " + this.certificateStore + " \"" + certificatePath + "\"";
            info.UseShellExecute = true;
            info.UserName = System.Diagnostics.Process.GetCurrentProcess().StartInfo.UserName;
            info.Password = System.Diagnostics.Process.GetCurrentProcess().StartInfo.Password;
            process.StartInfo = info;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Exited += new EventHandler(this.ProcessExited);
            process.EnableRaisingEvents = true;

            //Count certificates.
            // note: predefined, could be changed
            X509Store store = new X509Store(this.certificateStore, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection fcollection = collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            certificateCount = fcollection.Count;
            store.Close();

            if (!process.Start())
            {
                DialogResult reslt = BetterDialog.ShowDialog(Settings.Default.S4AzureCertificateHeader,
                    Settings.Default.S4AzureCertificateCreateError, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
            }
        }

        private void ProcessExited(object sender, EventArgs e)
        {
            //Verify that new certificate has created.
            try
            {
                X509Store store = new X509Store(this.certificateStore, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection fcollection = collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                //X509Certificate2Collection pcollection = fcollection.Find(X509FindType.FindBySubjectDistinguishedName, "CN=" + certificatePath, false);

                if (fcollection.Count > certificateCount)
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
                }


                int indexOfthumbprint = 0;
                if (fcollection.Count > 0)
                {
                    foreach (X509Certificate certificate in fcollection)
                    {
                        if (fcollection[fcollection.IndexOf(certificate)].NotBefore >
                            fcollection[indexOfthumbprint].NotBefore)
                        {
                            indexOfthumbprint = fcollection.IndexOf(certificate);
                        }
                    }
                }

                this.BeginInvoke(new Delegate(delegate { zoneComboBox.Text = fcollection[indexOfthumbprint].Thumbprint; }));
                store.Close();
            }
            catch (CryptographicException ex)
            {
                if (logger_.IsErrorEnabled)
                    logger_.Error(ex);
            }

            //Open browser with Windows Azure, for user to upload created certificate.
            Process.Start("https://manage.windowsazure.com/#Workspaces/AdminTasks/ListManagementCertificates");

            System.Windows.Forms.MessageBox.Show(this, Settings.Default.S4AzureCertificateUploadWait, Settings.Default.S4AzureCertificateHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #endregion Event handlers
    }
}
