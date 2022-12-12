using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace ApacheOpenOfficeDownloader.Modules.Downloader
{
    /// <summary>
    /// Class <c>HashCheker</c> used to check if downloaded file isn`t corrupter
    /// </summary>
    public class HashCheker
    {
        /// <summary>
        /// This method require path to <c>file</c>, <c>expectedMD5</c> hash and <c>expectedMD5</c> hash
        /// and check if this hash matches
        /// </summary>
        public static bool ChekHash(string file, string expectedMD5, string expectedSHA1)
        {
            //Load all file to buffer
            byte[] buffer = File.ReadAllBytes(file);

            //Initialise hash makers

            SHA1 sha = new SHA1CryptoServiceProvider();
            MD5 md5 = MD5.Create();

            //Compute hash from buffer and parse it to string value

            string sha1Hash = string.Concat(sha.ComputeHash(buffer).Select(b => b.ToString("x2")));
            string md5Hash = string.Concat(md5.ComputeHash(buffer).Select(b => b.ToString("x2")));

            //Check if expected hash and actual compared

            if(string.Compare(sha1Hash, expectedSHA1) != 0)
            {
                return false;
            }
            if (string.Compare(md5Hash, expectedMD5) != 0)
            {
                return false;
            }

            return true;
        }
    }
}
