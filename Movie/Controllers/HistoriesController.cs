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

namespace Movie.Controllers
{
    public class HistoriesController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: Histories
        public ActionResult Index(string UserId, string VideoId, int? page)
        {
            var history = from m in db.Histories select m;
            if (!String.IsNullOrEmpty(UserId))
            {
                int uid = int.Parse(UserId);
                history = history.Where(s => s.UserId == uid);
            }
            if (!String.IsNullOrEmpty(VideoId))
            {
                int vid = int.Parse(VideoId);
                history = history.Where(s => s.VideoId == vid);
            }
            List<Total> total = new List<Total>();
            foreach(var item in history)
            {
                Total detail = new Total();
                detail.history = item;
                var user = db.Users.Where(c => c.UserId == item.UserId).FirstOrDefault();
                detail.user = user;
                var video = db.Videos.Where(c => c.VideoId == item.VideoId).FirstOrDefault();
                detail.video = video;
                total.Add(detail);
            }
            int pagenumber = page ?? 1;
            int pagesize = 10;
            IPagedList<Total> pagelist = total.ToPagedList(pagenumber, pagesize);
            return View(pagelist);
        }



        // GET: Histories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        // GET: Histories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Histories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoryId,UserId,VideoId,HistoryTime")] History history)
        {
            if (ModelState.IsValid)
            {
                // 如果是，则在评论表取得历史记录表中最大的历史记录编号
                var MaxId = db.Histories.Any() ? db.Histories.Max(p => p.HistoryId) : 0;
                // 将取得最大评论编号加一赋值给将要创建的评论
                history.HistoryId = MaxId + 1;
                db.Histories.Add(history);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(history);
        }

        // GET: Histories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        // POST: Histories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HistoryId,UserId,VideoId,HistoryTime")] History history)
        {
            if (ModelState.IsValid)
            {
                db.Entry(history).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(history);
        }

        // GET: Histories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        // POST: Histories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // 通过Delete.cshtml页面传来的id来查询历史表
            History history = db.Histories.Find(id);
            // 历史表里面删除该历史记录
            db.Histories.Remove(history);
            var comment = db.Comments.Where(c => c.UserId == history.UserId).Where(c => c.VideoId == history.VideoId);
            foreach (var item in comment)
            {
                db.Comments.Remove(item);
            }
            var favorite = db.Favorites.Where(c => c.UserId == history.UserId).Where(c => c.VideoId == history.VideoId).FirstOrDefault();
            db.Favorites.Remove(favorite);
            // 数据表保存
            db.SaveChanges();
            // 跳转历史表首页
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
