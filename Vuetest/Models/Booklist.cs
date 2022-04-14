using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vuetest.Models
{
    public class Booklist
    {
        public Booklist(int get_id,string get_name,string get_productID,int get_price,string get_source,int get_count,string get_link)
        {
            id = get_id;
            name = get_name;
            productID = get_productID;
            price = get_price;
            source = get_source;
            count = get_count;
            link = get_link;
            total = 0;
        }

        public int id{ get; set; }

        public string name { get; set; }

        public string productID { get; set; }

        public int price { get; set; }

        public string source { get; set; }

        public int count { get; set; }

        public string link { get; set; }

        public int total { get; set; }

    }
}