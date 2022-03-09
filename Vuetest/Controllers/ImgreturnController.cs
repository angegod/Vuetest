using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Vuetest.Models;
using System.ComponentModel;
using System.Drawing;

namespace Vuetest.Controllers
{
    public class ImgreturnController : ApiController
    {
        

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        [Route("api/Imgreturn/Test")]
        [HttpGet]
        public string Test()
        {
            return "10";
        }



        
        [Route("api/Imgreturn/ReturnValue")]
        [HttpGet]
        public string ReturnValue()
        {
            string conn = "Server=TR\\SQLEXPRESS;Database=Imgtext;uid=ange;pwd=ange0909;Trusted_Connection=True;MultipleActiveResultSets=True;";

            string search = "select * from storage ";

            SqlConnection mycon = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(search, mycon);

            
            mycon.Open();

            List<ShowImage> list1 = new List<ShowImage>();
            SqlDataReader mydr = cmd.ExecuteReader();

            while (mydr.HasRows)
            {
                while (mydr.Read())
                {
                    string get_name = mydr.GetString(1);
                    byte[] imagearray = (byte[])mydr[2];

                    TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                    Bitmap MyBitmap = (Bitmap)tc.ConvertFrom(imagearray);

                    string imgString = "data:image/JPG;base64," + Convert.ToBase64String(imagearray);


                    list1.Add(new ShowImage(get_name, imgString));
                }
                mydr.NextResult();
            }
            mycon.Close();

            string returnString = JsonConvert.SerializeObject(list1);

            return returnString;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}