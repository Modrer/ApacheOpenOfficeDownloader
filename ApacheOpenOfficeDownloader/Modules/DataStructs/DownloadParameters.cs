using System;
using System.Collections.Generic;
using System.Text;

namespace ApacheOpenOfficeDownloader.Modules.DataStructs
{
    /// <summary>
    /// Class <c>DownloadParameters</c> contains required parameters to download file
    /// </summary>
    public class DownloadParameters
    {
        public string FileName;
        public string Folger;
        public string DownloadUrl;
        public string SHA1Hash;
        public string MD5Hash;
    }
}
