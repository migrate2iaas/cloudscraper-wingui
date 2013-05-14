using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using CloudScraper.Properties;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.EC2;
using Amazon.EC2.Model;
using System.Text.RegularExpressions;

namespace CloudScraper
{
    public partial class EHCloudParametersForm : Form
    {
        public static string uuid_ = "";
        public static string apiKey_ = "";
        public static string region_;
        public static bool directUpload_ = false;
        public static bool isElasticHosts_ = false;
        
        ChooseCloudForm chooseCloudForm_;
        ImagesPathForm imagesPathForm_;
        SaveTransferTaskForm saveTransferTaskForm_;

        SortedDictionary<string, string> regionList_;
        SortedDictionary<string, string> serverTypeList_;

        public EHCloudParametersForm(ChooseCloudForm chooseCloudForm)
        {
            this.chooseCloudForm_ = chooseCloudForm;
            this.regionList_ = new SortedDictionary<string, string>();
            this.serverTypeList_ = new SortedDictionary<string, string>();

            InitializeComponent();

            //Move regions strings from settings file to regionComboBox.
            foreach (string str in Settings.Default.EHRegions)
            {
                string key = str.Split(new char[] { Settings.Default.Separator }, 2)[1];
                string value = str.Split(new char[] { Settings.Default.Separator }, 2)[0];
                this.regionList_.Add(key, value);

                this.regionComboBox.Items.Add(key);
                if (value == "sat-p")
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

            }

            //Set basic UI strings in Form. 
            this.helpButton.Image = new Bitmap(System.Drawing.Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.toolTip.SetToolTip(this.helpButton, Settings.Default.HelpButtonToolTip);
            this.nextButton.Enabled = false;
            this.Text = Settings.Default.S4Header;
            this.backButton.Text = Settings.Default.S4BackButtonText;
            this.toolTip.SetToolTip(this.backButton, Settings.Default.S4BackButtonToolTip);
            this.nextButton.Text = Settings.Default.S4NextButtonText;
            this.toolTip.SetToolTip(this.nextButton, Settings.Default.S4NextButtonToolTip);
            this.testButton.Text = Settings.Default.S4TestButtonText;
            this.toolTip.SetToolTip(this.testButton, Settings.Default.S4TestButtonToolTip);
            //this.regionLabel.Text = Settings.Default.S4RegionLabelText;
            //this.awsIdLabel.Text = Settings.Default.S4awsIdLabelText;
            //this.awsKeyLabel.Text = Settings.Default.S4awsKeyLabelText;
            //this.advancedCheckBox.Text = Settings.Default.S4AdvancedCheckBoxText;
            
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            this.Hide();
            this.chooseCloudForm_.StartPosition = FormStartPosition.Manual;
            this.chooseCloudForm_.Location = this.Location;
            this.chooseCloudForm_.Show();
        }

        private void NextButtonClick(object sender, EventArgs e)
        {            
            this.Hide();
            isElasticHosts_ = true;

            if (!directUpload_)
            {
                if (this.imagesPathForm_ == null)
                {
                    this.imagesPathForm_ = new ImagesPathForm(this);
                }

                imagesPathForm_.ShowDialog();
            }
            else
            {
                if (this.saveTransferTaskForm_ == null)
                {
                    this.saveTransferTaskForm_ = new SaveTransferTaskForm(this);
                }

                saveTransferTaskForm_.ShowDialog();
            }
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            this.chooseCloudForm_.Close();
        }

        private void UUIDChanged(object sender, EventArgs e)
        {
            uuid_ = (sender as TextBox).Text;
            this.CheckEnter();
        }

        private void ApiKeyChanged(object sender, EventArgs e)
        {
            apiKey_ = (sender as TextBox).Text;
            this.CheckEnter();
        }

        private void AdvancedChecked(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                directUpload_ = true;
                this.CheckEnter();
            }
            else
            {
                directUpload_ = false;
                this.CheckEnter();
            }
        }

        //Check enter in Form for activate Next button.
        private void CheckEnter()
        {
            Guid guid = StringToGuid(uuid_);

            if (guid != Guid.Empty
                && apiKey_ != "" && apiKey_.Length == 40)
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

        
        private static Regex reGuid = new Regex(@"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$",  
            RegexOptions.Compiled);  
  
        public static Guid StringToGuid(string id)  
        {  
            if (id == null || id.Length != 36 ) return Guid.Empty;  
            if (reGuid.IsMatch(id))  
                return new Guid(id);  
            else  
                return Guid.Empty;  
        }  
        
        //Test connection.
        private void TestButtonClick(object sender, EventArgs e)
        {            
            try
            {
                this.testButton.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                //If there are no keys entered.
                if (uuid_ == "" || apiKey_ == "")
                {
                    DialogResult result = MessageBox.Show(Settings.Default.S4EnterUUID, Settings.Default.S4TestConnectionHeader,
                    MessageBoxButtons.OK);
                    this.testButton.Enabled = true;
                    this.Cursor = Cursors.Arrow;
                    return;
                }

                string credentials = uuid_ + ":" + apiKey_;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://api-" + region_ + ".elastichosts.com/servers/list");
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));
                //request.PreAuthenticate = true;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

   
                DialogResult reslt = MessageBox.Show(Settings.Default.S4TestConnectionText, 
                    Settings.Default.S4TestConnectionHeader,
                        MessageBoxButtons.OK);
                this.testButton.Enabled = true;
                this.Cursor = Cursors.Arrow;
                return;
              

            }
            catch (WebException ex)
            {
                //Show dialog  when auth failed.
                DialogResult result = MessageBox.Show(ex.Status + "\n" + 
                    Settings.Default.S4IDKeyInvalid, Settings.Default.S4TestConnectionHeader,
                    MessageBoxButtons.OK);
                this.testButton.Enabled = true;
                this.Cursor = Cursors.Arrow;
            }
        }

        private void EHCloudParametersLoad(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = this.chooseCloudForm_.Location;
            isElasticHosts_ = false;
        }

        private void RegionChanged(object sender, EventArgs e)
        {
            region_ = this.regionList_[(string)(sender as ComboBox).SelectedItem];
        }

    }
}
