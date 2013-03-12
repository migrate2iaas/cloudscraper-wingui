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
        CloudParametersForm cloudParametersForm_;
        SaveTransferTaskForm saveTransferTaskForm_;

        public ImagesPathForm(CloudParametersForm cloudParametersForm)
        {
            this.cloudParametersForm_ = cloudParametersForm;
            InitializeComponent();
            
            this.totalSpace.Text = ChooseDisksForm.totalSpaceRequired_.ToString() + "GB"; 
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.browseTextBox.Text = this.folderBrowserDialog.SelectedPath;

                //DirectoryInfo info = Directory.GetParent(this.folderBrowserDialog.SelectedPath);
                string rootName = Directory.GetDirectoryRoot(this.folderBrowserDialog.SelectedPath);
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (rootName == drive.Name)
                    {
                        this.freeSpace.Text = (drive.AvailableFreeSpace / (1024 * 1024 * 1024)).ToString() + "GB";
                        break;
                    }
                }
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.cloudParametersForm_.Show();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (this.saveTransferTaskForm_ == null)
            {
                this.saveTransferTaskForm_ = new SaveTransferTaskForm(this);
            }

            this.saveTransferTaskForm_.ShowDialog();
        }

        private void On_closed(object sender, FormClosedEventArgs e)
        {
            this.cloudParametersForm_.Close();
        }

    }
}
