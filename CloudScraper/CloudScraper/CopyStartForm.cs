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
        
        private System.Timers.Timer timer_ = new System.Timers.Timer();
        delegate void MyDelegate();
        public object lockObject;
        public bool migrateStopped;
        public bool withError;

        public CopyStartForm(SaveTransferTaskForm saveTransferForm)
        {
            this.lockObject = new Object();
            this.messages_ = new BindingList<MessageInfo>();

            this.saveTransferForm_ = saveTransferForm;
            ResumeTransferForm.resumeUpload_ = false; 
            ResumeTransferForm.skipUpload_ = false; 
            ResumeTransferForm.resumeFilePath_ = null;
            
            InitializeComponent();
            this.Text = Settings.Default.S7Header;
            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            withError = false;
            
        }

        public CopyStartForm(ResumeTransferForm resumeTransferForm)
        {
            this.lockObject = new Object();
            this.messages_ = new BindingList<MessageInfo>();
            this.resumeTransferForm_ = resumeTransferForm;
            
            InitializeComponent();
            this.Text = Settings.Default.S7Header;
            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            this.Hide();

            if (this.saveTransferForm_ != null && this.resumeTransferForm_ == null)
            {
                this.saveTransferForm_.Show();
            }

            if (this.resumeTransferForm_ != null && this.saveTransferForm_ == null)
            {
                this.resumeTransferForm_.Show();
            }
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            this.backButton.Enabled = false;

            if (!ResumeTransferForm.resumeUpload_ && !ResumeTransferForm.skipUpload_ && ResumeTransferForm.resumeFilePath_ == null)
            {
                using (StreamWriter stream = new StreamWriter(SaveTransferTaskForm.transferPath_, false))
                {
                    stream.WriteLine("[EC2]");
                    stream.WriteLine("region = " + CloudParametersForm.region_);
                    stream.WriteLine("zone = " + CloudParametersForm.zone_);
                    if (CloudParametersForm.advanced_)
                    {
                        stream.WriteLine("instance-type = " + CloudParametersForm.type_);
                    }
                    else
                    {
                        stream.WriteLine("instance-type = ");
                    }
                    stream.WriteLine("target-arch = x86_64");
                    stream.WriteLine("s3prefix = " + CloudParametersForm.folderKey_);
                    stream.WriteLine("s3key = " + CloudParametersForm.awsId_);
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

            using (StreamWriter stream = new StreamWriter("migrate.cmd", false))
            {
                stream.WriteLine("@echo off");
                stream.WriteLine("set PATH=%PATH%;%~dp0\\3rdparty\\Portable_Python_2.7.3.1\\App");
                stream.WriteLine("cd /d \"%~dp0\\Migrate\\Migrate\"");
                if (!ResumeTransferForm.resumeUpload_ && !ResumeTransferForm.skipUpload_ && ResumeTransferForm.resumeFilePath_ == null)
                {
                    stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                        " -k " + CloudParametersForm.awsKey_ +
                        " -c " + SaveTransferTaskForm.transferPath_ +
                        " -o " + Directory.GetCurrentDirectory() + "\\test.txt");
                }
                else if (ResumeTransferForm.resumeUpload_)
                {
                    stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                        " --resumeupload " +
                        " -k " + ResumeTransferForm.awsKey_ +
                        " -c " + ResumeTransferForm.resumeFilePath_ +
                        " -o " + Directory.GetCurrentDirectory() + "\\test.txt");
                }
                else if (ResumeTransferForm.skipUpload_)
                {
                    stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                        " --skipupload " +
                        " -k " + ResumeTransferForm.awsKey_ +
                        " -c " + ResumeTransferForm.resumeFilePath_ +
                        " -o " + Directory.GetCurrentDirectory() + "\\test.txt");
                }
                else if (ResumeTransferForm.resumeFilePath_ != null)
                {
                    stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                        " -k " + ResumeTransferForm.awsKey_ +
                        " -c " + ResumeTransferForm.resumeFilePath_ +
                        " -o " + Directory.GetCurrentDirectory() + "\\test.txt");
                }
            }

            if (File.Exists("test.txt"))
                File.Delete("test.txt");

            //if (File.Exists("testcopy.txt"))
            //    File.Delete("testcopy.txt");

            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo("migrate.cmd");
            info.UseShellExecute = true;
            p.StartInfo = info;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Exited += new EventHandler(p_Exited);
            p.EnableRaisingEvents = true;
            p.Start();
            this.startButton.Enabled = false;
            this.backButton.Enabled = false;
            this.migrateStopped = false;

            //timer_.Interval = 1000;
            //timer_.Elapsed += new System.Timers.ElapsedEventHandler(timer__Elapsed);
            //timer_.Start();

            //Thread.Sleep(2000);
            Thread task = new Thread(new ThreadStart(this.Work));
            task.Priority = ThreadPriority.Normal;
            task.IsBackground = true;
            task.Start();

            //this.SendMail();
        }


        public void Work()
        {
            lock(this.lockObject)
            {
                long length = 0;
                long lineNumber = 0;
                while (true)
                {
                    Thread.Sleep(3000);

                    if (File.Exists("test.txt"))
                    {
                        //if (File.Exists("testcopy.txt"))
                        //    File.Delete("testcopy.txt");

                        //File.Copy("test.txt", "testcopy.txt");
                        //StreamReader stream = new StreamReader("testcopy.txt");

                        var fs = new FileStream("test.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        StreamReader stream = new StreamReader(fs);
                        
                        if (stream.BaseStream.Length == length)
                        {
                            stream.Close();
                            if (this.migrateStopped)
                            {
                                
                                this.BeginInvoke(new MyDelegate(() =>
                                {
                                    this.startButton.Visible = false;
                                    this.finishButton.Visible = true;
                                    if (File.Exists("test.txt"))
                                    this.fullOutputButton.Visible = true;

                                    if (withError)
                                        this.mailButton.Visible = true;
                                    //if (File.Exists("testcopy.txt"))
                                    //    File.Delete("testcopy.txt");
                                }));
                                return;
                            }
                            //Thread.Sleep(1000);
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
                    else if (this.migrateStopped)
                    {
                        
                        this.BeginInvoke(new MyDelegate(() =>
                        {
                            this.startButton.Visible = false;
                            this.finishButton.Visible = true;
                            if (File.Exists("test.txt"))
                                this.fullOutputButton.Visible = true;
                            if (this.withError)
                                this.mailButton.Visible = true;

                        }));
                        return;
                    }
                }                           
            }
        }

        public void InsertMessage(string str)
        {
            if (str.Length > 3 && str.Substring(0, 3) == ">>>")
            {
                while (str[0] == '>')
                {
                    str = str.Remove(0, 1);
                }
                this.messages_.Add(new MessageInfo()
                {
                    Image = new Bitmap(Image.FromFile("Icons\\arrow.png"), new Size(16, 16)),
                    Message = str,
                    Type = 1
                });
                return;
            }

            if (str.Length > 3 && str.Substring(0, 3) == "!!!")
            {
                this.messages_.Add(new MessageInfo()
                {
                    Image = new Bitmap(Image.FromFile("Icons\\error.png"), new Size(16, 16)),
                    Message = str.Remove(0, 3),
                    Type = 2
                });
                withError = true;
                return;
            }

            if (str.Length >= 2 && str.Substring(0, 1) == "!")
            {
                this.messages_.Add(new MessageInfo()
                {
                    Image = new Bitmap(Image.FromFile("Icons\\warning.png"), new Size(16, 16)),
                    Message = str.Remove(0, 1),
                    Type = 3
                });
                return;
            }

            if (str.Length > 2 && str.Substring(0, 1) == "%")
            {
                if (this.messages_[this.messages_.Count - 1].Type == 4)
                    this.messages_.RemoveAt(this.messages_.Count - 1);
                this.messages_.Add(new MessageInfo()
                {
                    Image = new Bitmap(Image.FromFile("Icons\\hourglass.png"), new Size(16, 16)),
                    Message = str.Remove(0,2),
                    Type = 4
                });
                return;
            }
        }

        void p_Exited(object sender, EventArgs e)
        {
            this.migrateStopped = true;
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            if (saveTransferForm_ != null)
            {
                this.saveTransferForm_.Close();
            }

            if (this.resumeTransferForm_ != null)
            {
                this.resumeTransferForm_.Close();
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

                string file = "test.txt";
                FastZip fz = new FastZip();
                Directory.CreateDirectory("ToZip");
                File.Copy("test.txt", "ToZip\\test.txt");
                fz.CreateZip("test.zip", "ToZip", true, null);
                Directory.Delete("ToZip", true);

                Attachment attach = new Attachment("test.zip", MediaTypeNames.Application.Octet);

                ContentDisposition disposition = attach.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);

                Message.Attachments.Add(attach);
                Smtp.Send(Message);
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        private void CopyStartForm_Load(object sender, EventArgs e)
        {
            this.messageGridView.DataSource = this.messages_;
            this.messageGridView.AutoResizeColumn(0);
        }


        private void finishButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fullOutputButton_Click(object sender, EventArgs e)
        {
            FullOutputForm form = new FullOutputForm("test.txt");
            form.ShowDialog();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Settings.Default.S7Link);
        }

        private void MailButtonClick(object sender, EventArgs e)
        {
            MailForm mail = new MailForm(this);
            mail.ShowDialog();
        }

    
    }

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
