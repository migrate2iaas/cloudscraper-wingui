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

namespace CloudScraper
{
    public partial class CopyStartForm : Form
    {
        SaveTransferTaskForm saveTransferForm_;
        ResumeTransferForm resumeTransferForm_;

        BindingList<MessageInfo> messages_;

        public CopyStartForm(SaveTransferTaskForm saveTransferForm)
        {
            this.messages_ = new BindingList<MessageInfo>();
            for (int i = 1; i < 1000; i++)
            {
                this.messages_.Add(new MessageInfo()
                {
                    Image = new Bitmap(Image.FromFile("Icons\\WindowsDrive.ico"), new Size(24, 24)),
                    Message = "Message"
                });
            }
            this.saveTransferForm_ = saveTransferForm;
            
            InitializeComponent();

            this.messageGridView.DataSource = this.messages_;
        }

        public CopyStartForm(ResumeTransferForm resumeTransferForm)
        {
            this.messages_ = new BindingList<MessageInfo>();
            for (int i = 1; i < 1000; i++)
            {
                this.messages_.Add(new MessageInfo()
                {
                    Image = new Bitmap(Image.FromFile("Icons\\WindowsDrive.ico"), new Size(24, 24)),
                    Message = i.ToString()
                });
            }
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

            if (!ResumeTransferForm.resumeUpload_ && !ResumeTransferForm.skipUpload_)
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
                stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                    " -k " + CloudParametersForm.awsKey_ + 
                    " -c " + SaveTransferTaskForm.transferPath_ + 
                    " -o " + Directory.GetCurrentDirectory() + "\\test.txt");
            }

            Process p = new Process();

            ProcessStartInfo info = new ProcessStartInfo("migrate.cmd");

            info.UseShellExecute = true;
            p.StartInfo = info;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Exited += new EventHandler(p_Exited);

            // p.EnableRaisingEvents = true;
            p.Start();
            this.processListBox.Items.Add("Process start...");
            p.WaitForExit();
            this.processListBox.Items.Add("Process stoped...");

            //this.SendMail();
        }

        void p_Exited(object sender, EventArgs e)
        {
            //this.processListBox.BeginInvoke(new Action<object>((obj) =>
            //{
            //    this.processListBox.Items.Add("Process stop...");
            //}));
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
    }

    public class MessageInfo
    {
        [DisplayName(" ")]
        public Image Image { get; set; }

        [DisplayName("Message")]
        public string Message { get; set; }
    }
}
