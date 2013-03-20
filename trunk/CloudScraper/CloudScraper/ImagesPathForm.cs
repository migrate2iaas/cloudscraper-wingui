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
            this.Text = Settings.Default.S5Header;
            this.nextButton.Text = Settings.Default.S5NextButtonText;
            this.backButton.Text = Settings.Default.S5BackButtonText;
            this.browseButton.Text = Settings.Default.S5BrowseButtonText;
            this.mainLabel.Text = Settings.Default.S5MainLabelText;
            this.totalSpaceLabel.Text = Settings.Default.S5TotalSpaceLabelText;
            this.freeSpaceLabel.Text = Settings.Default.S5FreeSpaceLabelText;
            this.errorLabel.Text = Settings.Default.S5ErrorLabelText;
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
                        if (ChooseDisksForm.totalSpaceRequired_ > Math.Round((decimal)drive.AvailableFreeSpace / (1024 * 1024 * 1024), 1) - 4)
                        {
                            this.errorLabel.Visible = true;
                            this.errorPicture.Visible = true;
                        }
                        else
                        {
                            this.errorLabel.Visible = false;
                            this.errorPicture.Visible = false;
                        }
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
                    if (ChooseDisksForm.totalSpaceRequired_ > Math.Round((decimal)drive.AvailableFreeSpace / (1024 * 1024 * 1024), 1) - 4)
                    {
                        this.errorLabel.Visible = true;
                        this.errorPicture.Visible = true;
                    }
                    else
                    {
                        this.errorLabel.Visible = false;
                        this.errorPicture.Visible = false;
                    }
                    break;
                }
            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Settings.Default.S5Link);
        }

        private void BrowseTextChanged(object sender, EventArgs e)
      {
            imagesPath_ = this.browseTextBox.Text;
            if (imagesPath_.Length >= 2)
            {
                string rootName = Directory.GetDirectoryRoot(imagesPath_);
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (rootName == drive.Name)
                    {
                        this.freeSpace.Text = Math.Round((decimal)drive.AvailableFreeSpace / (1024 * 1024 * 1024), 1).ToString() + "GB";
                        if (ChooseDisksForm.totalSpaceRequired_ > Math.Round((decimal)drive.AvailableFreeSpace / (1024 * 1024 * 1024), 1) - 4)
                        {
                            this.errorLabel.Visible = true;
                            this.errorPicture.Visible = true;
                        }
                        else
                        {
                            this.errorLabel.Visible = false;
                            this.errorPicture.Visible = false;
                        }
                        break;
                    }
                }
            }
            else
            {
                this.freeSpace.Text = "0GB";
                this.errorLabel.Visible = true;
                this.errorPicture.Visible = true;
            }
        }

    }
}
