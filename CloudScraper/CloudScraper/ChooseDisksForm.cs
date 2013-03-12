using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.IO;
using CloudScraper.Properties;

namespace CloudScraper
{
    public partial class ChooseDisksForm : Form
    {
        NewResumeForm newResumeForm_;
        DriveInfo[] drives_;
        bool loaded_;
        int? systemVolumeIndex_;
        BindingList<VolumeInfo> volumes_;
        
        public static long totalSpaceRequired_;

        ChooseCloudForm chooseCloudForm_;

        public ChooseDisksForm(NewResumeForm newResumeForm)
        {
            ChooseDisksForm.totalSpaceRequired_ = 0;
            this.loaded_ = false;
            this.systemVolumeIndex_ = null;
            this.newResumeForm_ = newResumeForm;
            this.drives_ = DriveInfo.GetDrives();
            this.volumes_ = new BindingList<VolumeInfo>();
            
            InitializeComponent();

            dataGridView.AutoGenerateColumns = true;

            this.totalSpaceLabel.Text = ChooseDisksForm.totalSpaceRequired_.ToString() + "GB";
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

                            VolumeInfo volume = new VolumeInfo()
                            {
                                IsChecked = false,
                                Image = Resources.HD_Drive,
                                Name = info.VolumeLabel + " (" + info.Name.Replace('\\', ')'),
                                TotalSpace = info.TotalSize / (1024 * 1024 * 1024),
                                UsedSpace = (info.TotalSize - info.TotalFreeSpace) / (1024 * 1024 * 1024),
                                FreeSpace = info.AvailableFreeSpace / (1024 * 1024 * 1024)
                            };

                            if (Directory.Exists(info.Name + Settings.Default.OSFolder))
                            {
                                volume.IsChecked = true;
                                volume.Image = Resources.WindowsDrive;  
                                this.systemVolumeIndex_ = this.drivesCheckedList.Items.Count - 1; 
                                this.drivesCheckedList.SetItemChecked((int)this.systemVolumeIndex_, true);
                                this.drivesCheckedList.SetSelected((int)this.systemVolumeIndex_, true);

                                this.volumes_.Insert(0, volume);
                                continue;
                            }
                            
                            this.volumes_.Add(volume);
                        }
                    }

                    dataGridView.DataSource = this.volumes_;
                    dataGridView.Columns[0].Width = 30;
                    dataGridView.Columns[1].Width = 50;
                    dataGridView.Columns[1].ReadOnly = true;
                    dataGridView.Columns[2].ReadOnly = true;
                    dataGridView.Columns[3].ReadOnly = true;
                    dataGridView.Columns[4].ReadOnly = true;
                    dataGridView.Columns[5].ReadOnly = true;

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

            totalSpaceRequired_ = 0;

            if (this.nextButton.Enabled)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    totalSpaceRequired_ += (this.drives_[e.Index].TotalSize -
                        this.drives_[e.Index].TotalFreeSpace) / (1024 * 1024 * 1024);
                }

                foreach (int index in this.drivesCheckedList.CheckedIndices)
                {
                    if (index != e.Index)
                    {
                        totalSpaceRequired_ += (this.drives_[index].TotalSize -
                        this.drives_[index].TotalFreeSpace) / (1024 * 1024 * 1024);
                    }
                }
            }

            this.totalSpaceLabel.Text = totalSpaceRequired_.ToString() + "GB";
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

        private void On_closed(object sender, FormClosedEventArgs e)
        {
            this.newResumeForm_.Close();
        }

        private void OnSelect(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sender is DataGridView && e.ColumnIndex == 0)
            {
                //if (this.drivesCheckedList.CheckedItems.Count == 0 && e.CurrentValue == CheckState.Unchecked)
                //{
                //    this.nextButton.Enabled = true;
                //}
                //else if (this.drivesCheckedList.CheckedItems.Count == 1 && e.CurrentValue == CheckState.Checked)
                //{
                //    this.nextButton.Enabled = false;
                //}

                if (e.RowIndex == 0 && this.volumes_[0].IsChecked == true)
                {
                    
                        DialogResult result = MessageBox.Show("Are you sure you don't want to move your system? \n" +
                        "In this case no cloud servers will be created!", "Message",
                            MessageBoxButtons.OKCancel);


                        if (result == DialogResult.Cancel)
                        {
                            this.volumes_[0].IsChecked = true;
                        }
                }

                //totalSpaceRequired_ = 0;

                //if (this.nextButton.Enabled)
                //{
                //    if (e.NewValue == CheckState.Checked)
                //    {
                //        totalSpaceRequired_ += (this.drives_[e.Index].TotalSize -
                //            this.drives_[e.Index].TotalFreeSpace) / (1024 * 1024 * 1024);
                //    }

                //    foreach (int index in this.drivesCheckedList.CheckedIndices)
                //    {
                //        if (index != e.Index)
                //        {
                //            totalSpaceRequired_ += (this.drives_[index].TotalSize -
                //            this.drives_[index].TotalFreeSpace) / (1024 * 1024 * 1024);
                //        }
                //    }
                //}

                //this.totalSpaceLabel.Text = totalSpaceRequired_.ToString() + "GB";
            }
        }

    }

    public class VolumeInfo
    {
        [DisplayName(" ")]
        public bool IsChecked
        {
            get;
            set;
        }
        
        [DisplayName(" ")]
        public Icon Image { get; set; }
        
        [DisplayName("Name")]
        public string Name { get; set; }  

        [DisplayName("Total Space, GB")]
        public decimal TotalSpace { get; set; }

        [DisplayName("Used Space, GB")]
        public decimal UsedSpace { get; set; }

        [DisplayName("Free Space, GB")]
        public decimal FreeSpace { get; set; }
    }
}
