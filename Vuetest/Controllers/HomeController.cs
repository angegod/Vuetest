using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vuetest.Models;

namespace Vuetest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult GetMessages()
        {
            List<Product> messages = new List<Product>();
            Repository r = new Repository();
            messages = r.GetAllMessages();
            return Json(messages, JsonRequestBehavior.AllowGet);
        }
    }
}