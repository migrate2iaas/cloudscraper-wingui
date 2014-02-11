using System;
using System.Collections.Generic;
using System.Text;

namespace CloudScraper.Azure
{
    public class AzureParams
    {
        #region Data members

        private readonly string storageAcc_;
        private readonly string primaryAccessKey_;
        private readonly string region_;
        private readonly string subscrId_;
        private readonly string thumbprint_;
        private readonly string certSelection_;
        private readonly string virtualNetwork_;
        private readonly string subnet_;
        private readonly string affinity_;
        private readonly string container_;

        #endregion Data members

        #region Constructors

        public AzureParams(string storageAcc, string primaryAccessKey, string region,
            string certSelection, string subscrId, string thumbprint,
            string virtualNetwork, string subnet, string affinity, string container)
        {
            storageAcc_ = storageAcc;
            primaryAccessKey_ = primaryAccessKey;
            region_ = region;
            subscrId_ = subscrId;
            certSelection_ = certSelection;
            thumbprint_ = thumbprint;
            virtualNetwork_ = virtualNetwork;
            subnet_ = subnet;
            affinity_ = affinity;
            container_ = container;
        }

        #endregion Constructors

        #region Public methods

        public void WriteToIni(System.IO.StreamWriter stream)
        {
            stream.WriteLine("[Azure]");
            stream.WriteLine("region = " + region_);
            stream.WriteLine("storage-account = " + storageAcc_);
            
            if (!string.IsNullOrEmpty(thumbprint_))
            {
                stream.WriteLine("certificate-thumbprint = " + thumbprint_);
            }

            if (!string.IsNullOrEmpty(certSelection_))
            {
                stream.WriteLine("certificate-selection = " + certSelection_);
            }

            if (!string.IsNullOrEmpty(subscrId_))
            {
                stream.WriteLine("subscription-id = " + subscrId_);
            }

            if (!string.IsNullOrEmpty(container_))
            {
                stream.WriteLine("container-name = " + container_);
            }

            if (!string.IsNullOrEmpty(affinity_))
            {
                stream.WriteLine("affinity-group = " + affinity_);
            }

            if (!string.IsNullOrEmpty(virtualNetwork_))
            {
                stream.WriteLine("virtual-network = " + virtualNetwork_);
            }

            if (!string.IsNullOrEmpty(subnet_))
            {
                stream.WriteLine("virtual-subnet = " + subnet_);
            }
        }

        #endregion Public methods
    }
}
