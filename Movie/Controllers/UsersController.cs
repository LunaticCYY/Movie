using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Movie.Models;
using PagedList;
using Movie.App_Start;

namespace Movie.Controllers
{
    [CheckLogin]
    public class UsersController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: Users
        public ActionResult Index(string NickName, string Email,int? page)
        {
            if (Cookies.CheckPrivilege() == true)
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
                users = users.OrderBy(c => c.UserId);
                int pagenumber = page ?? 1;
                int pagesize = 10;
                IPagedList<User> pagelist = users.ToPagedList(pagenumber, pagesize);
                return View(pagelist);
            }
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
            
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (Cookies.CheckPrivilege() == true)
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
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
            
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (Cookies.CheckPrivilege() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
            
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,NickName,Password,Email,Privilege")] User user)
        {
            if (Cookies.CheckPrivilege() == true)
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
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
            
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Cookies.CheckPrivilege() == true)
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
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
            
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,NickName,Password,Email,Privilege")] User user)
        {
            if (Cookies.CheckPrivilege() == true)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
            
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Cookies.CheckPrivilege() == true)
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
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
            
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Cookies.CheckPrivilege() == true)
            {
                // 在用户表中查询该用户
                User user = db.Users.Find(id);
                var vid = db.Videos.Where(c => c.UserId == user.UserId);
                var his = db.Histories.Where(c => c.UserId == user.UserId);
                var com = db.Comments.Where(c => c.UserId == user.UserId);
                var fav = db.Favorites.Where(c => c.UserId == user.UserId);
                if (vid != null)
                {
                    foreach (var a in vid)
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
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
            
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
