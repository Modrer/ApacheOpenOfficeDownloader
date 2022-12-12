using ApacheOpenOfficeDownloader.Modules.Downloader;
using ApacheOpenOfficeDownloader.Modules.DataStructs;
using System;
using System.IO;
using System.Globalization;

namespace ApacheOpenOfficeDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //Load default settings
            ConsoleArguments consoleArguments = new ConsoleArguments
            {
                Culture = CultureInfo.InstalledUICulture,
                FileName = null,
                FolgerName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            //Parse console arguments
            foreach (var arg in args)
            {
                //Choose language
                if (arg.Contains("-forse-language:"))
                {

                    consoleArguments.Culture = System.Globalization.CultureInfo.CreateSpecificCulture(arg.Split(':')[1]);
                    continue;
                }
                //Choose filename
                if (arg.Contains("-forse-filename:"))
                {
                    consoleArguments.FileName = arg.Split(':')[1];
                    continue;
                }
                //Choose folger to download
                if (arg.Contains("-forse-folger:"))
                {
                    consoleArguments.FolgerName = arg.Replace("-forse-folger:","");
                    if (!Directory.Exists(consoleArguments.FolgerName))
                    {
                        Console.WriteLine("Folger doesn`t exist");
                        return;
                    }
                    continue;
                }
                //Choose custom version
                //Works only with version 4.0.0 and above
                if (arg.Contains("-forse-version:"))
                {
                    if(!Version.TryParse(arg.Split(':')[1], out consoleArguments.Version))
                    {
                        Console.WriteLine("Can`t parse version");
                        return;
                    }

                    continue;
                }
            }
            //Dovnload file
            OpenOfficeDownloader.Download(consoleArguments);

            
        }
    }
}
