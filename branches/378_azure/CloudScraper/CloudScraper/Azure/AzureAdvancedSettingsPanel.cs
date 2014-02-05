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

namespace CloudScraper.Azure
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
            MakeCertLauncher launcher = new MakeCertLauncher(logger_, delegate(string thumbprint)
                {
                    this.BeginInvoke(new Action(() => { textThumbprint.Text = thumbprint; }));
                    // Open browser with Windows Azure, for user to upload created certificate.
                    Process.Start("https://manage.windowsazure.com/#Workspaces/AdminTasks/ListManagementCertificates");
                    System.Windows.Forms.MessageBox.Show(this, Settings.Default.S4AzureCertificateUploadWait, Settings.Default.S4AzureCertificateHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                });

            if (!launcher.Start())
            {
                DialogResult reslt = BetterDialog.ShowDialog(Settings.Default.S4AzureCertificateHeader,
                    Settings.Default.S4AzureCertificateCreateError, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);            
            }
        }

        #endregion Event handlers
    }
}
