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

            this.amazonButton.Image = new Bitmap(Image.FromFile("Icons\\Amazon.ico"), new Size(32, 32));
            this.windowsAzureButton.Image = new Bitmap(Image.FromFile("Icons\\Azure.ico"), new Size(32, 32));
            this.elasticHostsButton.Image = new Bitmap(Image.FromFile("Icons\\Elastic.ico"), new Size(32, 32));
            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.Text = Settings.Default.S3Header;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.chooseDiskForm_.Show();
        }

        private void amazonButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (this.cloudParametersForm_ == null)
            {
                this.cloudParametersForm_ = new CloudParametersForm(this);
            }

            cloudParametersForm_.ShowDialog();
        }

        private void On_closed(object sender, FormClosedEventArgs e)
        {
            this.chooseDiskForm_.Close();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ya.ru");
        }
    }
}
