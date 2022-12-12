using ApacheOpenOfficeDownloader.Modules.DataStructs;
using System;
using System.IO;
using System.Net;

namespace ApacheOpenOfficeDownloader.Modules.Downloader
{
    /// <summary>
    /// Class <c>FileDownloader</c> used to download Apache OpenOffice
    /// </summary>
    internal class FileDownloader
    {
        /// <summary>
        /// this method download Apache OpenOffice
        /// </summary>
        internal static void Download(DownloadParameters downloadParameters)
        {
            //Chek if Folger field ends with "\"
            if (!downloadParameters.Folger.EndsWith(@"\"))
            {

                //If not, add "\"at the end
                downloadParameters.Folger += @"\";
            }

            //Chek if folger exists
            if (!Directory.Exists(downloadParameters.Folger))
            {
                throw new DirectoryNotFoundException($"Directory {downloadParameters.Folger} doesn`t exist");
            }

            //Create string - path to file in which will be download OpenOffice
            string filePath = $"{downloadParameters.Folger}{downloadParameters.FileName}";

            //Make uri from url string
            Uri downloadUri = new Uri(downloadParameters.DownloadUrl);

            //Make new webClient
            WebClient webClient = new WebClient();

            //Download file
            webClient.DownloadFile(downloadUri, filePath);

            //Inform user about successful download
            Console.WriteLine("Download complete");

            //Chek if file isn`t corrupted 
            if(!HashCheker.ChekHash(filePath, downloadParameters.MD5Hash, downloadParameters.SHA1Hash))
            {
                throw new Exception("File hash doesn`t match. May be file corrupted or something went wrong");
            }

            //Inform user about successful hash check
            Console.WriteLine("Hash confirmed");
        }


    }
}
