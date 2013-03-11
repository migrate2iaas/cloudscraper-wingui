using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CloudScraper
{
    public partial class SaveTransferTaskForm : Form
    {
        ImagesPathForm imagesPathForm_;
        CopyStartForm copyStartForm_;

        public SaveTransferTaskForm(ImagesPathForm imagesPathForm)
        {
            this.imagesPathForm_ = imagesPathForm;
            InitializeComponent();
        }

        private void saveTransferTask_Click(object sender, EventArgs e)
        {

            this.saveFileDialog.Filter = "Transfer Task File (*.ini)|*.ini";
            this.saveFileDialog.DefaultExt = "." + "ini";
 
            DialogResult result = this.saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.saveTransferTextBox.Text = this.saveFileDialog.FileName;            
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.imagesPathForm_.Show();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (this.copyStartForm_ == null)
            {
                this.copyStartForm_ = new CopyStartForm(this);
            }

            copyStartForm_.ShowDialog();
        }
    }
}
