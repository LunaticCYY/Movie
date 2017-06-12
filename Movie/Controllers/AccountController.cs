using Movie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie.Controllers
{
    public class AccountController : Controller
    {
        // 数据库连接
        private MovieContext db = new MovieContext();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        // 登录Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            // 通过用户邮箱和密码在Users表里查询
            var user = db.Users.Where(c => c.Email == model.Email).Where(c => c.Password == model.Password).FirstOrDefault();
            if (user != null)
            {
                // 如果存在该用户且密码正确，则将该用户加入cookie
                Response.Cookies.Add(Cookies.create_cookies(user));
                if (user.Privilege != 3)
                {
                    // 如果该用户不是管理员，跳转普通用户页面
                    return RedirectToAction("Index", "UserOperation");
                }
                else
                {
                    // 如果该用户是管理员，跳转管理员页面
                    return RedirectToAction("List", "Users");
                }
            }
            else
            {
                // 如果该用户不存在或者密码错误
                ModelState.AddModelError("", "用户名或密码错误");
            }
            // 返回登录页面重新登录
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        // 注册Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // 获取当前用户表内最大编号，如果用户表为空MaxId则为0
                var MaxId = db.Users.Any() ? db.Users.Max(p => p.UserId) : 0;
                // 当前用户的编号自加一
                user.UserId = MaxId + 1;
                // 当前用户默认权限为0
                user.Privilege = 0;
                // 用户表内加入加入该用户
                db.Users.Add(user);
                // 数据库保存
                db.SaveChanges();
                // 跳转登录页面
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}
