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
using NLog;
using DotNetPerls;

namespace CloudScraper
{
    public partial class ChooseDisksForm : Form
    {
        public static decimal totalSpaceRequired_;
        public static List<string> selectedDisks_ = new List<string>();
        private static Logger logger_ = LogManager.GetLogger("ChooseDisksForm");

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
            
            //Use totalSpaceRequired for use in other forms.
            totalSpaceRequired_ = 0;
            this.totalSpaceLabel.Text = totalSpaceRequired_ == 0 ? totalSpaceRequired_.ToString() + "GB" :
                (totalSpaceRequired_ + Settings.Default.TotalSizeGap).ToString() + "GB";
            
            //Initialize basic strings UI from from settings file.
            this.Text = Settings.Default.S2Header;
            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.labelTotalSpace.Text = Settings.Default.S2LabelTotalSpace;
            this.nextButton.Text = Settings.Default.S2NextButtonText;
            this.toolTip.SetToolTip(this.nextButton, Settings.Default.S2NextButtonToolTip); 
            this.backButton.Text = Settings.Default.S2BackButtonText;
            this.toolTip.SetToolTip(this.backButton, Settings.Default.S2BackButtonToolTip);
            this.toolTip.SetToolTip(this.helpButton, Settings.Default.HelpButtonToolTip);
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Return to NewResumeForm.");

            this.Hide();
            this.newResumeForm_.StartPosition = FormStartPosition.Manual;
            this.newResumeForm_.Location = this.Location;
            this.newResumeForm_.Show();
        }

        private void ChooseDisksLoad(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Form loaded.");
            
            //For show Form in the same position as prev.
            this.StartPosition = FormStartPosition.Manual;
            this.Location = this.newResumeForm_.Location;

            if (!this.loaded_ && this.drives_ != null)
            {
                foreach (DriveInfo info in this.drives_)
                {
                    if (info.IsReady)
                    {
                        //Create VolumeInfo object.
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

                        //Check System drive.
                        if (Environment.GetEnvironmentVariable("SystemRoot").Contains(info.Name))
                        {
                            //string str = Environment.GetEnvironmentVariable("windir");
                            volume.IsChecked = true;
                            volume.Image = new Bitmap(Image.FromFile("Icons\\WindowsDrive.ico"), new Size(24, 24));
                            totalSpaceRequired_ = volume.UsedSpace;
                            this.nextButton.Enabled = true;
                            this.totalSpaceLabel.Text = totalSpaceRequired_ == 0 ? Math.Round(totalSpaceRequired_, 1).ToString() + "GB" :
                                Math.Round(totalSpaceRequired_ + Settings.Default.TotalSizeGap, 1).ToString() + "GB";
                            selectedDisks_.Add(volume.ShortName);
                            this.volumes_.Insert(0, volume);
                            continue;
                        }

                        this.volumes_.Add(volume);
                    }
                }

                //DataGrid change columns width.
                dataGridView.DataSource = this.volumes_;
                dataGridView.AutoResizeColumn(0);
                dataGridView.AutoResizeColumn(1);

                this.loaded_ = true;
            }
            else if (this.drives_ != null)
            {
                this.UpdateVolumes();
            }
        }

        private void NextButtonClick(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("To the ChooseCloudForm.");
            
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

        private void UpdateVolumes()
        {
            this.drives_ = DriveInfo.GetDrives();
            foreach (DriveInfo info in this.drives_)
            {
                if (info.IsReady)
                {
                    foreach (VolumeInfo volume in this.volumes_)
                    {
                        if (volume.ShortName == info.Name)
                        {
                            //Update volumme info.
                            volume.TotalSpace = Math.Round((decimal)info.TotalSize / (1024 * 1024 * 1024), 1);
                            volume.UsedSpace = Math.Round((decimal)(info.TotalSize - info.TotalFreeSpace) / (1024 * 1024 * 1024), 1);
                            volume.FreeSpace = Math.Round((decimal)info.AvailableFreeSpace / (1024 * 1024 * 1024), 1);
                            break;
                        }
                    }
                }
            }
        }
        
        private void OnSelect(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sender is DataGridView && e.ColumnIndex == 0)
            {
                this.volumes_[e.RowIndex].IsChecked = !this.volumes_[e.RowIndex].IsChecked;

                this.UpdateVolumes();

                if (e.RowIndex == 0 && this.volumes_[0].IsChecked == false)
                { 
                    //Show message when system drive is unchecked.
                    DialogResult result = BetterDialog.ShowDialog(Settings.Default.S2MessgeHeader,
                        Settings.Default.S2MessageFirst + "\n" +
                    Settings.Default.S2MessageSecond,"", "OK", "Cancel",
                        Image.FromFile("Icons\\WarningDialog.png"), true);

                    //DialogResult result = MessageBox.Show(Settings.Default.S2MessageFirst + "\n" +
                    //Settings.Default.S2MessageSecond, Settings.Default.S2MessgeHeader,
                    //    MessageBoxButtons.OKCancel);


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

                this.totalSpaceLabel.Text = totalSpaceRequired_ == 0 ? Math.Round(totalSpaceRequired_,1).ToString() + "GB" :
                    Math.Round(totalSpaceRequired_ + Settings.Default.TotalSizeGap, 1).ToString() + "GB";
            }
        }

        private void HelpButtonClick(object sender, EventArgs e)
        {
            //Help button url.
            System.Diagnostics.Process.Start(Settings.Default.S2Link);
        }

    }

    //Class to store volume info information.
    public class VolumeInfo
    {   
        [DisplayName("       ")]
        public bool IsChecked { get; set; }

        [DisplayName("        ")]
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
