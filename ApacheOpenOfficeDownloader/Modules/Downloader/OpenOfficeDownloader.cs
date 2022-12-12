using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using ApacheOpenOfficeDownloader.Modules.RegexFilters;
using ApacheOpenOfficeDownloader.Modules.DataStructs;


namespace ApacheOpenOfficeDownloader.Modules.Downloader
{
    /// <summary>
    /// Class <c>OpenOfficeDownloader</c> used to download  Apache OpenOffice
    /// </summary>
    public class OpenOfficeDownloader
    {
        //Folger name with binaries files
        private readonly static string _binaries = "binaries";

        //Url where need search Apache OpenOffice files
        private readonly static string _siteUrl = $"https://sourceforge.net/projects/openofficeorg.mirror/files/";

        //If don`t find OpenOffice with system language search OpenOffice with this language
        private readonly static CultureInfo _culture = new CultureInfo("en-us");

        //Filter to select file for windows
        private readonly static IFiltrator _fileFilter = ApacheOpenOfficeExeFileFilter.Instance;

        /// <summary>
        /// This method simplify PageParser calls
        /// </summary>
        private static IEnumerable<TableInfo> Parse(string url)
        {
            return PageParser.PageParser.Parse(url);
        }
        /// <summary>
        /// This method parse html page with OpenOffice files and search 
        /// folger which contains last version
        /// </summary>
        private static TableInfo FindLastVersionFolger()
        {
            var rows = Parse(_siteUrl);
            return VersionWorker.VersionWorker.GetLastVersionFolgers(rows);
        }
        /// <summary>
        /// This method search url to binaries folger
        /// Require <c>url</c> where search binaries folger
        /// </summary>
        private static string GetBinariesFolgerUrl(string url)
        {
            //Gets all rows in table
            var rows = Parse(url);

            //Find first row with binaries folger
            var binariesFolger = rows.FirstOrDefault(row => row.Name.ToLower() == _binaries);

            //If it exists return url to load this folger
            if (binariesFolger != null)
            {
                return binariesFolger.Url; 

            }
            return null;
        }
        /// <summary>
        /// This method search folger with localised version of Apache OpenOffice
        /// Require <c>url</c> where search binaries folger and <c>searchCulture</c> with 
        /// language to search
        /// If doesn`t contain that language, then returns en-us language folger
        /// </summary>
        private static string GetLanguageFolgerUrl(string url, CultureInfo searchCulture)
        {
            var rows = Parse(url);

            //Search row with required language
            var table = CultureWorker.CultureWorker.FindCultureData(rows, searchCulture);

            //If don`t find, search row with default (en-us) language
            if (table == null)
            {
                table = CultureWorker.CultureWorker.FindCultureData(rows, _culture);
            }

            //If don`t find throw exeption
            if (table == null)
            {
                throw new KeyNotFoundException("Cannot find language folger");
            }
            return table.Url;
        }
        /// <summary>
        /// This method returns info about Apache OpenOffice file to download
        /// <c>folgerUrl</c> is url to folger with Apache OpenOffice files
        /// </summary>
        private static FileInfo FindDownloadInfo(string folgerUrl) 
        {
            //Parse all files in table
            var files = PageParser.PageParser.ParseFiles(folgerUrl);

            //Search Apache OpenOffice installation file for windows
            var key = files.Keys.FirstOrDefault(key => _fileFilter.Match(key));

            //Return info about this file
            return files[key];
        }
        /// <summary>
        /// This method parse site in search of Apache OpenOffice last version file,
        /// with required language
        /// </summary>
        private static FileInfo GetFileInfo(CultureInfo culture)
        {
            //Find folger with last version files
            var row = FindLastVersionFolger();

            //Then go to binaries folger
            string binariesUrl = GetBinariesFolgerUrl(row.Url);

            //Then go to folger with localised version of Apache OpenOffice
            string folgerUrl = GetLanguageFolgerUrl(binariesUrl, culture);

            //Then returns info about Apache OpenOffice file to download
            return FindDownloadInfo(folgerUrl);
        }
        /// <summary>
        /// This method parse site in search of Apache OpenOffice vith <c>version</c> version file,
        /// with required language
        /// </summary>
        private static FileInfo GetFileInfo(CultureInfo culture, Version version)
        {
            //Find folger with required version files
            var row = FindVersionFolger(version);

            //Then go to binaries folger
            string binariesUrl = GetBinariesFolgerUrl(row.Url);

            //Then go to folger with localised version of Apache OpenOffice
            string folgerUrl = GetLanguageFolgerUrl(binariesUrl, culture);

            //Then returns info about Apache OpenOffice file to download
            return FindDownloadInfo(folgerUrl);
        }
        /// <summary>
        /// This method parse site in search of Apache OpenOffice with <c>version</c> version folger
        /// </summary>
        private static TableInfo FindVersionFolger(Version version)
        {
            var rows = Parse(_siteUrl);
            return VersionWorker.VersionWorker.GetVersionFolger(rows,version);
        }
        /// <summary>
        /// This method prepare for download Apache OpenOffice with required parameters
        /// </summary>
        public static void Download(ConsoleArguments consoleArguments)
        {
            //Load fileInfo
            FileInfo fileInfo;

            //If required specific version then load with it, otherwise load last 
            if (consoleArguments.Version != null)
            {
                fileInfo = GetFileInfo(consoleArguments.Culture, consoleArguments.Version);
            }
            else
            {
                fileInfo = GetFileInfo(consoleArguments.Culture);
            }

            //Make new download parameters 
            DownloadParameters downloadParameters = new DownloadParameters();

            //Fill by special required values
            downloadParameters.FileName = consoleArguments.FileName;
            downloadParameters.Folger = consoleArguments.FolgerName;

            //If haven`t special required values then fills with default
            if (downloadParameters.FileName == null)
            {
                downloadParameters.FileName = fileInfo.name;
            }

            if (downloadParameters.Folger == null)
            {
                downloadParameters.FileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            downloadParameters.DownloadUrl = fileInfo.download_url;
            downloadParameters.MD5Hash = fileInfo.md5;
            downloadParameters.SHA1Hash = fileInfo.sha1;

            //Download file
            FileDownloader.Download(downloadParameters);
            


        }
    }
}
