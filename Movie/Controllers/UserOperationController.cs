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
    public class UserOperationController : Controller
    {
        private MovieContext db = new MovieContext();



        // GET: UserOperation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int? id)
        {
            var vi = db.Videos.Find(id);
            VideoDetail detail = new VideoDetail();
            if (Request.Cookies["uid"] != null)
            {
                //从cookie中获取当前用户uid
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                //创建播放历史记录
                History history = new History();
                var MaxId = db.Histories.Any() ? db.Histories.Max(p => p.HistoryId) : 0;
                history.HistoryId = MaxId + 1;
                history.UserId = uid;
                history.Vid = vi.VideoId;
                history.HistoryTime = DateTime.Now.ToString();
                db.Histories.Add(history);
                if (db.SaveChanges() != 1)
                {
                    ModelState.AddModelError("", "服务器错误");
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
        public ActionResult Detail(VideoDetail Detail)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment();
                var MaxId = db.Comments.Any() ? db.Comments.Max(p => p.CommentId) : 0;
                comment.CommentId = MaxId + 1;
                comment.UserId = Detail.UserId;
                comment.VideoId = Detail.VideoId;
                comment.Content = Detail.Content;
                comment.CommentTime = DateTime.Now.ToString();
                db.Comments.Add(comment);
                db.SaveChanges();
                return View();
            }
            return View();
        }
    }
}
