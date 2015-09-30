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

    public partial class CheckAgreement : Form
    {
        private void SetImagesPathForm()
        {
            //Init basic UI strings from seettings file.
            this.CheckLabel.Text = Settings.Default.S5FreeSpaceLabelText;
            this.LicenseValiditi.Text = Settings.Default.S5FreeSpaceLabelText;
        }
        public CheckAgreement()
        {
            InitializeComponent();
        }

        private void CheckAgreement_Load(object sender, EventArgs e)
        {
            //Date.Text = NewResumeForm.line;
            //Date.BackColor = Color.Transparent;
            //BackColor = Color.Transparent;
            //LicenseValiditi.BackColor = Color.Transparent;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewResumeForm.agree = true;
            //NewResumeForm frm1 = new NewResumeForm();
            //frm1.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewResumeForm.agree = false;
            //NewResumeForm frm1 = new NewResumeForm();
            //frm1.Show();
            Hide();
        }

        private void buttonNo_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Date_Click(object sender, EventArgs e)
        {

        }

        private void LicenseValiditi_Click(object sender, EventArgs e)
        {

        }
    }
}
