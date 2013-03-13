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

namespace CloudScraper
{
    public partial class CopyStartForm : Form
    {
        SaveTransferTaskForm saveTransferForm_;
        ResumeTransferForm resumeTransferForm_;

        public CopyStartForm(SaveTransferTaskForm saveTransferForm)
        {
            this.saveTransferForm_ = saveTransferForm;

            CopyStartForm.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        public CopyStartForm(ResumeTransferForm resumeTransferForm)
        {
            this.resumeTransferForm_ = resumeTransferForm;
            InitializeComponent();
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

            Process p = new Process();

            ProcessStartInfo info = new ProcessStartInfo(Directory.GetCurrentDirectory() + "\\CloudScraper.exe");
            p.StartInfo = info;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.Exited += new EventHandler(p_Exited);

            p.EnableRaisingEvents = true;
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
            //Авторизация на SMTP сервере
            SmtpClient Smtp = new SmtpClient("smtp.mail.ru", 25);
            Smtp.Credentials = new NetworkCredential("login", "pass");
            //Smtp.EnableSsl = false;

            //Формирование письма
            MailMessage Message = new MailMessage();
            Message.From = new MailAddress("from@mail.ru");
            Message.To.Add(new MailAddress("to@mail.ru"));
            Message.Subject = "Заголовок";
            Message.Body = "Сообщение";

            //Прикрепляем файл
            string file = "C:\\file.zip";
            Attachment attach = new Attachment(file, MediaTypeNames.Application.Octet);

            // Добавляем информацию для файла
            ContentDisposition disposition = attach.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);

            Message.Attachments.Add(attach);

            Smtp.Send(Message);//отправка
        }
    }
}
