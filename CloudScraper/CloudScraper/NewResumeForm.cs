using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CloudScraper.Properties;
using NLog;


namespace CloudScraper
{
    public partial class NewResumeForm : Form
    {
        ChooseDisksForm chooseDiskForm_;
        ResumeTransferForm resumeTransferForm_;

        private static Logger logger_ = LogManager.GetLogger("NewResumeForm");

        public NewResumeForm()
        {
            InitializeComponent();
            
            this.startNewButton.Image = new Bitmap(Image.FromFile("Icons\\StartNew.ico"), new Size(32, 32));
            this.startNewButton.Text = Settings.Default.S1StartNewButtonText;
            this.toolTip.SetToolTip(this.startNewButton, Settings.Default.S1StartNewButtonToolTip);
            this.resumeButton.Image = new Bitmap(Image.FromFile("Icons\\Resume.ico"), new Size(32, 32));
            this.resumeButton.Text = Settings.Default.S1ResumeButtonText;
            this.toolTip.SetToolTip(this.resumeButton, Settings.Default.S1ResumeButtonToolTip);
            this.Text = Settings.Default.S1Header;
            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.toolTip.SetToolTip(this.helpButton, Settings.Default.HelpButtonToolTip);
            this.logoPicture.Image = new Bitmap(Image.FromFile("Icons\\logo4a.png"));
        }

        private void StartNewButtonClick(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("New scenario select.");
            
            this.Hide();
            
            if (this.chooseDiskForm_ == null)
            {
                this.chooseDiskForm_ = new ChooseDisksForm(this);
            }

            chooseDiskForm_.ShowDialog();
        }

        private void ResumeButtonClick(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Resume scenario select.");
            
            this.Hide();

            if (this.resumeTransferForm_ == null)
            {
                this.resumeTransferForm_ = new ResumeTransferForm(this);
            }

            resumeTransferForm_.ShowDialog();
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void HelpButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Settings.Default.S1Link);
        }

        private void NewResumeFormLoad(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Form load.");
        }
    }
}
