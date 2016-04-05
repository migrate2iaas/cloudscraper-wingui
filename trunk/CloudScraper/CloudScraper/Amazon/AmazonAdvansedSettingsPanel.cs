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

        #region Exceptions

        public class RegionMismatchException : ApplicationException
        {
        }

        #endregion Exceptions

        #region Constants

        private const string EmptySelection = "None";

        #endregion Constants

        #region Data members

        private static Logger logger_ = LogManager.GetLogger("AmazonAdvancedParametersPanel");

        private readonly SortedDictionary<string, string> serverTypeList_;
        
        private readonly ToolTipContainer toolTipContainer_;
        private readonly ControlDebugPrintoutContainer debugPrintoutContainer_;

        private readonly Dictionary<string, List<string>> vpcIdToSubnetsMap_ = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, string> vpcTitleToVpcIdMap_ = new Dictionary<string, string>();
        
        private string region_ = string.Empty;
        private string s3Bucket_ = string.Empty;  // bucketTextBox.Text
        private string folderKey_ = string.Empty; // folderKeyBox.Text
        private string serverType_ = string.Empty; // the selection in serverTypeComboBox
        private string zone_ = string.Empty; // the selection in zoneComboBox
        private string group_ = string.Empty; // the selection in groupComboBox
        private string subnetId_ = string.Empty; // the selection in subnetComboBox

        #endregion Data members

        #region Constructors

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

        protected AmazonAdvansedSettingsPanel()
        {
            InitializeComponent();

            //Set basic UI strings in Form. 
            this.advancedCheckBox.Text = Settings.Default.S4AmazonAdvancedCheckBoxText;
            this.bucketLabel.Text = Settings.Default.S4AmazonBucketLabelText;
            this.folderKeyLabel.Text = Settings.Default.S4AmazonFolderKeyLabelText;
            this.typeLabel.Text = Settings.Default.S4AmazonTypeLabelText;
            this.zoneLabel.Text = Settings.Default.S4AmazonZoneLabelText;
            this.groupLabel.Text = Settings.Default.S4AmazonGroupLabelText;
            this.tagSelectVpc.Text = Settings.Default.S4AmazonVPCLabelText;

            // Set a tool tip.
            toolTipContainer_ = new ToolTipContainer(toolTip);
            toolTipContainer_.Create(serverTypeComboBox, string.Empty, null);
            toolTipContainer_.Create(groupComboBox, Settings.Default.S4AmazonSecurityGroupToolTip, null);
            toolTipContainer_.Create(zoneComboBox, Settings.Default.S4AmazonAvailabilityZoneToolTip, null);
            toolTipContainer_.Create(bucketTextBox, Settings.Default.S4AmazonS3BucketToolTip, null);
            toolTipContainer_.Create(folderKeyBox, Settings.Default.S4AmazonFolderToolTip, null);
            toolTipContainer_.Create(vpcComboBox, Settings.Default.S4AmazonVPCToolTip, null);

            // Set a debug print-outs for controls.
            debugPrintoutContainer_ = new ControlDebugPrintoutContainer(logger_);
            debugPrintoutContainer_.Create(serverTypeComboBox, "ServerType selected: ");
            debugPrintoutContainer_.Create(groupComboBox, "Group selected: ");
            debugPrintoutContainer_.Create(zoneComboBox, "Zone selected: ");
            debugPrintoutContainer_.Create(advancedCheckBox, "Advanced check box set to: ");
            debugPrintoutContainer_.Create(folderKeyBox, "Folder entered: ");
            debugPrintoutContainer_.Create(vpcComboBox, "VPC selected: ");
            debugPrintoutContainer_.Create(bucketTextBox, "Bucket enter: ");
        }

        #endregion Constructors

        #region Public methods

        public void ChangeRegion(string region)
        {
            region_ = region;
            
            MyClearZones();
            MyClearGroups();
            MyClearS3Bucket();
            MyClearVpc();

            MyVerifyContent();
        }

        public bool IsAdvancedMode()
        {
            return advancedCheckBox.Checked;
        }

        public bool IsContentValid()
        {
            bool groupOk = 0 == groupComboBox.Items.Count || !string.IsNullOrEmpty(group_);
            bool subnetOk = 0 == subnetComboBox.Items.Count || !string.IsNullOrEmpty(subnetId_) || (0 != subnetComboBox.Items.Count && EmptySelection == subnetComboBox.SelectedItem as string);
            bool zoneOk = 0 == zoneComboBox.Items.Count || !string.IsNullOrEmpty(zone_);

            // we do not need to check folder parameter here.
            // TODO: do we need to check serverType_ here as well?
            return !advancedCheckBox.Checked ||
                (advancedCheckBox.Checked && 
                !string.IsNullOrEmpty(s3Bucket_) &&
                zoneOk &&
                subnetOk &&
                groupOk);
        }

        public void VerifyBucket()
        {
            //Check if bucket name is correct.
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

            Amazon.RegionEndpoint region = Amazon.RegionEndpoint.GetBySystemName(region_);
            AmazonEC2Client client = new AmazonEC2Client(id, key, region);
            DescribeRegionsResponse regionResponse = client.DescribeRegions(new DescribeRegionsRequest());

            // Get zones.
            DescribeAvailabilityZonesResponse availabilityZonesResponse = client.DescribeAvailabilityZones(new DescribeAvailabilityZonesRequest());
            MyClearZones();
            if (null != availabilityZonesResponse.AvailabilityZones &&
                0 != availabilityZonesResponse.AvailabilityZones.Count)
            {
                zoneComboBox.TextChanged -= zoneComboBox_TextChanged;
                foreach (AvailabilityZone zone in availabilityZonesResponse.AvailabilityZones)
                {
                    if (zone.State == AvailabilityZoneState.Available)
                    {
                        zoneComboBox.Items.Add(zone.ZoneName);
                    }
                }
                
                zoneComboBox.DropDownStyle = ComboBoxStyle.DropDown;
                if (0 != zoneComboBox.Items.Count)
                {
                    zoneComboBox.SelectedIndex = 0;
                    zoneComboBox.SelectedItem = zoneComboBox.Items[0];
                    zone_ = zoneComboBox.Items[0] as string;
                }
                zoneComboBox.TextChanged += zoneComboBox_TextChanged;
            }

            // Get security groups.
            DescribeSecurityGroupsResponse securityGroupResponse =
                client.DescribeSecurityGroups(new DescribeSecurityGroupsRequest());
            MyClearGroups();
            if (null != securityGroupResponse.SecurityGroups &&
                0 != securityGroupResponse.SecurityGroups.Count)
            {
                groupComboBox.TextChanged -= groupComboBox_TextChanged;
                foreach (SecurityGroup group in securityGroupResponse.SecurityGroups)
                {
                    groupComboBox.Items.Add(group.GroupName);
                }

                groupComboBox.DropDownStyle = ComboBoxStyle.DropDown;
                if (0 != groupComboBox.Items.Count)
                {
                    groupComboBox.SelectedIndex = 0;
                    groupComboBox.SelectedItem = groupComboBox.Items[0];
                    group_ = groupComboBox.Items[0] as string;
                }
                groupComboBox.TextChanged += groupComboBox_TextChanged;
            }

            // Get the VPC and subnet list.
            DescribeVpcsResponse vpcResponse = client.DescribeVpcs(new DescribeVpcsRequest());
            DescribeSubnetsResponse subnetResponse = client.DescribeSubnets(new DescribeSubnetsRequest());

            MyClearVpc();
            
            if (null != vpcResponse && null != vpcResponse.Vpcs 
                && 0 != vpcResponse.Vpcs.Count &&
                null != subnetResponse )
            {
                vpcComboBox.TextChanged -= vpcComboBox_TextChanged;
                subnetComboBox.TextChanged -= subnetComboBox_TextChanged;

                foreach (Subnet subnet in subnetResponse.Subnets)
                {
                    List<string> subnetsOfVpc = null;
                    if (!vpcIdToSubnetsMap_.TryGetValue(subnet.VpcId, out subnetsOfVpc))
                    {
                        subnetsOfVpc = new List<string>(new string[] { EmptySelection });
                        vpcIdToSubnetsMap_.Add(subnet.VpcId, subnetsOfVpc);
                        vpcTitleToVpcIdMap_.Add(subnet.VpcId, subnet.VpcId);
                    }
                    if (!subnetsOfVpc.Contains(subnet.SubnetId))
                    {
                        subnetsOfVpc.Add(subnet.SubnetId);
                    }
                }

                foreach (Vpc vpc in vpcResponse.Vpcs)
                {
                    string vpcTitle = vpc.VpcId;
                    if (null != vpc.Tags && 0 != vpc.Tags.Count)
                    {
                        Amazon.EC2.Model.Tag tag = vpc.Tags.Find(delegate(Amazon.EC2.Model.Tag findMe)
                        {
                            return findMe.Key == "Name";
                        });

                        if (null != tag)
                        {
                            vpcTitle = string.Format("{0} ({1})", vpc.VpcId, tag.Value);
                        }
                    }

                    if (!vpcTitleToVpcIdMap_.ContainsKey(vpc.VpcId))
                    {
                        vpcTitleToVpcIdMap_.Add(vpcTitle, vpc.VpcId);
                        vpcIdToSubnetsMap_.Add(vpc.VpcId, new List<string>());
                    }
                    else
                    {
                        vpcTitleToVpcIdMap_.Remove(vpc.VpcId);
                        vpcTitleToVpcIdMap_.Add(vpcTitle, vpc.VpcId);
                    }
                    vpcComboBox.Items.Add(vpcTitle);
                }

                vpcComboBox.DropDownStyle = ComboBoxStyle.DropDown;
                vpcComboBox.SelectedIndex = 0;
                vpcComboBox.SelectedItem = vpcComboBox.Items[0];

                MySyncVpcWithSubnetsComboBox();

                vpcComboBox.TextChanged += vpcComboBox_TextChanged;
                subnetComboBox.TextChanged += subnetComboBox_TextChanged;
            }
        }

        public void LoadS3UserSettingsFromServer(string id, string key)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(key) || !IsAdvancedMode())
            {
                return;
            }
            Amazon.RegionEndpoint region = Amazon.RegionEndpoint.GetBySystemName(region_);
            // works only in advanced mode
            AmazonS3Client s3Client = new AmazonS3Client(id, key, region);
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
            return new AmazonParams(id, region_, s3Bucket_, folderKey_, serverType_, zone_, group_, subnetId_);
        }

        #endregion Public methods

        #region Private methods

        private void MyClearZones()
        {
            zoneComboBox.TextChanged -= zoneComboBox_TextChanged;
            zoneComboBox.Items.Clear();
            zoneComboBox.Text = string.Empty;
            zoneComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            zone_ = string.Empty;
            zoneComboBox.TextChanged += zoneComboBox_TextChanged;
        }

        private void MyClearGroups()
        {
            groupComboBox.TextChanged -= groupComboBox_TextChanged;
            groupComboBox.Items.Clear();
            groupComboBox.Text = string.Empty;
            groupComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            group_ = string.Empty;
            groupComboBox.TextChanged += groupComboBox_TextChanged;
        }

        private void MyClearS3Bucket()
        {
            bucketTextBox.TextChanged -= bucketTextBox_TextChanged;
            bucketTextBox.Clear();
            bucketTextBox.Text = string.Empty;
            s3Bucket_ = string.Empty;
            bucketTextBox.TextChanged += bucketTextBox_TextChanged;
        }

        private void MyClearVpc()
        {
            vpcComboBox.TextChanged -= vpcComboBox_TextChanged;
            vpcComboBox.Items.Clear();

            vpcIdToSubnetsMap_.Clear();
            vpcTitleToVpcIdMap_.Clear();

            vpcIdToSubnetsMap_.Add(EmptySelection, new List<string>(new string[] {EmptySelection}));
            vpcTitleToVpcIdMap_.Add(EmptySelection, EmptySelection);
            
            // Add a single "none" entry to the drop-down list
            vpcComboBox.Items.Add(EmptySelection);
            vpcComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            vpcComboBox.SelectedIndex = 0;

            MySyncVpcWithSubnetsComboBox();

            vpcComboBox.TextChanged += vpcComboBox_TextChanged;
        }

        private void MyClearSubnet()
        {
            subnetComboBox.TextChanged -= subnetComboBox_TextChanged;
            subnetComboBox.Items.Clear();
            subnetComboBox.Text = string.Empty;
            subnetComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            subnetId_ = string.Empty;
            subnetComboBox.TextChanged += subnetComboBox_TextChanged;
        }

        private void MyVerifyContent()
        {
            if (null != OnContentVerified)
            {
                OnContentVerified(this, new ContentVerificationArgs(IsContentValid()));
            }
        }

        private void MySyncVpcWithSubnetsComboBox()
        {
            string vpcId = null;
            subnetComboBox.Enabled = false;
            
            if (vpcTitleToVpcIdMap_.TryGetValue(vpcComboBox.Text, out vpcId))
            {
                List<string> subnets = null;
                MyClearSubnet();
                
                if (vpcIdToSubnetsMap_.TryGetValue(vpcId, out subnets))
                {
                    subnetComboBox.Enabled = true;
                    foreach (string subnetId in subnets)
                    {
                        subnetComboBox.Items.Add(subnetId);
                    }
                    subnetComboBox.DropDownStyle = ComboBoxStyle.DropDown;
                    subnetComboBox.SelectedIndex = 0;
                    subnetComboBox.SelectedItem = subnetComboBox.Items[0];
                    string subnetStr = (subnetComboBox.Items[0] as string) ?? string.Empty;
                    subnetId_ = EmptySelection == subnetStr ? string.Empty : subnetStr;
                }
            }
            MyVerifyContent();
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

        private void folderKeyBox_TextChanged(object sender, EventArgs e)
        {
            folderKey_ = folderKeyBox.Text;
            MyVerifyContent();
        }

        private void serverTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            serverType_ = string.Empty;
            serverTypeList_.TryGetValue(serverTypeComboBox.Text, out serverType_);
            MyVerifyContent();
        }

        private void zoneComboBox_TextChanged(object sender, EventArgs e)
        {
            zone_ = zoneComboBox.Text;
            MyVerifyContent();
        }

        private void groupComboBox_TextChanged(object sender, EventArgs e)
        {
            group_ = groupComboBox.Text;
            MyVerifyContent();
        }

        private void vpcComboBox_TextChanged(object sender, EventArgs e)
        {
            MySyncVpcWithSubnetsComboBox();
        }

        private void subnetComboBox_TextChanged(object sender, EventArgs e)
        {
            subnetId_ = EmptySelection == subnetComboBox.Text ? string.Empty : subnetComboBox.Text;
            MyVerifyContent();
        }

        #endregion Event handlers
    }
}
