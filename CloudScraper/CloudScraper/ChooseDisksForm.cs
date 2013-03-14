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
        public static decimal totalSpaceRequired_;
        public static List<string> selectedDisks_ = new List<string>();

        ChooseCloudForm chooseCloudForm_;
        NewResumeForm newResumeForm_;
        DriveInfo[] drives_;
        bool loaded_;
        BindingList<VolumeInfo> volumes_;

        public ChooseDisksForm(NewResumeForm newResumeForm)
        {
            this.loaded_ = false;
            this.newResumeForm_ = newResumeForm;
            this.drives_ = DriveInfo.GetDrives();
            this.volumes_ = new BindingList<VolumeInfo>();
            
            InitializeComponent();
            
            totalSpaceRequired_ = 0;
            this.totalSpaceLabel.Text = totalSpaceRequired_.ToString() + "GB";
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            this.Hide();
            this.newResumeForm_.Show();
        }

        private void ChooseDisksLoad(object sender, EventArgs e)
        {
            if (!this.loaded_ && this.drives_ != null)
            {
                foreach (DriveInfo info in this.drives_)
                {
                    if (info.IsReady)
                    {
                        VolumeInfo volume = new VolumeInfo()
                        {
                            IsChecked = false,
                            Image = new Bitmap(Image.FromFile("Icons\\HD-Drive.ico"), new Size(24, 24)),
                            Name = info.VolumeLabel == "" ? info.DriveType + " (" + info.Name.Replace('\\', ')') :
                                    info.VolumeLabel + " (" + info.Name.Replace('\\', ')'),
                            ShortName = info.Name,
                            TotalSpace = Math.Round((decimal)info.TotalSize / (1024 * 1024 * 1024), 1),
                            UsedSpace = Math.Round((decimal)(info.TotalSize - info.TotalFreeSpace) / (1024 * 1024 * 1024), 1),
                            FreeSpace = Math.Round((decimal)info.AvailableFreeSpace / (1024 * 1024 * 1024), 1)
                        };
                            
                        if (Environment.GetEnvironmentVariable("SystemRoot").Contains(info.Name))
                        {
                            //string str = Environment.GetEnvironmentVariable("windir");
                            volume.IsChecked = true;
                            volume.Image = new Bitmap(Image.FromFile("Icons\\WindowsDrive.ico"), new Size(24, 24));
                            totalSpaceRequired_ = volume.UsedSpace;
                            this.nextButton.Enabled = true;
                            this.totalSpaceLabel.Text = Math.Round(totalSpaceRequired_, 1).ToString() + "GB";
                            selectedDisks_.Add(volume.ShortName);
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

        private void NextButtonClick(object sender, EventArgs e)
        {
            this.Hide();

            if (this.chooseCloudForm_ == null)
            {
                this.chooseCloudForm_ = new ChooseCloudForm(this);
            }

            chooseCloudForm_.ShowDialog();
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            this.newResumeForm_.Close();
        }

        private void OnSelect(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sender is DataGridView && e.ColumnIndex == 0)
            {
                this.volumes_[e.RowIndex].IsChecked = !this.volumes_[e.RowIndex].IsChecked;

                if (e.RowIndex == 0 && this.volumes_[0].IsChecked == false)
                { 
                    DialogResult result = MessageBox.Show("Are you sure you don't want to move your system? \n" +
                    "In this case no cloud servers will be created!", "Message",
                        MessageBoxButtons.OKCancel);


                    if (result == DialogResult.Cancel)
                    {
                        this.volumes_[0].IsChecked = true;
                    }
                    else
                    {
                        this.volumes_[0].IsChecked = false;
                    }
                }

                this.nextButton.Enabled = false;

                foreach (VolumeInfo vol in this.volumes_)
                {
                    if (vol.IsChecked)
                    {
                        this.nextButton.Enabled = true;
                        break;
                    }
                }

                totalSpaceRequired_ = 0;

                if (this.nextButton.Enabled)
                {
                    selectedDisks_.Clear();

                    foreach (VolumeInfo vol in this.volumes_)
                    {
                        if (vol.IsChecked)
                        {
                            totalSpaceRequired_ += vol.UsedSpace;
                            selectedDisks_.Add(vol.ShortName);
                        }
                    }
                }

                this.totalSpaceLabel.Text = Math.Round(totalSpaceRequired_,1).ToString() + "GB";
            }
        }

    }

    public class VolumeInfo
    {   
        [DisplayName(" ")]
        public bool IsChecked { get; set; }
        
        [DisplayName(" ")]
        public Image Image { get; set; }
        
        [DisplayName("Name")]
        public string Name { get; set; }

        [Browsable(false)]
        public string ShortName { get; set; }

        [DisplayName("Total Space, GB")]
        public decimal TotalSpace { get; set; }

        [DisplayName("Used Space, GB")]
        public decimal UsedSpace { get; set; }

        [DisplayName("Free Space, GB")]
        public decimal FreeSpace { get; set; }
    }
}
