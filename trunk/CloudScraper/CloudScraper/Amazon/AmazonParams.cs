using System;
using System.Collections.Generic;
using System.Text;

namespace CloudScraper
{
    public class AmazonParams
    {
        #region Data members

        private readonly string id_;
        private readonly string region_;
        private readonly string s3Bucket_;
        private readonly string folderKey_;
        private readonly string serverType_;
        private readonly string zone_;
        private readonly string group_;
        private readonly string vpc_;

        #endregion Data members

        public AmazonParams(string id, string region, string s3Bucket, string folderKey, string serverType, string zone, string group, string vpc)
        {
            id_ = id;
            region_ = region;
            s3Bucket_ = s3Bucket;
            folderKey_ = folderKey;
            serverType_ = serverType;
            zone_ = zone;
            group_ = group;
            vpc_ = vpc;
        }

        #region Public methods

        public void WriteToIni(System.IO.StreamWriter stream)
        {
            stream.WriteLine("[EC2]");
            stream.WriteLine("region = " + region_);
            
            if (!string.IsNullOrEmpty(zone_))
            {
                stream.WriteLine("zone = " + zone_);
            }
            if (!string.IsNullOrEmpty(group_))
            {
                stream.WriteLine("security-group = " + group_);
            }

            if (!string.IsNullOrEmpty(vpc_))
            {
                stream.WriteLine("vpcsubnet = subnet-" + vpc_);
            }

            if (!string.IsNullOrEmpty(serverType_))
            {
                stream.WriteLine("instance-type = " + serverType_);
            }
            
            stream.WriteLine("target-arch = x86_64");
            if (!string.IsNullOrEmpty(folderKey_))
            {
                stream.WriteLine("s3prefix = " + folderKey_);
            }
            stream.WriteLine("s3key = " + id_);

            if (!string.IsNullOrEmpty(s3Bucket_))
            {
                stream.WriteLine("bucket = " + s3Bucket_);
            }
        }

        #endregion Public methods
    }
}
