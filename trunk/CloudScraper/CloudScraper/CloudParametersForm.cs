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
                this.regionList_.Add(str.Split(new char[] { Settings.Default.Separator }, 2)[0],
                    str.Split(new char[] { Settings.Default.Separator }, 2)[1]);
            }

            foreach (string str in Settings.Default.ServerTypes)
            {
                this.serverTypeList_.Add(str.Split(new char[] { Settings.Default.Separator }, 2)[0],
                    str.Split(new char[] { Settings.Default.Separator }, 2)[1]);
            }

            InitializeComponent();

            foreach (KeyValuePair<string, string> region in this.regionList_)
            {
                this.regionComboBox.Items.Add(region.Value);
                if (region.Key == "us-east-1")
                {
                    this.regionComboBox.SelectedItem = region.Value;
                }
            }

            foreach (KeyValuePair<string, string> type in this.serverTypeList_)
            {
                this.serverTypeComboBox.Items.Add(type.Value);
            }

            //this.regionComboBox.SelectedIndex = 0;
            //this.serverTypeComboBox.SelectedIndex = 0;
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

        private void On_closed(object sender, FormClosedEventArgs e)
        {
            this.chooseCloudForm_.Close();
        }

    }
}
