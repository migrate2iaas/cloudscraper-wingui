using System;
using System.Collections.Generic;
using System.Text;

namespace CloudScraper.Azure
{
    internal class AzureCertificateException : ApplicationException
    {
        public AzureCertificateException(string msg) : base(msg)
        {
        }
    }

    internal class InconsistentAzueDataException : ApplicationException
    {
        public InconsistentAzueDataException(string msg)
            : base(msg)
        {
        }
    }
}
