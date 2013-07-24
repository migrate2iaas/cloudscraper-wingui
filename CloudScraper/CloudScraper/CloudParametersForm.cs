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

        void tabControl1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        
        public void SetChooseCloudForm(ChooseCloudForm chooseCloudForm)
        {
            this.chooseCloudForm_ = chooseCloudForm;          
        }

        protected virtual void BackButtonClick(object sender, EventArgs e)
        {
            this.Hide();
            this.chooseCloudForm_.StartPosition = FormStartPosition.Manual;
            this.chooseCloudForm_.Location = this.Location;
            this.chooseCloudForm_.Show();
        }

        protected virtual void NextButtonClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            this.chooseCloudForm_.Close();
        }

        protected virtual void IDChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void KeyChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void RegionListBoxChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void AdvancedChecked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void AzureDeployVirtualMachineChecked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void BucketChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void FolderKeyChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void ServerTypeChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void ZoneComboBoxIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void GroupComboBoxIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void GroupComboBoxTextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void ZoneComboBoxTextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //Check bucket when lost focus.
        // See http://docs.aws.amazon.com/AmazonS3/latest/dev/BucketRestrictions.html 
        protected virtual void BucketTextBoxLeave(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void HelpButtonClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //Test connection.
        protected virtual void TestButtonClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        protected virtual void CloudParametersLoad(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = this.chooseCloudForm_.Location;
            //isAmazon_ = false;
        }

        protected virtual void TextBoxMouseEnter(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void DrivesSelect(object sender, ItemCheckEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void UseDeduplicationChecked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnSelect(object sender, DataGridViewCellMouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void SelectAll(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void AzureCreateNewCertificateButtonClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void AzureSubscriptionIdTextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void AzureContainerChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnLeaveEnter(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 'z')
                e.KeyChar = '\a';
        }
    }
}
