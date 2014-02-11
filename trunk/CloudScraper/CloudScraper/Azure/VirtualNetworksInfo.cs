using System;
using System.Collections.Generic;
using System.Text;

namespace CloudScraper.Azure
{
    public class VirtualNetworksInfo
    {
        private readonly string name_;
        private readonly List<VirtualNetworksInfo> subnets_;

        public VirtualNetworksInfo(string name)
        {
            name_ = name;
            subnets_ = new List<VirtualNetworksInfo>();
        }

        public void AddSubnet(VirtualNetworksInfo subnet)
        {
            subnets_.Add(subnet);
        }

        public string Name
        {
            get { return name_; }
        }

        public IList<VirtualNetworksInfo> Subnets
        {
            get { return subnets_; }
        }
    }
}
