using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CloudScraper
{
    public partial class CloudParametersForm : Form
    {
        ChooseCloudForm chooseCloudForm_;
        ImagesPathForm imagesPathForm_;

        SortedDictionary<string, string> regionList_;
        SortedDictionary<string, string> serverTypeList_;

        public CloudParametersForm(ChooseCloudForm chooseCloudForm)
        {
            this.chooseCloudForm_ = chooseCloudForm;

            this.regionList_ = new SortedDictionary<string, string>();
            this.regionList_.Add("us-east-1", "US East (Northern Virginia)");
            this.regionList_.Add("us-west-2", "US West (Oregon)");
            this.regionList_.Add("us-west-1", "US West (Northern California)");
            this.regionList_.Add("eu-west-1", "EU (Ireland)");
            this.regionList_.Add("ap-southeast-1", "Asia Pacific (Singapore)");
            this.regionList_.Add("ap-southeast-2", "Asia Pacific (Sydney)");
            this.regionList_.Add("ap-northeast-1", "Asia Pacific (Tokyo)");
            this.regionList_.Add("sa-east-1", "South America (Sao Paulo)");

            this.serverTypeList_ = new SortedDictionary<string, string>();
            this.serverTypeList_.Add("m1.medium", "M1 Medium");
            this.serverTypeList_.Add("m1.small", "M1 Small");
            this.serverTypeList_.Add("m1.large", "M1 Large");
            this.serverTypeList_.Add("m1.xlarge", "M1 Extra Large");
            this.serverTypeList_.Add("m3.2xlarge", "M3 Double Extra Large");
            this.serverTypeList_.Add("m3.xlarge", "M3 Extra Large");

            InitializeComponent();

            foreach (KeyValuePair<string, string> region in this.regionList_)
            {
                this.regionComboBox.Items.Add(region.Value);
            }

            foreach (KeyValuePair<string, string> type in this.serverTypeList_)
            {
                this.serverTypeComboBox.Items.Add(type.Value);
            }

            this.regionComboBox.SelectedIndex = 0;
            this.serverTypeComboBox.SelectedIndex = 0;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.chooseCloudForm_.Show();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (this.imagesPathForm_ == null)
            {
                this.imagesPathForm_ = new ImagesPathForm(this);
            }

            imagesPathForm_.ShowDialog();
        }
    }
}
