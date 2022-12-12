using System.Globalization;

namespace ApacheOpenOfficeDownloader.Modules.CultureWorker
{
    /// <summary>
    /// Class <c>CultureComparator</c> used to compare 2 cultures
    /// </summary>
    public static class CultureComparator
    {
        /// <summary>
        /// Compare 2 cultures, parse first to CultureInfo class
        /// </summary>
        public static bool Compare(string firstCulture, CultureInfo secondCulture)
        {
            //Compare 2 cultures
            return Compare(CultureInfo
                .CreateSpecificCulture(firstCulture),
                secondCulture
                );

        }
        /// <summary>
        /// Compare 2 cultures
        /// </summary>
        public static bool Compare(CultureInfo firstCulture, CultureInfo secondCulture)
        {
            return firstCulture
                .Equals(
                secondCulture
                );

        }
        /// <summary>
        /// Compare 2 cultures, parse to specific CultureInfo and then compare 2 culture by parent
        /// </summary>
        public static bool CompareByParent(string firstCulture, CultureInfo secondCulture)
        {
            return Compare(
                CultureInfo.CreateSpecificCulture(firstCulture).Parent,
                CultureInfo.CreateSpecificCulture(secondCulture.Name).Parent
                );

        }
        /// <summary>
        /// Compare 2 cultures, parse to specific CultureInfo and then compare 2 culture by parent
        /// </summary>
        public static bool CompareByParent(CultureInfo firstCulture, CultureInfo secondCulture)
        {
            return Compare(
                CultureInfo.CreateSpecificCulture(firstCulture.Name).Parent,
                CultureInfo.CreateSpecificCulture(secondCulture.Name).Parent
                );

        }

    }
}
