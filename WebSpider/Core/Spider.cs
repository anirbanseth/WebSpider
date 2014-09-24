using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Data;
using Newtonsoft.Json;

namespace WebSpider.Core
{
   public class Spider
    {
       Cache Cache = new Cache();
       Browser browser = new Browser();

       public Spider() {}
       public Spider(Browser browser)
       {
           this.browser = browser;
       }

       public List<Categories> ParseCatagory(string url)
        {
            List<Categories> categoriesList = new List<Categories>();
            #region [URL Scraping]
            //var getHtmlWeb = new HtmlWeb();
            //var doc = getHtmlWeb.Load(url);
            //var doc = Cache.GetUrl(url);
            browser.Url = url;
            var doc = browser.GetWebRequest();
            #endregion


            HtmlNode node = doc.DocumentNode.SelectSingleNode("//ul[@class='subCategory']");
            var hoteleWebsiteDoc = (from element in node.Descendants("a")
                                    where element.ParentNode.Name.Equals("li") &&
                                    element.Attributes.Contains("href")
                                    select new
                                    {
                                        URL = element.Attributes["href"].Value,
                                        Desc=element.InnerText
                                    }).ToList();

            foreach (var obj in hoteleWebsiteDoc)
            {
                Categories oCategories = new Categories();
                oCategories.categoriesUrl ="http://adiglobal.us"+ obj.URL.Replace("amp;","");
                oCategories.categoriesName = RemoveHtmlCharacters(obj.Desc);
                oCategories.SubCatagoryList = ParseSubCatagory(oCategories.categoriesUrl);
                categoriesList.Add(oCategories);
            }
            return categoriesList;
        }


        private List<SubCatagory> ParseSubCatagory(string url)
        {
            List<SubCatagory> subCatagoryList = new List<SubCatagory>();

            #region [URL Scraping]
            //var getHtmlWeb1 = new HtmlWeb();
            //var doc1 = getHtmlWeb1.Load(url);
            //var doc1 = Cache.GetUrl(url);
            browser.Url = url;
            browser.Url = url;
            var doc1 = browser.GetWebRequest();
            #endregion

            HtmlNode node = doc1.DocumentNode.SelectSingleNode("//div[@class='categories']");
            node = node.SelectSingleNode("//ul[@class='subCategory toplvls']");

            var hoteleWebsiteDoc = (from element in node.Descendants("a")
                                    where element.ParentNode.Name.Equals("li") &&
                                    element.Attributes.Contains("href")
                                    select new
                                    {
                                        URL = element.Attributes["href"].Value,
                                        Desc = element.InnerText
                                    }).ToList();

            foreach (var obj in hoteleWebsiteDoc)
            {
                SubCatagory oSubCatagory = new SubCatagory();
                oSubCatagory.subCategoriesUrl = "http://adiglobal.us" + obj.URL.Replace("amp;", ""); ;
                oSubCatagory.subCategoriesName = RemoveHtmlCharacters(obj.Desc);
                oSubCatagory.subSubCatagoryList = ParseSubSubCatagory(oSubCatagory.subCategoriesUrl);
                subCatagoryList.Add(oSubCatagory);
            }

            return subCatagoryList;
        }


        private List<SubSubCatagory> ParseSubSubCatagory(string url)
        {
            List<SubSubCatagory> subSubCatagoryList = new List<SubSubCatagory>();

            #region [URL Scraping]
            //var getHtmlWeb = new HtmlWeb();
            //var doc = getHtmlWeb.Load(url);
            //var doc = Cache.GetUrl(url);
            browser.Url = url;
            var doc = browser.GetWebRequest();
            #endregion


            HtmlNode node = doc.DocumentNode.SelectSingleNode("//ul[@class='subCategory']");
            var hoteleWebsiteDoc = (from element in node.Descendants("a")
                                    where element.ParentNode.Name.Equals("li") &&
                                    element.Attributes.Contains("href")
                                    select new
                                    {
                                        URL = element.Attributes["href"].Value,
                                        Desc = element.InnerText
                                    }).ToList();

            foreach (var obj in hoteleWebsiteDoc)
            {
                SubSubCatagory oSubSubCatagory = new SubSubCatagory();
                oSubSubCatagory.subSubCategoriesUrl = "http://adiglobal.us" + obj.URL.Replace("amp;", ""); ;
                oSubSubCatagory.subSubCategoriesName = RemoveHtmlCharacters(obj.Desc);
                subSubCatagoryList.Add(oSubSubCatagory);
            }

            return subSubCatagoryList;
        }


        private String RemoveHtmlCharacters(String text)
        {
            text = text.Replace("&amp;", "&");
            return text;
        }


        public List<Product> ParseProducts(String url)
        {
            List<Product> Products = new List<Product>();

            var doc = Cache.GetUrl(url);
            HtmlNodeCollection productCollection = doc.DocumentNode.SelectNodes("//li[@class='productView']");
            HtmlNodeCollection pagesCollection = doc.DocumentNode.SelectNodes("//div[@class='paging']");

            if (!ReferenceEquals(productCollection, null))
            {
                //HtmlNodeCollection productHtmlNode;
                Product product;
                foreach (HtmlNode productNode in productCollection)
                {
                    product = new Product();

                    product.SmallImage = productNode.Descendants("div")
                        .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("product-image"))
                        .FirstOrDefault().Descendants("img").FirstOrDefault().Attributes["src"].Value;

                    var productPrice = productNode.Descendants("div")
                        .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("price"));

                    product.Name = productNode.Descendants("li")
                        .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("product-title"))
                        .FirstOrDefault().Descendants("a").FirstOrDefault().Attributes["title"].Value;

                    product.Url = "http://adiglobal.us" + productNode.Descendants("li")
                        .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("product-title"))
                        .FirstOrDefault().Descendants("a").FirstOrDefault().Attributes["href"].Value;

                    product.VendorName = productNode.Descendants("li").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("vendorName")).FirstOrDefault().InnerHtml;
                    product.VendorModel = productNode.Descendants("li").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("vendorNbr")).FirstOrDefault().InnerHtml.Replace("Model #: ", "");
                    product.PartNumber = productNode.Descendants("li").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("partNbr")).FirstOrDefault().InnerHtml.Replace("ADI #: ", "");

                    GetProductDetails(ref product);

                    Products.Add(product);
                }
            }
            return Products;
        }


        private void GetProductDetails(ref Product product) 
        {
            var productDoc = Cache.GetUrl(product.Url);

            product.BigImage = productDoc.DocumentNode.SelectSingleNode("//div[@class='product-img-big']").Descendants("img").FirstOrDefault().Attributes["src"].Value;
            var productDetails = productDoc.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("tab-content overview")).FirstOrDefault();
            var productSpecs = productDoc.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("tab-content specs")).FirstOrDefault();

            if (!ReferenceEquals(productDetails, null))
            {
                var mainFeatures = productDetails.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("spec-sectionfea")).FirstOrDefault();
                var marketingInfo1 = productDetails.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("spec-sectionmktg")).FirstOrDefault();
                var marketingInfo2 = productDetails.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("spec-sectionmktg")).LastOrDefault();
            }
            if (!ReferenceEquals(productSpecs, null))
            {
                var productSpecsSection = productDetails.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("spec-section")).FirstOrDefault();
            }
        }


        #region [Product Specifications]
        public void GetProductSpecification(String ProductID, String ProductName)
        {
            WebClient webclient = new WebClient();
            Uri uristring = null;
            uristring = new Uri("http://adiglobal.us/_vti_bin/requests.asmx/ProductSpecifications");
            webclient.Headers.Add("Content-Type", "application/json; charset=utf-8");
            webclient.Headers["ContentType"] = "application/json";
            string JsonStringParams = String.Format("{{\'productID\':\'{0}\',\'productDefinitionName\':\'{1}\'}}", ProductID, ProductName);
            webclient.UploadStringCompleted += wc_ParseProductSpecs;
            //String responce = webclient.UploadString("http://adiglobal.us/_vti_bin/requests.asmx/ProductSpecifications", JsonStringParams);
            //Post data like this
            webclient.UploadStringAsync(uristring, "POST", JsonStringParams);
        }
        private void wc_ParseProductSpecs(object sender, UploadStringCompletedEventArgs e)
        {
            try
            {

                if (e.Result != null)
                {
                    string responce = e.Result.ToString();
                    //To Do Your functionality
                    DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(responce);
                }
            }
            catch
            {
            }
        }
        #endregion
        
    }
}
