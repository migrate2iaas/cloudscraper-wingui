using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using CloudScraper.Properties;
using System.Drawing;

namespace CloudScraper
{
    public class EHCloudParameters : CloudParametersForm
    {

        public static string uuid_ = "";
        public static string apiKey_ = "";
        public static string region_;
        public static bool directUpload_ = false;
        public static bool isElasticHosts_ = false;

        public SaveTransferTaskForm saveTransferTaskForm_;
        
        public EHCloudParameters(ChooseCloudForm chooseCloudForm)
        {
            isElasticHosts_ = false;

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
            this.Text = Settings.Default.S4ehHeader;
            this.backButton.Text = Settings.Default.S4BackButtonText;
            this.toolTip.SetToolTip(this.backButton, Settings.Default.S4BackButtonToolTip);
            this.nextButton.Text = Settings.Default.S4NextButtonText;
            this.toolTip.SetToolTip(this.nextButton, Settings.Default.S4NextButtonToolTip);
            this.testButton.Text = Settings.Default.S4TestButtonText;
            this.toolTip.SetToolTip(this.testButton, Settings.Default.S4TestButtonToolTip);

            this.idTextBox.MaxLength = 40;

            this.regionLabel.Text = Settings.Default.S4ehRegionLabelText;
            this.idLabel.Text = Settings.Default.S4ehIdLabelText;
            this.keyLabel.Text = Settings.Default.S4ehKeyLabelText;
            this.advancedCheckBox.Text = Settings.Default.S4ehDirectUploadCheckBoxText;
            
            this.bucketLabel.Visible = false;
            this.folderKeyLabel.Visible = false;
            this.typeLabel.Visible = false;
            this.zoneLabel.Visible = false;
            this.groupLabel.Visible = false;
            this.bucketTextBox.Visible = false;
            this.folderKeyBox.Visible = false;
            this.serverTypeComboBox.Visible = false;
            this.zoneComboBox.Visible = false;
            this.groupComboBox.Visible = false;
            
            
            this.SetChooseCloudForm(chooseCloudForm);
        }

        public override void RegionListBoxChanged(object sender, EventArgs e)
        {
            region_ = this.regionList_[(string)(sender as ComboBox).SelectedItem];
        }

        public override void IDChanged(object sender, EventArgs e)
        {
            uuid_ = (sender as TextBox).Text;
            this.CheckEnter();
        }

        public override void KeyChanged(object sender, EventArgs e)
        {
            apiKey_ = (sender as TextBox).Text;
            this.CheckEnter();
        }


        public override void AdvancedChecked(object sender, EventArgs e)
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

        public override void BackButtonClick(object sender, EventArgs e)
        {
            isElasticHosts_ = false;
            base.BackButtonClick(sender, e);
        }

        public override void NextButtonClick(object sender, EventArgs e)
        {
            isElasticHosts_ = true;
            this.Hide();

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

        public override void HelpButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Settings.Default.S4Link);
        }

        public override void TestButtonClick(object sender, EventArgs e)
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

        public override void CloudParametersLoad(object sender, EventArgs e)
        {
            isElasticHosts_ = false;
            base.CloudParametersLoad(sender, e);
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

        private static Regex reGuid = new Regex(@"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$",
            RegexOptions.Compiled);

        public static Guid StringToGuid(string id)
        {
            if (id == null || id.Length != 36) return Guid.Empty;
            if (reGuid.IsMatch(id))
                return new Guid(id);
            else
                return Guid.Empty;
        }  
    }
}
