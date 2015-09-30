using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CloudScraper.Properties;
using NLog;
using DotNetPerls;
using System.Runtime.InteropServices;


namespace CloudScraper
{
    public partial class ImagesPathForm : Form
    {
        public static string imagesPath_;

        CloudParametersForm cloudParametersForm_;
        SaveTransferTaskForm saveTransferTaskForm_;
        private static Logger logger_ = LogManager.GetLogger("ImagesPathForm");
        
        public ImagesPathForm(CloudParametersForm cloudParametersForm)
        {
            this.cloudParametersForm_ = cloudParametersForm;
            InitializeComponent();
            SetImagesPathForm();
        }

        private void SetImagesPathForm()
        {
            //Init basic UI strings from seettings file.
            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.toolTip.SetToolTip(this.helpButton, Settings.Default.HelpButtonToolTip);
            this.Text = Settings.Default.S5Header;
            this.nextButton.Text = Settings.Default.S5NextButtonText;
            this.toolTip.SetToolTip(this.nextButton, Settings.Default.S5NextButtonToolTip);
            this.backButton.Text = Settings.Default.S5BackButtonText;
            this.toolTip.SetToolTip(this.backButton, Settings.Default.S5BackButtonToolTip);
            this.browseButton.Text = Settings.Default.S5BrowseButtonText;
            this.toolTip.SetToolTip(this.browseButton, Settings.Default.S5BrowseButtonToolTip);
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
                        //If  Free Space not enough (4GB ,exact value got from config, of additional space required).
                        if (ChooseDisksForm.totalSpaceRequired_ + Settings.Default.TotalSizeGap > Math.Round((decimal)drive.AvailableFreeSpace / (1024 * 1024 * 1024), 1))
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
            if (logger_.IsDebugEnabled)
                logger_.Debug("Return to the CloudParametersForm.");
            
            this.Hide();
            if (this.cloudParametersForm_ != null)
            {
                this.cloudParametersForm_.StartPosition = FormStartPosition.Manual;
                this.cloudParametersForm_.Location = this.Location;
                this.cloudParametersForm_.Show();
            }
        }


        // helper functions\struct from WINAPI to logon to network share
        [StructLayout(LayoutKind.Sequential)]
        public struct NETRESOURCE
        {
            public int dwScope;
            public int dwType;
            public int dwDisplayType;
            public int dwUsage;
            public string lpLocalName;
            public string lpRemoteName;
            public string lpComment;
            public string lpProvider;
        }

        [DllImport("mpr.dll", EntryPoint = "WNetAddConnection2A")]
        public static extern int WNetAddConnection2(ref NETRESOURCE lpNetResource, string lpPassword, string lpUserName, int dwFlags);
        [DllImport("mpr.dll", EntryPoint = "WNetCancelConnection2A")]
        public static extern int WNetCancelConnection2(string lpName, int dwFlags, int fForce);

        // tries to make a logon to network share, will prompt user for login and password in case interactive flag specified 
        // if interactive is ommited, current credentials would be used
        private bool LogonNetworkShare(string UNCPath , bool interactive)
        {
            NETRESOURCE nr;
            string strUsername;
            string strPassword;
            nr = new NETRESOURCE();
            nr.lpRemoteName = UNCPath;
            nr.lpLocalName = null; // (DriveLetter + ":");
            strUsername = null;
            strPassword = null;

            int CONNECT_INTERACTIVE = 0x8;
            int CONNECT_PROMPT = 0x10;

            int result;
            // calls the plain C WINAPI function exported from dll
            // http://msdn.microsoft.com/en-us/library/windows/desktop/aa385413%28v=vs.85%29.aspx 
            result = WNetAddConnection2(ref nr, strPassword, strUsername, interactive?(CONNECT_PROMPT+CONNECT_INTERACTIVE):0x0);

            if (logger_.IsDebugEnabled)
                logger_.Debug("WNetAddConnection2 result is " + result.ToString());

            if ((result == 0))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool CheckValidFileSystemPath()
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
                    DialogResult result = BetterDialog.ShowDialog(Settings.Default.S5WarningHeader,
                        Settings.Default.S5WrongSymbolsWarningMessage, "", "OK", "OK",
                        Image.FromFile("Icons\\ErrorDialog.png"), false);

                    return false;
                }
            }

            imagesPath_ = imagesPath_.Insert(0, root);
            return true;
        }
        
        
        private void NextButtonClick(object sender, EventArgs e)
        {
            try
            {
                // try to login to network share
                if (Path.IsPathRooted(imagesPath_))
                {
                    string networkroot = Path.GetPathRoot(imagesPath_);
                    if (networkroot.StartsWith("\\\\"))
                    {
                        if (logger_.IsDebugEnabled)
                            logger_.Debug("Openning network path: '" + networkroot + "'");
                        if (LogonNetworkShare(networkroot, true) == false)
                        {
                            DialogResult result5 = BetterDialog.ShowDialog(Settings.Default.S5WarningHeader,
                                Settings.Default.S5PathNetworkLoginFailure, "", "OK", "OK",
                                Image.FromFile("Icons\\WarningDialog.png"), false);
                            
                            return;
                        }
                    }   
                }
                if (!Directory.Exists(imagesPath_))
                {
                    if (Path.IsPathRooted(imagesPath_))
                    {

                        if (!this.CheckValidFileSystemPath())
                            return;

                        DialogResult reslt = BetterDialog.ShowDialog(Settings.Default.S5WarningHeader,
                            Settings.Default.S5DirectoryNotExistWarningMessage, "", "OK", "Cancel",
                            Image.FromFile("Icons\\WarningDialog.png"), true);

                        if (reslt == DialogResult.OK)
                        {
                            Directory.CreateDirectory(imagesPath_);
                        }
                        else
                        {
                            return;
                        }

                    }
                    else
                    {
                        DialogResult result = BetterDialog.ShowDialog(Settings.Default.S5WarningHeader,
                         Settings.Default.S5PathIncorrectWarningMessage, "", "OK", "OK",
                         Image.FromFile("Icons\\ErrorDialog.png"), false);
                        
                        return;
                    }
                }
            }
            catch (ArgumentException expt)
            {
                if (logger_.IsErrorEnabled)
                    logger_.Error(expt.Message);

                DialogResult result2 = BetterDialog.ShowDialog(Settings.Default.S5WarningHeader,
                     Settings.Default.S5PathIncorrectWarningMessage, "", "OK", "OK",
                     Image.FromFile("Icons\\ErrorDialog.png"), false);
                
                return;
            }
            
            if (Directory.GetFiles(imagesPath_).Length != 0)
            {
                DialogResult result = BetterDialog.ShowDialog(Settings.Default.S5WarningHeader,
                    Settings.Default.S5WarningMessage, "", "OK", "Cancel",
                    Image.FromFile("Icons\\WarningDialog.png"), true);

                if (result == DialogResult.Cancel)
                    return;
            }

            if (logger_.IsDebugEnabled)
                logger_.Debug("Next to the SaveTransferTaskForm.");
            
            this.Hide();

            if (this.saveTransferTaskForm_ == null)
            {
                this.saveTransferTaskForm_ = new SaveTransferTaskForm(this);
            }

            this.saveTransferTaskForm_.ShowDialog();
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            if (this.cloudParametersForm_ != null)
                this.cloudParametersForm_.Close();
        }

        private void ImagesPathFormLoad(object sender, EventArgs e)
        {
            logoPicture.BackColor = Color.Transparent;
            helpButton.BackColor = Color.Transparent;
            mainLabel.BackColor = Color.Transparent;
            totalSpaceLabel.BackColor = Color.Transparent;
            totalSpace.BackColor = Color.Transparent;
            freeSpaceLabel.BackColor = Color.Transparent;
            freeSpace.BackColor = Color.Transparent;
            errorLabel.BackColor = Color.Transparent;
            errorPicture.BackColor = Color.Transparent;

            if (logger_.IsDebugEnabled)
                logger_.Debug("Form loaded.");
            
            this.StartPosition = FormStartPosition.Manual;
            if (this.cloudParametersForm_ != null)
                this.Location = this.cloudParametersForm_.Location;

            this.totalSpace.Text = (ChooseDisksForm.totalSpaceRequired_ + Settings.Default.TotalSizeGap).ToString() + "GB";

            DriveInfo[] drives = DriveInfo.GetDrives();
            DriveInfo driveHi = null;
            
            foreach (DriveInfo drive in drives)
            {
                if (drive.DriveType == DriveType.Fixed && drive.IsReady && (driveHi == null || drive.AvailableFreeSpace > driveHi.AvailableFreeSpace))
                {
                    driveHi = drive;
                }
            }

            this.browseTextBox.Text = driveHi.Name + "cloudscraper-images\\" + DateTime.Now.ToString("yyyy-MM-dd");
            imagesPath_ = this.browseTextBox.Text;

            string rootName = Directory.GetDirectoryRoot(this.browseTextBox.Text);
           
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

        private void mainLabel_Click(object sender, EventArgs e)
        {

        }

        private void totalSpaceLabel_Click(object sender, EventArgs e)
        {

        }

        private void errorLabel_Click(object sender, EventArgs e)
        {

        }

        private void logoPicture_Click(object sender, EventArgs e)
        {

        }

        private void errorPicture_Click(object sender, EventArgs e)
        {

        }
    }
}
