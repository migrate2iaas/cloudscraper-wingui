using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CloudScraper.Properties;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace CloudScraper
{
    public partial class CloudParametersForm : Form
    {
        public SortedDictionary<string, string> regionList_;
        public SortedDictionary<string, string> serverTypeList_;

        public ChooseCloudForm chooseCloudForm_;
        public ImagesPathForm imagesPathForm_;

        public CloudParametersForm()
        {
            this.regionList_ = new SortedDictionary<string, string>();
            this.serverTypeList_ = new SortedDictionary<string, string>();

            InitializeComponent();
        }
        
        public void SetChooseCloudForm(ChooseCloudForm chooseCloudForm)
        {
            this.chooseCloudForm_ = chooseCloudForm;          
        }

        public virtual void BackButtonClick(object sender, EventArgs e)
        {
            this.Hide();
            this.chooseCloudForm_.StartPosition = FormStartPosition.Manual;
            this.chooseCloudForm_.Location = this.Location;
            this.chooseCloudForm_.Show();
        }

        public virtual void NextButtonClick(object sender, EventArgs e)
        {
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            this.chooseCloudForm_.Close();
        }

        public virtual void IDChanged(object sender, EventArgs e)
        {
        }

        public virtual void KeyChanged(object sender, EventArgs e)
        {
        }

        public virtual void RegionListBoxChanged(object sender, EventArgs e)
        {
        }

        public virtual void AdvancedChecked(object sender, EventArgs e)
        {
        }

        public virtual void BucketChanged(object sender, EventArgs e)
        {
        }

        public virtual void FolderKeyChanged(object sender, EventArgs e)
        {
        }

        public virtual void ServerTypeChanged(object sender, EventArgs e)
        {
        }

        public virtual void ZoneComboBoxIndexChanged(object sender, EventArgs e)
        {
        }

        public virtual void GroupComboBoxIndexChanged(object sender, EventArgs e)
        {
        }

        public virtual void GroupComboBoxTextChanged(object sender, EventArgs e)
        {
        }

        public virtual void ZoneComboBoxTextChanged(object sender, EventArgs e)
        {
        }

        //Check bucket when lost focus.
        // See http://docs.aws.amazon.com/AmazonS3/latest/dev/BucketRestrictions.html 
        public virtual void BucketTextBoxLeave(object sender, EventArgs e)
        {
        }

        public virtual void HelpButtonClick(object sender, EventArgs e)
        {
        }

        //Test connection.
        public virtual void TestButtonClick(object sender, EventArgs e)
        {
        }


        public virtual void CloudParametersLoad(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = this.chooseCloudForm_.Location;
            //isAmazon_ = false;
        }

        public virtual void TextBoxMouseEnter(object sender, EventArgs e)
        {
        }

    }
}
