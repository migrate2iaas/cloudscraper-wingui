using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;
using CloudScraper.Properties;
using System.Drawing;
using System.IO;
using NLog;
using DotNetPerls;

namespace CloudScraper
{
    public class EHCloudParameters : CloudParametersForm
    {
        public static string uuid_ = "";
        public static string apiKey_ = "";
        public static string region_;
        public static bool directUpload_ = false;
        public static bool isElasticHosts_ = false;
        public static bool useDeduplication_ = false;
        public static List<string> drivesList_ = new List<string>();
        private static Logger logger_ = LogManager.GetLogger("EHCloudParametersForm");

        public SaveTransferTaskForm saveTransferTaskForm_;

        BindingList<DrivesInfo> drives_;
        
        public EHCloudParameters(ChooseCloudForm chooseCloudForm)
        {
            isElasticHosts_ = false;
            this.drives_ = new BindingList<DrivesInfo>();  

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

            this.toolTip.SetToolTip(this.advancedCheckBox, Settings.Default.S4EHDirectUploadCheckBoxToolTip);
            this.toolTip.SetToolTip(this.deduplcationCheckBox, Settings.Default.S4EHDeduplicationCheckBoxToolTip);
            this.toolTip.SetToolTip(this.drivesDataGridView, Settings.Default.S4EHDrivesListBoxToolTip);
            
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
            this.azureContainerComboBox.Visible = false;
            this.azureDeployVirtualMachineCheckBox.Visible = false;
            this.azureSubscriptionId.Visible = false;
            this.azureCreateNewCertificateButton.Visible = false;
        
            this.SetChooseCloudForm(chooseCloudForm);
        }

        protected override void RegionListBoxChanged(object sender, EventArgs e)
        {
            region_ = this.regionList_[(string)(sender as ComboBox).SelectedItem];
        }

        protected override void IDChanged(object sender, EventArgs e)
        {
            uuid_ = (sender as TextBox).Text;
            this.CheckEnter();
        }

        protected override void KeyChanged(object sender, EventArgs e)
        {
            apiKey_ = (sender as TextBox).Text;
            this.CheckEnter();
        }


        protected override void AdvancedChecked(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                directUpload_ = true;
                //this.CheckEnter();
            }
            else
            {
                directUpload_ = false;
                //this.CheckEnter();
            }
        }


        protected override void UseDeduplicationChecked(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                useDeduplication_ = true;
                //this.CheckEnter();
            }
            else
            {
                useDeduplication_ = false;
                //this.CheckEnter();
            }
        }

        protected override void BackButtonClick(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Return to the ChooseCloudForm.");
            
            isElasticHosts_ = false;
            base.BackButtonClick(sender, e);
        }

        protected override void NextButtonClick(object sender, EventArgs e)
        {            
            isElasticHosts_ = true;
            this.Hide();

            if (!directUpload_)
            {
                if (logger_.IsDebugEnabled)
                    logger_.Debug("Next to the ImagesPathForm.");

                if (this.imagesPathForm_ == null)
                {
                    this.imagesPathForm_ = new ImagesPathForm(this);
                }

                imagesPathForm_.ShowDialog();
            }
            else
            {
                if (logger_.IsDebugEnabled)
                    logger_.Debug("Next to the SaveTransferTaskForm.");
                
                if (this.saveTransferTaskForm_ == null)
                {
                    this.saveTransferTaskForm_ = new SaveTransferTaskForm(this);
                }

                saveTransferTaskForm_.ShowDialog();
            }
        }

        protected override void HelpButtonClick(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Help button click.");

            System.Diagnostics.Process.Start(Settings.Default.S4EHLink);
        }

        protected override void TestButtonClick(object sender, EventArgs e)
        {
            try
            {
                this.testButton.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                //If there are no keys entered.
                if (uuid_ == "" || apiKey_ == "")
                {
                    DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                        Settings.Default.S4EnterUUID, "", "OK", "OK",
                        System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
                    
                    this.testButton.Enabled = true;
                    this.Cursor = Cursors.Arrow;
                    return;
                }

                //! Move checking code to seaprate function e.g. CheckCredentials()
                string credentials = uuid_ + ":" + apiKey_;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://api-" + region_ + ".elastichosts.com/drives/info"); 
                    //".elastichosts.com/servers/list");
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));
                //request.PreAuthenticate = true;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                this.drives_.Clear();

                string key = null;
                string value = null;
                using (StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1251)))
                {
                    while (!myStreamReader.EndOfStream)
                    {
                        string str = myStreamReader.ReadLine();
                        if (str.Contains("drive"))
                            key = str.Remove(0, 6);
                        if (str.Contains("name"))
                            value = str.Remove(0,5);

                        if (key != null && value != null)
                        {
                            //Create DriveInfo object.
                            DrivesInfo drive = new DrivesInfo()
                            {
                                IsChecked = false,
                                Name = value,
                                UUID = key
                            };

                            this.drives_.Add(drive);

                            key = null;
                            value = null;
                        }
                    }
                    
                    //drivesDataGridView.AutoResizeColumn(0);
                }

                DialogResult reslt = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                    Settings.Default.S4EHTestConnectionText, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);

                this.testButton.Enabled = true;
                this.Cursor = Cursors.Arrow;
                return;


            }
            catch (WebException ex)
            {
                //Show dialog  when auth failed.

                DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4TestConnectionHeader,
                    ex.Status + "\n" +
                    Settings.Default.S4EHIDKeyInvalid, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
                
                this.testButton.Enabled = true;
                this.Cursor = Cursors.Arrow;
            }
        }

        protected override void CloudParametersLoad(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Form loaded.");

            //DataGrid change columns width.
            drivesDataGridView.DataSource = this.drives_;
            drivesDataGridView.Columns[0].Width = 30;
            drivesDataGridView.Columns[1].Width = 240;
            drivesDataGridView.Columns[2].Width = 240;

            drivesDataGridView.Columns[1].DefaultCellStyle.Font = 
                new Font(drivesDataGridView.Font.Name, 8, FontStyle.Bold);
           

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

        protected override void OnLeaveEnter(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
            {
                if (sender is ComboBox)
                {
                    if ((sender as ComboBox) == regionComboBox)
                        logger_.Debug("Region select: " + (sender as ComboBox).Text);
                }
                if (sender is TextBox)
                {
                    //if ((sender as TextBox) == keyTextBox)
                    //    logger_.Debug("Key enter: " + (sender as TextBox).Text);
                    if ((sender as TextBox) == idTextBox)
                        logger_.Debug("Id enter: " + (sender as TextBox).Text);
                }
                if (sender is CheckBox)
                {
                    if ((sender as CheckBox) == advancedCheckBox)
                        logger_.Debug("Direct upload checked to: " + (sender as CheckBox).Checked.ToString());
                    if ((sender as CheckBox) == deduplcationCheckBox)
                        logger_.Debug("Use deduplcation checked to: " + (sender as CheckBox).Checked.ToString());
                    if ((sender as CheckBox) == selectAllCheckBox)
                        logger_.Debug("Select all checked to: " + (sender as CheckBox).Checked.ToString());
                }
            }
        }

        protected override void TextBoxMouseEnter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                if ((sender as TextBox).Text == "")
                {
                    if ((sender as TextBox) == keyTextBox)
                        this.toolTip.SetToolTip((sender as TextBox), Settings.Default.S4EHApiKeyToolTip);
                    if ((sender as TextBox) == idTextBox)
                        this.toolTip.SetToolTip((sender as TextBox), Settings.Default.S4EHUserUIDToolTip);
                }
                else
                {
                    this.toolTip.SetToolTip((sender as TextBox), "");
                }
            }

            if ((sender is ComboBox))
            {
                if ((sender as ComboBox).Text == "")
                {
                    this.toolTip.SetToolTip((sender as ComboBox), "Test");
                }
                else
                {
                    this.toolTip.SetToolTip((sender as ComboBox), "");
                }
            }
        }

        protected override void DrivesSelect(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0 && e.CurrentValue == CheckState.Unchecked && e.NewValue == CheckState.Checked)
            {
                (sender as CheckedListBox).ItemCheck -= DrivesSelect; 
                for (int i = 1; i < (sender as CheckedListBox).Items.Count; i++)
                    (sender as CheckedListBox).SetItemCheckState(i, CheckState.Checked);
                (sender as CheckedListBox).ItemCheck += DrivesSelect;
            }
            if (e.Index == 0 && e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
            {
                (sender as CheckedListBox).ItemCheck -= DrivesSelect;
                for (int i = 1; i < (sender as CheckedListBox).Items.Count; i++)
                    (sender as CheckedListBox).SetItemCheckState(i, CheckState.Unchecked);
                (sender as CheckedListBox).ItemCheck += DrivesSelect;
            }

            drivesList_.Clear();
           
            foreach (var pair in (sender as CheckedListBox).CheckedItems)
            {
                if (pair is KeyValuePair<string, string>)
                    drivesList_.Add(((KeyValuePair<string, string>)pair).Key);
            }

            if (e.NewValue == CheckState.Checked && 
                !(sender as CheckedListBox).CheckedItems.Contains((sender as CheckedListBox).SelectedItem)
                && ((sender as CheckedListBox).SelectedItem is KeyValuePair<string, string>))
            {
                drivesList_.Add(((KeyValuePair<string, string>)((sender as CheckedListBox).SelectedItem)).Key);
            }

        }

        protected override void OnSelect(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sender is DataGridView && e.ColumnIndex == 0)
            {
                this.drives_[e.RowIndex].IsChecked = !this.drives_[e.RowIndex].IsChecked;
            }

            drivesList_.Clear();

            foreach (DrivesInfo drive in this.drives_)
            {
                if (drive.IsChecked == true)
                    drivesList_.Add(drive.UUID);
            }
        }

        protected override void SelectAll(object sender, EventArgs e)
        {
            drivesList_.Clear();
            if ((sender as CheckBox).Checked)
            {
                foreach (DrivesInfo drive in this.drives_)
                {
                    drive.IsChecked = true;
                    drivesList_.Add(drive.UUID);
                }
                drivesDataGridView.Refresh();
            }
            else
            {
                foreach (DrivesInfo drive in this.drives_)
                {
                    drive.IsChecked = false;
                }
                drivesDataGridView.Refresh();
            }
        }

    }

    public class DrivesInfo
    {
        [DisplayName("     ")]
        public bool IsChecked { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("UUID")]
        public string UUID { get; set; }
    }
}
