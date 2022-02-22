using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Data.SqlTypes;
using Newtonsoft.Json;
using Vuetest.Models;


namespace Vuetest.Controllers
{
    public class MysqlController : Controller
    {
        // GET: Mysql
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test1()
        {
            return View();
        }


        public ActionResult Test2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test1_output(string list)
        {

            if (string.IsNullOrEmpty(list))
            {
                TempData["Message"] = "輸入值不可以為空值";
            }

            string connString = "server=localhost;port=3306;user=root;password=ange0909;database=vuetest;";

            MySqlConnection conn = new MySqlConnection(connString);

            data[] list1 = JsonConvert.DeserializeObject<data[]>(list);


            foreach (var item in list1)
            {
                string insert = "insert into vuetest (nickname,name) values (@nickname,@name)";


                MySqlCommand cmd = new MySqlCommand(insert, conn);

                cmd.Parameters.Add("@nickname", MySqlDbType.VarChar, 200).Value = item.nickname;
                cmd.Parameters.Add("@name", MySqlDbType.VarChar, 200).Value = item.name;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

          
            return RedirectToAction("Test1");
        }
    }
}