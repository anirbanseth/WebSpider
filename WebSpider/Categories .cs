using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider
{
   public class Categories
    {
        public string categoriesId { get; set; }
        public string categoriesName { get; set; }
        public string categoriesUrl { get; set; }
        public List<SubCatagory> SubCatagoryList { get; set; }
    }
}
