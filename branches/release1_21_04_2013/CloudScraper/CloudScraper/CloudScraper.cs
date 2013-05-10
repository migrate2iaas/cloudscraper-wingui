using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CloudScraper
{
    static class CloudScraper
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NewResumeForm());
        }
    }
}
