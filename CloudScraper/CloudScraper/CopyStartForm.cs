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
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

using CloudScraper.Properties;

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
        private bool migrateStopped;

        public CopyStartForm(SaveTransferTaskForm saveTransferForm)
        {
            this.lockObject = new Object();
            this.messages_ = new BindingList<MessageInfo>();

            this.saveTransferForm_ = saveTransferForm;
            
            InitializeComponent();
            this.messageGridView.DataSource = this.messages_;
        }

        public CopyStartForm(ResumeTransferForm resumeTransferForm)
        {
            this.lockObject = new Object();
            this.messages_ = new BindingList<MessageInfo>();
            this.resumeTransferForm_ = resumeTransferForm;
            
            InitializeComponent();
            this.messageGridView.DataSource = this.messages_;
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
                        " -resume-upload " +
                        //" -k " + CloudParametersForm.awsKey_ +
                        " -c " + ResumeTransferForm.resumeFilePath_ +
                        " -o " + Directory.GetCurrentDirectory() + "\\test.txt");
                }
                else if (ResumeTransferForm.skipUpload_)
                {
                    stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                        " -skip-upload " +
                        //" -k " + CloudParametersForm.awsKey_ +
                        " -c " + ResumeTransferForm.resumeFilePath_ +
                        " -o " + Directory.GetCurrentDirectory() + "\\test.txt");
                }
                else if (ResumeTransferForm.resumeFilePath_ != null)
                {
                    stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                        //" -k " + CloudParametersForm.awsKey_ +
                        " -c " + ResumeTransferForm.resumeFilePath_ +
                        " -o " + Directory.GetCurrentDirectory() + "\\test.txt");
                }
            }

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

            //timer_.Interval = 10000;
            //timer_.Elapsed += new System.Timers.ElapsedEventHandler(timer__Elapsed);
            //timer_.Start();

            
            Thread task = new Thread(new ThreadStart(this.Work));
            task.Start();
            //this.processListBox.Items.Add("Process start...");
            //Thread.Sleep(200000);
           
            //lock(this.lockObject)
            //{
            //}

            //this.processListBox.Items.Add("Process stoped...");
            //this.startButton.Enabled = true;
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
                            if (File.Exists("test.txt"))
                            {
                                Thread.Sleep(1000);

                                StreamReader stream = new StreamReader("test.txt");

                                if (stream.BaseStream.Length == length)
                                {
                                    stream.Close();
                                    if (this.migrateStopped)
                                    {
                                        return;
                                    }
                                    Thread.Sleep(1000);
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
                                return;
                            }
                        }                           
            }
        }

        public void InsertMessage(string str)
        {
            if (str.Length > 3 && str.Substring(0, 3) == ">>>")
            {
                this.messages_.Insert(0, new MessageInfo()
                {
                    Image = new Bitmap(Image.FromFile("Icons\\arrow.png"), new Size(16, 16)),
                    Message = str
                });
                return;
            }

            if (str.Length > 3 && str.Substring(0, 3) == "!!!")
            {
                this.messages_.Insert(0, new MessageInfo()
                {
                    Image = new Bitmap(Image.FromFile("Icons\\error.png"), new Size(16, 16)),
                    Message = str
                });
                return;
            }

            if (str.Length >= 2 && str.Substring(0, 1) == "!")
            {
                this.messages_.Insert(0, new MessageInfo()
                {
                    Image = new Bitmap(Image.FromFile("Icons\\warning.png"), new Size(16, 16)),
                    Message = str
                });
                return;
            }
        }

        void timer__Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.messageGridView.BeginInvoke(new MyDelegate(() =>
            {
                this.Work();
            })); 
        }

        
        void p_Exited(object sender, EventArgs e)
        {
            this.migrateStopped = true;

            this.BeginInvoke(new MyDelegate(() =>
            {
                this.startButton.Visible = false;
                this.finishButton.Visible = true;
            }));
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

        public void SendMail()
        {
            SmtpClient Smtp = new SmtpClient("smtp.mail.ru", 25);
            Smtp.Credentials = new NetworkCredential("login", "pass");
            //Smtp.EnableSsl = false;


            MailMessage Message = new MailMessage();
            Message.From = new MailAddress("from@mail.ru");
            Message.To.Add(new MailAddress("to@mail.ru"));
            Message.Subject = "Заголовок";
            Message.Body = "Сообщение";


            string file = "C:\\file.zip";
            Attachment attach = new Attachment(file, MediaTypeNames.Application.Octet);

 
            ContentDisposition disposition = attach.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);

            Message.Attachments.Add(attach);

            Smtp.Send(Message);
        }

        private void CopyStartForm_Load(object sender, EventArgs e)
        {
            this.messageGridView.Columns[0].Width = 50;
            this.messageGridView.Columns[0].ReadOnly = true;
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    
    }

    public class MessageInfo
    {
        [DisplayName(" ")]
        public Image Image { get; set; }

        [DisplayName("Message")]
        public string Message { get; set; }

    }
}
