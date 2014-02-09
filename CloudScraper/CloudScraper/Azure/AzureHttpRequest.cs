using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Net;

namespace CloudScraper.Azure
{
    public class AzureHttpRequest
    {
        public static HttpWebResponse DoRequest(string headerVersion, string thumbprint, string uriString)
        {
            X509Certificate2 certificate = CertificateUtils.GetCertificate(thumbprint, CertificateUtils.CertificateStore).certificate;
            if (certificate == null)
            {
                throw new AzureCertificateException("Failed to obtain certificate.");
            }

            Uri uri = new Uri(uriString);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = "GET";
            request.Headers.Add("x-ms-version", headerVersion);
            request.ClientCertificates.Add(certificate);
            request.ContentType = "application/xml";

            return (HttpWebResponse)request.GetResponse();
        }
    }
}
