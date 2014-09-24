using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Collections.Specialized;
using System.IO;

namespace WebSpider.Core
{
    public class Browser : CacheDb
    {
        #region [Data Members]
        public String Url { get; set; }
        private WebHeaderCollection WebHeader { get; set; }
        private NameValueCollection FormData { get; set; }
        private CacheDb cache;
        //WebClient webClient;
        HttpClient httpClient;
        #endregion

        #region [Constructor]
        public Browser()
        {
            cache = new CacheDb();
            httpClient = new HttpClient();
        }

        public Browser(String Url)
        {
            cache = new Cache();
            httpClient = new HttpClient();
            this.Url = Url;
        }
        #endregion
        
        public HtmlDocument GetWebRequest()
        {
            HtmlDocument document = new HtmlDocument();
            byte[] responseBytes;
            if (!cache.IsCachedUrl(Url))
            {
                responseBytes = httpClient.DownloadData(Url);
                CacheDb.SaveCache(Url, responseBytes);
            }
            else
                responseBytes = cache.GetCachedUrl(Url);
            MemoryStream mStream = new MemoryStream(responseBytes);
            document.Load(mStream);
            return document;
        }

        public HtmlDocument PostRequest(WebHeaderCollection Header, NameValueCollection formData)
        {
            Crawler crawler = new Crawler();
            byte[] responseBytes;
            crawler.Url = Url;
            //WebClient httpClient = new WebClient();
            if (!ReferenceEquals(Header, null))
            {
                httpClient.Headers = Header;
            }
            if (!ReferenceEquals(formData, null))
            {
                httpClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                responseBytes = httpClient.UploadValues(Url, "POST", formData);
            }
            else
            {
                responseBytes = httpClient.DownloadData(Url);
            }
            string resultAuthTicket = Encoding.UTF8.GetString(responseBytes);
            httpClient.Dispose();
            MemoryStream mStream = new MemoryStream(responseBytes);
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.Load(mStream);
            return document;
        }

        public String AjaxPost(NameValueCollection parameters)
        {
            Uri uristring = new Uri(Url);
            httpClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
            httpClient.Headers["ContentType"] = "application/json";
            List<String> Parameters = new List<String>();
            foreach (String key in parameters.AllKeys)
            {
                Parameters.Add(String.Format("\'{0}\':\'{1}\'", key, parameters[key]));
            }

            string JsonStringParams = "{" + String.Join(",", Parameters)  + "}";
            return httpClient.UploadString(Url, JsonStringParams);
        }

        //public static object parseJson(String htmlText)
        //{
        //    var jsonSerialization = new JavaScriptSerializer();
        //    var dictObj = jsonSerialization.Deserialize<Dictionary<string, dynamic>>(json);

        //}

        public void DownloadFile(String FileName)
        {
            httpClient.DownloadFile(Url, FileName);
        }

        public NameValueCollection GetFormData(HtmlDocument document)
        {
            NameValueCollection formData = new NameValueCollection();
            var inputItems = document.DocumentNode.SelectNodes("//body").Descendants()
                .Where(x => x.Attributes.Contains("name") && x.Attributes.Contains("value"))
                .Select(x => new
                {
                    Name = x.Attributes["name"].Value.ToString(),
                    Value = x.Attributes["value"].Value.ToString()
                }
                );


            foreach (var items in inputItems)
            {
                formData.Add(items.Name, items.Value);
            }
            return formData;
        }
    }
}
