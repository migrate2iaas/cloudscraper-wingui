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
        [STAThread]
        static void Main()
        {
            if (logger_.IsDebugEnabled)
            {

                System.Collections.IDictionary var = Environment.GetEnvironmentVariables();
                logger_.Debug("=============================================================");
                logger_.Debug("Application started " + DateTime.UtcNow.ToString() + " " + String.Format("{0:zzz}", DateTime.Now));
                logger_.Debug(Environment.CurrentDirectory);
                logger_.Debug(Environment.UserDomainName);
                logger_.Debug(Environment.UserName);

                logger_.Debug(OSInfo.Name);
                logger_.Debug(OSInfo.Edition);
                logger_.Debug(OSInfo.ServicePack);
                logger_.Debug(OSInfo.VersionString);
                logger_.Debug(OSInfo.Bits);
                if (var.Contains("PROCESSOR_ARCHITEW6432"))
                    logger_.Debug(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NewResumeForm());
        }
    }
}
