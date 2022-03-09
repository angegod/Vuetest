using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Vuetest.Models
{
    public class ShowImage
    {
        public ShowImage(string name,string inputimg)
        {
            ImgName = name;
            image = inputimg;
        }

        public string ImgName { get; set; }

        public string image { get; set; }
    }
}