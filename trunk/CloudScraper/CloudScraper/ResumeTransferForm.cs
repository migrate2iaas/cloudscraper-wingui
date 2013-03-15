using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CloudScraper
{
    public partial class ResumeTransferForm : Form
    {
        public static bool resumeUpload_;
        public static bool skipUpload_;
        public static string resumeFilePath_;
        
        NewResumeForm newResumeForm_;
        CopyStartForm copyStartForm_;

        public ResumeTransferForm(NewResumeForm newResumeForm)
        {
            this.newResumeForm_ = newResumeForm;

            InitializeComponent();

            resumeUpload_ = false;
            skipUpload_ = false;
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            this.Hide();
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
                this.nextButton.Enabled = true;
            }
        }

        private void OnPathChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                this.nextButton.Enabled = false;
                resumeFilePath_ = this.resumeTextBox.Text;
            }
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

        private void helpButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ya.ru");
        }
    }
}
