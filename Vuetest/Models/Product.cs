using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vuetest.Models
{
    public class Product
    {
        public Product(string In_id,string In_name,int In_count,int In_Price,string In_locate)
        {
            itemId = In_id;
            itemName = In_name;
            itemCount = In_count;
            itemPrice = In_Price;
            itemLocate = In_locate;
        }


        public string itemId { get; set; }

        public string itemName { get; set;}

        public int itemCount { get; set; }

        public int itemPrice { get; set; }

        public string itemLocate { get; set; }

    }
}