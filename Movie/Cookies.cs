using Movie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie
{
    public class Cookies
    {
        public static HttpCookie create_cookies(User user)
        {
            if (user.Privilege == 0)
            {
                HttpCookie mycoo = new HttpCookie("uid");
                mycoo.Path = "/UserOperation";
                mycoo.Value = user.UserId.ToString();
                mycoo.Expires = DateTime.Now.AddMinutes(1);
                return mycoo;
            }
            else
            {
                HttpCookie mycoo = new HttpCookie("uid");
                mycoo.Path = "/";
                mycoo.Value = user.UserId.ToString();
                mycoo.Expires = DateTime.Now.AddMinutes(1);
                return mycoo;
            }
        }
    }
}