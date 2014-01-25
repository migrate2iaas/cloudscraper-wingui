using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.EC2;
using Amazon.EC2.Model;
using CloudScraper.Properties;
using DotNetPerls;
using NLog;

namespace CloudScraper
{
    public partial class AmazonAdvansedSettingsPanel : UserControl
    {
        #region Events

        public class ContentVerificationArgs : EventArgs
        {
            private readonly bool isContentValid_;

            public ContentVerificationArgs(bool isContentValid)
            {
                isContentValid_ = isContentValid;
            }

            public bool IsContentValid
            {
                get { return isContentValid_; }
            }
        }

        public event EventHandler<ContentVerificationArgs> OnContentVerified;

        #endregion Events

        #region Data members

        private static Logger logger_ = LogManager.GetLogger("AmazonCloudParametersForm");

        private readonly SortedDictionary<string, string> serverTypeList_;
        private string region_ = string.Empty;

        private string s3Bucket_ = string.Empty;  // bucketTextBox.Text
        private string folderKey_ = string.Empty; // folderKeyBox.Text
        private string serverType_ = string.Empty; // the selection in serverTypeComboBox
        private string zone_ = string.Empty; // the selection in zoneComboBox
        private string group_ = string.Empty; // the selection in groupComboBox

        #endregion Data members

        #region Constructors

        public AmazonAdvansedSettingsPanel()
        {
            InitializeComponent();
            
            //Set basic UI strings in Form. 
            this.advancedCheckBox.Text = Settings.Default.S4AmazonAdvancedCheckBoxText;
            this.bucketLabel.Text = Settings.Default.S4AmazonBucketLabelText;
            this.folderKeyLabel.Text = Settings.Default.S4AmazonFolderKeyLabelText;
            this.typeLabel.Text = Settings.Default.S4AmazonTypeLabelText;
            this.zoneLabel.Text = Settings.Default.S4AmazonZoneLabelText;
            this.groupLabel.Text = Settings.Default.S4AmazonGroupLabelText;
        }

        public AmazonAdvansedSettingsPanel(string region, SortedDictionary<string, string> serverTypeList)
            : this()
        {
            region_ = region;
            serverTypeList_ = serverTypeList;

            foreach (string key in serverTypeList_.Keys)
            {
                serverTypeComboBox.Items.Add(key);
            }
            serverTypeComboBox.SelectedIndex = 0;
        }

        #endregion Constructors

        #region Public methods

        public void ChangeRegion(string region)
        {
            region_ = region;
            
            // clear zones
            zoneComboBox.Items.Clear();
            zoneComboBox.Text = string.Empty;
            zoneComboBox.DropDownStyle = ComboBoxStyle.Simple;
            zone_ = string.Empty;

            // clear groups
            groupComboBox.Items.Clear();
            groupComboBox.Text = string.Empty;
            groupComboBox.DropDownStyle = ComboBoxStyle.Simple;
            group_ = string.Empty;
            
            // clear bucket
            bucketTextBox.Clear();
            bucketTextBox.Text = string.Empty;

            MyVerifyContent();
        }

        public bool IsAdvancedMode()
        {
            return advancedCheckBox.Checked;
        }

        public bool IsContentValid()
        {
            // TODO: do we need to check serverType_ here as well?
            return !advancedCheckBox.Checked ||
                (advancedCheckBox.Checked && 
                !string.IsNullOrEmpty(s3Bucket_) &&
                !string.IsNullOrEmpty(folderKey_) &&
                !string.IsNullOrEmpty(zone_) &&
                !string.IsNullOrEmpty(group_));
        }

        public void VerifyBucket()
        {
            //Check bucket name is correct.
            //See http://docs.aws.amazon.com/AmazonS3/latest/dev/BucketRestrictions.html 
            if (!string.IsNullOrEmpty(s3Bucket_))
            {
                // The migration utility doesn't support non-strict naming available for us-east-1 region
                // if (region_.Substring(0, 7) != "us-east")
                {
                    if (s3Bucket_[0] == '.' || s3Bucket_[s3Bucket_.Length - 1] == '.' || s3Bucket_.Contains("..")
                        || s3Bucket_.Length < 3 || s3Bucket_.Length > 63)
                    {
                        DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                            Settings.Default.S4InvalidBucketText, "", "OK", "OK",
                            System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);

                        return;
                    }

                    bool lookLikeIp = true;
                    bool lookDigitOnly = true;
                    foreach (char ch in s3Bucket_)
                    {
                        if (!char.IsDigit(ch) && ch != '.')
                        {
                            lookLikeIp = false;
                        }

                        if (!char.IsDigit(ch))
                        {
                            lookDigitOnly = false;
                        }
                    }

                    if (lookLikeIp && !lookDigitOnly)
                    {
                        DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                            Settings.Default.S4InvalidBucketText, "", "OK", "OK",
                            System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);

                        return;
                    }
                }
                /* if (region_.Substring(0, 7) == "us-east")
                 {
                     if (s3bucket_.Length > 255)
                     {
                         DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                             Settings.Default.S4InvalidBucketText, "", "OK", "OK",
                             System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
                        
                         //DialogResult result = MessageBox.Show(Settings.Default.S4InvalidBucketText,
                         //    Settings.Default.S4TestConnectionHeader,
                         //MessageBoxButtons.OK);
                         return;
                     }
                 }*/
            }            
        }

        public void LoadEC2UserSettingsFromServer(string id, string key)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(key))
            {
                return;
            }

            AmazonEC2Config config = new AmazonEC2Config();
            config.ServiceURL = "https://ec2." + region_ + ".amazonaws.com";

            AmazonEC2 client = new AmazonEC2Client(id, key, config);
            DescribeRegionsResponse regionResponse = client.DescribeRegions(new DescribeRegionsRequest());

            //Download zones.
            DescribeAvailabilityZonesResponse availabilityZonesResponse =
                client.DescribeAvailabilityZones(new DescribeAvailabilityZonesRequest());
            this.zoneComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            this.zoneComboBox.Items.Clear();

            foreach (AvailabilityZone zone in availabilityZonesResponse.DescribeAvailabilityZonesResult.AvailabilityZone)
            {
                if (zone.ZoneState == "available")
                {
                    this.zoneComboBox.Items.Add(zone.ZoneName);
                }
            }
            zoneComboBox.SelectedIndex = 0;
            zoneComboBox.SelectedItem = zoneComboBox.Items[0];
            //zone_ = (string)zoneComboBox.SelectedItem;

            //Download security groups.
            DescribeSecurityGroupsResponse securityGroupResponse =
                client.DescribeSecurityGroups(new DescribeSecurityGroupsRequest());
            this.groupComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            this.groupComboBox.Items.Clear();
            foreach (SecurityGroup group in securityGroupResponse.DescribeSecurityGroupsResult.SecurityGroup)
            {
                this.groupComboBox.Items.Add(group.GroupName);
            }
            groupComboBox.SelectedIndex = 0;
            groupComboBox.SelectedItem = groupComboBox.Items[0];
        }

        public class RegionMismatchException : ApplicationException
        {
        }

        public void LoadS3UserSettingsFromServer(string id, string key)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(key) || !IsAdvancedMode())
            {
                return;
            }

            // works only in advanced mode
            AmazonS3 s3Client = Amazon.AWSClientFactory.CreateAmazonS3Client(id, key);
            if (!string.IsNullOrEmpty(s3Bucket_))
            {
                GetBucketLocationRequest req = new GetBucketLocationRequest();
                req.BucketName = s3Bucket_;
                GetBucketLocationResponse bucketResponse = s3Client.GetBucketLocation(req);
                if (bucketResponse.Location != region_)
                {
                    throw new RegionMismatchException();
                }
            }
        }

        public AmazonParams GetUserInput(string id)
        {
            return new AmazonParams(id, region_, s3Bucket_, folderKey_, serverType_, zone_, group_);
        }

        #endregion Public methods

        #region Private methods

        private void MyVerifyContent()
        {
            if (null != OnContentVerified)
            {
                OnContentVerified(this, new ContentVerificationArgs(IsContentValid()));
            }
        }

        #endregion Private methods

        #region Event handlers

        private void advancedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            pnlAdvanced.Enabled = advancedCheckBox.Checked;
            MyVerifyContent();
        }

        private void bucketTextBox_TextChanged(object sender, EventArgs e)
        {
            //Check correct enter for bucket name.
            //See http://docs.aws.amazon.com/AmazonS3/latest/dev/BucketRestrictions.html 
            if (!string.IsNullOrEmpty(bucketTextBox.Text))
            {
                if (region_.Substring(0, 7) != "us-east" && bucketTextBox.Text[bucketTextBox.Text.Length - 1] != '.'
                    && bucketTextBox.Text[bucketTextBox.Text.Length - 1] != '-'
                    && !char.IsLower(bucketTextBox.Text[bucketTextBox.Text.Length - 1])
                    && !char.IsNumber(bucketTextBox.Text[bucketTextBox.Text.Length - 1]))
                {
                    string str = bucketTextBox.Text.Remove(bucketTextBox.Text.Length - 1);
                    bucketTextBox.TextChanged -= bucketTextBox_TextChanged;
                    bucketTextBox.Text = string.Empty;
                    bucketTextBox.AppendText(str);
                    bucketTextBox.TextChanged += new System.EventHandler(bucketTextBox_TextChanged);
                }
            }
            s3Bucket_ = bucketTextBox.Text;
            MyVerifyContent();
        }

        private void bucketTextBox_Leave(object sender, EventArgs e)
        {
            //Check bucket when lost focus.
            // See http://docs.aws.amazon.com/AmazonS3/latest/dev/BucketRestrictions.html 
            if (!string.IsNullOrEmpty(s3Bucket_))
            {
                //if (region_.Substring(0, 7) != "us-east")
                {
                    if (s3Bucket_[0] == '.' || s3Bucket_[s3Bucket_.Length - 1] == '.' || s3Bucket_.Contains("..")
                        || s3Bucket_.Length < 3 || s3Bucket_.Length > 63)
                    {
                        DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                            Settings.Default.S4InvalidBucketText, "", "OK", "OK",
                            System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);

                        return;
                    }

                    bool lookLikeIp = true;
                    bool lookDigitOnly = true;
                    foreach (char ch in s3Bucket_)
                    {
                        if (!char.IsDigit(ch) && ch != '.')
                        {
                            lookLikeIp = false;
                        }

                        if (!char.IsDigit(ch))
                        {
                            lookDigitOnly = false;
                        }
                    }



                    if (lookLikeIp && !lookDigitOnly)
                    {
                        DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                            Settings.Default.S4InvalidBucketText, "", "OK", "OK",
                            System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);

                        return;
                    }
                }
                // The migration utility doesn't support non-strict naming available for us-east-1 region
                /* if (region_.Substring(0, 7) == "us-east")
                 {
                     if (s3bucket_.Length > 255)
                     {
                         DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                             Settings.Default.S4InvalidBucketText, "", "OK", "OK",
                             System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
                        
                         //DialogResult result = MessageBox.Show(Settings.Default.S4InvalidBucketText,
                         //    Settings.Default.S4TestConnectionHeader,
                         //MessageBoxButtons.OK);
                         return;
                     }
                 }*/
            }

            if (logger_.IsDebugEnabled)
            {
                logger_.Debug("Bucket enter: " + bucketTextBox.Text);
            }
        }

        private void folderKeyBox_TextChanged(object sender, EventArgs e)
        {
            folderKey_ = folderKeyBox.Text;
            MyVerifyContent();
        }

        private void serverTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            serverType_ = this.serverTypeList_[(string)(sender as ComboBox).SelectedItem];
            MyVerifyContent();
        }

        private void zoneComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            zone_ = zoneComboBox.SelectedItem as string;
            MyVerifyContent();
        }

        private void zoneComboBox_TextChanged(object sender, EventArgs e)
        {
            zone_ = zoneComboBox.Text;
            MyVerifyContent();
        }

        private void groupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            group_ = groupComboBox.SelectedItem as string;
            MyVerifyContent();
        }

        private void groupComboBox_TextChanged(object sender, EventArgs e)
        {
            group_ = groupComboBox.Text;
            MyVerifyContent();
        }

        private void bucketTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLower(e.KeyChar) || Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '-' || e.KeyChar == '\b')
            {
                return;
            }
            else
            {
                e.KeyChar = '\a';
            }
        }

        #endregion Event handlers

        #region Tool tips

        private void MySetToolTip(Control ctrl, string text)
        {
            if (!string.IsNullOrEmpty(ctrl.Text))
            {
                // the control contains text, drop the tool tip
                text = string.Empty;
            }
            toolTip.SetToolTip(ctrl, text);
        }

        private void serverTypeComboBox_MouseEnter(object sender, EventArgs e)
        {
            MySetToolTip(serverTypeComboBox, string.Empty);
        }

        private void serverTypeComboBox_MouseHover(object sender, EventArgs e)
        {
            MySetToolTip(serverTypeComboBox, string.Empty);
        }

        private void groupComboBox_MouseEnter(object sender, EventArgs e)
        {
            MySetToolTip(groupComboBox, Settings.Default.S4AmazonSecurityGroupToolTip);
        }

        private void groupComboBox_MouseHover(object sender, EventArgs e)
        {
            MySetToolTip(groupComboBox, Settings.Default.S4AmazonSecurityGroupToolTip);
        }

        private void zoneComboBox_MouseEnter(object sender, EventArgs e)
        {
            MySetToolTip(zoneComboBox, Settings.Default.S4AmazonAvailabilityZoneToolTip);
        }

        private void zoneComboBox_MouseHover(object sender, EventArgs e)
        {
            MySetToolTip(zoneComboBox, Settings.Default.S4AmazonAvailabilityZoneToolTip);
        }

        private void bucketTextBox_MouseHover(object sender, EventArgs e)
        {
            MySetToolTip(bucketTextBox, Settings.Default.S4AmazonS3BucketToolTip);
        }

        private void folderKeyBox_MouseHover(object sender, EventArgs e)
        {
            MySetToolTip(folderKeyBox, Settings.Default.S4AmazonFolderToolTip);
        }

        #endregion Tool tips

        #region Debug print-out

        private void serverTypeComboBox_Leave(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
            {
                logger_.Debug("ServerType select: " + serverTypeComboBox.Text);
            }
        }

        private void groupComboBox_Leave(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
            {
                logger_.Debug("Group select: " + groupComboBox.Text);
            }
        }

        private void zoneComboBox_Leave(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
            {
                logger_.Debug("Zone select: " + zoneComboBox.Text);
            }
        }

        private void advancedCheckBox_Leave(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
            {
                logger_.Debug("Advanced checked to: " + advancedCheckBox.Checked.ToString());
            }
        }

        private void folderKeyBox_Leave(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
            {
                logger_.Debug("Folder enter: " + folderKeyBox.Text);
            }
        }

        #endregion Debug print-out

        private void vpcComboBox_Leave(object sender, EventArgs e)
        {

        }

        private void vpcComboBox_MouseEnter(object sender, EventArgs e)
        {

        }

        private void vpcComboBox_MouseHover(object sender, EventArgs e)
        {

        }

        private void vpcComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void vpcComboBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
