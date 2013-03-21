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
        public static string awsId_ = "";
        public static string awsKey_ = "";
        public static string region_;
        public static bool advanced_ = false;
        public static string s3bucket_ = "";
        public static string folderKey_ = "";
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

            InitializeComponent();

            //Move regions strings from settings file to regionComboBox.
            foreach (string str in Settings.Default.Regions)
            {
                string key = str.Split(new char[] { Settings.Default.Separator }, 2)[1];
                string value = str.Split(new char[] { Settings.Default.Separator }, 2)[0];
                this.regionList_.Add(key, value);

                this.regionComboBox.Items.Add(key);
                if (value == "us-east-1")
                {
                    this.regionComboBox.SelectedItem = key;
                }
            }

            //Move server types strings from settings file to serverTypeComboBox.
            foreach (string str in Settings.Default.ServerTypes)
            {
                string key = str.Split(new char[] { Settings.Default.Separator }, 2)[1];
                string value = str.Split(new char[] { Settings.Default.Separator }, 2)[0];
                this.serverTypeList_.Add(key, value);
                this.serverTypeComboBox.Items.Add(key);
            }

            //Set basic UI strings in Form. 
            this.helpButton.Image = new Bitmap(System.Drawing.Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.serverTypeComboBox.SelectedIndex = 0;
            this.nextButton.Enabled = false;
            this.Text = Settings.Default.S4Header;
            this.backButton.Text = Settings.Default.S4BackButtonText;
            this.nextButton.Text = Settings.Default.S4NextButtonText;
            this.testButton.Text = Settings.Default.S4TestButtonText;
            this.regionLabel.Text = Settings.Default.S4RegionLabelText;
            this.awsIdLabel.Text = Settings.Default.S4awsIdLabelText;
            this.awsKeyLabel.Text = Settings.Default.S4awsKeyLabelText;
            this.advancedCheckBox.Text = Settings.Default.S4AdvancedCheckBoxText;
            this.bucketLabel.Text = Settings.Default.S4BucketLabelText;
            this.folderKeyLabel.Text = Settings.Default.S4FolderKeyLabelText;
            this.typeLabel.Text = Settings.Default.S4TypeLabelText;
            this.zoneLabel.Text = Settings.Default.S4ZoneLabelText;
            this.groupLabel.Text = Settings.Default.S4GroupLabelText;
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            this.Hide();
            this.chooseCloudForm_.Show();
        }

        private void NextButtonClick(object sender, EventArgs e)
        {
            //Check bucket name is correct.
            if (s3bucket_ != "")
            {
                if (region_.Substring(0, 7) != "us-east")
                {
                    if (s3bucket_[0] == '.' || s3bucket_[s3bucket_.Length - 1] == '.' || s3bucket_.Contains("..")
                        || s3bucket_.Length < 3 || s3bucket_.Length > 63)
                    {
                        DialogResult result = MessageBox.Show(Settings.Default.S4InvalidBucketText,
                            Settings.Default.S4TestConnectionHeader,
                        MessageBoxButtons.OK);
                        return;
                    }

                    bool lookLikeIp = true;
                    foreach (char ch in s3bucket_)
                    {
                        if (!char.IsDigit(ch) && ch != '.')
                        {
                            lookLikeIp = false;
                        }
                    }

                    if (lookLikeIp)
                    {
                        DialogResult result = MessageBox.Show(Settings.Default.S4InvalidBucketText,
                            Settings.Default.S4TestConnectionHeader,
                        MessageBoxButtons.OK);
                        return;
                    }
                }
                if (region_.Substring(0, 7) == "us-east")
                {
                    if (s3bucket_.Length > 255)
                    {
                        DialogResult result = MessageBox.Show(Settings.Default.S4InvalidBucketText,
                            Settings.Default.S4TestConnectionHeader,
                        MessageBoxButtons.OK);
                        return;
                    }
                }
            }
            
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

        private void AmazonRegionChanged(object sender, EventArgs e)
        {
            region_ = this.regionList_[(string)(sender as ComboBox).SelectedItem];
            this.zoneComboBox.Items.Clear();
            this.zoneComboBox.Text = "";
            this.zoneComboBox.DropDownStyle = ComboBoxStyle.Simple;
            zone_ = "";
            this.groupComboBox.Items.Clear();
            this.groupComboBox.Text = "";
            this.groupComboBox.DropDownStyle = ComboBoxStyle.Simple;
            group_ = "";
            this.bucketTextBox.Text = "";
        }

        private void AdvancedChecked(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                advanced_ = true;
                this.bucketTextBox.Enabled = true;
                this.bucketLabel.Enabled = true;
                this.folderKeyBox.Enabled = true;
                this.folderKeyLabel.Enabled = true;
                this.serverTypeComboBox.Enabled = true;
                this.typeLabel.Enabled = true;
                this.zoneComboBox.Enabled = true;
                this.groupComboBox.Enabled = true;
                this.zoneLabel.Enabled = true;
                this.groupLabel.Enabled = true;
                this.CheckEnter();
            }
            else
            {
                advanced_ = false;
                s3bucket_ = "";
                bucketTextBox.Text = "";
                this.bucketTextBox.Enabled = false;
                this.bucketLabel.Enabled = false;
                this.folderKeyBox.Enabled = false;
                this.folderKeyLabel.Enabled = false;
                this.serverTypeComboBox.Enabled = false;
                this.typeLabel.Enabled = false;
                this.zoneComboBox.Enabled = false;
                this.groupComboBox.Enabled = false;
                this.zoneLabel.Enabled = false;
                this.groupLabel.Enabled = false;
                this.CheckEnter();
            }
        }

        private void BucketChanged(object sender, EventArgs e)
        {
            //Check correct enter for bucket name.
            if (bucketTextBox.Text != "")
            {
                if (region_.Substring(0, 7) != "us-east" && bucketTextBox.Text[bucketTextBox.Text.Length - 1] != '.'
                    && bucketTextBox.Text[bucketTextBox.Text.Length - 1] != '-'
                    && !char.IsLower(bucketTextBox.Text[bucketTextBox.Text.Length - 1])
                    && !char.IsNumber(bucketTextBox.Text[bucketTextBox.Text.Length - 1]))
                {
                    string str = bucketTextBox.Text.Remove(bucketTextBox.Text.Length - 1);
                    bucketTextBox.TextChanged -= new System.EventHandler(this.BucketChanged);
                    bucketTextBox.Text = "";
                    bucketTextBox.AppendText(str);
                    bucketTextBox.TextChanged += new System.EventHandler(this.BucketChanged);

                }
                if (region_.Substring(0, 7) == "us-east" && bucketTextBox.Text[bucketTextBox.Text.Length - 1] != '.'
                    && bucketTextBox.Text[bucketTextBox.Text.Length - 1] != '-'
                    && bucketTextBox.Text[bucketTextBox.Text.Length - 1] != '_'
                    && !char.IsLower(bucketTextBox.Text[bucketTextBox.Text.Length - 1])
                    && !char.IsUpper(bucketTextBox.Text[bucketTextBox.Text.Length - 1])
                    && !char.IsNumber(bucketTextBox.Text[bucketTextBox.Text.Length - 1]))
                {
                    string str = bucketTextBox.Text.Remove(bucketTextBox.Text.Length - 1);
                    bucketTextBox.TextChanged -= new System.EventHandler(this.BucketChanged);
                    bucketTextBox.Text = "";
                    bucketTextBox.AppendText(str);
                    bucketTextBox.TextChanged += new System.EventHandler(this.BucketChanged);
                }
            }

            s3bucket_ = (sender as TextBox).Text;
            this.CheckEnter();
        }

        private void FolderKeyChanged(object sender, EventArgs e)
        {            
            folderKey_ = (sender as TextBox).Text;
            this.CheckEnter();
        }

        private void ServerTypeChanged(object sender, EventArgs e)
        {
            type_ = this.serverTypeList_[(string)(sender as ComboBox).SelectedItem];
        }

        //Check enter in Form for activate Next button.
        private void CheckEnter()
        {
            if (!advanced_ && awsId_ != "" && awsKey_ != "")
            {
                this.nextButton.Enabled = true;
            }
            else if (advanced_ && awsId_ != "" && awsKey_ != ""
                && s3bucket_ != "" && zone_ != "" && group_ != "" && folderKey_ != "")
            {
                this.nextButton.Enabled = true;
            }
            else
            {
                this.nextButton.Enabled = false;
            }
        }

        private void HelpButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Settings.Default.S4Link);
        }

        //Test connection.
        private void TestButtonClick(object sender, EventArgs e)
        {
            try
            {
                this.testButton.Enabled = false;

                //If there are no keys entered.
                if (awsId_ == "" || awsKey_ == "")
                {
                    DialogResult result = MessageBox.Show(Settings.Default.S4EnterAWS, Settings.Default.S4TestConnectionHeader,
                    MessageBoxButtons.OK);
                    this.testButton.Enabled = true;
                    return;
                }

                AmazonEC2Config config = new AmazonEC2Config();
                config.ServiceURL = "https://ec2." + region_+ ".amazonaws.com";
                AmazonEC2 client = new AmazonEC2Client(awsId_, awsKey_, config);
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
                //group_ = (string)groupComboBox.SelectedItem;

                //If not advanced mode, show done message and return.
                if (!advanced_)
                {     
                    DialogResult result = MessageBox.Show(Settings.Default.S4TestConnectionText, 
                        Settings.Default.S4TestConnectionHeader,
                         MessageBoxButtons.OK);
                    this.testButton.Enabled = true;
                    return;
                }

                //If advanced mode, continue. 
                AmazonS3 client2 = Amazon.AWSClientFactory.CreateAmazonS3Client(
                    awsId_, awsKey_);

                if (s3bucket_ != "")
                {
                    try
                    {
                        GetBucketLocationRequest req = new GetBucketLocationRequest();
                        req.BucketName = s3bucket_;
                        GetBucketLocationResponse bucketResponse = client2.GetBucketLocation(req);
                        if (bucketResponse.Location != region_)
                        {
                            DialogResult result = MessageBox.Show(Settings.Default.S4BucketLocated, 
                                Settings.Default.S4TestConnectionHeader,
                                MessageBoxButtons.OK);
                            this.testButton.Enabled = true;
                            return;
                        }
                    }
                    catch (AmazonS3Exception ex)
                    {
                        if (ex.ErrorCode == "NoSuchBucket")
                        {
                            //If no such bucket.
                            DialogResult result = MessageBox.Show(Settings.Default.S4NoBucketExists, 
                                Settings.Default.S4TestConnectionHeader,
                                MessageBoxButtons.OK);
                        }
                        else
                        {
                            //If bucket exist but locked.
                            DialogResult result = MessageBox.Show(Settings.Default.S4CannotAccessBucketText, 
                                Settings.Default.S4TestConnectionHeader,
                            MessageBoxButtons.OK);
                            this.testButton.Enabled = true;
                            return;
                        }
                    }
                }

                //Show done message in advanced mode.
                DialogResult result2 = MessageBox.Show(Settings.Default.S4TestConnectionText, 
                    Settings.Default.S4TestConnectionHeader,
                    MessageBoxButtons.OK);
                this.testButton.Enabled = true;
                return;

            }
            catch (AmazonEC2Exception amazonEC2Exception)
            {
                //Show dialog  when auth failed.
                DialogResult result = MessageBox.Show(amazonEC2Exception.ErrorCode + "\n" + 
                    Settings.Default.S4IDKeyInvalid, Settings.Default.S4TestConnectionHeader,
                    MessageBoxButtons.OK);
            }
        }

        private void ZoneComboBoxIndexChanged(object sender, EventArgs e)
        {
            zone_ = (string)(sender as ComboBox).SelectedItem;
        }

        private void GroupComboBoxIndexChanged(object sender, EventArgs e)
        {
            group_ = (string)(sender as ComboBox).SelectedItem;
        }

        private void GroupComboBoxTextChanged(object sender, EventArgs e)
        {
            group_ = (sender as ComboBox).Text;
            this.CheckEnter();
        }

        private void ZoneComboBoxTextChanged(object sender, EventArgs e)
        {
            zone_ = (sender as ComboBox).Text;
            this.CheckEnter();
        }

        //Check bucket when lost focus.
        private void BucketTextBoxLeave(object sender, EventArgs e)
        {
            if (s3bucket_ != "")
            {
                if (region_.Substring(0, 7) != "us-east")
                {
                    if (s3bucket_[0] == '.' || s3bucket_[s3bucket_.Length - 1] == '.' || s3bucket_.Contains("..")
                        || s3bucket_.Length < 3 || s3bucket_.Length > 63)
                    {
                        DialogResult result = MessageBox.Show(Settings.Default.S4InvalidBucketText, 
                            Settings.Default.S4TestConnectionHeader,
                        MessageBoxButtons.OK);
                        return;
                    }

                    bool lookLikeIp = true;
                    foreach (char ch in s3bucket_)
                    {
                        if (!char.IsDigit(ch) && ch != '.')
                        {
                            lookLikeIp = false;
                        }
                    }

                    if (lookLikeIp)
                    {
                        DialogResult result = MessageBox.Show(Settings.Default.S4InvalidBucketText, 
                            Settings.Default.S4TestConnectionHeader,
                        MessageBoxButtons.OK);
                        return;
                    }
                }
                if (region_.Substring(0, 7) == "us-east")
                {
                    if (s3bucket_.Length > 255)
                    {
                        DialogResult result = MessageBox.Show(Settings.Default.S4InvalidBucketText, 
                            Settings.Default.S4TestConnectionHeader,
                        MessageBoxButtons.OK);
                        return;
                    }
                }
            }
        }
    }
}
