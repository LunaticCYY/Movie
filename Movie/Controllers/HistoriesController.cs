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
    public class HistoriesController : Controller
    {
        // 数据库连接
        private MovieContext db = new MovieContext();

        // Index.cshtml返回所有历史记录
        public ActionResult Index(string UserId, string VideoId)
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
            return View(history);
        }

        // Detail.cshtml 获取某个HistoryId显示某个历史记录的详细信息
        public ActionResult Details(int? id)
        {
            // 如果id为空
            if (id == null)
            {
                // 请求错误信息
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // 通过id查询该历史
            History history = db.Histories.Find(id);
            // 如果没有查询到该历史
            if (history == null)
            {
                // 返回没有找到信息
                return HttpNotFound();
            }
            // 返回该历史信息页面
            return View(history);
        }

        // GET: Histories/Create
        public ActionResult Create()
        {
            return View();
        }

        // 创建播放历史Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoryId,UserId,VideoId,HistoryTime")] History history)
        {
            // 判断从create.cshtml页面传过来的history类中是否合法
            if (ModelState.IsValid)
            {
                // 如果是，则在评论表取得历史记录表中最大的历史记录编号
                var MaxId = db.Histories.Any() ? db.Histories.Max(p => p.HistoryId) : 0;
                // 将取得最大评论编号加一赋值给将要创建的评论
                history.HistoryId = MaxId + 1;
                // Histories表里增加新评论
                db.Histories.Add(history);
                // 数据库保存
                db.SaveChanges();
                // 跳转历史记录首页
                return RedirectToAction("Index");
            }
            // 从create.cshtml页面传过来的history类中不合法，直接返回原来的页面
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

        // 编辑Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HistoryId,UserId,VideoId,HistoryTime")] History history)
        {
            // 判断从Edit.cshtml页面传过来的history类中是否合法
            if (ModelState.IsValid)
            {
                // 更改history数据
                db.Entry(history).State = EntityState.Modified;
                // 数据库保存
                db.SaveChanges();
                // 跳转评论首页
                return RedirectToAction("Index");
            }
            // 从Edit.cshtml页面传过来的history类不合法，直接返回原来的页面
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

        // 删除Action
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // 通过Delete.cshtml页面传来的id来查询历史表
            History history = db.Histories.Find(id);
            // 历史表里面删除该历史记录
            db.Histories.Remove(history);
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
