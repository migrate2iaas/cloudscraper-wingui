using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CloudScraper
{
    public partial class ResumeTransferForm : Form
    {
        NewResumeForm newResumeForm_;
        CopyStartForm copyStartForm_;

        public ResumeTransferForm(NewResumeForm newResumeForm)
        {
            this.newResumeForm_ = newResumeForm;

            InitializeComponent();
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
            this.openFileDialog.Filter = "Transfer Task File (*.ini)|*.ini";
            this.openFileDialog.Multiselect = false;

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
    }
}
