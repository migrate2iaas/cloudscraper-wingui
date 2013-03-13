using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CloudScraper
{
    public partial class SaveTransferTaskForm : Form
    {
        ImagesPathForm imagesPathForm_;
        CopyStartForm copyStartForm_;

        public static string transferPath_;

        public SaveTransferTaskForm(ImagesPathForm imagesPathForm)
        {
            this.imagesPathForm_ = imagesPathForm;
            InitializeComponent();

            this.saveTransferTextBox.Text = Directory.GetCurrentDirectory() + "\\" + "transfer.ini";

            transferPath_ = this.saveTransferTextBox.Text;
        }

        private void saveTransferTask_Click(object sender, EventArgs e)
        {

            this.saveFileDialog.Filter = "Transfer Task File (*.ini)|*.ini";
            this.saveFileDialog.DefaultExt = "." + "ini";
 
            DialogResult result = this.saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.saveTransferTextBox.Text = this.saveFileDialog.FileName;
                transferPath_ = this.saveTransferTextBox.Text;
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

        private void On_closed(object sender, FormClosedEventArgs e)
        {
            this.imagesPathForm_.Close();
        }
    }
}
