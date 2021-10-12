using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStokTakibi.Models.Entity;

namespace MVCStokTakibi.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        DB_MVCSTOKEntities db = new DB_MVCSTOKEntities();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.TBL_MUSTERILER select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));
            }
            return View(degerler.ToList());
            //var degerler = db.TBL_MUSTERILER.ToList();
            //return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TBL_MUSTERILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TBL_MUSTERILER.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult SIL(int id)
        {
            var mstr = db.TBL_MUSTERILER.Find(id);
            db.TBL_MUSTERILER.Remove(mstr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.TBL_MUSTERILER.Find(id);
            return View("MusteriGetir", musteri);
        }
        public ActionResult Guncelle(TBL_MUSTERILER p1)
        {
            var mstri = db.TBL_MUSTERILER.Find(p1.MUSTERIID);
            mstri.MUSTERIAD = p1.MUSTERIAD;
            mstri.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}