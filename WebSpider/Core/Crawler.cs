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
using Newtonsoft.Json;
using System.Data;

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


        class ProductSpecs
        {
            String [][]GeneralInformation;
            String [][]TechnicalInformation;
        }

        

        public void GetInventory()
        {
            WebClient webclient = new WebClient();
            Uri uristring = null;
            //Please replace your webservice url here                                                  
            uristring = new Uri("https://adiglobal.us/_vti_bin/requests.asmx/InventoryRingPuddle ");
            webclient.Headers["ContentType"] = "application/json";
            webclient.Headers["Content-Type"] = "application/json";
            webclient.Headers["Cookie"] = "__CSCookie=j5QZ5EvO8xL6LbfevfyKyav1l+r1EwOmqdsspk6kpxr6tbCYt9AKwq2K1V74H+yN; s_pers=%20s_vnum%3D1412961730920%2526vn%253D21%7C1412961730920%3B%20s_fid%3D3F324E676008E0F6-1103D4C5C8F30F98%7C1474627790718%3B%20s_visitStart%3D1%7C1411471190749%3B%20s_invisit%3Dtrue%7C1411471190752%3B%20s_nr%3D1411469390754-Repeat%7C1414061390754%3B%20s_lv%3D1411469390756%7C1506077390756%3B%20s_lv_s%3DLess%2520than%25201%2520day%7C1411471190756%3B%20gpv_p28%3Dno%2520value%7C1411471190762%3B%20s_depth%3D3%7C1411471190768%3B; s_vi=[CS]v1|2A084468052A3415-60000110E00B2ECD[CE]; adiglobal-cookie=R4024463689; ASP.NET_SessionId=4pxkjsbzk5agicfkkfabic55; CampaignHistory=891,893,891,893,894,894,872,864,680,872,872,872,896,897,896,897,895,895,872,896,897,896,897,895,895,872,872,872,891,893,891,893,894,894,872; s_sess=%20s_cc%3Dtrue%3B%20s_ppv%3D-%3B%20s_sq%3D%3B; __UserDateCookie=JcDxK6GanQdwRyrrtgVyLOryaE5m1xKJ79peCcFPNlU=; __CSUserNameCookie=ba0zvFoUZVwD8h5F9Rz9Jw==; __CSCookie_Auth=C71D6F433CB2C6E23445262225FA81646A9F3A94=KTnKPgThX/B2cFH3HD8LQIE6TAH82HNiJEzlJr2PmUiNQ5kHBduZAyu9gmoRxq2c&FA46A75AEDB98EC4BA344FC277C99D2CBDE6A9B4=4fdMEjYid7I00wE1PqhMVo5DEGWvpQlhh5S8Kdr1sONxTTR8oh2tz3xe0UELPToC&2B1D97B254C24987FB33EC1FD490AF7EC83D07F7=bvtKt8SRFvvWx4oAQeOjiO7N2Hcqaa22IX0L6PdwsgAc4ThjDw0zwBh7sRXTYgp0&92A3498774182CBE9B9B3818F9B3D4B774C6A626=cphpEz2dO47ti+075L4dX24WW1Axu5sGSoia8phtr5u4N4sX0khvLadLI/tMjIod; __OrgDateCookie=13gcMo4VwBukDJ82O+wuoYTBH880yNo8ycCDlhbTMYM=; __CSOrgNameCookie=CRnZIIVwp2EZsbXKthLKfg==; __CSOrgNumberCookie=rpR1WhmRG3SrsZoVl/9orQ==; __CSOrgSuffixCookie=000; __SessionCookie=5O2613uoaNozNXLkGtIEHdyPNiTelHguBnsFYUT1QUX6Zb9MuDeZ6nSlrdixzZsk; __CSCheckout_Anonymous=B8u8TL/8mKOAxwSXtolicw==; .ASPXAUTH=08F4F270FD51DA51BDA04F6B3D552CAC425E2716AA2899EF8669A0BF9A7553A5B4182AED1B8A6CE02E3B68344E8401BCACCAC16A61E471A4AF18E530F73A998E151595B126C2CEFA1ECB8149F933AB2DBB624CC73414CA11538BA50A00500259A01A479BF04132DBA5ED4C0CAC51646CFA8B94F3";
            //webclient.Headers["Cookie"] = "ASP.NET_SessionId=4pxkjsbzk5agicfkkfabic55";
            string JsonStringParams = "{'productId':'BS-BEPHOC'}";
            webclient.UploadStringCompleted += wc_UploadStringCompleted;
            //Post data like this                                                                          
            webclient.UploadStringAsync(uristring, "POST", JsonStringParams);
        }

        //public void GetProductSpecification(String ProductID, String ProductName)
        //{
        //    WebClient webclient = new WebClient();
        //    Uri uristring = null;
        //    //Please replace your webservice url here                                                 
        //    uristring = new Uri("http://adiglobal.us/_vti_bin/requests.asmx/SubmitQuery");
        //    webclient.Headers["ContentType"] = "application/json";
        //    webclient.Headers["Content-Type"] = "application/json";
        //    string JsonStringParams = "{'request':{'SearchCriterias':[],'CategoryName':'6400','SortBy':'b','PageNumber':5,'ResultsPerPage':64,'ReturnRefinersOnly':false,'PageCode':6,'ExcludedRefiners':'','SearchTerm':''},'Adsrequest':{'Rcat':'6400','FirstParentRcat':'6000','VendorId':'','Mode':'c','PromoOption':null,'SearchTerm':null}}";
        //    webclient.UploadStringCompleted += wc_UploadStringCompleted;
        //    //Post data like this                                                                         
        //    webclient.UploadStringAsync(uristring, "POST", JsonStringParams);
        //}

        private void wc_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            try
            {

                if (e.Result != null)
                {
                    string responce = e.Result.ToString();
                    //To Do Your functionality
                }
            }
            catch
            {
            }
        }
    }
}
