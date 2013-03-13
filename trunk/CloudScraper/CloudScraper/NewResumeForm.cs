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

        static GhostForm ghost_;

        public NewResumeForm()
        {
            ghost_ = new GhostForm();
            ghost_.Show();

            InitializeComponent();
        }

        private void StartNewButtonClick(object sender, EventArgs e)
        {            
            this.Hide();
            
            if (this.chooseDiskForm_ == null)
            {
                this.chooseDiskForm_ = new ChooseDisksForm(this);
            }

            chooseDiskForm_.ShowDialog();
        }

        private void ResumeButtonClick(object sender, EventArgs e)
        {
            this.Hide();

            if (this.resumeTransferForm_ == null)
            {
                this.resumeTransferForm_ = new ResumeTransferForm(this);
            }

            resumeTransferForm_.ShowDialog();
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            ghost_.Close();
        }
    }
}
