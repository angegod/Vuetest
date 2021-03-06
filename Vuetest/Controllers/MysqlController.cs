using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Data.SqlTypes;
using Newtonsoft.Json;
using Vuetest.Models;
using System.Data.SqlClient;
using System.Data;


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
            string conn = "Server=TR\\SQLEXPRESS;Database=Imgtext;uid=ange;pwd=ange0909;Trusted_Connection=True;MultipleActiveResultSets=True;";
            string search = "select * from storage";
            SqlConnection mycon = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(search,mycon);

            List<Booklist> list = new List<Booklist>();
            mycon.Open();
            SqlDataReader mydr= cmd.ExecuteReader();
            int id = 0;

            while (mydr.HasRows)
            {
                while (mydr.Read())
                {
                    id++;
                    string name = mydr.GetString(1);
                    string productID = mydr.GetString(0);
                    int count = mydr.GetInt32(2);
                    string link = mydr.GetString(3);
                    int price = mydr.GetInt32(4);
                    string locate = mydr.GetString(5);

                    list.Add(new Booklist(id,name,productID,price,locate,count,link));
                }
                mydr.NextResult();
            }
            mycon.Close();
            var json = list.ToArray();

           ViewBag.getlist = list;

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

        [HttpPost]
        public ActionResult Test2_output([System.Web.Http.FromBody] string list)
        {

                if (string.IsNullOrEmpty(list))
                {
                    TempData["list"] = "輸入值不可以為空值";
                    //return RedirectToAction("Test1");
                }
                else
                {
                    TempData["list"] = list;
                }

                
                string conn = "Server=TR\\SQLEXPRESS;Database=Imgtext;uid=ange;pwd=ange0909;Trusted_Connection=True;MultipleActiveResultSets=True;";
                string insert = "insert into orders (id,userName,orders) values(@id,@userName,@orderdata)";

                //string order = JsonConvert.SerializeObject(list);

                SqlConnection mycon = new SqlConnection(conn);
                SqlCommand cmd = new SqlCommand(insert, mycon);

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@userName", SqlDbType.VarChar, 200).Value = "Ange";
                cmd.Parameters.Add("@orderdata", SqlDbType.VarChar).Value = list;

                cmd.CommandType = CommandType.Text;

                mycon.Open();

                cmd.ExecuteNonQuery();
                mycon.Close();


                return RedirectToAction("Test2");
            
            
            
        }
    }
}