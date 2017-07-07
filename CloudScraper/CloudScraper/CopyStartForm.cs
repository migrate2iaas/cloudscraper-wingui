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
using System.Collections;
using AssemblaAPI;

using System.Collections.Specialized;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

using CloudScraper.Properties;
using ICSharpCode.SharpZipLib.Zip;

using NLog;
using DotNetPerls;

namespace CloudScraper
{
    public partial class CopyStartForm : Form
    {   
        SaveTransferTaskForm saveTransferForm_;
        ResumeTransferForm resumeTransferForm_;

        private static Logger logger_ = LogManager.GetLogger("CopyStartForm");

        public BindingList<MessageInfo> messages_;
        public object lockObject;
        public bool migrateStopped;
        public bool withError;
        private bool isAmazon;
        private bool isElasticHosts;
        private bool isAzure;
        private string password;

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

            if (AmazonCloudParameters.isAmazon_)
            {
                this.isAmazon = true;
                this.password = AmazonCloudParameters.awsKey_;
            }
            if (EHCloudParameters.isElasticHosts_)
            {
                this.isElasticHosts = true;
                this.password = EHCloudParameters.apiKey_;
            }
            if (AzureCloudParameters.isAzure_)
            {
                this.isAzure = true;
                this.password = AzureCloudParameters.primaryAccessKey_;
            }


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

            // TODO: make enum/constants for cloud names
            if (resumeTransferForm.getCloudName() == "EC2")
            {
                this.isAmazon = true;
            }
            else if (resumeTransferForm.getCloudName() == "ElasticHosts")
            {
                this.isElasticHosts = true;
            }
            else if (resumeTransferForm.getCloudName() == "Azure")
            {
                this.isAzure = true;
            }
            this.password = resumeTransferForm.getPassword();

            InitializeComponent();

            if (logger_.IsDebugEnabled)
            {
                string currentdir = Directory.GetCurrentDirectory();
                logger_.Debug("Initing from Resume dialog. Current directory is  " + currentdir);
            }
            
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

        private void GenerateInf()
        {
            var utf16 = new System.Text.UnicodeEncoding(false, true);
            using (StreamWriter stream = new StreamWriter(SaveTransferTaskForm.transferPath_, false, utf16))
            {
                if (this.isAmazon)
                {
                    // we use VHD only if system natively support this format
                    string imagetype = "RAW";
                    bool win2008r2_or_above = ((OSInfo.MajorVersion >= 6) && (OSInfo.MinorVersion >= 1));
                    if (win2008r2_or_above)
                    {
                        if (logger_.IsDebugEnabled)
                             logger_.Debug("Using VHD image type for Windows 2008R2+");
                        imagetype = "VHD";
                    }
                    else
                    {
                       if (logger_.IsDebugEnabled)
                            logger_.Debug("Using RAW image for Windows version " + System.Environment.Version.Major.ToString() + "." + System.Environment.Version.Minor.ToString());
                    }

                    Debug.Assert(null != AmazonCloudParameters.userInput_);
                    AmazonCloudParameters.userInput_.WriteToIni(stream);

                    stream.WriteLine("[Image]");
                    stream.WriteLine("image-dir = " + ImagesPathForm.imagesPath_);
                    stream.WriteLine("source-arch = x86_64");
                    stream.WriteLine("image-type = " + imagetype);
                }
                else if (this.isElasticHosts)
                {
                    stream.WriteLine("[ElasticHosts]");
                    stream.WriteLine("region = " + EHCloudParameters.region_);
                    stream.WriteLine("user-uuid = " + EHCloudParameters.uuid_);
                    if (EHCloudParameters.drivesList_.Count != 0)
                    {
                        string str = "avoid-disks = ";
                        foreach (string disk in EHCloudParameters.drivesList_)
                        {
                            if (EHCloudParameters.drivesList_.IndexOf(disk) !=
                                EHCloudParameters.drivesList_.Count - 1)
                                str += disk + "; ";
                            else
                                str += disk;
                        }
                        stream.WriteLine(str);
                    }
                    if (EHCloudParameters.useDeduplication_)
                        stream.WriteLine("deduplication = True");
                    stream.WriteLine("[Image]");
                    if (!EHCloudParameters.directUpload_)
                        stream.WriteLine("image-dir = " + ImagesPathForm.imagesPath_);
                    stream.WriteLine("source-arch = x86_64");
                    if (EHCloudParameters.directUpload_)
                        stream.WriteLine("image-type = raw");
                    else
                        stream.WriteLine("image-type = raw.tar");
                    stream.WriteLine("image-chunck = 4194304");
                    stream.WriteLine("compression =" + EHCloudParameters.compressionLevel_.ToString());
                    if (EHCloudParameters.directUpload_)
                        stream.WriteLine("image-placement = direct");
                    else
                        stream.WriteLine("image-placement = local");
                }
                else if (this.isAzure && null != AzureCloudParameters.params_)
                {
                    AzureCloudParameters.params_.WriteToIni(stream);
                    stream.WriteLine("[Image]");
                    stream.WriteLine("image-dir = " + ImagesPathForm.imagesPath_);
                    stream.WriteLine("source-arch = x86_64");
                    stream.WriteLine("image-type = fixed.VHD");
                    stream.WriteLine("image-chunck = 4194304");
                    stream.WriteLine("image-placement = local");
                }
                // include DR to perform resume uploads by default
                stream.WriteLine("[DR]");
                stream.WriteLine("increment_depth = 1");
                stream.WriteLine("manifest_path = " + Directory.GetCurrentDirectory()+"\\backup-manifest");

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

        private void StartCopyProcess()
        {
            // arguments for migrate.py
            string arguments = "";
            string passwordarg = "";
            if (this.isAmazon)
                passwordarg = " -k " + this.password;
            else if (this.isElasticHosts)
                passwordarg = " --ehkey " + this.password;
            else if (this.isAzure)
                passwordarg = " --azurekey " + this.password;
            //using (StreamWriter stream = new StreamWriter("migrate.cmd", false))
            //{
            //stream.WriteLine("@echo off");
            //stream.WriteLine("set PATH=%PATH%;%~dp0\\3rdparty\\Portable_Python_2.7.3.1\\App");
            //stream.WriteLine("cd /d \"%~dp0\\Migrate\\Migrate\"");
            if (!ResumeTransferForm.resumeUpload_ && !ResumeTransferForm.skipUpload_ && ResumeTransferForm.resumeFilePath_ == null)
            {
                //stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                //Encoding.UTF8.GetString(Encoding.ASCII.GetBytes(SaveTransferTaskForm.transferPath_))
                arguments +=
                passwordarg +
                " -c " + "\"" + SaveTransferTaskForm.transferPath_ + "\"" +
                " -o " + "\"" + Directory.GetCurrentDirectory() + "\\" + Properties.Settings.Default.TextFile + "\"";
                //);
            }
            else if (ResumeTransferForm.resumeUpload_)
            {
                //stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                arguments =
                    passwordarg +
                    " --resumeupload " +
                    " -c " + "\"" + ResumeTransferForm.resumeFilePath_ + "\"" +
                    " -o " + "\"" + Directory.GetCurrentDirectory() + "\\" + Properties.Settings.Default.TextFile + "\"";
                //);
            }
            else if (ResumeTransferForm.skipUpload_)
            {
                //stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                arguments =
                    passwordarg +
                    " --skipupload " +
                    " -c " + "\"" + ResumeTransferForm.resumeFilePath_ + "\"" +
                    " -o " + "\"" + Directory.GetCurrentDirectory() + "\\" + Properties.Settings.Default.TextFile + "\"";
                //);
            }
            else if (ResumeTransferForm.resumeFilePath_ != null)
            {
                //stream.WriteLine("..\\..\\3rdparty\\Portable_Python_2.7.3.1\\App\\python.exe migrate.py" +
                arguments =
                    passwordarg +
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
                + ";" + Directory.GetCurrentDirectory() + "\\3rdparty\\Python_2.7.10");
            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + "\\Migrate\\Migrate");

            if (!File.Exists("..\\..\\3rdparty\\Python_2.7.10\\python.exe") ||
                (!File.Exists("migrate.py") && !File.Exists("migrate.pyc")))
            {
                //There are no python.exe
                DialogResult result = BetterDialog.ShowDialog(Settings.Default.S7PythonErrorHeader,
                    Settings.Default.S7PythonErrorMessage, "", "OK", "OK",
                    Image.FromFile("Icons\\ErrorDialog.png"), false);

                return;
            }

            info.FileName = "..\\..\\3rdparty\\Python_2.7.10\\python.exe";

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
                //Logs python start.
                if (logger_.IsDebugEnabled)
                    logger_.Debug("Python started.");

                this.startButton.Enabled = false;
                this.backButton.Enabled = false;
                this.migrateStopped = false;
                //this.Cursor = Cursors.WaitCursor;

                //Start background worker for reading exchange .txt file.
                Thread task = new Thread(new ThreadStart(this.Work));
                task.Priority = ThreadPriority.Normal;
                task.IsBackground = true;
                task.Start();

                this.inProgressPictureBox.Visible = true;
                this.inProgressPictureBox.Enabled = true;
            }
            else
            {
                //Logs errors.
                if (logger_.IsErrorEnabled)
                    logger_.Error("Python started with errors.");

                //Python started with errors.
                DialogResult result = BetterDialog.ShowDialog(Settings.Default.S7PythonErrorHeader,
                    Settings.Default.S7PythonErrorMessage, "", "OK", "OK",
                    Image.FromFile("Icons\\ErrorDialog.png"), false);
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
                // Create transfer file, encode in utf with no BOM (first marking bytes)
                this.GenerateInf();
            }
            //Start python process.
            this.StartCopyProcess();
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
                                    this.inProgressPictureBox.Enabled = false;
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
                            this.inProgressPictureBox.Enabled = false;
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
                //Logs errors.
                if (logger_.IsErrorEnabled)
                    logger_.Error(e.Message);
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

            //System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcesses();
            //foreach (System.Diagnostics.Process pr in localByName)
            //{
            //    if (pr.ProcessName == "python" && pr.Id == pythonProcess.Id && !pr.HasExited)
            //    {
            //        pr.Kill();
            //    }
            //}

         
            if (pythonProcess != null && !pythonProcess.HasExited)
            {
                pythonProcess.Exited -= new EventHandler(PythonExited);
                pythonProcess.Kill();
            }
        }

        // gather logs and return a path to the newly created zip file
        private string GenerateLogsZip()
        {
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

                //if (fileToAttach == "logs\\gui.log")
                //{
                //    string fileName = Path.GetFileName(Application.StartupPath + 
                //        "\\logs\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                //    if (File.Exists(Application.StartupPath +
                //        "\\logs\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt"))
                //    {
                //        File.Copy(Application.StartupPath +
                //            "\\logs\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", Application.StartupPath + "\\ToZip\\" + fileName);
                //    }
                //    continue;
                //}
            }


            if (File.Exists(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile))
                File.Delete(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile);

            fz.CreateZip(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile,
                Application.StartupPath + "\\ToZip", true, null);


            //delete archive
            Directory.Delete(Application.StartupPath + "\\ToZip", true);

            return Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile;
        }

        private static WebResponse sendMultipartHttpRequest(string url, string authInfo, NameValueCollection values, NameValueCollection files = null)
        {
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
            // The first boundary
            byte[] boundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            // The last boundary
            byte[] trailer = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            // The first time it itereates, we need to make sure it doesn't put too many new paragraphs down or it completely messes up poor webbrick
            byte[] boundaryBytesF = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "\r\n");

            // Create the request and set parameters
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.KeepAlive = false;
            request.Accept = "*/*";
            request.Headers["Authorization"] = "Basic " + authInfo;
            request.ContentType = "multipart/form-data; boundary=" + boundary;

            // Get request stream
            Stream requestStream = request.GetRequestStream();

            foreach (string key in values.Keys)
            {
                // Write item to stream
                byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}", key, values[key]));
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                requestStream.Write(formItemBytes, 0, formItemBytes.Length);
            }

            if (files != null)
            {
                foreach (string key in files.Keys)
                {
                    if (File.Exists(files[key]))
                    {
                        int bytesRead = 0;
                        byte[] buffer = new byte[2048];
                        byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n", key, files[key]));
                        requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                        requestStream.Write(formItemBytes, 0, formItemBytes.Length);

                        using (FileStream fileStream = new FileStream(files[key], FileMode.Open, FileAccess.Read))
                        {
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                // Write file content to stream, byte by byte
                                requestStream.Write(buffer, 0, bytesRead);
                            }

                            fileStream.Close();
                        }
                    }
                }
            }

            // Write trailer and close stream
            requestStream.Write(trailer, 0, trailer.Length);
            requestStream.Close();

            return request.GetResponse();
        }

        // send a ticket via assembla API
        public void SendFreshdeskTicket(string userName, string email, string comments)
        {
            string fdDomain = "migrate2iaas"; // your freshdesk domain
            string apiKey = "0VyZehHMRCiDDU3S3jwd";
            string apiPath = "/helpdesk/tickets.json"; // API path
            string subject = "Support request submitted by " + userName;
            
            string message = "";
            foreach (MessageInfo info in messages_)
            {
                if (info.Type == 2)
                {
                    message += info.Message + "\n";
                }
            }
            message += "\n-- User comments:\n";

            message += comments;
            //string json = "{\"status\": 2, \"priority\": 1, \"email\":\"" + email + "\",\"subject\":\"" + subject + "\",\"description\":\"" + message + "\"}";

            // Title, description and email:
            NameValueCollection values = new NameValueCollection();
            values.Add("helpdesk_ticket[email]", email);
            values.Add("helpdesk_ticket[subject]", subject);
            values.Add("helpdesk_ticket[description]", message);
            values.Add("helpdesk_ticket[name]", userName);

            // Attachments:
            NameValueCollection files = new NameValueCollection();
            
            string authInfo = apiKey + ":X"; // It could be your username:password also.
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            string zipfile = GenerateLogsZip();
            files.Add("helpdesk_ticket[attachments][][resource]" , zipfile);
            
            WebResponse response = sendMultipartHttpRequest("http://" + fdDomain + ".freshdesk.com" + apiPath  /*"http://scooterlabs.com/echo"*/ , authInfo, values, files);

        }

        // send a ticket via assembla API
        public void SendAssemblaTicket(string userName, string email, string comments)
        {
            // a small hack to make assembla trustworthy
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            // keys for support user. change to any other ones in case keys are compromised
            string Key = "61468a3d89f06ca1d740";
            string Secret = "9a968afd2af6bfb238ece4731255c746816ac2f9";
		    string space = "cloudscraper";
            IAssemblaWrapper wrapper = new AssemblaWrapper();
            //string userNameToCheck = "CloudscraperSupport";
            // 1: Log in:
            wrapper.LoginWithKeyAndSecret(Key, Secret);
            Console.WriteLine(((AssemblaWrapper)(wrapper)).UserString.ToLower());

            string message = "";
            foreach (MessageInfo info in messages_)
            {
                if (info.Type == 2)
                {
                    message += info.Message + "\n";
                }
            }
            message += "\n------------------- User comments:\n";
            message += comments;

            // 2: Post a ticket with Summary, Description, Permisson, Followers and Tags:
            string ticketId = wrapper.PostTicket(
                space,
                "Support ticket submitted by " + userName,
                message,
                PermissionType.Public,
                new[] { email },
                new[] { "auto", "support" });

            // 3: Add attachement(s):

            // a) You can use any stream do define the attachement content:
            var stream = new FileStream(this.GenerateLogsZip(), FileMode.Open);
            wrapper.AddAttachement(space, ticketId, "logs.zip", stream);

            if (File.Exists(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile))
                File.Delete(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile);

            string disclaimer = Properties.Settings.Default.SupportDisclaimer;
            wrapper.AddComment(space, ticketId, disclaimer);

        }

        // send a ticket via e-mail
        public void SendMail(string userName, string email, string comments)
        {
            SmtpClient Smtp = new SmtpClient(Properties.Settings.Default.SMTPServer, 25);
            Smtp.Credentials = new NetworkCredential(Properties.Settings.Default.SMTPLogin,
                Properties.Settings.Default.SMTPPassword);
            //Smtp.EnableSsl = false;

            MailMessage Message = new MailMessage();
            Message.From = new MailAddress(Properties.Settings.Default.SMTPLogin);
            Message.ReplyTo = new MailAddress(email);
            Message.To.Add(new MailAddress(Properties.Settings.Default.SupportEmail));
            Message.Subject = "Automatic failure report by " + userName;
            Message.Body = "Description:" + "\n";
            foreach (MessageInfo info in messages_)
            {
                if (info.Type == 2)
                {
                    Message.Body += info.Message + "\n";
                }
            }
            Message.Body += "\n------------------- User comments:\n";
            Message.Body += comments;

            string zipfile = GenerateLogsZip();

            this.attach = new Attachment(zipfile, 
                MediaTypeNames.Application.Octet);

            ContentDisposition disposition = attach.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(zipfile);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(zipfile);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(zipfile);

            Message.Attachments.Add(attach);
            Smtp.Send(Message);

            this.attach.Dispose();
 
            if (File.Exists(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile))
                File.Delete(Application.StartupPath + "\\" + Properties.Settings.Default.ZipFile);
          
        }

        private void CopyStartFormLoad(object sender, EventArgs e)
        {
            inProgressPictureBox.BackColor = Color.Transparent;

            if (logger_.IsDebugEnabled)
                logger_.Debug("Form load.");
            
            this.StartPosition = FormStartPosition.Manual;
            this.Location = this.resumeTransferForm_ != null ? this.resumeTransferForm_.Location : this.saveTransferForm_.Location; 
            this.messageGridView.DataSource = this.messages_;
            this.AdjustColumnOrder(this.messageGridView);
            this.messageGridView.AutoResizeColumns();
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
                        //Logs error.
                        if (logger_.IsErrorEnabled)
                            logger_.Error(exept.Message);
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
            mail.Show();
        }

        private void CopyStartFormKeyDown(object sender, KeyEventArgs e)
        {
            //When "X" button pressed.
            if (e.KeyData == Keys.X)
            {
                DialogResult result = BetterDialog.ShowDialog(Settings.Default.S7WarningHeader,
                     Settings.Default.S7XKeyWarningMessage, "", "OK", "Cancel",
                    Image.FromFile("Icons\\WarningDialog.png"), true);

                if (result == DialogResult.Cancel)
                    return;

                //Kill python.exe process in TaskManager if exists.
                //System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcesses();
                //foreach (System.Diagnostics.Process pr in localByName)
                //{
                //    if (pr.ProcessName == "python" && pr.Id == pythonProcess.Id && !pr.HasExited)
                //    {
                //        try
                //        {
                //            pr.Kill();
                //        }
                //        catch(Exception expt)
                //        {
                //            //Logs error.
                //            if (logger_.IsErrorEnabled)
                //                logger_.Error(expt.Message);
                //        }
                //    }
                //}

                //Kill local process if still work.
                if (pythonProcess != null && !pythonProcess.HasExited)
                {
                    try
                    {
                        pythonProcess.Kill();
                    }
                    catch (Exception exp)
                    {
                        //Logs error.
                        if (logger_.IsErrorEnabled)
                            logger_.Error(exp.Message);
                    }
                }             
            }
        }
        private void AdjustColumnOrder(DataGridView view)
        {
            view.Columns["Image"].DisplayIndex = 0;
            view.Columns["Image"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            view.Columns["Message"].DisplayIndex = 1;
            view.Columns["Message"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
