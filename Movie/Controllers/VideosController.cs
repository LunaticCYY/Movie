using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Movie.Models;
using System.IO;

namespace Movie.Controllers
{
    public class VideosController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: Videos
        public ActionResult Index()
        {
            var video = from m in db.Videos select m;
            List<Total> total = new List<Total>();
            foreach (var item in video)
            {
                Total detail = new Total();
                detail.video = item;
                var user = db.Users.Where(c => c.UserId == item.UserId).FirstOrDefault();
                detail.user = user;
                total.Add(detail);
            }
            return View(total);
        }

        // GET: Videos/Details/5
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

        // GET: Videos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Videos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VideoId,Vname,Vurl,Thumbnail,ViewedNum,UploadTime,Vtype,UserId,Vinfo")] Video video)
        {
            if (ModelState.IsValid)
            {
                db.Videos.Add(video);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(video);
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

        // POST: Videos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VideoId,Vname,Vurl,Thumbnail,ViewedNum,UploadTime,Vtype,UserId,Vinfo")] Video video)
        {
            if (ModelState.IsValid)
            {
                var MaxId = db.Videos.Any() ? db.Videos.Max(p => p.VideoId) : 0;
                // 将取得最大电影编号加一赋值给将要创建的电影
                video.VideoId = MaxId + 1;
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

        // POST: Videos/Delete/5
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

        public ActionResult Upload()
        {
            return View();
        }

        // 创建视频Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file2, HttpPostedFileBase file1, [Bind(Include = "VideoId,Vname,Vurl,Thumbnail,ViewedNum,UploadTime,Vtype,UserId,Vinfo")] Video video)
        {
            if (file1 == null)
            {
                return Content("没有文件！", "text/plain");
            }
            if (file2 == null)
            {
                return Content("没有图片！", "text/plain");
            }
            var picname = Path.Combine(Request.MapPath("~/Image"), Path.GetFileName(file2.FileName));
            var filename = Path.Combine(Request.MapPath("~/Video"), Path.GetFileName(file1.FileName));
            // 在用电影表取得电影表中最大的电影编号
            var MaxId = db.Videos.Any() ? db.Videos.Max(p => p.VideoId) : 0;
            // 将取得最大电影编号加一赋值给将要创建的电影
            video.VideoId = MaxId + 1;
            // 电影默认观看数为0
            video.ViewedNum = 0;
            // 电影的封面
            //video.Thumbnail = "123";
            // 用户ID
            video.UserId = int.Parse(Request.Cookies["uid"].Value);
            // 电影上传时间
            video.UploadTime = DateTime.Now.ToString();
            try
            {
                file1.SaveAs(filename);
                file2.SaveAs(picname);
                // 电影存储地址;//得到全部model信息
                video.Thumbnail = "~/Image/" + Path.GetFileName(file2.FileName);
                video.Vurl = "~/Video/" + Path.GetFileName(file1.FileName);
                //return Content("上传成功！", "text/plain");
                // 将这个对象插入数据库
                db.Videos.Add(video);
                // 数据库保存
                db.SaveChanges();
                return RedirectToAction("index");
            }
            catch
            {
                return Content("上传异常 ！", "text/plain");
            }
        }
    }
}
