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
        NewResumeForm newResumeForm_;
        CopyStartForm copyStartForm_;

        public static bool resumeUpload_;
        public static bool skipUpload_;

        public ResumeTransferForm(NewResumeForm newResumeForm)
        {
            this.newResumeForm_ = newResumeForm;

            InitializeComponent();

            resumeUpload_ = false;
            skipUpload_ = false;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.newResumeForm_.Show();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (this.copyStartForm_ == null)
            {
                this.copyStartForm_ = new CopyStartForm(this);
            }

            copyStartForm_.ShowDialog();
        }

        private void On_closed(object sender, FormClosedEventArgs e)
        {
            this.newResumeForm_.Close();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = "Transfer task file (*.ini)|*.ini";
            this.openFileDialog.Multiselect = false;
            this.openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            DialogResult result = this.openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.resumeTextBox.Text = this.openFileDialog.FileName;
                this.nextButton.Enabled = true;
            }

        }

        private void OnPathChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                this.nextButton.Enabled = false;
            }
        }

        private void resumeUploadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.redeployUploadCheckBox.Checked &&
                this.resumeUploadCheckBox.Checked)
            {
                this.redeployUploadCheckBox.Checked = false;
                skipUpload_ = false;
            }

            resumeUpload_ = this.resumeUploadCheckBox.Checked;
        }

        private void redeployUploadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.redeployUploadCheckBox.Checked &&
                this.resumeUploadCheckBox.Checked)
            {
                this.resumeUploadCheckBox.Checked = false;
                resumeUpload_ = false;
            }

            skipUpload_ = this.redeployUploadCheckBox.Checked;
        }
    }
}
