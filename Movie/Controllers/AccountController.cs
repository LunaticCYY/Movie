using Movie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie.Controllers
{
    public class AccountController : Controller
    {
        private MovieContext db = new MovieContext();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            var user = db.Users.Where(c => c.Email == model.Email).Where(c => c.Password == model.Password).FirstOrDefault();
            if (user != null)
            {

                HttpCookie mycoo = new HttpCookie("uid");
                mycoo.Path = "/UserOperation";
                mycoo.Value = user.UserId.ToString();
                mycoo.Expires = DateTime.Now.AddMinutes(1);
                Response.Cookies.Add(mycoo);
                if (user.Privilege != 1)
                {
                    return RedirectToAction("Index", "UserOperation");
                }
                else
                {

                    return RedirectToAction("List", "Users");
                }
            }
            else
            {
                ModelState.AddModelError("", "用户名或密码错误");
            }
            return RedirectToAction("Login", "Account");
            //return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserId,NickName,Password,Email,Privilege")] User user)
        {
            if (ModelState.IsValid)
            {
                var MaxId = db.Users.Any() ? db.Users.Max(p => p.UserId) : 0;
                user.UserId = MaxId + 1;
                user.Privilege = 1;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }
    }
}
