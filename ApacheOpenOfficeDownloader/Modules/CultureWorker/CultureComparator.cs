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
            return CultureInfo
                .CreateSpecificCulture(firstCulture)
                .Equals(
                CultureInfo.CreateSpecificCulture(secondCulture.Name)
                );

        }
        /// <summary>
        /// Compare 2 cultures, parse second to CultureInfo class
        /// </summary>
        public static bool Compare(CultureInfo firstCulture, string secondCulture)
        {
            return Compare(secondCulture, firstCulture);

        }
        /// <summary>
        /// Compare 2 cultures
        /// </summary>
        public static bool Compare(string firstCulture, string secondCulture)
        {
            return CultureInfo
                .CreateSpecificCulture(firstCulture)
                .Equals(
                CultureInfo.CreateSpecificCulture(secondCulture)
                );

        }
        /// <summary>
        /// Compare 2 cultures
        /// </summary>
        public static bool Compare(CultureInfo firstCulture, CultureInfo secondCulture)
        {
            return CultureInfo
                .CreateSpecificCulture(firstCulture.Name)
                .Equals(
                CultureInfo.CreateSpecificCulture(secondCulture.Name)
                );

        }
    }
}
