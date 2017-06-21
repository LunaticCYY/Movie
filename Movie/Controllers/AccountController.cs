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
                if (user.Privilege != Models.User.Privileges.管理员)
                {
                    // 如果该用户不是管理员，跳转普通用户页面
                    return RedirectToAction("Index", "UserOperation");
                }
                else
                {
                    // 如果该用户是管理员，跳转管理员页面
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                // 如果该用户不存在或者密码错误
                ModelState.AddModelError("", "用户名或密码错误");
                return View();
            }
        }
        public ActionResult Register()
        {
            return View();
        }

        // 注册Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Total detail)
        {
            if(detail.RePassword!=detail.user.Password)
            {
                ModelState.AddModelError("", "两次密码不一致");
                return View();
            }
            else
            {
                var user = db.Users.Where(c => c.Email == detail.user.Email).FirstOrDefault();
                if(user!=null)
                {
                    ModelState.AddModelError("", "此邮箱已被注册");
                    return View();
                }
                else
                {
                    User NewUser = new Models.User();
                    var MaxId = db.Users.Any() ? db.Users.Max(p => p.UserId) : 0;
                    NewUser.UserId = MaxId + 1;
                    NewUser.Password = detail.user.Password;
                    NewUser.NickName = detail.user.NickName;
                    NewUser.Email = detail.user.Email;
                    NewUser.Privilege = Models.User.Privileges.普通会员;
                    db.Users.Add(NewUser);
                    if (db.SaveChanges() != 1)
                    {
                        ModelState.AddModelError("", "服务器错误");
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }
            }
        }
    }
}
