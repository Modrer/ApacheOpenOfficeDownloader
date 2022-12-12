using System.Text.RegularExpressions;
using ApacheOpenOfficeDownloader.Modules.DataStructs;

namespace ApacheOpenOfficeDownloader.Modules.RegexFilters
{
    /// <summary>
    /// Class <c>ApacheOpenOfficeExeFileFilter</c> used to check if Apache OpenOffice file is file 
    /// for windows
    /// </summary>
    public class ApacheOpenOfficeExeFileFilter : IFiltrator
    {
        private static ApacheOpenOfficeExeFileFilter _instance;

        public static ApacheOpenOfficeExeFileFilter Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ApacheOpenOfficeExeFileFilter();
                }
                return _instance;
            }
        }

        private ApacheOpenOfficeExeFileFilter() { }

        private readonly static string _filter = @"Apache_OpenOffice_[\d,.]{0,8}._Win_x\d{2}_install_.{0,6}.exe";
        public bool Match(string input)
        {
            return Regex.IsMatch(input, _filter);
        }
    }
}
