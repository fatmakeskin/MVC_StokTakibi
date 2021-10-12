using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStokTakibi.Models.Entity;

namespace MVCStokTakibi.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        DB_MVCSTOKEntities db = new DB_MVCSTOKEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(TBL_SATISLAR p)
        {
            db.TBL_SATISLAR.Add(p);
            db.SaveChanges();
            return View("Index");
        }
    }
}