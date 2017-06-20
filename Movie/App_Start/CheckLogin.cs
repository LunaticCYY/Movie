using Movie.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie.App_Start
{
    public class CheckLogin : ActionFilterAttribute
    {
        private MovieContext db = new MovieContext();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
          if(HttpContext.Current.Request.Cookies["uid"] == null)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}