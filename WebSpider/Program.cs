using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Net;
using System.Collections;
using System.Xml.XPath;
using WebSpider.Core;
using System.Windows.Forms;

namespace WebSpider
{
    static class Program
    {
        ///// <summary>
        ///// The main entry point for the application.
        ///// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //Application.Run(new WebBrowser());
            //Application.Run(new CacheTest());
        }
        ////static void Main(string[] args)
        ////{
        ////    string url = "http://adiglobal.us/Company/Pages/Mktg_ShopProducts.aspx?cat=ADI%20US&category=0000&parent=0000";
        ////    string xmlns = "{http://www.w3.org/1999/xhtml}";
        ////    Spider oSpider = new Spider();
        ////    oSpider.ParseCatagory(url);
            //Crawler cl = new Crawler(url);
            //XDocument xdoc = cl.GetXDocument();

           

            //var res = from item in xdoc.Descendants(xmlns + "div")
            //          where item.Attribute("class") != null && item.Attribute("class").Value == "drop-frame"
            //          && item.Element(xmlns + "a") != null
            //          //select item;
            //          select new
            //          {
            //              Link = item.Element(xmlns + "a").Attribute("href").Value,
            //              Image = item.Element(xmlns + "a").Element(xmlns + "img").Attribute("src").Value,
            //              Title = item.Elements(xmlns + "p").ElementAt(0).Element(xmlns + "a").Value,
            //              Desc = item.Elements(xmlns + "p").ElementAt(1).Value
            //          };


            //foreach (var node in res)
            //{
            //    Console.WriteLine(node);
            //    Console.WriteLine("\n");
            //}
            //Console.ReadKey();
        //}        
    }
}
