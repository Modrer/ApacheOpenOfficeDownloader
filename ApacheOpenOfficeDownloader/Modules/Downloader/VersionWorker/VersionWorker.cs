using System;
using System.Collections.Generic;
using System.Linq;
using ApacheOpenOfficeDownloader.Modules.DataStructs;

namespace ApacheOpenOfficeDownloader.Modules.Downloader.VersionWorker
{
    /// <summary>
    /// Class <c>VersionWorker</c> used to parse all folgers and return folger with last or required version 
    /// </summary>
    public class VersionWorker
    {
        /// <summary>
        /// This method return all folgers which contains 4.0.0 version of Apache OpenOffice
        /// </summary>
        private static IEnumerable<TableInfo> GetFolgersWithNewestVersionsOfProgram(IEnumerable<TableInfo> folgerInfo)
        {
            //Temp for save parsing data
            Version version;

            return folgerInfo.Where(info =>
            {
                
                if (Version.TryParse(info.Name, out version))
                {
                    return true;
                }
                return false;
            }
            );
        }
        /// <summary>
        /// This method parse table data and returns row which contains folger with last version
        /// </summary>
        public static TableInfo GetLastVersionFolgers(IEnumerable<TableInfo> folgerInfo)
        {
            //Get folgers with OpenOffice versions 4.0.0 and above
            var newestVersionFolgers = GetFolgersWithNewestVersionsOfProgram(folgerInfo);
            //Find last
            var lastVersion = newestVersionFolgers.Max(info => Version.Parse(info.Name));
            //Find folger with last version
            return GetVersionFolger(newestVersionFolgers, lastVersion);

        }
        /// <summary>
        /// This method returns first row which contains folger with <c>version</c> of program
        /// </summary>
        public static TableInfo GetVersionFolger(IEnumerable<TableInfo> folgerInfo, string version)
        {
            return folgerInfo.First(info => info.Name == version);

        }
        /// <summary>
        /// This method returns first row which contains folger with <c>version</c> of program
        /// </summary>
        public static TableInfo GetVersionFolger(IEnumerable<TableInfo> folgerInfo, Version version)
        {
            return folgerInfo.First(info =>
            {
                Version tmpVersion;
                if (!Version.TryParse(info.Name, out tmpVersion))
                {
                    return false;
                }
                return tmpVersion == version;
            });

        }
    }
}
