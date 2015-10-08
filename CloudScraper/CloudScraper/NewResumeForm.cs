using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CloudScraper.Properties;
using NLog;
using System.IO;


namespace CloudScraper
{
    public partial class NewResumeForm : Form
    {
        ChooseDisksForm chooseDiskForm_;
        ResumeTransferForm resumeTransferForm_;
        private static Logger logger_ = LogManager.GetLogger("NewResumeForm");
        
        public NewResumeForm()
        {
            InitializeComponent();
            
            this.startNewButton.Image = new Bitmap(Image.FromFile("Icons\\StartNew.ico"), new Size(32, 32));
            this.startNewButton.Text = Settings.Default.S1StartNewButtonText;
            this.toolTip.SetToolTip(this.startNewButton, Settings.Default.S1StartNewButtonToolTip);
            this.resumeButton.Image = new Bitmap(Image.FromFile("Icons\\Resume.ico"), new Size(32, 32));
            this.resumeButton.Text = Settings.Default.S1ResumeButtonText;
            this.toolTip.SetToolTip(this.resumeButton, Settings.Default.S1ResumeButtonToolTip);
            this.Text = Settings.Default.S1Header;
            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.toolTip.SetToolTip(this.helpButton, Settings.Default.HelpButtonToolTip);
            // Кнопка лицензии
            this.AddLicenseButton.Image = new Bitmap(Image.FromFile("Icons\\Elastic.ico"), new Size(32, 32));
            //this.button1.Text = Settings.Default.S1StartNewButtonText;
            //this.toolTip.SetToolTip(this.button1, Settings.Default.S1button1ToolTip);
            //this.LicenseButton.Text = Settings.Default.S1AddLicenseText;
            //this.logoPicture.Image = new Bitmap(Image.FromFile("Icons\\logo4a.png"));
        }

        enum state {expired = 0, exist = 1, notFound};
        state validityOfLcns = 0;
        private void StartNewButtonClick(object sender, EventArgs e)
        {
            if ((int)validityOfLcns == 0)
            {
                string mesg1 = "License period expired";
                MessageBox.Show(mesg1, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if ((int)validityOfLcns == 2)
            {
                string mesg2 = "No license found";
                MessageBox.Show(mesg2, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (logger_.IsDebugEnabled)
                    logger_.Debug("New scenario select.");

            this.Hide();

            if (this.chooseDiskForm_ == null)
            {
                this.chooseDiskForm_ = new ChooseDisksForm(this);
            }

            chooseDiskForm_.ShowDialog();
            
            
        }

        private void ResumeButtonClick(object sender, EventArgs e)
        {
            if ((int)validityOfLcns == 0)
            {
                string mesg1 = "License period expired";
                MessageBox.Show(mesg1, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if ((int)validityOfLcns == 2)
            {
                string mesg2 = "No license found";
                MessageBox.Show(mesg2, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
            if (logger_.IsDebugEnabled)
                logger_.Debug("Resume scenario select.");

            this.Hide();

            if (this.resumeTransferForm_ == null)
            {
                this.resumeTransferForm_ = new ResumeTransferForm(this);
            }

            resumeTransferForm_.ShowDialog();
            
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void HelpButtonClick(object sender, EventArgs e)
        {
            if (logger_.IsDebugEnabled)
                logger_.Debug("Help button click.");
            
            //Start help url.
            System.Diagnostics.Process.Start(Settings.Default.S1Link);
        }

        

        private void NewResumeFormLoad(object sender, EventArgs e)
        {
            /*
             * YOU SHOLD WRITE DATE IN AMERICAN STYLE:
             * Mon dd, yyyy
             * E.g. Feb 04, 2015 or Feb 4, 2015
             * E.g. Oct 10, 2009
            */

            helpButton.BackColor = Color.Transparent;

            string path1 = Environment.CurrentDirectory + "/lcns.msg";
            if (logger_.IsDebugEnabled)
                logger_.Debug("Form load.");
            DateTime DayNow = DateTime.Now;
            //MessageBox.Show("Today is "+ DayNow.Date);
            if (File.Exists(path1))
            {
                int counter = 0;
                System.IO.StreamReader file = new System.IO.StreamReader(path1);
                while ((line = file.ReadLine()) != null)
                {
                    counter++;
                    if (counter == 2)
                        break;
                }
                DateTime MyDateTime = DateTime.Parse(line);
                //MessageBox.Show("Today is " + DayNow.Date + "  License File " + Convert.ToString(MyDateTime.Date));
                if (Convert.ToInt32(MyDateTime.Year) - Convert.ToInt32(DayNow.Year) >= 0)
                {
                    if (Convert.ToInt32(MyDateTime.Year) > Convert.ToInt32(DayNow.Year))
                    {
                        validityOfLcns = (state)1;
                    }

                    if (Convert.ToInt32(MyDateTime.Month) - Convert.ToInt32(DayNow.Month) >= 0)
                    {
                        if (Convert.ToInt32(MyDateTime.Month) > Convert.ToInt32(DayNow.Month))
                        {
                            validityOfLcns = (state)1;
                        }
                        if (Convert.ToInt32(MyDateTime.Day) - Convert.ToInt32(DayNow.Day) >= 0)
                        {
                            validityOfLcns = (state)1;
                        }
                    }
                }
                file.Close();
                // Проверка действительности лицензии работает верно
            }
            else
            {
                validityOfLcns = (state)2;
            }

            
            

        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        // объявим глобальную переменную-флаг
        public static bool agree = false;
        string line;
        string openedFile;

        private void AddLicenseButton_Click_1(object sender, EventArgs e)
        {

            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "license files (*.msg)|*.msg";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            
            string licenseFile = Directory.GetCurrentDirectory() + "\\lcns.msg";
            //MessageBox.Show(licenseFile);
            bool resultOfAsk = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            openedFile = openFileDialog1.FileName;
                            //MessageBox.Show(openedFile);

                            //Получили имя файла, пишем условия копирования
                            // Запомним  копирование System.IO.File.Copy(openedFile, licenseFile, true);

                             
                            // проверка даты и времени
                            if (File.Exists(licenseFile))
                            {
                                int counter = 0;
                                System.IO.StreamReader file1 = new System.IO.StreamReader(licenseFile);
                                string path = openFileDialog1.FileName;
                                while ((line = file1.ReadLine()) != null)
                                {
                                    counter++;
                                    if (counter == 2)
                                        break;
                                }
                                file1.Close();
                                DateTime MyDateTime = DateTime.Parse(line);
                                string Data = Convert.ToString(MyDateTime.Day) + '.' + Convert.ToString(MyDateTime.Month) + '.' + Convert.ToString(MyDateTime.Year);
                                
                                //MessageBox.Show(Data);
                                //проверка действительности лицензии есть.
                                //Пишем условия и замену файлов

                                /*
                                 * Идея с вызовом новой формы оказалась неудачной((
                                CheckAgreement frm = new CheckAgreement();
                                frm.Show();
                                 */

                                string prom = "Your license is valid until " + Data + "\n Are you sure that you would like to overwrite this file?";
                                string message = prom;
                                const string caption = "Form Closing";
                                var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                // If the no button was pressed ...
                                if (result == DialogResult.Yes)
                                {
                                    //System.IO.File.Copy(openedFile, licenseFile, true);
                                    resultOfAsk = true;
                                }
                                file1.Close();
                            }
                            else
                            {
                                // просто копирование файла в новую папку
                                //MessageBox.Show("Файла лицензии нет");
                                System.IO.File.Copy(openedFile, licenseFile, true);
                            }
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }


            }
            
            if (resultOfAsk)
            {
                System.IO.File.Copy(openedFile, licenseFile, true);
            }
            
           
        }

        private void logoPicture_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
