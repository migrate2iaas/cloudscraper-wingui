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
            
            //Init basic UI strings from seettings file.
            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.Text = Settings.Default.S5Header;
            this.nextButton.Text = Settings.Default.S5NextButtonText;
            this.backButton.Text = Settings.Default.S5BackButtonText;
            this.browseButton.Text = Settings.Default.S5BrowseButtonText;
            this.mainLabel.Text = Settings.Default.S5MainLabelText;
            this.totalSpaceLabel.Text = Settings.Default.S5TotalSpaceLabelText;
            this.freeSpaceLabel.Text = Settings.Default.S5FreeSpaceLabelText;
            this.errorLabel.Text = Settings.Default.S5ErrorLabelText;
            this.logoPicture.Image = new Bitmap(Image.FromFile("Icons\\logo4a.png"));
        }

        //Choose path through Brose button.
        private void BrowseButtonClick(object sender, EventArgs e)
        {
            this.folderBrowserDialog.SelectedPath = Directory.GetCurrentDirectory();

            if (this.folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.browseTextBox.Text = this.folderBrowserDialog.SelectedPath;
                imagesPath_ = this.browseTextBox.Text;

                //Count Free Space on selected volume.
                string rootName = Directory.GetDirectoryRoot(this.folderBrowserDialog.SelectedPath);
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (rootName == drive.Name)
                    {
                        this.freeSpace.Text = Math.Round((decimal)drive.AvailableFreeSpace / (1024 * 1024 * 1024),1).ToString() + "GB";
                        //If  Free Space not enough (4GB additional program required).
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
            this.cloudParametersForm_.StartPosition = FormStartPosition.Manual;
            this.cloudParametersForm_.Location = this.Location;
            this.cloudParametersForm_.Show();
        }

        private void NextButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(imagesPath_))
                {
                    if (Path.IsPathRooted(imagesPath_))
                    {
                        string root = Path.GetPathRoot(imagesPath_);

                        imagesPath_ = imagesPath_.Replace(root, "");

                        foreach (char c in Path.GetInvalidPathChars())
                        {
                            if (imagesPath_.Contains(c.ToString()) ||
                                imagesPath_.Contains("/") ||
                                imagesPath_.Contains("\\\\") ||
                                imagesPath_.Contains(":") ||
                                imagesPath_.Contains("*") ||
                                imagesPath_.Contains("?") ||
                                imagesPath_.Contains("\"") ||
                                imagesPath_.Contains("<") || imagesPath_.Contains(">") ||
                                imagesPath_.Contains("|"))
                            {
                                DialogResult result = MessageBox.Show(
                                    "Path contains: \\ / : * ? \" < > |",
                                    Settings.Default.S5WarningHeader,
                                    MessageBoxButtons.OK);
                                return;
                            }
                        }

                        imagesPath_ = imagesPath_.Insert(0, root);
                        Directory.CreateDirectory(imagesPath_);

                    }
                    else
                    {
                        DialogResult result = MessageBox.Show(
                            "Path incorrect",
                            Settings.Default.S5WarningHeader,
                            MessageBoxButtons.OK);
                        return;
                    }
                }
            }
            catch (ArgumentException)
            {
                DialogResult result2 = MessageBox.Show(
                    "Path incorrect",
                    Settings.Default.S5WarningHeader,
                    MessageBoxButtons.OK);
                return;
            }
            
            if (Directory.GetFiles(imagesPath_).Length != 0)
            {
                DialogResult result = MessageBox.Show(Settings.Default.S5WarningMessage,
                Settings.Default.S5WarningHeader,
                MessageBoxButtons.OKCancel);

                if (result == DialogResult.Cancel)
                    return;
            }

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
            this.StartPosition = FormStartPosition.Manual;
            this.Location = this.cloudParametersForm_.Location;

            this.totalSpace.Text = ChooseDisksForm.totalSpaceRequired_.ToString() + "GB";

            this.browseTextBox.Text = Directory.GetCurrentDirectory();
            imagesPath_ = this.browseTextBox.Text;

            string rootName = Directory.GetDirectoryRoot(this.browseTextBox.Text);
            DriveInfo[] drives = DriveInfo.GetDrives();

            //Free space on volume where program starts.
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

        private void HelpButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Settings.Default.S5Link);
        }

        //Choose path throw keyboard input.
        private void BrowseTextChanged(object sender, EventArgs e)
        {
            imagesPath_ = this.browseTextBox.Text;
            if (imagesPath_.Length >= 2) 
                //&& Directory.Exists(imagesPath_))
            {
                try
                {
                    string rootName = Directory.GetDirectoryRoot(imagesPath_).ToUpper();
                
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
                catch
                {
                    return;
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
