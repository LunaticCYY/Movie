using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Movie.Models;

namespace Movie.Controllers
{
    public class UsersController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: Users
        public ActionResult Index(string NickName, string Email)
        {
            var users = from m in db.Users select m;
            if (!String.IsNullOrEmpty(NickName))
            {
                users = users.Where(s => s.NickName.Contains(NickName));
            }
            if (!string.IsNullOrEmpty(Email))
            {
                users = users.Where(s => s.Email.Contains(Email));
            }
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,NickName,Password,Email,Privilege")] User user)
        {
            if (ModelState.IsValid)
            {
                // 如果是，则在用户表取得用户表中最大的用户编号
                var MaxId = db.Users.Any() ? db.Users.Max(p => p.UserId) : 0;
                // 将取得最大用户编号加一赋值给将要创建的用户
                user.UserId = MaxId + 1;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,NickName,Password,Email,Privilege")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // 在用户表中查询该用户
            User user = db.Users.Find(id);
            var vid = db.Videos.Where(c => c.UserId == user.UserId);
            var his = db.Histories.Where(c => c.UserId == user.UserId);
            var com = db.Comments.Where(c => c.UserId == user.UserId);
            var fav = db.Favorites.Where(c => c.UserId == user.UserId);
            if(vid != null)
            {
                foreach(var a in vid)
                {
                    db.Videos.Remove(a);
                }
            }
            if (his != null)
            {
                foreach (var a in his)
                {
                    db.Histories.Remove(a);
                }
            }  
            if (com != null)
            {
                foreach (var a in com)
                {
                    db.Comments.Remove(a);
                }
            }    
            if (fav != null)
            {
                foreach (var a in fav)
                {
                    db.Favorites.Remove(a);
                }
            }  
            // 删除该用户
            db.Users.Remove(user);
            // 数据库保存
            db.SaveChanges();
            // 跳转List页
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
