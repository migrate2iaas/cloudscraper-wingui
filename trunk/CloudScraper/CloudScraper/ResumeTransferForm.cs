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
    }
}
