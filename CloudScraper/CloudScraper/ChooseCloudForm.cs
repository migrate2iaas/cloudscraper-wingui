using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CloudScraper
{
    public partial class ChooseCloudForm : Form
    {
        ChooseDisksForm chooseDiskForm_;
        CloudParametersForm cloudParametersForm_;
        
        public ChooseCloudForm(ChooseDisksForm chooseDiskForm)
        {
            this.chooseDiskForm_ = chooseDiskForm;

            InitializeComponent();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.chooseDiskForm_.Show();
        }

        private void amazonButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (this.cloudParametersForm_ == null)
            {
                this.cloudParametersForm_ = new CloudParametersForm(this);
            }

            cloudParametersForm_.ShowDialog();
        }
    }
}
