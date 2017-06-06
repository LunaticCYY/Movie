using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie.Controllers
{
    public class HomeController : Controller
    {
        // 首页
        public ActionResult Index()
        {
            return View();
        }

        // 关于
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //联系
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}