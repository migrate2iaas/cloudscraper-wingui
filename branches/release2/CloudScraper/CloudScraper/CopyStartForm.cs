﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Web;
using System.Collections;

using System.Net;
using System.Net.Mail;
using System.Net.Mime;

using CloudScraper.Properties;
using ICSharpCode.SharpZipLib.Zip;

namespace CloudScraper
{
    public partial class CopyStartForm : Form
    {
        SaveTransferTaskForm saveTransferForm_;
        ResumeTransferForm resumeTransferForm_;

        public BindingList<MessageInfo> messages_;
        public object lockObject;
        public bool migrateStopped;
        public bool withError;

        private Attachment attach;
        
        delegate void MyDelegate();
        Process pythonProcess;

        /// <summary>
        /// There are two constructors. First for Start New way.
        /// </summary>
        /// <param name="saveTransferForm"></param>
        public CopyStartForm(SaveTransferTaskForm saveTransferForm)
        {
            this.lockObject = new Object();
            this.messages_ = new BindingList<MessageInfo>();
            this.saveTransferForm_ = saveTransferForm;
            
            ResumeTransferForm.resumeUpload_ = false; 
            ResumeTransferForm.skipUpload_ = false; 
            ResumeTransferForm.resumeFilePath_ = null;
            this.resumeTransferForm_ = null;
            
            InitializeComponent();

            // Initialize UI strings in Form. 
            this.Text = Settings.Default.S7Header;
            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.toolTip.SetToolTip(this.helpButton, Settings.Default.HelpButtonToolTip);
            this.backButton.Text = Settings.Default.S7BackButtonText;
            this.toolTip.SetToolTip(this.backButton, Settings.Default.S7BackButtonToolTip);
            this.startButton.Text = Settings.Default.S7StartButtonText;
            this.toolTip.SetToolTip(this.startButton, Settings.Default.S7StartButtonToolTip);
            this.mailButton.Text = Settings.Default.S7MailButtonText;
            this.toolTip.SetToolTip(this.mailButton, Settings.Default.S7MailButtonToolTip);
            this.fullOutputButton.Text = Settings.Default.S7FullOutputButtonText;
            this.toolTip.SetToolTip(this.fullOutputButton, Settings.Default.S7FullOutputButtonToolTip);
            
            this.withError = false;
            this.pythonProcess = null;
        }

        /// <summary>
        /// There are two constructors. Second for Resume way. 
        /// </summary>
        /// <param name="resumeTransferForm"></param>
        public CopyStartForm(ResumeTransferForm resumeTransferForm)
        {
            this.lockObject = new Object();
            this.messages_ = new BindingList<MessageInfo>();
            this.resumeTransferForm_ = resumeTransferForm;
            this.saveTransferForm_ = null;
            
            InitializeComponent();

            // Initialize UI strings in Form.
            this.Text = Settings.Default.S7Header;
            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.toolTip.SetToolTip(this.helpButton, Settings.Default.HelpButtonToolTip);
            this.backButton.Text = Settings.Default.S7BackButtonText;
            this.toolTip.SetToolTip(this.backButton, Settings.Default.S7BackButtonToolTip);
            this.startButton.Text = Settings.Default.S7StartButtonText;
            this.toolTip.SetToolTip(this.startButton, Settings.Default.S7StartButtonToolTip);
            this.mailButton.Text = Settings.Default.S7MailButtonText;
            this.toolTip.SetToolTip(this.mailButton, Settings.Default.S7MailButtonToolTip);
            this.fullOutputButton.Text = Settings.Default.S7FullOutputButtonText;
            this.toolTip.SetToolTip(this.fullOutputButton, Settings.Default.S7FullOutputButtonToolTip);

            this.withError = false;
            pythonProcess = null;
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            this.Hide();

            // For S way
            if (this.saveTransferForm_ != null && this.resumeTransferForm_ == null)
            {
                this.saveTransferForm_.StartPosition = FormStartPosition.Manual;
                this.saveTransferForm_.Location = this.Location;
                this.saveTransferForm_.Show();
            }

            // For R way
            if (this.resumeTransferForm_ != null && this.saveTransferForm_ == null)
            {
                this.resumeTransferForm_.StartPosition = FormStartPosition.Manual;
                this.resumeTransferForm_.Location = this.Location;
                this.resumeTransferForm_.Show();
            }
        }


        private void StartButtonClick(object sender, EventArgs e)
        {
            Properties.Settings.Default.TextFile = Properties.Settings.Default.TextFile.Substring(0,
                Properties.Settings.Default.TextFile.Length - 4) + "_" +
                DateTime.Now.Year.ToString() + "_" + 
                DateTime.Now.Month.ToString() + "_" + 
                DateTime.Now.Day.ToString() + "_" + 
                DateTime.Now.Hour.ToString() + "-" + 
                DateTime.Now.Minute.ToString() +
                Properties.Settings.Default.TextFile.Substring(Properties.Settings.Default.TextFile.Length - 4);
            
            this.backButton.Enabled = false;

            if (!ResumeTransferForm.resumeUpload_ && !ResumeTransferForm.skipUpload_ && ResumeTransferForm.resumeFilePath_ == null)
            {
                // Create transfer file.
                using (StreamWriter stream = new StreamWriter(SaveTransferTaskForm.transferPath_, false))
                {
                    if (AmazonCloudParameters.isAmazon_)
                    {
                        stream.WriteLine("[EC2]");
                        stream.WriteLine("region = " + AmazonCloudParameters.region_);
                        if (AmazonCloudParameters.zone_ != "")
                            stream.WriteLine("zone = " + AmazonCloudParameters.zone_);
                        if (AmazonCloudParameters.group_ != "")
                            stream.WriteLine("security-group = " + AmazonCloudParameters.group_);
                        if (AmazonCloudParameters.type_ != "")
                            stream.WriteLine("instance-type = " + AmazonCloudParameters.type_);
                        stream.WriteLine("target-arch = x86_64");
                        if (AmazonCloudParameters.folderKey_ != "")
                            stream.WriteLine("s3prefix = " + AmazonCloudParameters.folderKey_);
                        stream.WriteLine("s3key = " + AmazonCloudParameters.awsId_);
                        if (AmazonCloudParameters.s3bucket_ != "")
                            stream.WriteLine("bucket = " + AmazonCloudParameters.s3bucket_);
                        stream.WriteLine("[Image]");
                        stream.WriteLine("image-dir = " + ImagesPathForm.imagesPath_);
                        stream.WriteLine("source-arch = x86_64");
                        stream.WriteLine("image-type = VHD");
                    }
                    else if (EHCloudParameters.isElasticHosts_)
                    {
                        stream.WriteLine("[ElasticHosts]");
                        stream.WriteLine("region = " + EHCloudParameters.region_);
                        stream.WriteLine("user-uuid = " + EHCloudParameters.uuid_);
                        stream.WriteLine("[Image]");
                        if (!EHCloudParameters.directUpload_)
                            stream.WriteLine("image-dir = " + ImagesPathForm.imagesPath_);
                        stream.WriteLine("source-arch = x86_64");
                        if (EHCloudParameters.directUpload_)
                            stream.WriteLine("image-type = raw");
                        else
                            stream.WriteLine("image-type = raw.tar");
                        stream.WriteLine("image-chunck = 4194304");
                        if (EHCloudParameters.directUpload_)
                            stream.WriteLine("image-placement = direct");
                        else
                            stream.WriteLine("image-placement = local");
                    }
                    
                    stream.WriteLine("[Volumes]");
                    string letters = null;
                    foreach (string str in ChooseDisksForm.selectedDisks_)
                    {
                        letters = letters == null ? str.Replace(":\\", "") : letters + "," + str.Replace(":\\", "");
                    }
                    stream.WriteLine("letters = " + letters);
                    foreach (string str in ChooseDisksForm.selectedDisks_)
                    {
                        stream.WriteLine("[" + str.Replace(":\\", "") + "]");
                    }
                }
            }

            // arguments for migrate.py
            string arguments = "";

            //using (StreamWriter stream = new StreamWriter("migrate.cmd", false))
            //{
                //stream.WriteLine("@echo off");
                //stream.WriteLine("set PATH=%PATH%;%~dp0\\3rdparty\\Portable_Python_2.7.3.1\\App");
                //stream.WriteLine("cd /d \"%~dp0\\Migrate\\Migrate\"");
                if (!ResumeTransferForm.resumeUpload_ && !ResumeTransferForm.skipUpload_ && ResumeTransferForm.resumeFilePath_ == null)
                {
                    //stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                    if (AmazonCloudParameters.isAmazon_)
                        arguments = " -k " + AmazonCloudParameters.awsKey_;
                    else if (EHCloudParameters.isElasticHosts_)
                        arguments = " --ehkey " + EHCloudParameters.apiKey_;
                    arguments += 
                    " -c " + "\"" + SaveTransferTaskForm.transferPath_ + "\"" +
                    " -o " + "\"" + Directory.GetCurrentDirectory() + "\\" + Properties.Settings.Default.TextFile + "\"";
                    //);
                }
                else if (ResumeTransferForm.resumeUpload_)
                {
                    //stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                    arguments =
                        " --resumeupload " +
                        " -k " + ResumeTransferForm.awsKey_ +
                        " -c " + "\"" + ResumeTransferForm.resumeFilePath_ + "\"" +
                        " -o " + "\"" + Directory.GetCurrentDirectory() + "\\" + Properties.Settings.Default.TextFile + "\"";
                    //);
                }
                else if (ResumeTransferForm.skipUpload_)
                {
                    //stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                    arguments =
                        " --skipupload " +
                        " -k " + ResumeTransferForm.awsKey_ +
                        " -c " + "\"" + ResumeTransferForm.resumeFilePath_ + "\"" +
                        " -o " + "\"" + Directory.GetCurrentDirectory() + "\\" + Properties.Settings.Default.TextFile + "\"";
                    //);
                }
                else if (ResumeTransferForm.resumeFilePath_ != null)
                {
                    //stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                    arguments =
                        " -k " + ResumeTransferForm.awsKey_ +
                        " -c " + "\"" + ResumeTransferForm.resumeFilePath_ + "\"" +
                        " -o " + "\"" + Directory.GetCurrentDirectory() + "\\" + Properties.Settings.Default.TextFile + "\"";
                    //);
                }
            //}

            if (File.Exists(Application.StartupPath + "\\" + Properties.Settings.Default.TextFile))
                File.Delete(Application.StartupPath + "\\" + Properties.Settings.Default.TextFile);

            //python process.
            pythonProcess = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            
            //If we start throwgh .cmd file.
            //info.FileName = "migrate.cmd";
            Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") 
                + ";" + Directory.GetCurrentDirectory() + "\\3rdparty\\Portable_Python_2.7.3.1\\App");            
            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + "\\Migrate\\Migrate");

            if (!File.Exists("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe") || 
                (!File.Exists("migrate.py") && !File.Exists("migrate.pyc")))
            {
                //There are no python.exe
                DialogResult result = MessageBox.Show(Settings.Default.S7PythonErrorMessage,
                Settings.Default.S7PythonErrorHeader,
                MessageBoxButtons.OK);
                return;
            }
            
            info.FileName = "..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe";

            if (File.Exists("migrate.py"))
            {
                info.Arguments = "migrate.py" + arguments;
            }
            else
            {
                info.Arguments = "migrate.pyc" + arguments;
            }

            info.UseShellExecute = true;
            //info.LoadUserProfile = true;
            //info.Domain = "domainname";
            //info.UserName = System.Environment.UserName;
            //string password = "test";
            //System.Security.SecureString ssPwd = new System.Security.SecureString();
            //for (int x = 0; x < password.Length; x++)
            //{
            //    ssPwd.AppendChar(password[x]);
            //}
            //info.Password = ssPwd;

            info.UserName = System.Diagnostics.Process.GetCurrentProcess().StartInfo.UserName;
            info.Password = System.Diagnostics.Process.GetCurrentProcess().StartInfo.Password;
            
            pythonProcess.StartInfo = info;           
            pythonProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pythonProcess.Exited += new EventHandler(this.PythonExited);
            pythonProcess.EnableRaisingEvents = true;

            if (pythonProcess.Start())
            {
                this.startButton.Enabled = false;
                this.backButton.Enabled = false;
                this.migrateStopped = false;
                this.Cursor = Cursors.WaitCursor;

                //Start background worker for reading exchange .txt file.
                Thread task = new Thread(new ThreadStart(this.Work));
                task.Priority = ThreadPriority.Normal;
                task.IsBackground = true;
                task.Start();
            }
            else
            {
                //Python started with errors.
                DialogResult result = MessageBox.Show(Settings.Default.S7PythonErrorMessage,
                Settings.Default.S7PythonErrorHeader,
                MessageBoxButtons.OK);                
            }
        }


        //Background worker. Periodicly ~3 seconds read .txt exchange file and print messages. 
        public void Work()
        {
            lock(this.lockObject)
            {
                long length = 0;
                long lineNumber = 0;
                //Background worker main loop. 
                while (true)
                {
                    // ~3s sleep for reduce processor usage.
                    Thread.Sleep(3000);

                    if (File.Exists(Application.StartupPath + "\\" + Properties.Settings.Default.TextFile))
                    {
                        var fs = new FileStream(Application.StartupPath + "\\" + Properties.Settings.Default.TextFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        StreamReader stream = new StreamReader(fs);
                        
                        //There are no new messages in .txt exchange file.
                        if (stream.BaseStream.Length == length)
                        {
                            stream.Close();
                            // if python stopped.
                            if (this.migrateStopped)
                            {                               
                                this.BeginInvoke(new MyDelegate(() =>
                                {
                                    this.startButton.Visible = false;
                                    this.finishButton.Visible = true;
                                    this.Cursor = Cursors.Arrow;
                                    if (File.Exists(Application.StartupPath + "\\" + Properties.Settings.Default.TextFile))
                                    this.fullOutputButton.Visible = true;

                                    if (withError)
                                        this.mailButton.Visible = true;
                                }));
                                return;
                            }
                            continue;
                        }
                        else
                        {
                            length = stream.BaseStream.Length;
                            long currentLineNum = 0;

                            while (!stream.EndOfStream)
                            {
                                string str = stream.ReadLine();
                                currentLineNum++;

                                if (currentLineNum > lineNumber)
                                {
                                    this.messageGridView.BeginInvoke(new MyDelegate(() =>
                                    {
                                        this.InsertMessage(str);
                                    }));
                                }
                            }
                            stream.Close();
                            lineNumber = currentLineNum;
                        }
                    }
                    //If there are no .txt exchange file and python.exe stooped
                    else if (this.migrateStopped)
                    {
                        this.BeginInvoke(new MyDelegate(() =>
                        {
                            this.startButton.Visible = false;
                            this.finishButton.Visible = true;
                            this.Cursor = Cursors.Arrow;
                            if (File.Exists(Application.StartupPath + "\\" + Properties.Settings.Default.TextFile))
                                this.fullOutputButton.Visible = true;
                            if (this.withError)
                                this.mailButton.Visible = true;
                        }));
                        return;
                    }
                }                           
            }
        }

        //Insert messge in Form.
        public void InsertMessage(string str)
        {
            try
            {
                //Basic message (type = 1).
                if (str.Length > 3 && str.Substring(0, 3) == ">>>")
                {
                    while (str[0] == '>')
                    {
                        str = str.Remove(0, 1);
                    }
                    this.messages_.Add(new MessageInfo()
                    {
                        Image = new Bitmap(Image.FromFile(Application.StartupPath + "\\Icons\\arrow.png"), new Size(16, 16)),
                        Message = DateTime.Now.ToString("HH:mm:ss") + " " + str,
                        Type = 1
                    });
                    return;
                }

                //Error message (type = 2).
                if (str.Length > 3 && str.Substring(0, 3) == "!!!")
                {

                    this.messages_.Add(new MessageInfo()
                    {
                        Image = new Bitmap(Image.FromFile(Application.StartupPath + "\\Icons\\error.png"), new Size(16, 16)),
                        Message = DateTime.Now.ToString("HH:mm:ss") + " " + str.Remove(0, 3),
                        Type = 2
                    });
                    withError = true;
                    return;
                }

                //Warninig message (type = 3).
                if (str.Length >= 2 && str.Substring(0, 1) == "!")
                {
                    this.messages_.Add(new MessageInfo()
                    {
                        Image = new Bitmap(Image.FromFile(Application.StartupPath + "\\Icons\\warning.png"), new Size(16, 16)),
                        Message = DateTime.Now.ToString("HH:mm:ss") + " " + str.Remove(0, 1),
                        Type = 3
                    });
                    return;
                }

                //Time message (type = 4).
                if (str.Length > 2 && str.Substring(0, 1) == "%")
                {
                    if (this.messages_.Count != 0 && this.messages_[this.messages_.Count - 1].Type == 4)
                        this.messages_.RemoveAt(this.messages_.Count - 1);

                    this.messages_.Add(new MessageInfo()
                    {
                        Image = new Bitmap(Image.FromFile(Application.StartupPath + "\\Icons\\hourglass.png"), new Size(16, 16)),
                        Message = DateTime.Now.ToString("HH:mm:ss") + " " + str.Remove(0, 2),
                        Type = 4
                    });
                    return;
                }
            }
            catch(Exception e)
            {
                using (StreamWriter stream = new StreamWriter(CloudScraper.logPath_, true))
                {
                    stream.WriteLine(DateTime.Now.ToString() + " " + e);
                }
            }
        }

        void PythonExited(object sender, EventArgs e)
        {
            //Rise flag for Background worker.
            this.migrateStopped = true;
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            //For S way. When we start new copy process.
            if (saveTransferForm_ != null)
            {
                this.saveTransferForm_.Close();
            }

            //For R way. When we resume previous copy process.
            if (this.resumeTransferForm_ != null)
            {
                this.resumeTransferForm_.Close();
            }

            //Kill python.exe process in TaskManager if exists. 

            System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pr in localByName)
            {
                if (pr.ProcessName == "python" && pr.Id == pythonProcess.Id && !pr.HasExited)
                {
                    pr.Kill();
                }
            }

         
            if (pythonProcess != null && !pythonProcess.HasExited)
            {
                pythonProcess.Exited -= new EventHandler(PythonExited);
                pythonProcess.Kill();
            }
        }

        public void SendMail(string userName, string email, string comments)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                SmtpClient Smtp = new SmtpClient(Properties.Settings.Default.SMTPServer, 25);
                Smtp.Credentials = new NetworkCredential(Properties.Settings.Default.SMTPLogin,
                    Properties.Settings.Default.SMTPPassword);
                //Smtp.EnableSsl = false;


                MailMessage Message = new MailMessage();
                Message.From = new MailAddress(Properties.Settings.Default.SMTPLogin);
                Message.ReplyTo = new MailAddress(email);
                Message.To.Add(new MailAddress(Properties.Settings.Default.SupportEmail));
                Message.Subject = "Support ticket: failure reported by " + userName + " " + email;
                Message.Body = "Milestone: Release 0.1" + "\n" +
                               "Component: migrate.py" + "\n" +
                               "Priority: 3" + "\n" +
                               "Permission type: Public" + "\n" +
                               "Description:" + "\n";
                foreach (MessageInfo info in messages_)
                {
                    if (info.Type == 2)
                    {
                        Message.Body += info.Message + "\n";
                    }
                }
                Message.Body += "------------------- User comments:\n";
                Message.Body += comments;

                string file = Application.StartupPath + "\\" + Properties.Settings.Default.TextFile;
                FastZip fz = new FastZip();

                if (Directory.Exists(Application.StartupPath + "\\ToZip"))
                    Directory.Delete(Application.StartupPath + "\\ToZip", true);
                //Create folder for zip archive.
                Directory.CreateDirectory(Application.StartupPath + "\\ToZip");
                //Add report file.
                File.Copy(Application.StartupPath + "\\" + Properties.Settings.Default.TextFile, 
                   Application.StartupPath + "\\ToZip\\" + Properties.Settings.Default.TextFile);

                //Add atachment files from configs.
                foreach (string fileToAttach in Settings.Default.FilesToAttach)
                {
                    string fileToAttachTmp = fileToAttach;
                    //For system environment.
                    IDictionary dictionary = Environment.GetEnvironmentVariables();
                    foreach (DictionaryEntry de in dictionary)
                    {
                        string entry = "%" + de.Key.ToString() + "%";
                        if (fileToAttachTmp.ToLower().Contains(entry.ToLower()))
                        {
                            int index = fileToAttachTmp.ToLower().IndexOf(entry.ToLower());
                            fileToAttachTmp = fileToAttachTmp.Remove(index, entry.Length).Insert(index, de.Value.ToString());
                            break;
                        }
                    }
                    
                    //For absolute path.
                    if (File.Exists(fileToAttachTmp))
                    {
                        string fileName = Path.GetFileName(fileToAttachTmp);
                        File.Copy(fileToAttachTmp, Application.StartupPath + "\\ToZip\\" + fileName);
                        continue;
                    }
                    
                    //For relative path.
                    if (File.Exists(Application.StartupPath + "\\" + fileToAttachTmp))
                    {
                        string fileName = Path.GetFileName(Application.StartupPath + "\\" + fileToAttachTmp);
                        File.Copy(Application.StartupPath + "\\" + fileToAttachTmp, Application.StartupPath + "\\ToZip\\" + fileName);
                        continue;
                    }
                }


                if (File.Exists(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile))
                    File.Delete(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile);

                fz.CreateZip(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile, 
                    Application.StartupPath + "\\ToZip", true, null);


                //delete archive
                Directory.Delete(Application.StartupPath + "\\ToZip", true);

                this.attach = new Attachment(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile, 
                    MediaTypeNames.Application.Octet);

                ContentDisposition disposition = attach.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);

                Message.Attachments.Add(attach);
                Smtp.Send(Message);

               this.attach.Dispose();
 
               if (File.Exists(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile))
                    File.Delete(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile);

               this.Cursor = Cursors.Arrow;
               MessageBox.Show(Settings.Default.MailSendMessage,
                   Settings.Default.MailSendHeader, MessageBoxButtons.OK); 
            }
            catch(Exception e)
            {
                using (StreamWriter stream = new StreamWriter(CloudScraper.logPath_, true))
                {
                    stream.WriteLine(DateTime.Now.ToString() + " " + e);
                }

                this.attach.Dispose();
                this.Cursor = Cursors.Arrow;
                MessageBox.Show(e.ToString(),   
                    Settings.Default.MailSendFailedHeader,    MessageBoxButtons.OK);  
            }
        }

        private void CopyStartFormLoad(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = this.resumeTransferForm_ != null ? this.resumeTransferForm_.Location : this.saveTransferForm_.Location; 
            this.messageGridView.DataSource = this.messages_;
            this.messageGridView.AutoResizeColumn(0);
        }


        private void FinishButtonClick(object sender, EventArgs e)
        {
            //Kill python.exe process in TaskManager if exists. 
            System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pr in localByName)
            {
                if (pr.ProcessName == "python" && pr.Id == pythonProcess.Id && !pr.HasExited)
                {
                    try
                    {
                        pr.Kill();
                    }
                    catch(Exception exept)
                    {
                        using (StreamWriter stream = new StreamWriter(CloudScraper.logPath_, true))
                        {
                            stream.WriteLine(DateTime.Now.ToString() + " " + exept);
                        }
                    }
                }
            }
            Application.Exit();
        }

        private void FullOutputButtonClick(object sender, EventArgs e)
        {
            FullOutputForm form = new FullOutputForm(Application.StartupPath + "\\" + Properties.Settings.Default.TextFile);
            form.ShowDialog();
        }

        private void HelpButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Settings.Default.S7Link);
        }

        private void MailButtonClick(object sender, EventArgs e)
        {
            MailForm mail = new MailForm(this);
            mail.ShowDialog();
        }

        private void CopyStartForm_KeyDown(object sender, KeyEventArgs e)
        {
            //When "X" button pressed.
            if (e.KeyData == Keys.X)
            {

                DialogResult result = MessageBox.Show(
                Settings.Default.S7XKeyWarningMessage,
                Settings.Default.S7WarningHeader,
                MessageBoxButtons.OKCancel);

                if (result == DialogResult.Cancel)
                    return;

                //Kill python.exe process in TaskManager if exists.
                System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcesses();
                foreach (System.Diagnostics.Process pr in localByName)
                {
                    if (pr.ProcessName == "python" && pr.Id == pythonProcess.Id && !pr.HasExited)
                    {
                        try
                        {
                            pr.Kill();
                        }
                        catch(Exception expt)
                        {
                            using (StreamWriter stream = new StreamWriter(CloudScraper.logPath_, true))
                            {
                                stream.WriteLine(DateTime.Now.ToString() + " " + expt);
                            }
                        }
                    }
                }

                //Kill local process if still work.
                if (pythonProcess != null && !pythonProcess.HasExited)
                {
                    try
                    {
                        pythonProcess.Kill();
                    }
                    catch(Exception exp)
                    {
                        using (StreamWriter stream = new StreamWriter(CloudScraper.logPath_, true))
                        {
                            stream.WriteLine(DateTime.Now.ToString() + " " + exp);
                        }
                    }
                }             
            }
        }    
    }

    //Class for storage messages from .txt exchange file.
    public class MessageInfo
    {
        [DisplayName("      ")]
        public Image Image { get; set; }

        [DisplayName("Message")]
        public string Message { get; set; }

        [Browsable(false)]
        public int Type { get; set; }
    }
}