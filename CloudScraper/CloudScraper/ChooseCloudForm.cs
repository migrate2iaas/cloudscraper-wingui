using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CloudScraper.Properties;

namespace CloudScraper
{
    public partial class ChooseCloudForm : Form
    {
        ChooseDisksForm chooseDiskForm_;
        CloudParametersForm cloudParametersForm_;
        
        public ChooseCloudForm(ChooseDisksForm chooseDiskForm)
        {
            this.chooseDiskForm_ = chooseDiskForm;

            InitializeComponent();

            //Initialize basic UI strings from settings file.
            this.amazonButton.Image = new Bitmap(Image.FromFile("Icons\\Amazon.ico"), new Size(32, 32));
            this.windowsAzureButton.Image = new Bitmap(Image.FromFile("Icons\\Azure.ico"), new Size(32, 32));
            this.elasticHostsButton.Image = new Bitmap(Image.FromFile("Icons\\Elastic.ico"), new Size(32, 32));
            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.toolTip.SetToolTip(this.helpButton, Settings.Default.HelpButtonToolTip);
            this.Text = Settings.Default.S3Header;
            this.backButton.Text = Settings.Default.S3BackButtonText;
            this.toolTip.SetToolTip(this.backButton, Settings.Default.S3BackButtonToolTip);
            this.amazonButton.Text = Settings.Default.S3AmazonButtonText;
            this.toolTip.SetToolTip(this.amazonButton, Settings.Default.S3AmazonButtonToolTip);
            this.windowsAzureButton.Text = Settings.Default.S3WindowsAzureButtonText;
            this.toolTip.SetToolTip(this.windowsAzureButton, Settings.Default.S3WindowsAzureButtonToolTip);
            this.elasticHostsButton.Text = Settings.Default.S3ElasticHostsButtonText;
            this.toolTip.SetToolTip(this.elasticHostsButton, Settings.Default.S3ElasticHostsButtonToolTip);
            this.logoPicture.Image = new Bitmap(Image.FromFile("Icons\\logo4a.png"));
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            this.Hide();
            this.chooseDiskForm_.StartPosition = FormStartPosition.Manual;
            this.chooseDiskForm_.Location = this.Location;
            this.chooseDiskForm_.Show();
        }

        private void AmazonButtonClick(object sender, EventArgs e)
        {
            this.Hide();

            if (this.cloudParametersForm_ == null)
            {
                this.cloudParametersForm_ = new AmazonCloudParameters(this);
                //this.cloudParametersForm_ = new CloudParametersForm(this);
            }

            cloudParametersForm_.ShowDialog();
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            this.chooseDiskForm_.Close();
        }

        private void HelpButtonClick(object sender, EventArgs e)
        {
            //Start help url.
            System.Diagnostics.Process.Start(Settings.Default.S3Link);
        }

        private void ChooseCloudLoad(object sender, EventArgs e)
        {
            //For show  window in same position as prev.
            this.StartPosition = FormStartPosition.Manual;
            this.Location = this.chooseDiskForm_.Location;
        }

        private void ElasticHostsButtonClick(object sender, EventArgs e)
        {
            this.Hide();

            if (this.cloudParametersForm_ == null)
            {
                //this.eHCloudParametersForm_ = new EHCloudParametersForm(this);
                this.cloudParametersForm_ = new EHCloudParameters(this);
            }

            this.cloudParametersForm_.ShowDialog();
            //eHCloudParametersForm_.ShowDialog();
        }
    }
}
