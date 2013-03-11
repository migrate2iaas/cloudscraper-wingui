using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.IO;

namespace CloudScraper
{
    public partial class ChooseDisksForm : Form
    {
        NewResumeForm newResumeForm_;
        DriveInfo[] drives_;
        bool loaded_;
        int? systemVolumeIndex_;
        long totalSpaceRequired_;

        ChooseCloudForm chooseCloudForm_;

        public ChooseDisksForm(NewResumeForm newResumeForm)
        {
            this.totalSpaceRequired_ = 0;
            this.loaded_ = false;
            this.systemVolumeIndex_ = null;
            this.newResumeForm_ = newResumeForm;
            this.drives_ = DriveInfo.GetDrives();
            
            InitializeComponent();

            this.totalSpaceLabel.Text = this.totalSpaceRequired_.ToString() + "GB";
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.newResumeForm_.Show();
        }

        private void ChooseDisksLoad(object sender, EventArgs e)
        {
            if (!this.loaded_)
            {
                if (this.drives_ != null)
                {
                    foreach (DriveInfo info in this.drives_)
                    {
                        if (info.IsReady)
                        {
                            this.drivesCheckedList.Items.Add(info.Name + info.VolumeLabel + "   " + "Total space:" +
                                (info.TotalSize / (1024 * 1024 * 1024)).ToString() + "GB" + "   " + "Used space:" +
                                ((info.TotalSize - info.TotalFreeSpace) / (1024 * 1024 * 1024)).ToString() + "GB" + "   " + "Free space:" +
                                (info.TotalFreeSpace / (1024 * 1024 * 1024)).ToString() + "GB");


                            if (Directory.Exists(info.Name + "Windows"))
                            {
                                this.systemVolumeIndex_ = this.drivesCheckedList.Items.Count - 1; 
                                this.drivesCheckedList.SetItemChecked((int)this.systemVolumeIndex_, true);
                                this.drivesCheckedList.SetSelected((int)this.systemVolumeIndex_, true);
                            }
                        }
                    }

                    this.loaded_ = true;
                }
            }
        }

        private void DrivesChecked(object sender, ItemCheckEventArgs e)
        {
            if (this.drivesCheckedList.CheckedItems.Count == 0 && e.CurrentValue == CheckState.Unchecked)
            {
                this.nextButton.Enabled = true;
            }
            else if(this.drivesCheckedList.CheckedItems.Count == 1 && e.CurrentValue == CheckState.Checked)
            {
                this.nextButton.Enabled = false;
            }

            if (e.CurrentValue == CheckState.Checked)
            {
                if (systemVolumeIndex_ != null && e.Index == systemVolumeIndex_)
                {
                    DialogResult result = MessageBox.Show("Are you sure you don't want to move your system? \n" +
                    "In this case no cloud servers will be created!", "Message",
                        MessageBoxButtons.OKCancel);


                    if (result == DialogResult.Cancel)
                    {
                        e.NewValue = CheckState.Checked;
                    }
                }
            }

            if (this.nextButton.Enabled)
            {

            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (this.chooseCloudForm_ == null)
            {
                this.chooseCloudForm_ = new ChooseCloudForm(this);
            }

            chooseCloudForm_.ShowDialog();
        }
    }
}
