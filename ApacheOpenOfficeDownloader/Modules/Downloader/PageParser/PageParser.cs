using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.Json;
using ApacheOpenOfficeDownloader.Modules.RegexFilters;
using ApacheOpenOfficeDownloader.Modules.DataStructs;

namespace ApacheOpenOfficeDownloader.Modules.Downloader.PageParser
{
    /// <summary>
    /// Class <c>PageParser</c> used to parse sourseforge site with table data
    /// </summary>
    public class PageParser
    {
        //Filters javascript strings to find Apache OpenOffice file data json
        private static readonly IFiltrator _htmlJsFilter = HtmlJsFilesInfoFilter.Instance;

        private static readonly WebClient _webClient = new WebClient();

        /// <summary>
        /// This method load html page file from <c>url</c> like a string
        /// </summary>
        private static string LoadHtmlFromUrl(string url)
        {
            
            return _webClient.DownloadString(url);
        }
        /// <summary>
        /// This method load and parse json file which contains information about Apache OpenOffice files 
        /// </summary>
        public static Dictionary<string, DataStructs.FileInfo> ParseFiles(string url)
        {
            var html = LoadHtmlFromUrl(url);

            //Load html string as html document from HtmlAgilityPack to easies work with html
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            //Search json data from scripts node
            var preJson = doc.DocumentNode.SelectNodes("//script").FirstOrDefault(script =>

            //Check if it contains needed part of js 
            _htmlJsFilter.Match(script.InnerHtml)
            )

                //Search row
                .InnerHtml.Split('\n').FirstOrDefault(text =>
            _htmlJsFilter.Match(text)
            );

            //Load json
            //Start with first '{' char
            var json = new StringBuilder(preJson[preJson.IndexOf('{')..]);

            //Remove last char (json ends with ';', but this don`t need) 
            json = json.Remove(json.Length - 1, 1);

            //Deserialize json and return it
            return JsonSerializer.Deserialize<Dictionary<string, DataStructs.FileInfo>>(json.ToString());

        }

        /// <summary>
        /// This method parse table data from table on <c>url</c> page 
        /// </summary>
        public static IEnumerable<TableInfo> Parse(string url)
        {
            
            return ParseHtml(LoadHtmlFromUrl(url));
        }
        /// <summary>
        /// This method parse table data from table from html
        /// </summary>
        private static IEnumerable<TableInfo> ParseHtml(string html)
        {
            //Make HtmlDocument from html string 
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            //Search rows in table
            var htmlBody = doc.DocumentNode.SelectSingleNode("//tbody").SelectNodes(".//tr");

            //Serialize and return table data
            return htmlBody.Select(node =>
            {
                return new TableInfo
                {
                    Name = node.GetAttributeValue("title", null),
                    Url = node.SelectSingleNode(".//a").GetAttributeValue("href", ""),
                    Type = (FieldType)Enum.Parse(typeof(FieldType), node.GetAttributeValue("class", null), true)
                };

            });

        }
    }
}
