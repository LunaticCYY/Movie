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
            var vi = db.Videos.Find(id);
            if (Request.Cookies["uid"] != null)
            {
                HttpCookie hc = Request.Cookies["uid"];
                int uid = int.Parse(hc.Value);
                History history = new History();
                var MaxId = db.Histories.Any() ? db.Histories.Max(p => p.HistoryId) : 0;
                history.HistoryId = MaxId + 1;
                history.UserId = uid;
                history.Vid = vi.VideoId;
                history.HistoryTime = DateTime.Now.ToString();
                db.Histories.Add(history);
                if (db.SaveChanges() != 1)
                {
                    ModelState.AddModelError("", "服务器错误");
                }
            }
            return View(vi);
        }

    }
}
