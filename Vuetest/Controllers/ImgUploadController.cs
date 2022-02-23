using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Data.SqlTypes;

namespace Vuetest.Controllers
{
    public class ImgUploadController : Controller
    {
        // GET: ImgUpload
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test1()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase File)
        {
            if (File != null && File.ContentLength > 0)
            {
                //存到資料夾
                //var FileName = Path.GetFileName(File.FileName);
                //var FilePath = Path.Combine(Server.MapPath("~/Images/"), FileName);
                //File.SaveAs(FilePath);


                //轉成byte 方法一 直接轉
                byte[] FileBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    File.InputStream.CopyTo(ms);
                    FileBytes = ms.GetBuffer();
                }

                string conn = "server=localhost;port=3306;user=root;password=ange0909;database=vuetest;";
                string insert = "insert into imgtext (img) values(@img)";
                MySqlConnection mycon = new MySqlConnection(conn);
                MySqlCommand cmd = new MySqlCommand(insert, mycon);

                cmd.Parameters.AddWithValue("@img", FileBytes);

                mycon.Open();
                cmd.ExecuteNonQuery();
                mycon.Close();

                TempData["Data"] = FileBytes;

            }



            return View();
        }
    }
}