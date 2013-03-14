﻿using System;
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

            this.startNewButton.Image = new Bitmap(Image.FromFile("Icons\\StartNew.ico"), new Size(32, 32));
            this.resumeButton.Image = new Bitmap(Image.FromFile("Icons\\Resume.ico"), new Size(32, 32));
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