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
        // 数据库连接
        private MovieContext db = new MovieContext();

        // 视频首页
        public ActionResult Index(string Vtype, string Vname)
        {
            var TypeList = new List<string>();
            var TypeQuery = from d in db.Videos orderby d.Vtype select d.Vtype;
            TypeList.AddRange(TypeQuery.Distinct());
            ViewBag.Vtype = new SelectList(TypeList);
            var videos = from m in db.Videos select m;
            if (!String.IsNullOrEmpty(Vname))
            {
                videos = videos.Where(s => s.Vname.Contains(Vname));
            }
            if (!String.IsNullOrEmpty(Vtype))
            {
                videos = videos.Where(s => s.Vtype == Vtype);
            }
            return View(videos);
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

        public ActionResult Upload()
        {
            return View();
        }
        
        // 创建视频Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file2,HttpPostedFileBase file1,[Bind(Include = "VideoId,Vname,Vurl,Thumbnail,ViewedNum,UploadTime,Vtype,UserId,Vinfo")] Video video)
        {
            if(file1==null)
            {
                return Content("没有文件！", "text/plain");
            }
            if(file2==null)
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
            video.Thumbnail = "123";
            // 用户ID
            //video.UserId = int.Parse(Request.Cookies["uid"].Value);
            video.UserId = 1;
            // 电影上传时间
            video.UploadTime = DateTime.Now.ToString("0:yyyy-MM-dd");
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
            Video vi = db.Videos.Find(id);
            var com = db.Comments.Where(c=>c.VideoId==vi.VideoId);
            var his = db.Histories.Where(c => c.VideoId == vi.VideoId);
            var score = from m in db.Scores select m;
            score = score.Where(c => c.VideoId == id);
            var videofile = Request.MapPath(vi.Vurl);
            var pic = Request.MapPath(vi.Thumbnail);
            System.IO.File.Delete(videofile);
            System.IO.File.Delete(pic);
            if(score!=null)
                foreach(var a in score)
            db.Scores.Remove(a);
            if(his!=null)
                foreach(var a in his)
            db.Histories.Remove(a);
            if(com!=null)
                foreach(var a in com)
            db.Comments.Remove(a);
            db.Videos.Remove(vi);
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
