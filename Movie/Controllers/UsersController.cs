using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Movie.Models;
using System.Data.Entity.Infrastructure;

namespace Movie.Controllers
{
    public class UsersController : Controller
    {
        // 数据库连接
        private MovieContext db = new MovieContext();

        // List.cshtml 显示用户表的所有内容
        public ActionResult List(string NickName, string Email)
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

        // Detail.cshtml 获取某个UserId显示某个用户的详细信息
        public ActionResult Details(int? id)
        {
            // 如果id为空
            if (id == null)
            {
                // 请求错误信息
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // 通过id查询该用户
            User user = db.Users.Find(id);
            // 如果没有查询到该用户
            if (user == null)
            {
                // 返回没有找到信息
                return HttpNotFound();
            }
            // 返回该用户信息页面
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        // 创建用户Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,NickName,Password,Email,Privilege")] User user)
        {
            // 判断从create.cshtml页面传过来的user类中是否合法
            if (ModelState.IsValid)
            {
                // 如果是，则在用户表取得用户表中最大的用户编号
                var MaxId = db.Users.Any() ? db.Users.Max(p => p.UserId) : 0;
                // 将取得最大用户编号加一赋值给将要创建的用户
                user.UserId = MaxId + 1;
                // 默认用户权限为0
                user.Privilege = 0;
                // 用户表中插入该用户
                db.Users.Add(user);
                // 数据库保存
                db.SaveChanges();
                // 跳转用户List表查看所有用户
                return RedirectToAction("List");
            }
            // 从create.cshtml页面传过来的user类中不合法，直接返回原来的页面
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

        // 编辑用户Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Edit(User user)
        {
            // 判断user类是否合法
            var users = new User
            {
                UserId = user.UserId,
                NickName = user.NickName,
                Password = user.Password,
                Email = user.Email,
                Privilege = user.Privilege
            };
            User u = db.Users.Find(user.UserId);
            db.Users.Remove(u);
            db.SaveChanges();
            db.Users.Add(users);
            db.SaveChanges();
            // 跳转List页
                return RedirectToAction("List");


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
            return View();
        }

        // 删除用户Action
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // 在用户表中查询该用户
            User user = db.Users.Find(id);
            var his = db.Histories.Where(c => c.UserId == user.UserId);
            var com = db.Comments.Where(c => c.UserId == user.UserId);
            var fav = db.Favorites.Where(c => c.UserId == user.UserId);
            if (his != null)
                foreach(var a in his)
                db.Histories.Remove(a);
            if (com != null)
                foreach (var a in com)
                    db.Comments.Remove(a);
            if (fav != null)
                foreach (var a in fav)
                    db.Favorites.Remove(a);
            // 删除该用户
            db.Users.Remove(user);
            // 数据库保存
            db.SaveChanges();
            // 跳转List页
            return RedirectToAction("List");
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
