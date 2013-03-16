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
    public partial class CloudParametersForm : Form
    {
        public static string awsId_ = "";
        public static string awsKey_ = "";
        public static string region_;
        public static bool advanced_ = false;
        public static string s3bucket_ = "";
        public static string type_;
        public static string zone_ = "";
        public static string group_ = "";
        
        ChooseCloudForm chooseCloudForm_;
        ImagesPathForm imagesPathForm_;

        SortedDictionary<string, string> regionList_;
        SortedDictionary<string, string> serverTypeList_;

        public CloudParametersForm(ChooseCloudForm chooseCloudForm)
        {
            this.chooseCloudForm_ = chooseCloudForm;
            this.regionList_ = new SortedDictionary<string, string>();
            this.serverTypeList_ = new SortedDictionary<string, string>();

            foreach (string str in Settings.Default.Regions)
            { 
                this.regionList_.Add(str.Split(new char[] { Settings.Default.Separator }, 2)[1],
                    str.Split(new char[] { Settings.Default.Separator }, 2)[0]);
            }

            foreach (string str in Settings.Default.ServerTypes)
            {
                this.serverTypeList_.Add(str.Split(new char[] { Settings.Default.Separator }, 2)[1],
                    str.Split(new char[] { Settings.Default.Separator }, 2)[0]);
            }

            InitializeComponent();

            foreach (KeyValuePair<string, string> region in this.regionList_)
            {
                this.regionComboBox.Items.Add(region.Key);
                if (region.Value == "us-east-1")
                {
                    this.regionComboBox.SelectedItem = region.Key;
                }
            }

            foreach (KeyValuePair<string, string> type in this.serverTypeList_)
            {
                this.serverTypeComboBox.Items.Add(type.Key);
            }

            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.serverTypeComboBox.SelectedIndex = 0;
            this.nextButton.Enabled = false;
            this.Text = Settings.Default.S4Header;
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            this.Hide();
            this.chooseCloudForm_.Show();
        }

        private void NextButtonClick(object sender, EventArgs e)
        {
            this.Hide();

            if (this.imagesPathForm_ == null)
            {
                this.imagesPathForm_ = new ImagesPathForm(this);
            }

            imagesPathForm_.ShowDialog();
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            this.chooseCloudForm_.Close();
        }

        private void AwsIDChanged(object sender, EventArgs e)
        {
            awsId_ = (sender as TextBox).Text;
            this.CheckEnter();
        }

        private void AwsKeyChanged(object sender, EventArgs e)
        {
            awsKey_ = (sender as TextBox).Text;
            this.CheckEnter();
        }

        private void RegChanged(object sender, EventArgs e)
        {
            region_ = this.regionList_[(string)(sender as ComboBox).SelectedItem];
        }

        private void AdvancedChecked(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                advanced_ = true;
                this.bucketTextBox.Enabled = true;
                this.bucketLabel.Enabled = true;
                this.serverTypeComboBox.Enabled = true;
                this.typeLabel.Enabled = true;
                this.zoneTextBox.Enabled = true;
                this.groupTextBox.Enabled = true;
                this.zoneLabel.Enabled = true;
                this.groupLabel.Enabled = true;
                this.CheckEnter();
            }
            else
            {
                advanced_ = false;
                this.bucketTextBox.Enabled = false;
                this.bucketLabel.Enabled = false;
                this.serverTypeComboBox.Enabled = false;
                this.typeLabel.Enabled = false;
                this.zoneTextBox.Enabled = false;
                this.groupTextBox.Enabled = false;
                this.zoneLabel.Enabled = false;
                this.groupLabel.Enabled = false;
                this.CheckEnter();
            }
        }

        private void BucketChanged(object sender, EventArgs e)
        {
            s3bucket_ = (sender as TextBox).Text;
            this.CheckEnter();
        }

        private void ZoneChanged(object sender, EventArgs e)
        {
            zone_ = (sender as TextBox).Text;
            this.CheckEnter();
        }

        private void GroupChanged(object sender, EventArgs e)
        {
            group_ = (sender as TextBox).Text;
            this.CheckEnter();
        }

        private void ServerTypeChanged(object sender, EventArgs e)
        {
            type_ = this.serverTypeList_[(string)(sender as ComboBox).SelectedItem];
        }

        private void CheckEnter()
        {
            if (!advanced_ && awsId_ != "" && awsKey_ != "")
            {
                this.nextButton.Enabled = true;
            }
            else if (advanced_ && awsId_ != "" && awsKey_ != ""
                && s3bucket_ != "" && zone_ != "" && group_ != "")
            {
                this.nextButton.Enabled = true;
            }
            else
            {
                this.nextButton.Enabled = false;
            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ya.ru");
        }
    }
}
