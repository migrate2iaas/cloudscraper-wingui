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
            int a = 0 % 10;
            int b = 1 % 10;
            int c = 2 % 10;
            int d = 11 % 10;
            int e = 12 % 10;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NewResumeForm());
        }
    }
}
