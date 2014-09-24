using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Net;
using HtmlAgilityPack;
using System.Net.Sockets;

namespace WebSpider.Core
{
    class Crawler
    {
        public string Url { get; set; }

        public Crawler() { }

        public Crawler(string Url)
        {
            this.Url = Url;
        }


        public XDocument GetXDocument()
        {
            HtmlAgilityPack.HtmlWeb doc1 = new HtmlAgilityPack.HtmlWeb();
            doc1.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
            HtmlAgilityPack.HtmlDocument doc2 = doc1.Load(Url);
            doc2.OptionOutputAsXml = true;
            doc2.OptionAutoCloseOnEnd = true;
            doc2.OptionDefaultStreamEncoding = System.Text.Encoding.UTF8;
            XDocument xdoc = XDocument.Parse(doc2.DocumentNode.SelectSingleNode("html").OuterHtml);
            return xdoc;
        }

        public HtmlDocument GetDocument()
        {
            HtmlAgilityPack.HtmlWeb doc1 = new HtmlAgilityPack.HtmlWeb();
            doc1.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
            HtmlAgilityPack.HtmlDocument doc2 = doc1.Load(Url);
            doc2.OptionOutputAsXml = true;
            doc2.OptionAutoCloseOnEnd = true;
            doc2.OptionDefaultStreamEncoding = System.Text.Encoding.UTF8;
            return doc2;
        }


        public void Save(String FileName)
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            WebClient client = new WebClient();
            if (Url.StartsWith("http"))
                client.DownloadFile(Url, FileName);
            else
                client.DownloadFile("http:" + Url, FileName);
        }
    }
}
