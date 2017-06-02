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

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
                if(user.Privilege!=1)
                {
                    ModelState.AddModelError("", "抱歉，该用户没有权限");
                }
                else
                {
                    return RedirectToAction("Index", "Users");
                }
            }
            else
            {
                ModelState.AddModelError("", "用户名或密码错误");
            }
            return RedirectToAction("Login","Account");
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
                var MaxId = db.Users.Select(p => p.UserId).Max();
                if (MaxId == 0)
                {
                    user.UserId = 1;
                    user.Privilege = 1;
                }
                else
                {
                    user.UserId = MaxId + 1;
                    user.Privilege = 1;
                }
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }
    }
}
