using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CloudScraper
{
    public partial class CopyStartForm : Form
    {
        SaveTransferTaskForm saveTransferForm_;
        ResumeTransferForm resumeTransferForm_;

        public CopyStartForm(SaveTransferTaskForm saveTransferForm)
        {
            this.saveTransferForm_ = saveTransferForm;

            CopyStartForm.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }



        public CopyStartForm(ResumeTransferForm resumeTransferForm)
        {
            this.resumeTransferForm_ = resumeTransferForm;
            InitializeComponent();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (this.saveTransferForm_ != null && this.resumeTransferForm_ == null)
            {
                this.saveTransferForm_.Show();
            }

            if (this.resumeTransferForm_ != null && this.saveTransferForm_ == null)
            {
                this.resumeTransferForm_.Show();
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Process p = new Process();

            ProcessStartInfo info = new ProcessStartInfo(Directory.GetCurrentDirectory() + "\\CloudScraper.exe");
            p.StartInfo = info;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.Exited += new EventHandler(p_Exited);

            p.EnableRaisingEvents = true;
            p.Start();
            this.processListBox.Items.Add("Process start...");
        }

        void p_Exited(object sender, EventArgs e)
        {


            //this.processListBox.BeginInvoke(new Action<object>((obj) =>
            //{
            //    this.processListBox.Items.Add("Process stop...");
            //}));

        }
    }
}
