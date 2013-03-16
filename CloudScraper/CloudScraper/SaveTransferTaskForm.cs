using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CloudScraper.Properties;

namespace CloudScraper
{
    public partial class SaveTransferTaskForm : Form
    {
        public static string transferPath_;

        ImagesPathForm imagesPathForm_;
        CopyStartForm copyStartForm_;

        public SaveTransferTaskForm(ImagesPathForm imagesPathForm)
        {
            this.imagesPathForm_ = imagesPathForm;
            InitializeComponent();

            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.saveTransferTextBox.Text = Directory.GetCurrentDirectory() + "\\" + "transfer.ini";
            transferPath_ = this.saveTransferTextBox.Text;
            this.Text = Settings.Default.S6Header;
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

        private void BackButtonClick(object sender, EventArgs e)
        {
            this.Hide();
            this.imagesPathForm_.Show();
        }

        private void NextButtonClick(object sender, EventArgs e)
        {
            this.Hide();

            if (this.copyStartForm_ == null)
            {
                this.copyStartForm_ = new CopyStartForm(this);
            }

            copyStartForm_.ShowDialog();
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            this.imagesPathForm_.Close();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ya.ru");
        }
    }
}
