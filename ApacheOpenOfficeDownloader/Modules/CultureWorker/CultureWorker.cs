using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using ApacheOpenOfficeDownloader.Modules.DataStructs;

namespace ApacheOpenOfficeDownloader.Modules.CultureWorker
{
    public static class CultureWorker
    {
        /// <summary>
        /// Return first member in table which contains folger with required culture
        /// Must be called with binaries table data
        /// </summary>
        public static TableInfo FindCultureData(IEnumerable<TableInfo> tableInfos, CultureInfo cultureInfo)
        {
            //Compare by culture
            var folger = tableInfos.FirstOrDefault(info => CultureComparator.Compare(info.Name, cultureInfo));

            //If can`t find folger, then search by parent culture
            if(folger == null)
            {
                folger = tableInfos.FirstOrDefault(info => CultureComparator.CompareByParent(info.Name, cultureInfo));
            }

            return folger;
        }
    }
}
