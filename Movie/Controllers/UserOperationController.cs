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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vi = db.Videos.Find(id);
            if (vi == null)
            {
                return HttpNotFound();
            }
            return View(vi);
        }

    }
}
