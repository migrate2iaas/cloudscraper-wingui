using System;
using System.Collections.Generic;
using System.Text;

namespace CloudScraper.Azure
{
    internal class AffinityGroupInfo
    {
        private readonly string name_;

        public AffinityGroupInfo(string name)
        {
            name_ = name;
        }

        public string Name
        {
            get { return name_; }
        }
    }
}
