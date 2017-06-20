using Movie.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie.Controllers
{
    [CheckLogin]
    public class HomeController : Controller
    {
        // 首页
        public ActionResult Index()
        {
            if (Cookies.CheckPrivilege()==true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
        }

        // 关于
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            if (Cookies.CheckPrivilege())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
        }

        //联系
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            if (Cookies.CheckPrivilege())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
        }
    }
}