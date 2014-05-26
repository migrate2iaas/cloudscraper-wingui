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
        private string comments_;

        public MailForm(CopyStartForm copyStartForm)
        {
            this.copyStartForm_ = copyStartForm;
            this.userName_ = "";
            this.email_ = "";

            InitializeComponent();

            this.Text = Settings.Default.MailHeader;
            this.userNameLabel.Text = Settings.Default.MailUserNameLabelText;
            this.emailLabel.Text = Settings.Default.MailEmailLabelText;
        }

        private void UserChanged(object sender, EventArgs e)
        {
            this.userName_ = userTextBox.Text;
        }

        private void MailTextChanged(object sender, EventArgs e)
        {
            this.email_ = mailTextBox.Text;
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            if (userName_ != "" && email_ != "" && email_.Contains("@"))
            {
                this.Close();
                //this.copyStartForm_.SendMail(userName_, email_, comments_);
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.copyStartForm_.SendAssemblaTicket(userName_, email_, comments_);
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show(Settings.Default.MailSendMessage,
                       Settings.Default.MailSendHeader, MessageBoxButtons.OK); 
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show(ex.ToString(),
                        Settings.Default.MailSendFailedHeader, MessageBoxButtons.OK);
                    MessageBox.Show(Settings.Default.MailSendFailedMessage,
                                    Settings.Default.MailSendFailedHeader, MessageBoxButtons.OK);

                }

            }
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CommentsChanged(object sender, EventArgs e)
        {
            this.comments_ = commentsTextBox.Text;
        }

        private void MailForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void emailLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
