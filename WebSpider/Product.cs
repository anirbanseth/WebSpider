using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider
{
    public class Product
    {
        public String SmallImage { get; set; }
        public String BigImage { get; set; }
        public String Name { get; set; }
        public String VendorName { get; set; }
        public String VendorModel { get; set; }
        public String PartNumber { get; set; }

        public String Url { get; set; }

        public List<String> ProductFeatures { get; set; }

        public Product()
        {
            ProductFeatures = new List<string>();
        }

    }
}
