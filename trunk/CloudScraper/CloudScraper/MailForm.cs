using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CloudScraper.Properties;

namespace CloudScraper
{
    public partial class MailForm : Form
    {
        CopyStartForm copyStartForm_;
        private string userName_;
        private string email_;

        public MailForm(CopyStartForm copyStartForm)
        {
            this.copyStartForm_ = copyStartForm;
            userName_ = "";
            email_ = "";

            InitializeComponent();
            this.Text = Settings.Default.MailHeader;
            this.userNameLabel.Text = Settings.Default.MailUserNameLabelText;
            this.emailLabel.Text = Settings.Default.MailEmailLabelText;
        }

        private void UserChanged(object sender, EventArgs e)
        {
            userName_ = userTextBox.Text;
        }

        private void mailTextBox_TextChanged(object sender, EventArgs e)
        {
            email_ = mailTextBox.Text;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (userName_ != "" && email_ != "" && email_.Contains("@"))
            {
                this.Close();
                this.copyStartForm_.SendMail(userName_, email_);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
