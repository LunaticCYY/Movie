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
using Movie.App_Start;
using PagedList;

namespace Movie.Controllers
{
    public class UserOperationController : Controller
    {
        // 数据库连接
        private MovieContext db = new MovieContext();
        // 定义一个全局变量存放电影ID
        static private int vid;

        // 返回用户主页
        [CheckLogin]
        public ActionResult Index()
        {
            var videos = db.Videos.OrderByDescending(c => c.ViewedNum).Take(10);
            return View(videos);
        }

        // 用户点击主页中一个电影后返回电影信息
        public ActionResult Detail(int? id)
        {
            // 从数据库电影表中查找用户点击电影的对象
            var vi = db.Videos.Find(id);
            vid = vi.VideoId;
            Total detail = new Total();
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
                        return Content("<script>alert('服务器错误');history.go(-1);</script>");
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
                        return Content("<script>alert('服务器错误');history.go(-1);</script>");
                    }
                }
                //将当前视频的播放量加一
                vi.ViewedNum = vi.ViewedNum + 1;
                db.Entry(vi).State = EntityState.Modified;
                if (db.SaveChanges() != 1)
                {
                    return Content("<script>alert('服务器错误');history.go(-1);</script>");
                }
            }
            detail.video = vi;
            var CommentQuery = db.Comments.Where(c => c.VideoId == vi.VideoId);
            int CommentCount = CommentQuery.Count();
            detail.CommentNum = CommentCount;
            return View(detail);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Detail(Total model)
        //{
        //    Comment comment = new Comment();
        //    var MaxId = db.Comments.Any() ? db.Comments.Max(p => p.CommentId) : 0;
        //    comment.CommentId = MaxId + 1;
        //    if (Request.Cookies["uid"] != null)
        //    {
        //        //从cookie中获取当前用户uid
        //        HttpCookie hc = Request.Cookies["uid"];
        //        int uid = int.Parse(hc.Value);
        //        var user = db.Users.Where(c => c.UserId == uid).FirstOrDefault();
        //        if (user.Privilege == 0)
        //        {
        //            return Content("<script>alert('很抱歉，您没有权限评论');history.go(-1);</script>");
        //        }
        //        else
        //        {
        //            comment.UserId = uid;
        //            comment.VideoId = vid;
        //            comment.Content = model.comment.Content;
        //            comment.CommentTime = DateTime.Now.ToString();
        //            db.Comments.Add(comment);
        //            db.SaveChanges();
        //            Response.Write("<script>alert('评论成功');</script>");
        //            return Detail(vid);
        //        }
        //    }
        //    else
        //    {
        //        return Content("<script>alert('请先登录');window.location.href='../Account/Login';</script>");
        //        //return RedirectToAction("Login", "Account");
        //    }
        //}
        
        public ActionResult AddComment()
        {
            return View();
        }


        public IPagedList<Total> ReceiveComment()
        {
            var comment1 = db.Comments.Where(c => c.VideoId == vid);
            var video = db.Videos.Where(c => c.VideoId == vid).FirstOrDefault();
            List<Total> total = new List<Total>();
            foreach (var item in comment1)
            {
                Total detail = new Total();
                detail.video = video;
                var user1 = db.Users.Where(c => c.UserId == item.UserId).FirstOrDefault();
                detail.user = user1;
                detail.comment = item;
                total.Add(detail);
            }
            int number = comment1.Count();
            int pagenumber = number / 10 + 1;
            int pagesize = 10;
            IPagedList<Total> pagelist = total.ToPagedList(pagenumber, pagesize);
            return pagelist;
        }
        [HttpPost]
        public ActionResult AddComment(Total model)
        {
            Comment comment = new Comment();
            var MaxId = db.Comments.Any() ? db.Comments.Max(p => p.CommentId) : 0;
            comment.CommentId = MaxId + 1;
            if (Request.Cookies["uid"] != null)
            {
                //从cookie中获取当前用户uid
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                var user = db.Users.Where(c => c.UserId == uid).FirstOrDefault();
                if (user.Privilege == Movie.Models.User.Privileges.禁言会员)
                {
                    return Content("<h1>很抱歉，您没有权限评论</h1>");
                }
                else
                {
                    comment.UserId = uid;
                    comment.VideoId = vid;
                    comment.Content = model.comment.Content;
                    comment.CommentTime = DateTime.Now.ToString();
                    db.Comments.Add(comment);
                    db.SaveChanges();

                   
                    return PartialView("CommentList", ReceiveComment());
                }
            }
            else
            {
                return Content("<h1>请先登录</h1>");
                //return RedirectToAction("Login", "Account");
            }
        }


        public ActionResult AddFavorite()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddFavorite(int ?id)
        {
            if (Request.Cookies["uid"] != null)
            {
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                var favorite = db.Favorites.Where(c => c.VideoId == vid).Where(c => c.UserId == uid).FirstOrDefault();
                if (favorite != null)
                {
                    return Content("<script>alert('您已经收藏了该视频');history.go(-1);</script>");
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
                    return Content("<script>alert('收藏成功');history.go(-1);</script>");
                    //return Detail(vid);
                }
            }
            else
            {
                return Content("<script>alert('请先登录');window.location.href='/Account/Login';</script>");
                //return RedirectToAction("Login","Account");
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
            var filename = Path.Combine(Request.MapPath("~/Video"), Path.GetFileName(file1.FileName));
            // 在用电影表取得电影表中最大的电影编号
            var MaxId = db.Videos.Any() ? db.Videos.Max(p => p.VideoId) : 0;
            // 将取得最大电影编号加一赋值给将要创建的电影
            video.VideoId = MaxId + 1;
            // 电影默认观看数为0
            video.ViewedNum = 0;
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

        public ActionResult UserMessage()
        {
            if (Request.Cookies["uid"] != null)
            {
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                var user = db.Users.Where(c => c.UserId == uid).FirstOrDefault();
                return View(user);
            }
            else
            {
                return Content("<script>alert('请先登录');window.location.href='/Account/Login';</script>");
            }
        }

        public ActionResult UploadRecord(int ?page)
        {
            if (Request.Cookies["uid"] != null)
            {
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                var video = from m in db.Videos select m;
                List<Total> total = new List<Total>();
                foreach (var item in video)
                {
                    Total detail = new Total();
                    detail.video = item;
                    var user = db.Users.Where(c => c.UserId == uid).FirstOrDefault();
                    detail.user = user;
                    total.Add(detail);
                }
                int pagenumber = page ?? 1;
                int pagesize = 10;
                IPagedList<Total> pagelist = total.ToPagedList(pagenumber, pagesize);
                return View(pagelist);
            }
            else
            {
                return Content("<script>alert('请先登录');window.location.href='/Account/Login';</script>");
            }
        }

        public ActionResult ViewRecord(int? page)
        {
            if (Request.Cookies["uid"] != null)
            {
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                var history = db.Histories.Where(c => c.UserId == uid);
                List<Total> total = new List<Total>();
                foreach (var item in history)
                {
                    Total detail = new Total();
                    var video = db.Videos.Where(c => c.VideoId == item.VideoId).FirstOrDefault();
                    detail.video = video;
                    detail.history = item;
                    total.Add(detail);
                }
                int pagenumber = page ?? 1;
                int pagesize = 10;
                IPagedList<Total> pagelist = total.ToPagedList(pagenumber, pagesize);
                return View(pagelist);
            }
            else
            {
                return Content("<script>alert('请先登录');window.location.href='/Account/Login';</script>");
            }
        }

        public ActionResult UserFavorite(int? page)
        {
            if (Request.Cookies["uid"] != null)
            {
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                var favorite = db.Favorites.Where(c => c.UserId == uid);
                List<Total> total = new List<Total>();
                foreach (var item in favorite)
                {
                    Total detail = new Total();
                    var video = db.Videos.Where(c => c.VideoId == item.VideoId).FirstOrDefault();
                    detail.video = video;
                    detail.favorite = item;
                    total.Add(detail);
                }
                int pagenumber = page ?? 1;
                int pagesize = 10;
                IPagedList<Total> pagelist = total.ToPagedList(pagenumber, pagesize);
                return View(pagelist);
            }
            else
            {
                return Content("<script>alert('请先登录');window.location.href='/Account/Login';</script>");
            }
        }

        public ActionResult UserComment(int? page)
        {
            if (Request.Cookies["uid"] != null)
            {
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                var user = db.Users.Where(c => c.UserId == uid).FirstOrDefault();
                var comment = db.Comments.Where(c => c.UserId == uid);
                List<Total> total = new List<Total>();
                foreach (var item in comment)
                {
                    Total detail = new Total();
                    var video = db.Videos.Where(c => c.VideoId == item.VideoId).FirstOrDefault();
                    detail.video = video;
                    detail.user = user;
                    detail.comment = item;
                    total.Add(detail);
                }
                int pagenumber = page ?? 1;
                int pagesize = 10;
                IPagedList<Total> pagelist = total.ToPagedList(pagenumber, pagesize);
                return View(pagelist);
            }
            else
            {
                return Content("<script>alert('请先登录');window.location.href='/Account/Login';</script>");
            }
        }

        public ActionResult CommentList(int ?page)
        {
            var comment = db.Comments.Where(c => c.VideoId == vid);
            var video = db.Videos.Where(c => c.VideoId == vid).FirstOrDefault();
            List<Total> total = new List<Total>();
            foreach (var item in comment)
            {
                Total detail = new Total();
                detail.video = video;
                var user = db.Users.Where(c => c.UserId == item.UserId).FirstOrDefault();
                detail.user = user;
                detail.comment = item;
                total.Add(detail);
            }
            int pagenumber = page ?? 1;
            int pagesize = 10;
            IPagedList<Total> pagelist = total.ToPagedList(pagenumber, pagesize);
            return View(pagelist);
        }

        public ActionResult Loginout()
        {
            if (Request.Cookies["uid"] != null)
            {
                Response.Cookies.Remove("uid");
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult SearchResult()
        {
            return View();
        }

        // 创建视频Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchResult(Video video)
        {
            var videoSearch = db.Videos.Where(c => c.Vname.Contains(video.Vname));
            if(videoSearch!=null)
            {
                return View(videoSearch);
            }
            else
            {
                ModelState.AddModelError("", "搜索为空");
                return View();
            }
        }

    }
}