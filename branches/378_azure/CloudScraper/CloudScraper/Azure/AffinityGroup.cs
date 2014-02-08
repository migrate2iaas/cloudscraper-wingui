using System;
using System.Collections.Generic;
using System.Text;

namespace CloudScraper.Azure
{
    internal class AffinityGroup
    {
        private readonly string name_;

        public AffinityGroup(string name)
        {
            name_ = name;
        }

        public string Name
        {
            get { return name_; }
        }
    }
}
