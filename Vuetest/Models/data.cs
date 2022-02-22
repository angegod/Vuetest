using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Vuetest.Models
{
    public class data
    {
        public data(int get_id,string get_nickname,string get_name)
        {
            id = get_id;
            nickname = get_nickname;
            name = get_name;
        }


        public int id;

        public string nickname;

        public string name;
    }
}