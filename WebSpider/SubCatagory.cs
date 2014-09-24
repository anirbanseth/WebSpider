using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider
{
   public class SubCatagory
    {
        public string subCategoriesId { get; set; }
        public string subCategoriesName { get; set; }
        public string subCategoriesUrl { get; set; }
        public List<SubSubCatagory> subSubCatagoryList { get; set; }
    }
}
