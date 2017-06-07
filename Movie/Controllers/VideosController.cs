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
    public class VideosController : Controller
    {
        // 数据库连接
        private MovieContext db = new MovieContext();

        // 视频首页
        public ActionResult Index()
        {
            return View(db.Videos.ToList());
        }

        // 视频具体信息
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        // 创建视频Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VideoId,Vname,Vurl,Thumbnail,ViewedNum,UploadTime,Vtype,UserId,Vinfo")] Video video)
        {
            // 在用电影表取得电影表中最大的电影编号
            var MaxId = db.Videos.Any() ? db.Videos.Max(p => p.VideoId) : 0;
            // 将取得最大电影编号加一赋值给将要创建的电影
            video.VideoId = MaxId + 1;
            // 电影默认观看数为0
            video.ViewedNum = 0;
            // 电影上传时间
            video.UploadTime = DateTime.Now.ToString("0:yyyy-MM-dd");
            // 将这个对象插入数据库
            db.Videos.Add(video);
            // 数据库保存
            db.SaveChanges();
            // 跳转到主页
            return RedirectToAction("Index");
        }

        // GET: Videos/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // 编辑视频信息Action
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "VideoId,Vname,Vurl,Thumbnail,ViewedNum,UploadTime,Vtype,UserId,Vinfo")] Video video)
        {
            if (ModelState.IsValid)
            {
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(video);
        }

        // GET: Videos/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // 删除视频信息Action
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Video video = db.Videos.Find(id);
            db.Videos.Remove(video);
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
