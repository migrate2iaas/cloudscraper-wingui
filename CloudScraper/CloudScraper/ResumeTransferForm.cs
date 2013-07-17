using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CloudScraper.Properties;
using NLog;
using DotNetPerls;

namespace CloudScraper
{
    public partial class ResumeTransferForm : Form
    {
        public static bool resumeUpload_;
        public static bool skipUpload_;
        public static string resumeFilePath_;
        public static string awsKey_ = "";

        private static Logger logger_ = LogManager.GetLogger("ResumeTransferForm");

        public string cloudName;
        
        NewResumeForm newResumeForm_;
        CopyStartForm copyStartForm_;

        public ResumeTransferForm(NewResumeForm newResumeForm)
        {
            this.newResumeForm_ = newResumeForm;

            InitializeComponent();

            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.toolTip.SetToolTip(this.helpButton, Settings.Default.HelpButtonToolTip);
            resumeUpload_ = false;
            skipUpload_ = false;
            this.Text = Settings.Default.R2Header;
            this.mainLabel.Text = Settings.Default.R2MainLabelText;
            this.awsIdLabel.Text = Settings.Default.R2awsIdLabelText;
            this.redeployUploadCheckBox.Text = Settings.Default.R2RedeployCheckBoxText;
            this.resumeUploadCheckBox.Text = Settings.Default.R2UploadCheckBoxText;
            this.nextButton.Text = Settings.Default.R2NextButtonText;
            this.toolTip.SetToolTip(this.nextButton, Settings.Default.R2NextButtonToolTip);
            this.backButton.Text = Settings.Default.R2BackButtonText;
            this.toolTip.SetToolTip(this.backButton, Settings.Default.R2BackButtonToolTip);
            this.browseButton.Text = Settings.Default.R2BrowseButtonText;
            this.toolTip.SetToolTip(this.browseButton, Settings.Default.R2BrowseButtonToolTip);
            this.logoPicture.Image = new Bitmap(Image.FromFile("Icons\\logo4a.png"));
            this.toolTip.SetToolTip(this.resumeUploadCheckBox, Settings.Default.R2ResumeUploadCheckBoxToolTip);
            this.toolTip.SetToolTip(this.redeployUploadCheckBox, Settings.Default.R2RedeployUploadedCheckBoxToolTip);
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Return to NewResumeForm.");

            this.Hide();
            this.newResumeForm_.StartPosition = FormStartPosition.Manual;
            this.newResumeForm_.Location = this.Location;
            this.newResumeForm_.Show();
        }

        private void NextButtonClick(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("To CopyStartForm.");

            this.Hide();

            if (this.copyStartForm_ == null)
            {
                this.copyStartForm_ = new CopyStartForm(this);
            }

            copyStartForm_.ShowDialog();
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            this.newResumeForm_.Close();
        }

        private void BrowseButtonClick(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = "Transfer task file (*.ini)|*.ini";
            this.openFileDialog.Multiselect = false;
            if (!Directory.Exists(Environment.GetEnvironmentVariable("USERPROFILE") + "\\CloudScraper"))
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("USERPROFILE") + "\\CloudScraper");
            this.openFileDialog.InitialDirectory = Environment.GetEnvironmentVariable("USERPROFILE") + "\\CloudScraper";

            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.resumeTextBox.Text = this.openFileDialog.FileName;
                resumeFilePath_ = this.resumeTextBox.Text;
                this.CheckEdit();
            }
        }

        private void OnPathChanged(object sender, EventArgs e)
        {
            resumeFilePath_ = this.resumeTextBox.Text;
            this.CheckEdit();
        }

        private void ResumeUploadCheckedChanged(object sender, EventArgs e)
        {
            if (this.redeployUploadCheckBox.Checked &&
                this.resumeUploadCheckBox.Checked)
            {
                this.redeployUploadCheckBox.Checked = false;
                skipUpload_ = false;
            }

            resumeUpload_ = this.resumeUploadCheckBox.Checked;
        }

        private void RedeployUploadCheckedChanged(object sender, EventArgs e)
        {
            if (this.redeployUploadCheckBox.Checked &&
                this.resumeUploadCheckBox.Checked)
            {
                this.resumeUploadCheckBox.Checked = false;
                resumeUpload_ = false;
            }

            skipUpload_ = this.redeployUploadCheckBox.Checked;
        }

        private void HelpButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Settings.Default.R2Link);
        }

        private void AwsIdChanged(object sender, EventArgs e)
        {
            awsKey_ = (sender as TextBox).Text;
            this.CheckEdit();
        }

        public string getCloudName()
        {
            return this.cloudName;
        }

        public void setCloudName(string newname)
        {
            this.cloudName = newname;
        }

        public string getPassword()
        {
            return awsKey_;
        }

        private void CheckEdit()
        {
            this.redeployUploadCheckBox.Enabled = true;
            this.resumeUploadCheckBox.Enabled = true;

            if (resumeFilePath_ != "" && File.Exists(resumeFilePath_))
            {
	            var utf16WithBom = new System.Text.UnicodeEncoding(false , true);
                using (StreamReader stream = new StreamReader(resumeFilePath_, utf16WithBom))
                {

                    bool isHeaderPresent = false;
                    while (!stream.EndOfStream)
                    {
                        string header = stream.ReadLine();
                        if (header == "[EC2]")
                        {
                            this.mainLabel.Text = Settings.Default.R2MainLabelText + "\n\n for Amazon";
                            isHeaderPresent = true;
                            this.setCloudName("EC2");
                            this.awsIdTextBox.MaxLength = 40;
                            break;
                        }
                        else if (header == "[ElasticHosts]")
                        {
                            this.mainLabel.Text = Settings.Default.R2MainLabelText + "\n\n for Elastic Hosts";
                            this.redeployUploadCheckBox.Checked = false;
                            this.redeployUploadCheckBox.Enabled = false;
                            this.setCloudName("ElasticHosts");
                            this.awsIdTextBox.MaxLength = 40;
                            skipUpload_ = false;
                        
                            string body = stream.ReadToEnd();
                            if (body.Contains("image-placement = direct"))
                            {
                                this.resumeUploadCheckBox.Checked = false;
                                this.resumeUploadCheckBox.Enabled = false;
                                resumeUpload_ = false;
                            }
                            isHeaderPresent = true;
                            break;
                        }
                        else if (header == "[Azure]")
                        {
                            this.mainLabel.Text = Settings.Default.R2MainLabelText + "\n\n for Windows Azure";
                            isHeaderPresent = true;
                            this.setCloudName("Azure");
                            this.awsIdTextBox.MaxLength = 100;
                            break;
                        }
                    }

                    if (!isHeaderPresent)
                    {
                        DialogResult result = BetterDialog.ShowDialog(Settings.Default.R2WarningMessageHeader,
                            Settings.Default.R2WarningMessageText, "", "OK", "OK",
                            Image.FromFile("Icons\\WarningDialog.png"), false);

                        this.nextButton.Enabled = false;
                        return;
                    }
                }
            }
            else
            {
                this.mainLabel.Text = Settings.Default.R2MainLabelText;
            }

            if (resumeFilePath_ != "" && File.Exists(resumeFilePath_) && awsKey_ != "" && 
                ((this.cloudName != "Azure" && awsKey_.Length == 40) || (this.cloudName == "Azure")))
            {
                this.nextButton.Enabled = true;
            }
            else
            {
                this.nextButton.Enabled = false;
            }
        }

        private void ResumeTransferLoad(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Form load.");

            this.StartPosition = FormStartPosition.Manual;
            this.Location = this.newResumeForm_.Location;
        }

    }
}
