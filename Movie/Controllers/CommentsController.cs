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
    public class CommentsController : Controller
    {
        // 数据库连接
        private MovieContext db = new MovieContext();

        // Index.cshtml返回所有评论
        public ActionResult Index()
        {

             return View(db.Comments.ToList());

        }

        // Detail.cshtml 获取某个CommentId显示某个评论的详细信息
        public ActionResult Details(int? id)
        {
            // 如果id为空
            if (id == null)
            {
                // 请求错误信息
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // 通过id查询该评论
            Comment comment = db.Comments.Find(id);
            // 如果没有查询到该评论
            if (comment == null)
            {
                // 返回没有找到信息
                return HttpNotFound();
            }
            // 返回该评论信息页面
            return View(comment);
        }

        // GET: Comments/Create

        public ActionResult Create()
        {
            return View();
        }

        // 创建评论Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,UserId,VideoId,Content,CommentTime")] Comment comment)
        {
            // 判断从create.cshtml页面传过来的comment类中是否合法
            if (ModelState.IsValid)
            {
                // 如果是，则在评论表取得评论表中最大的评论编号
                var MaxId = db.Comments.Any() ? db.Comments.Max(p => p.CommentId) : 0;
                // 将取得最大评论编号加一赋值给将要创建的评论
                comment.CommentId = MaxId + 1;
                // Comments表里增加新评论
                db.Comments.Add(comment);
                // 数据库保存
                db.SaveChanges();
                // 跳转评论首页
                return RedirectToAction("Index");
            }
            // 从create.cshtml页面传过来的comment类中不合法，直接返回原来的页面
            return View(comment);
        }

        public ActionResult Edit(int? id)
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

        // 编辑Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,UserId,VideoId,Content,CommentTime")] Comment comment)
        {
            // 判断从Edit.cshtml页面传过来的comment类中是否合法
            if (ModelState.IsValid)
            {
                // 更改comment数据
                db.Entry(comment).State = EntityState.Modified;
                // 数据库保存
                db.SaveChanges();
                // 跳转评论首页
                return RedirectToAction("Index");
            }
            // 从Edit.cshtml页面传过来的comment类不合法，直接返回原来的页面
            return View(comment);
        }


        public ActionResult Delete(int? id)
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

        // 删除Action
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            // 通过Delete.cshtml页面传来的id来查询评论表
            Comment comment = db.Comments.Find(id);
            // 评论表里面删除该评论
            db.Comments.Remove(comment);
            // 数据表保存
            db.SaveChanges();
            // 跳转Comment表首页
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
