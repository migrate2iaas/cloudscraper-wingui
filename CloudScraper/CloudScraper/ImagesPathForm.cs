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
    public partial class ImagesPathForm : Form
    {
        public static string imagesPath_;

        CloudParametersForm cloudParametersForm_;
        SaveTransferTaskForm saveTransferTaskForm_;

        public ImagesPathForm(CloudParametersForm cloudParametersForm)
        {
            this.cloudParametersForm_ = cloudParametersForm;
            InitializeComponent();
            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
        }

        private void BrowseButtonClick(object sender, EventArgs e)
        {
            this.folderBrowserDialog.SelectedPath = Directory.GetCurrentDirectory();

            if (this.folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.browseTextBox.Text = this.folderBrowserDialog.SelectedPath;
                imagesPath_ = this.browseTextBox.Text;

                string rootName = Directory.GetDirectoryRoot(this.folderBrowserDialog.SelectedPath);
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (rootName == drive.Name)
                    {
                        this.freeSpace.Text = Math.Round((decimal)drive.AvailableFreeSpace / (1024 * 1024 * 1024),1).ToString() + "GB";
                        break;
                    }
                }
            }
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            this.Hide();
            this.cloudParametersForm_.Show();
        }

        private void NextButtonClick(object sender, EventArgs e)
        {
            this.Hide();

            if (this.saveTransferTaskForm_ == null)
            {
                this.saveTransferTaskForm_ = new SaveTransferTaskForm(this);
            }

            this.saveTransferTaskForm_.ShowDialog();
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            this.cloudParametersForm_.Close();
        }

        private void ImagesPathFormLoad(object sender, EventArgs e)
        {
            this.totalSpace.Text = ChooseDisksForm.totalSpaceRequired_.ToString() + "GB";

            this.browseTextBox.Text = Directory.GetCurrentDirectory();
            imagesPath_ = this.browseTextBox.Text;

            string rootName = Directory.GetDirectoryRoot(this.browseTextBox.Text);
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                if (rootName == drive.Name)
                {
                    this.freeSpace.Text = Math.Round((decimal)drive.AvailableFreeSpace / (1024 * 1024 * 1024), 1).ToString() + "GB";
                    break;
                }
            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ya.ru");
        }
    }
}
