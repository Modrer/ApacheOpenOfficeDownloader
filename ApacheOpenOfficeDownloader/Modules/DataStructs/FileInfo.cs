namespace ApacheOpenOfficeDownloader.Modules.DataStructs
{
    /// <summary>
    /// Class <c>FileInfo</c> used to load information about Apache OpenOffice file from site
    /// </summary>
    public class FileInfo
    {
        public string name { get; set; }
        public string path { get; set; }
        public string download_url { get; set; }
        public string url { get; set; }
        public string full_path { get; set; }
        public string type { get; set; }
        public string link { get; set; }
        public int downloads { get; set; }
        public string sha1 { get; set; }
        public string md5 { get; set; }
        public string Default { get; set; }
        public string download_label { get; set; }
        public bool exclude_reports { get; set; }
        public bool downloadable { get; set; }
        public string legacy_release_notes { get; set; }
        public bool staged { get; set; }
        public int stage { get; set; }
        public int staging_days { get; set; }
        public string files_url { get; set; }
        public bool explicitly_staged { get; set; }
        public string authorized { get; set; }
    }
    
}
