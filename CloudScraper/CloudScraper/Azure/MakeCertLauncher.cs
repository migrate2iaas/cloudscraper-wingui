using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

using DotNetPerls;
using CloudScraper.Properties;
using NLog;

namespace CloudScraper.Azure
{
    public delegate void Action();

    /// <summary>
    /// A helper class to create and upload certificate to Windows Azure
    /// </summary>
    internal class MakeCertLauncher
    {
        #region Data members

        private readonly Logger logger_;
        private readonly Action<string> action_;
        private int certificateCount_;

        #endregion Data members

        #region Constructors

        public MakeCertLauncher(Logger logger, Action<string> onThumbPrintCreated)
        {
            logger_ = logger;
            action_ = onThumbPrintCreated;
            certificateCount_ = 0;
        }

        #endregion Constructors

        #region Public methods

        public bool Start()
        {
            if (logger_.IsDebugEnabled)
            {
                logger_.Debug("Create Certificate button click.");
            }

            string certificatePath = MyShowDialog();
            Process process = MyCreateMakeCertProcess(certificatePath);

            return null != process && process.Start();
        }

        #endregion Public methods

        #region Private methods

        private void ProcessExited(object sender, EventArgs e)
        {
            //Verify that new certificate has created.
            X509Store store = null;
            string thumbprint = null;

            try
            {
                store = new X509Store(CertificateUtils.CertificateStore, StoreLocation.CurrentUser);
            }
            catch (CryptographicException ex)
            {
                if (logger_.IsErrorEnabled)
                    logger_.Error(ex);
                return;
            }

            try
            {
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection fcollection = collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                //X509Certificate2Collection pcollection = fcollection.Find(X509FindType.FindBySubjectDistinguishedName, "CN=" + certificatePath, false);

                if (fcollection.Count > certificateCount_)
                {
                    DialogResult reslt = BetterDialog.ShowDialog(Settings.Default.S4AzureCertificateHeader,
                    Settings.Default.S4AzureCertificateCreateSuccess, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\InfoDialog.png"), false);
                }
                else
                {
                    DialogResult reslt = BetterDialog.ShowDialog(Settings.Default.S4AzureCertificateHeader,
                    Settings.Default.S4AzureCertificateCreateError, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
                    return;
                }

                int indexOfthumbprint = 0;
                if (fcollection.Count > 0)
                {
                    foreach (X509Certificate certificate in fcollection)
                    {
                        if (fcollection[fcollection.IndexOf(certificate)].NotBefore >
                            fcollection[indexOfthumbprint].NotBefore)
                        {
                            indexOfthumbprint = fcollection.IndexOf(certificate);
                        }
                    }
                }
                thumbprint = fcollection[indexOfthumbprint].Thumbprint;
            }
            catch (CryptographicException ex)
            {
                if (logger_.IsErrorEnabled)
                    logger_.Error(ex);
            }
            finally
            {
                if (null != store)
                {
                    store.Close();
                }
            }

            action_(thumbprint);
        }

        private string MyShowDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Certificate File (*.cer)|*.cer";
            saveFileDialog.DefaultExt = "." + "cer";

            DialogResult result = saveFileDialog.ShowDialog();
            return (result == DialogResult.OK) ? saveFileDialog.FileName : null;
        }

        private Process MyCreateMakeCertProcess(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            string name = Path.GetFileName(path);

            // NOTE: we should alter the argeuments of makecert to create cert in the local machine location in order to run from service context
            // or impersonate alternatively the service process when accessing certs

            //Creating certificate process.
            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "makecert.exe";
            info.Arguments = "-sky exchange -r -n \"CN=" + name + "\" -pe -a sha1 -len 2048 -ss " + CertificateUtils.CertificateStore + " \"" + path + "\"";
            info.UseShellExecute = true;
            info.UserName = System.Diagnostics.Process.GetCurrentProcess().StartInfo.UserName;
            info.Password = System.Diagnostics.Process.GetCurrentProcess().StartInfo.Password;
            process.StartInfo = info;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Exited += new EventHandler(this.ProcessExited);
            process.EnableRaisingEvents = true;

            //Count certificates.
            // note: predefined, could be changed
            X509Store store = new X509Store(CertificateUtils.CertificateStore, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection fcollection = collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            certificateCount_ = fcollection.Count;
            store.Close();

            return process;
        }

        #endregion Private methods
    }
}
