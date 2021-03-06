﻿using System;
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

namespace CloudScraper
{
    public partial class SaveTransferTaskForm : Form
    {
        public static string transferPath_;
        private static Logger logger_ = LogManager.GetLogger("SaveTransferTaskForm");

        ImagesPathForm imagesPathForm_;
        CloudParametersForm cloudParametersForm_;
        CopyStartForm copyStartForm_;

        public SaveTransferTaskForm(CloudParametersForm cloudParametersForm)
        {
            this.cloudParametersForm_ = cloudParametersForm;
            InitializeComponent();
            this.SetSaveTransferTaskForm();
        }
        
        public SaveTransferTaskForm(ImagesPathForm imagesPathForm)
        {
            this.imagesPathForm_ = imagesPathForm;
            InitializeComponent();
            this.SetSaveTransferTaskForm();
        }

        private void SetSaveTransferTaskForm()
        {
            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.toolTip.SetToolTip(this.helpButton, Settings.Default.HelpButtonToolTip);

            if (!Directory.Exists(Environment.GetEnvironmentVariable("USERPROFILE") + "\\CloudScraper"))
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("USERPROFILE") + "\\CloudScraper");

            this.saveTransferTextBox.Text = Environment.GetEnvironmentVariable("USERPROFILE") + "\\CloudScraper\\transfer.ini";
            transferPath_ = this.saveTransferTextBox.Text;
            this.Text = Settings.Default.S6Header;
            this.mainLabel.Text = Settings.Default.S6MainLabelText;
            this.nextButton.Text = Settings.Default.S6NextButtonText;
            this.toolTip.SetToolTip(this.nextButton, Settings.Default.S6NextButtonToolTip);
            this.backButton.Text = Settings.Default.S6BackButtonText;
            this.toolTip.SetToolTip(this.backButton, Settings.Default.S6BackButtonToolTip);
            this.browseButton.Text = Settings.Default.S6BrowseButtonText;
            this.toolTip.SetToolTip(this.browseButton, Settings.Default.S6BrowseButtonToolTip);
            this.logoPicture.Image = new Bitmap(Image.FromFile("Icons\\logo4a.png"));
        }

        private void saveTransferTask_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.RestoreDirectory = true; // needed for WinXP and Win2003
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
            if (this.imagesPathForm_ != null)
            {
                if (logger_.IsDebugEnabled)
                    logger_.Debug("Back to the ImagesPathForm");
                
                this.Hide();
                this.imagesPathForm_.StartPosition = FormStartPosition.Manual;
                this.imagesPathForm_.Location = this.Location;
                this.imagesPathForm_.Show();
            }
            else if (this.cloudParametersForm_ != null)
            {
                if (logger_.IsDebugEnabled)
                    logger_.Debug("Back to the CloudParametersForm");
                
                this.Hide();
                this.cloudParametersForm_.StartPosition = FormStartPosition.Manual;
                this.cloudParametersForm_.Location = this.Location;
                this.cloudParametersForm_.Show();
            }
        }

        private void NextButtonClick(object sender, EventArgs e)
        {
            if (File.Exists(transferPath_))
            {
                DialogResult result = BetterDialog.ShowDialog(Settings.Default.S6WarningHeader,
                Settings.Default.S6WarningMessage, "", "OK", "Cancel",
                Image.FromFile("Icons\\WarningDialog.png"), true);
                
                if (result == DialogResult.Cancel)
                    return;
            }
            else
            {
                try
                {
                    if (Path.IsPathRooted(transferPath_))
                    {
                        string root = Path.GetPathRoot(transferPath_);
                        if (!Directory.Exists(Path.GetDirectoryName(transferPath_)))
                            Directory.CreateDirectory(Path.GetDirectoryName(transferPath_));

                        transferPath_ = transferPath_.Replace(root, "");

                        foreach (char c in Path.GetInvalidPathChars())
                        {
                            if (transferPath_.Contains(c.ToString()) ||
                                transferPath_.Contains("/") ||
                                transferPath_.Contains("\\\\") ||
                                transferPath_.Contains(":") ||
                                transferPath_.Contains("*") ||
                                transferPath_.Contains("?") ||
                                transferPath_.Contains("\"") ||
                                transferPath_.Contains("<") || transferPath_.Contains(">") ||
                                transferPath_.Contains("|"))
                            {
                                DialogResult result = BetterDialog.ShowDialog(Settings.Default.S6WarningHeader,
                                Settings.Default.S6WrongSymbolsWarningMessage, "", "OK", "OK",
                                Image.FromFile("Icons\\ErrorDialog.png"), false);

                                return;
                            }
                        }

                        transferPath_ = transferPath_.Insert(0, root);
                    }
                    else
                    {

                        DialogResult result = BetterDialog.ShowDialog(Settings.Default.S6WarningHeader,
                        Settings.Default.S6PathIncorrectWarningMessage, "", "OK", "OK",
                        Image.FromFile("Icons\\WarningDialog.png"), false);

                        return;
                    }
                }
                catch (Exception expt)
                {
                    if (logger_.IsErrorEnabled)
                        logger_.Error(expt.Message);

                    DialogResult result = BetterDialog.ShowDialog(Settings.Default.S6WarningHeader,
                        Settings.Default.S6PathIncorrectWarningMessage, "", "OK", "OK",
                        Image.FromFile("Icons\\WarningDialog.png"), false);

                    return;
                }
            }

            if (logger_.IsDebugEnabled)
                logger_.Debug("Next to the CopyStartForm");
            
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

        private void HelpButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Settings.Default.S6Link);
        }

        private void SaveTransferChanged(object sender, EventArgs e)
        {
            transferPath_ = this.saveTransferTextBox.Text;
            if (transferPath_ == "" || !transferPath_.Contains("."))
            {
                this.nextButton.Enabled = false;
            }
            else
            {
                this.nextButton.Enabled = true;
            }
        }

        private void SaveTransferTaskLoad(object sender, EventArgs e)
        {
            mainLabel.BackColor = Color.Transparent;
            logoPicture.BackColor = Color.Transparent;
            helpButton.BackColor = Color.Transparent;
            
            if (logger_.IsDebugEnabled)
                logger_.Debug("Form loaded.");
            
            this.StartPosition = FormStartPosition.Manual;
            if (this.imagesPathForm_!= null)
                this.Location = this.imagesPathForm_.Location;
            if (this.cloudParametersForm_ != null)
                this.Location = this.cloudParametersForm_.Location;
        }
    }
}
