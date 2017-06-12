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
        // 数据库连接
        private MovieContext db = new MovieContext();
        // 定义一个全局变量存放电影ID
        static private int vid;

        // 返回用户主页
        public ActionResult Index()
        {
            return View();
        }

        // 用户点击主页中一个电影后返回电影信息
        public ActionResult Detail(int? id)
        {
            // 从数据库电影表中查找用户点击电影的对象
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
            detail.Vurl = vi.Vurl;
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
                comment.VideoId = vid;
                comment.Content = model.Content;
                comment.CommentTime = DateTime.Now.ToString();
                db.Comments.Add(comment);
                db.SaveChanges();
                return Detail(vid);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult AddFavorite()
        {
            if(Request.Cookies["uid"]!=null)
            {
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                var favorite = db.Favorites.Where(c => c.VideoId == vid).Where(c => c.UserId == uid).FirstOrDefault();
                if(favorite!=null)
                {
                    ModelState.AddModelError("", "您已收藏该视频");
                    return Detail(vid);
                }
                else
                {
                    Favorite userFavorite = new Favorite();
                    var MaxId = db.Favorites.Any() ? db.Favorites.Max(p => p.FavoriteId) : 0;
                    userFavorite.FavoriteId = MaxId + 1;
                    userFavorite.UserId = uid;
                    userFavorite.VideoId = vid;
                    userFavorite.FavoriteTime = DateTime.Now.ToString();
                    db.Favorites.Add(userFavorite);
                    db.SaveChanges();
                    return Detail(vid);
                }
            }
            else
            {
                return RedirectToAction("Login","Account");
            }
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
            var filename = Path.Combine(Request.MapPath("~/Image"), Path.GetFileName(file1.FileName));
            // 在用电影表取得电影表中最大的电影编号
            var MaxId = db.Videos.Any() ? db.Videos.Max(p => p.VideoId) : 0;
            // 将取得最大电影编号加一赋值给将要创建的电影
            video.VideoId = MaxId + 1;
            // 电影默认观看数为0
            video.ViewedNum = 0;
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
                video.Vurl = "~/Image/" + Path.GetFileName(file1.FileName);
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
        public ActionResult UserMessage()
        {
            if (Request.Cookies["uid"] != null)
            {
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                var user = db.Users.Where(c => c.UserId == uid).FirstOrDefault();
                return View(user);
            }
            return View();
        }

        public ActionResult UploadRecord()
        {
            if (Request.Cookies["uid"] != null)
            {
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                var video = db.Videos.Where(c => c.UserId == uid);
                return View(video);
            }
            return View();
        }

        public ActionResult ViewRecord()
        {
            if (Request.Cookies["uid"] != null)
            {
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                var history = db.Histories.Where(c => c.UserId == uid);
                List<HistoryDetail> historyDetail = new List<HistoryDetail>();
                foreach (var item in history)
                {
                    HistoryDetail detail = new HistoryDetail();
                    var video = db.Videos.Where(c => c.VideoId == item.VideoId).FirstOrDefault();
                    detail.VideoId = item.VideoId;
                    detail.Vname = video.Vname;
                    detail.HistoryTime = item.HistoryTime;
                    historyDetail.Add(detail);
                }
                return View(historyDetail);
            }
            return View();
        }

        public ActionResult UserFavorite()
        {
            if(Request.Cookies["uid"]!=null)
            {
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                var favorite = db.Favorites.Where(c => c.UserId == uid);
                List<FavoriteDetail> favoriteDetail = new List<FavoriteDetail>();
                foreach (var item in favorite)
                {
                    FavoriteDetail detail = new FavoriteDetail();
                    var video = db.Videos.Where(c => c.VideoId == item.VideoId).FirstOrDefault();
                    detail.VideoId = item.VideoId;
                    detail.Vname = video.Vname;
                    detail.FavoriteTime = item.FavoriteTime;
                    favoriteDetail.Add(detail);
                }
                return View(favoriteDetail);
            }
            return View();
        }

        public ActionResult UserComment()
        {
            if (Request.Cookies["uid"] != null)
            {
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                var comment = db.Comments.Where(c => c.UserId == uid);
                List<CommentDetail> commentDetail = new List<CommentDetail>();
                foreach (var item in comment)
                {
                    CommentDetail detail = new CommentDetail();
                    var video = db.Videos.Where(c => c.VideoId == item.VideoId).FirstOrDefault();
                    detail.VideoId = item.VideoId;
                    detail.Vname = video.Vname;
                    detail.Content = item.Content;
                    detail.CommentTime = item.CommentTime;
                    commentDetail.Add(detail);
                }
                return View(commentDetail);
            }
            return View();
        }
    }
}
