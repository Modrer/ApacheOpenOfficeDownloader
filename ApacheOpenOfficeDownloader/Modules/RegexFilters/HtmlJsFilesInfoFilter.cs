using System.Text.RegularExpressions;
using ApacheOpenOfficeDownloader.Modules.DataStructs;

namespace ApacheOpenOfficeDownloader.Modules.RegexFilters
{
    /// <summary>
    /// Class <c>HtmlJsFilesInfoFilter</c> used to check if row contains data about Apache OpenOffice files
    /// for windows
    /// </summary>
    public class HtmlJsFilesInfoFilter : IFiltrator
    {
        private readonly static string _filter = @"net.sf.files";

        private static HtmlJsFilesInfoFilter _instance;

        public static HtmlJsFilesInfoFilter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HtmlJsFilesInfoFilter();
                }
                return _instance;
            }
        }

        private HtmlJsFilesInfoFilter() { }

        public bool Match(string input)
        {
            return Regex.IsMatch(input, _filter);
        }

    }
}
