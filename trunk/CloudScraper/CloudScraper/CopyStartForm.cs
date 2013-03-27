using System;
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
        
        delegate void MyDelegate();
        Process p;

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
            this.backButton.Text = Settings.Default.S7BackButtonText;
            this.startButton.Text = Settings.Default.S7StartButtonText;
            this.mailButton.Text = Settings.Default.S7MailButtonText;
            this.fullOutputButton.Text = Settings.Default.S7FullOutputButtonText;
            
            this.withError = false;
            this.p = null;
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
            this.backButton.Text = Settings.Default.S7BackButtonText;
            this.startButton.Text = Settings.Default.S7StartButtonText;
            this.mailButton.Text = Settings.Default.S7MailButtonText;
            this.fullOutputButton.Text = Settings.Default.S7FullOutputButtonText;

            this.withError = false;
            p = null;
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
            this.backButton.Enabled = false;

            if (!ResumeTransferForm.resumeUpload_ && !ResumeTransferForm.skipUpload_ && ResumeTransferForm.resumeFilePath_ == null)
            {
                // Create transfer file.
                using (StreamWriter stream = new StreamWriter(SaveTransferTaskForm.transferPath_, false))
                {
                    stream.WriteLine("[EC2]");
                    stream.WriteLine("region = " + CloudParametersForm.region_);
                    if (CloudParametersForm.zone_ != "")
                        stream.WriteLine("zone = " + CloudParametersForm.zone_);
                    if (CloudParametersForm.type_ != "")    
                        stream.WriteLine("instance-type = " + CloudParametersForm.type_);
                    stream.WriteLine("target-arch = x86_64");
                    stream.WriteLine("s3prefix = " + CloudParametersForm.folderKey_);
                    stream.WriteLine("s3key = " + CloudParametersForm.awsId_);
                    if (CloudParametersForm.s3bucket_ != "")
                        stream.WriteLine("bucket = " + CloudParametersForm.s3bucket_);
                    stream.WriteLine("[Image]");
                    stream.WriteLine("image-dir = " + ImagesPathForm.imagesPath_);
                    stream.WriteLine("source-arch = x86_64");
                    stream.WriteLine("image-type = VHD");
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
                    arguments =
                    " -k " + CloudParametersForm.awsKey_ +
                    " -c " + SaveTransferTaskForm.transferPath_ +
                    " -o " + Directory.GetCurrentDirectory() + "\\" + Properties.Settings.Default.TextFile;
                    //);
                }
                else if (ResumeTransferForm.resumeUpload_)
                {
                    //stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                    arguments =
                        " --resumeupload " +
                        " -k " + ResumeTransferForm.awsKey_ +
                        " -c " + ResumeTransferForm.resumeFilePath_ +
                        " -o " + Directory.GetCurrentDirectory() + "\\" + Properties.Settings.Default.TextFile;
                    //);
                }
                else if (ResumeTransferForm.skipUpload_)
                {
                    //stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                    arguments =
                        " --skipupload " +
                        " -k " + ResumeTransferForm.awsKey_ +
                        " -c " + ResumeTransferForm.resumeFilePath_ +
                        " -o " + Directory.GetCurrentDirectory() + "\\" + Properties.Settings.Default.TextFile;
                    //);
                }
                else if (ResumeTransferForm.resumeFilePath_ != null)
                {
                    //stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                    arguments =
                        " -k " + ResumeTransferForm.awsKey_ +
                        " -c " + ResumeTransferForm.resumeFilePath_ +
                        " -o " + Directory.GetCurrentDirectory() + "\\" + Properties.Settings.Default.TextFile;
                    //);
                }
            //}

            if (File.Exists(Application.StartupPath + "\\" + Properties.Settings.Default.TextFile))
                File.Delete(Application.StartupPath + "\\" + Properties.Settings.Default.TextFile);

            //python process.
            p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            
            //If we start throwgh .cmd file.
            //info.FileName = "migrate.cmd";
            Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") 
                + ";" + Directory.GetCurrentDirectory() + "\\3rdparty\\Portable_Python_2.7.3.1\\App");            
            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + "\\Migrate\\Migrate");

            if (!File.Exists("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe") || !File.Exists("migrate.py"))
            {
                //There are no python.exe
                DialogResult result = MessageBox.Show(Settings.Default.S7PythonErrorMessage,
                Settings.Default.S7PythonErrorHeader,
                MessageBoxButtons.OK);
                return;
            }
            
            info.FileName = "..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe";
            info.Arguments = "migrate.py" + arguments;
            info.UseShellExecute = true;
            p.StartInfo = info;           
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Exited += new EventHandler(this.PythonExited);
            p.EnableRaisingEvents = true;

            if (p.Start())
            {
                this.startButton.Enabled = false;
                this.backButton.Enabled = false;
                this.migrateStopped = false;

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
                if (this.messages_[this.messages_.Count - 1].Type == 4)
                    this.messages_.RemoveAt(this.messages_.Count - 1);
                this.messages_.Add(new MessageInfo()
                {
                    Image = new Bitmap(Image.FromFile(Application.StartupPath + "\\Icons\\hourglass.png"), new Size(16, 16)),
                    Message = DateTime.Now.ToString("HH:mm:ss") + " " +  str.Remove(0, 2),
                    Type = 4
                });
                return;
            }
        }

        void PythonExited(object sender, EventArgs e)
        {
            //Rise flag for Background worker.
            this.migrateStopped = true;
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            //For S way.
            if (saveTransferForm_ != null)
            {
                this.saveTransferForm_.Close();
            }

            //For R way.
            if (this.resumeTransferForm_ != null)
            {
                this.resumeTransferForm_.Close();
            }

            //Kill python.exe process in TaskManager if exists. 
            System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pr in localByName)
            {
                if (pr.ProcessName == "python")
                {
                    pr.Kill();
                }
            }

            //Kill local process if still work.
            if (p != null && !p.HasExited)
            {
                p.Exited -= new EventHandler(PythonExited);
                p.Kill();
            }
        }

        public void SendMail(string userName, string email)
        {
            try
            {
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

                string file = Application.StartupPath + "\\" + Properties.Settings.Default.TextFile;
                FastZip fz = new FastZip();
                
                //Create folder for zip archive.
                Directory.CreateDirectory(Application.StartupPath + "\\ToZip");
                //Add report file.
                File.Copy(Application.StartupPath + "\\" + Properties.Settings.Default.TextFile, 
                   Application.StartupPath + "\\ToZip\\" + Properties.Settings.Default.TextFile);

                //Add atachment files from configs.
                foreach (string fileToAttach in Settings.Default.FilesToAttach)
                {
                    if (File.Exists(Application.StartupPath + "\\" + fileToAttach))
                    {
                        File.Copy(Application.StartupPath + "\\" + fileToAttach, Application.StartupPath + "\\ToZip\\" + fileToAttach);
                    }
                }

                fz.CreateZip(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile, 
                    Application.StartupPath + "\\ToZip", true, null);
                //delete archive
                Directory.Delete(Application.StartupPath + "\\ToZip", true);

                Attachment attach = new Attachment(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile, 
                    MediaTypeNames.Application.Octet);

                ContentDisposition disposition = attach.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);

                Message.Attachments.Add(attach);
                Smtp.Send(Message);

                if (File.Exists(Properties.Settings.Default.ZipFile))
                    File.Delete(Properties.Settings.Default.ZipFile);
            }
            catch (Exception e)
            {
	MessageBox.Show( e.ToString(),   "Mail Send Failed",    MessageBoxButtons.OK);  
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
                if (pr.ProcessName == "python")
                {
                    pr.Kill();
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
                //Kill python.exe process in TaskManager if exists.
                System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcesses();
                foreach (System.Diagnostics.Process pr in localByName)
                {
                    if (pr.ProcessName == "python")
                    {
                        pr.Kill();
                    }
                }

                //Kill local process if still work.
                if (p != null && !p.HasExited)
                {
                    p.Kill();
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
