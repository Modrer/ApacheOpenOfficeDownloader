using System;
using System.Globalization;

namespace ApacheOpenOfficeDownloader.Modules.DataStructs
{
    /// <summary>
    /// Class <c>ConsoleArguments</c> contains some variables that can be set by launch arguments
    /// </summary>
    public class ConsoleArguments
    {
        public string FileName;
        public string FolgerName;
        public Version Version;
        public CultureInfo Culture;
    }
}
