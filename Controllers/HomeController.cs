using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaveTheCave.Models;

namespace WaveTheCave.Controllers
{
    
    
        public class HomeController : Controller
        {
            private static ModelDBContext db = new ModelDBContext();
        public ActionResult Index()
        {

            ViewBag.Title = "Home Page";

            return View();
        }

       
     
        public ActionResult ChiSiamo()
        {
            return View();
        }
        

    }
}
