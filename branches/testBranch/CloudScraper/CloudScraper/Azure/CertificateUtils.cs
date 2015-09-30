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
    public class CertificateUtils
    {
        #region Constants

        public const string CertificateStore = "My";

        #endregion  Constants

        // helper struct to contain both store and cert
        public class CertificatePath
        {
            public X509Certificate2 certificate;
            public X509Store store;

            public CertificatePath(X509Certificate2 cert, X509Store stre)
            {
                certificate = cert;
                store = stre;
            }

            // gets selection string compatible with IWinHttpRequest::SetClientCertificate 
            public string GetSelectionString()
            {
                string result = "";
                if (store.Location == StoreLocation.CurrentUser)
                    result += "CURRENT_USER";
                if (store.Location == StoreLocation.LocalMachine)
                    result += "LOCAL_MACHINE";
                result += "\\" + store.Name + "\\" + certificate.GetNameInfo(X509NameType.SimpleName, false);
                return result;
            }
        };

        public static CertificatePath GetCertificate(string thumbprint, string certstore = "My")
        {
            List<StoreLocation> locations = new List<StoreLocation> { StoreLocation.CurrentUser, StoreLocation.LocalMachine };

            foreach (var location in locations)
            {
                X509Store store = new X509Store(certstore, location);
                try
                {
                    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                    X509Certificate2Collection certificates = store.Certificates.Find(
                      X509FindType.FindByThumbprint, thumbprint, false);
                    if (certificates.Count == 1)
                    {
                        CertificatePath path = new CertificatePath(certificates[0], store);
                        return path;
                    }
                }
                finally
                {
                    store.Close();
                }
            }
            return null;
        }

        // checks if certificate installed. Also sets auxillary certificate fields
        public static bool CheckCertificateInstalled(string thumbprint, ref string selection)
        {
            // Try to open the store.
            CertificatePath certificatepath = null;
            try
            {
                certificatepath = GetCertificate(thumbprint, CertificateUtils.CertificateStore);
            }
            catch (Exception)
            {
                DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4AzureCertificateHeader,
                    Settings.Default.S4AzureCertificateStoreError, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
                return false;
            }

            // Check to see if our certificate was added to the collection. If no, throw an error, if yes, create a certificate using it.
            if (certificatepath == null)
            {
                DialogResult result = BetterDialog.ShowDialog(Settings.Default.S4AzureCertificateHeader,
                    Settings.Default.S4AzureCertificateThumbprintError, "", "OK", "OK",
                    System.Drawing.Image.FromFile("Icons\\ErrorDialog.png"), false);
                return false;
            }

            selection = certificatepath.GetSelectionString();

            return true;
        }
    }
}
