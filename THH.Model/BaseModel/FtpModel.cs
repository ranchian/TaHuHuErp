using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THH.Model
{
    public class FtpModel
    {
        public string FtpUrl { get; set; }
        public string FtpPort { get; set; }
        public string FtpPath { get; set; }
        public string FtpUser { get; set; }
        public string FtpPwd { get; set; }
        public string FtpFileName { get; set; }
        public string LocalPath { get; set; }
        public string UploadFtpUrl { get; set; }
        public string UploadFtpPort { get; set; }
        public string UploadFtpPath { get; set; }
        public string UploadFtpUser { get; set; }
        public string UploadFtpPwd { get; set; }
    }
}
