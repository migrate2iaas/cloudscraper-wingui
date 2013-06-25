using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using NLog;

namespace CloudScraper
{
    static class CloudScraper
    {
        private static Logger logger_ = LogManager.GetLogger("App");
        
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        public static string logPath_;
        
        [STAThread]
        static void Main()
        {
            InitFileLog();
            logger_.Debug("Application start");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NewResumeForm());
        }


        private static void InitFileLog()
        {
            string path = Directory.GetCurrentDirectory() + "\\logs\\";
            // create log file
            if (File.Exists(path + "gui.log"))
            {
                if (File.Exists(path + "gui_3.log") && File.Exists(path + "gui_2.log") &&
                    File.Exists(path + "gui_1.log"))
                {
                    File.Delete(path + "gui_3.log");
                }

                for (int i = 2; i >= 1; i--)
                {
                    if (File.Exists(path + "gui_" + i.ToString() + ".log"))
                    {
                        if ((i != 1 && File.Exists(path + "gui_" + (i - 1).ToString() + ".log")) ||
                            i == 1)
                        {
                            File.Copy(path + "gui_" + i.ToString() + ".log", path + "gui_" + (i + 1).ToString() + ".log");
                            File.Delete(path + "gui_" + i.ToString() + ".log");
                        }
                    }
                }

                File.Copy(path + "gui.log", path + "gui_1.log");
                File.Delete(path + "gui.log");
            }

            logPath_ = path + "gui.log";

            using (StreamWriter stream = new StreamWriter(logPath_, false))
            {
                stream.WriteLine(DateTime.Now.ToString() + " " + "Start application");
            }
        }
    }
}
