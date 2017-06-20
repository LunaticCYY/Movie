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
using Movie.App_Start;

namespace Movie.Controllers
{
    [CheckLogin]
    public class CommentsController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: Comments
        public ActionResult Index(string UserId, string VideoId,int ?page)
        {
            if (Cookies.CheckPrivilege() == true)
            {
                var comment = from m in db.Comments select m;
                if (!String.IsNullOrEmpty(UserId))
                {
                    int uid = int.Parse(UserId);
                    comment = comment.Where(s => s.UserId == uid);
                }
                if (!String.IsNullOrEmpty(VideoId))
                {
                    int vid = int.Parse(VideoId);
                    comment = comment.Where(s => s.VideoId == vid);
                }
                List<Total> total = new List<Total>();
                foreach (var item in comment)
                {
                    Total detail = new Total();
                    detail.comment = item;
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
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
            
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            
            if (Cookies.CheckPrivilege() == true)
            {
                return View(comment);
            }
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            if (Cookies.CheckPrivilege() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,UserId,VideoId,Content,CommentTime")] Comment comment)
        {
            if (Cookies.CheckPrivilege() == true)
            {
                if (ModelState.IsValid)
                {
                    // 如果是，则在评论表取得评论表中最大的评论编号
                    var MaxId = db.Comments.Any() ? db.Comments.Max(p => p.CommentId) : 0;
                    // 将取得最大评论编号加一赋值给将要创建的评论
                    comment.CommentId = MaxId + 1;
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(comment);
            }
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
            
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Cookies.CheckPrivilege() == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Comment comment = db.Comments.Find(id);
                if (comment == null)
                {
                    return HttpNotFound();
                }
                return View(comment);
            }
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
            
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,UserId,VideoId,Content,CommentTime")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Cookies.CheckPrivilege() == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Comment comment = db.Comments.Find(id);
                if (comment == null)
                {
                    return HttpNotFound();
                }
                return View(comment);
            }
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
            
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Cookies.CheckPrivilege() == true)
            {
                Comment comment = db.Comments.Find(id);
                db.Comments.Remove(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "UserOperation");
            }
            
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
