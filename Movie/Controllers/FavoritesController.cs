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
    public class FavoritesController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: Favorites
        public ActionResult Index(string UserId, string VideoId)
        {
            var favorite = from m in db.Favorites select m;
            if (!String.IsNullOrEmpty(UserId))
            {
                int uid = int.Parse(UserId);
                favorite = favorite.Where(s => s.UserId == uid);
            }
            if (!String.IsNullOrEmpty(VideoId))
            {
                int vid = int.Parse(VideoId);
                favorite = favorite.Where(s => s.VideoId == vid);
            }
            return View(favorite);
        }

        // GET: Favorites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // GET: Favorites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Favorites/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FavoriteId,UserId,VideoId,FavoriteTime")] Favorite favorite)
        {
            // 判断从create.cshtml页面传过来的favorite类中是否合法
            if (ModelState.IsValid)
            {
                // 如果是，则在评论表取得收藏表中最大的历史记录编号
                var MaxId = db.Favorites.Any() ? db.Favorites.Max(p => p.FavoriteId) : 0;
                // 将取得最大评论编号加一赋值给将要创建的收藏
                favorite.FavoriteId = MaxId + 1;
                // Favorites表里增加新评论
                db.Favorites.Add(favorite);
                // 数据库保存
                db.SaveChanges();
                // 跳转收藏首页
                return RedirectToAction("Index");
            }
            // 从create.cshtml页面传过来的history类中不合法，直接返回原来的页面
            return View(favorite);
        }

        // GET: Favorites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // POST: Favorites/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FavoriteId,UserId,VideoId,FavoriteTime")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favorite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(favorite);
        }

        // GET: Favorites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // POST: Favorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Favorite favorite = db.Favorites.Find(id);
            db.Favorites.Remove(favorite);
            db.SaveChanges();
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
