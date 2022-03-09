using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using Vuetest.Models;
using System.Drawing;
using System.Text;
using System.ComponentModel;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;
using System.Text.RegularExpressions;

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
        public ActionResult Upload()
        {
            //Byte[] bytes = null;
            HttpPostedFileBase file = Request.Files["File"];
            
            Stream fs = file.InputStream;

            BinaryReader br = new BinaryReader(fs);
            byte[] imgarray = new byte[(int)fs.Length];
            file.InputStream.Read(imgarray, 0, (int)fs.Length);


            string conn = "Server=TR\\SQLEXPRESS;Database=Imgtext;uid=ange;pwd=ange0909;Trusted_Connection=True;MultipleActiveResultSets=True;";
            string insert = "insert into storage (ImageName,Image) values (@ImageName,@Image);";
            SqlConnection mycon = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(insert,mycon);
            cmd.Parameters.Add("@ImageName", SqlDbType.VarChar, 200).Value = "Testname";
            cmd.Parameters.Add("@Image", SqlDbType.Image).Value = imgarray;
            

            mycon.Open();
            cmd.ExecuteNonQuery();
            mycon.Close();

            return RedirectToAction("Showimg");
        }

        public ActionResult ShowImg()
        {
            string conn = "Server=TR\\SQLEXPRESS;Database=Imgtext;uid=ange;pwd=ange0909;Trusted_Connection=True;MultipleActiveResultSets=True;";

            string search = "select * from storage";
            SqlConnection mycon = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(search, mycon);
            mycon.Open();

            SqlDataReader mydr = cmd.ExecuteReader();
            List<ShowImage> list1 = new List<ShowImage>();

            while (mydr.HasRows)
            {
                while (mydr.Read())
                {
                    //Image oImage;
                    //Bitmap oBitmap;

                    string ImgaeName = mydr.GetString(1);
                    byte[] imagearray = (byte[])mydr[2];

                    TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                    Bitmap MyBitmap = (Bitmap)tc.ConvertFrom(imagearray);

                    string imgString = Convert.ToBase64String(imagearray);
                    


                    list1.Add(new ShowImage(ImgaeName,imgString));


                }
                mydr.NextResult();
            }
            mycon.Close();
            if (ViewBag.imglist==null)
            {
                //ViewBag.imglist = list1;
                ViewBag.imglist = Session["imglist1"];
            }
            
            
            return View();
        }

        public ActionResult GetImgdata()
        {
            string data= Getting();
            Session["test1"] = data;
            data = data.TrimStart('"');
            data = data.TrimEnd('"');
            data=data.Replace(@"\", "");


            List<ShowImage> list1 = JsonConvert.DeserializeObject<List<ShowImage>>(data);

            Session["imglist1"] = list1;
            Session["test"] = data;
;
            return RedirectToAction("ShowImg");
        }

        public string Getting()
        {
            var client = new HttpClient();

            var Response = client.GetAsync("https://localhost:44310/api/Imgreturn/ReturnValue").Result;

            string returnvalue =  Response.Content.ReadAsStringAsync().Result;

            

            return returnvalue;
        }

    }
}