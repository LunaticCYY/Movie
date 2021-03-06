﻿using Movie.Models;
using System;
using System.Linq;
using System.Web;

namespace Movie
{
    public class Cookies
    {
        private static MovieContext db = new MovieContext();

        // 传入一个用户对象,创建一个Cookie
        public static HttpCookie create_cookies(User user)
        {
            // 如果用户的权限值为0，即普通用户
            if (user.Privilege != User.Privileges.管理员)
            {
                HttpCookie mycoo = new HttpCookie("uid");
                // 设置cookie的有效路径为用户操作目录 
                mycoo.Path = "/";
                // 设置cookie的值
                mycoo.Value = user.UserId.ToString();
                // 设置cookie的存在时间
                mycoo.Expires = DateTime.Now.AddDays(1);
                return mycoo;
            }
            // 如果用户的权限值不为0，即管理员
            else 
            {
                HttpCookie mycoo = new HttpCookie("uid");
                // 设置cookie的有效路径为全局
                mycoo.Path = "/";
                mycoo.Value = user.UserId.ToString();
                mycoo.Expires = DateTime.Now.AddDays(1);
                return mycoo;
            }
            
        }

        public static bool exist_cookies()
        {
            if (HttpContext.Current.Request.Cookies["uid"] != null)
                return true;
            else
                return false;
        }

        public static bool CheckPrivilege()
        {
            HttpCookie hc = HttpContext.Current.Request.Cookies["uid"];
            int uid = int.Parse(hc.Value);
            var user = db.Users.Where(c => c.UserId == uid).FirstOrDefault();
            if (user != null)
            {
                if(user.Privilege == Movie.Models.User.Privileges.管理员)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}