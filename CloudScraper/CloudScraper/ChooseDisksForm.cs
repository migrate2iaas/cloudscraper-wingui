using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CloudScraper
{
    public partial class ChooseDisksForm : Form
    {
        NewResumeForm _newResumeForm;

        public ChooseDisksForm(NewResumeForm newResumeForm)
        {
            this._newResumeForm = newResumeForm;
            InitializeComponent();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            this._newResumeForm.Show();
        }
    }
}
