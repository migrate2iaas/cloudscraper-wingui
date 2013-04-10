using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CloudScraper.Properties;

namespace CloudScraper
{
    public partial class ResumeTransferForm : Form
    {
        public static bool resumeUpload_;
        public static bool skipUpload_;
        public static string resumeFilePath_;
        public static string awsKey_ = "";
        
        NewResumeForm newResumeForm_;
        CopyStartForm copyStartForm_;

        public ResumeTransferForm(NewResumeForm newResumeForm)
        {
            this.newResumeForm_ = newResumeForm;

            InitializeComponent();

            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            resumeUpload_ = false;
            skipUpload_ = false;
            this.Text = Settings.Default.R2Header;
            this.mainLabel.Text = Settings.Default.R2MainLabelText;
            this.awsIdLabel.Text = Settings.Default.R2awsIdLabelText;
            this.redeployUploadCheckBox.Text = Settings.Default.R2RedeployCheckBoxText;
            this.resumeUploadCheckBox.Text = Settings.Default.R2UploadCheckBoxText;
            this.nextButton.Text = Settings.Default.R2NextButtonText;
            this.backButton.Text = Settings.Default.R2BackButtonText;
            this.browseButton.Text = Settings.Default.R2BrowseButtonText;
            this.logoPicture.Image = new Bitmap(Image.FromFile("Icons\\logo4a.png"));
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            this.Hide();
            this.newResumeForm_.StartPosition = FormStartPosition.Manual;
            this.newResumeForm_.Location = this.Location;
            this.newResumeForm_.Show();
        }

        private void NextButtonClick(object sender, EventArgs e)
        {
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
            this.openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

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

        private void CheckEdit()
        {
            if (resumeFilePath_ != "" && File.Exists(resumeFilePath_) && awsKey_ != "" && awsKey_.Length == 40)
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
            this.StartPosition = FormStartPosition.Manual;
            this.Location = this.newResumeForm_.Location;
        }

    }
}
