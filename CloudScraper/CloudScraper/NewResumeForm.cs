using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CloudScraper
{
    public partial class NewResumeForm : Form
    {
        ChooseDisksForm chooseDiskForm_;
        ResumeTransferForm resumeTransferForm_;

        public NewResumeForm()
        {
            InitializeComponent();
        }

        private void startNewButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            
            if (this.chooseDiskForm_ == null)
            {
                this.chooseDiskForm_ = new ChooseDisksForm(this);
            }

            chooseDiskForm_.ShowDialog();
        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (this.resumeTransferForm_ == null)
            {
                this.resumeTransferForm_ = new ResumeTransferForm(this);
            }

            resumeTransferForm_.ShowDialog();
        }
    }
}
