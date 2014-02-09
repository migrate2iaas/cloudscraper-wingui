using System;
using System.Collections.Generic;
using System.Text;

namespace CloudScraper.Azure
{
    public class VirtualNetworksInfo
    {
        private readonly string name_;

        public VirtualNetworksInfo(string name)
        {
            name_ = name;
        }

        public string Name
        {
            get { return name_; }
        }
    }
}
