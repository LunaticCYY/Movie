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
    public class UserOperationController : Controller
    {
        private MovieContext db = new MovieContext();
        static private int vid;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int? id)
        {
            var vi = db.Videos.Find(id);
            vid = vi.VideoId;
            VideoDetail detail = new VideoDetail();
            if (Request.Cookies["uid"] != null)
            {
                //从cookie中获取当前用户uid
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                //从历史记录里查询是否有该用户
                var history = db.Histories.Where(c => c.VideoId == vi.VideoId).Where(c => c.UserId == uid).FirstOrDefault();
                if (history != null)
                {
                    //如果有，则更新历史记录的时间
                    history.HistoryTime = DateTime.Now.ToString();
                    db.Entry(history).State = EntityState.Modified;
                    if (db.SaveChanges() != 1)
                    {
                        ModelState.AddModelError("", "服务器错误");
                    }
                }
                else
                {
                    //如果没有，则增加历史纪录
                    History addHistory = new History();
                    var MaxId = db.Histories.Any() ? db.Histories.Max(p => p.HistoryId) : 0;
                    addHistory.HistoryId = MaxId + 1;
                    addHistory.UserId = uid;
                    addHistory.VideoId = vi.VideoId;
                    addHistory.HistoryTime = DateTime.Now.ToString();
                    db.Histories.Add(addHistory);
                    if (db.SaveChanges() != 1)
                    {
                        ModelState.AddModelError("", "服务器错误");
                    }
                }
                //将当前视频的播放量加一
                vi.ViewedNum = vi.ViewedNum + 1;
                db.Entry(vi).State = EntityState.Modified;
                if (db.SaveChanges() != 1)
                {
                    ModelState.AddModelError("", "服务器错误");
                }
            }
            detail.VideoId = vi.VideoId;
            detail.Vname = vi.Vname;
            detail.ViewedNum = vi.ViewedNum;
            var CommentQuery = db.Comments.Where(c => c.VideoId == vi.VideoId);
            int CommentCount = CommentQuery.Count();
            detail.CommentNum = CommentCount;
            return View(detail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(VideoDetail model)
        {
            Comment comment = new Comment();
            var MaxId = db.Comments.Any() ? db.Comments.Max(p => p.CommentId) : 0;
            comment.CommentId = MaxId + 1;
            if (Request.Cookies["uid"] != null)
            {
                //从cookie中获取当前用户uid
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                comment.UserId = uid;
            }
            else
            {
                comment.UserId = 0;
            }
            comment.VideoId = vid;
            comment.Content = model.Content;
            comment.CommentTime = DateTime.Now.ToString();
            db.Comments.Add(comment);
            db.SaveChanges();
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files)
        {
            if (files == null || files.Count() == 0 || files.ToList()[0] == null)
            {
                ViewBag.ErrorMessage = "Please select a file!!";
                return View();
            }
            string filePath = string.Empty;
            Guid gid = Guid.NewGuid();
            foreach (HttpPostedFileBase file in files)
            {
                filePath = Path.Combine(HttpContext.Server.MapPath("~/Image"), gid.ToString() + Path.GetExtension(file.FileName));
                file.SaveAs(filePath);
            }
            return RedirectToAction("UploadResult", new { filePath = filePath });
        }
        public ActionResult UploadResult(string filePath)
        {
            ViewBag.FilePath = filePath;
            return View();
        }
    }
}
