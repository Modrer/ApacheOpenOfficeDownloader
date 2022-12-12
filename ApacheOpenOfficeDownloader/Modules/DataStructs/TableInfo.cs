namespace ApacheOpenOfficeDownloader.Modules.DataStructs
{
    /// <summary>
    /// Shows if table member is folger or file 
    /// </summary>
    public enum FieldType
    {
        Folder,
        File
    }
    /// <summary>
    /// Class <c>TableInfo</c> contains information about file or folger that 
    /// displayed on site in table 
    /// </summary>
    public class TableInfo
    {
        private string _site = "https://sourceforge.net";
        private string _url;

        public string Name;

        public FieldType Type;

        public string Url {
            get
            {
                return _url;
            }
            set
            {
                _url = $"{_site}{value}";
            }
        }
    }
}
