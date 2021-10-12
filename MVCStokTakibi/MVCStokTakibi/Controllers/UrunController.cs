using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStokTakibi.Models.Entity;

namespace MVCStokTakibi.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        DB_MVCSTOKEntities db = new DB_MVCSTOKEntities();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.TBL_URUNLER select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.URUNAD.Contains(p));
            }
            return View(degerler.ToList());
            //var degerler = db.TBL_URUNLER.ToList();
            //return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TBL_KATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TBL_URUNLER p1)
        {
            var ktg = db.TBL_KATEGORILER.Where(m => m.KATEGORIID == p1.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBL_KATEGORILER = ktg;
            db.TBL_URUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {
            var urn = db.TBL_URUNLER.Find(id);
            db.TBL_URUNLER.Remove(urn);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBL_MUSTERILER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBL_KATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(TBL_URUNLER p)
        {
            var urn = db.TBL_URUNLER.Find(p.URUNID);
            urn.URUNAD = p.URUNAD;
            urn.URUNID = p.URUNID;
            urn.STOK = p.STOK;
            urn.MARKA = p.MARKA;
            urn.FIYAT = p.FIYAT;
            //urn.URUNKATEGORI = p.URUNKATEGORI;
            var ktg = db.TBL_KATEGORILER.Where(m => m.KATEGORIID == p.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
            urn.URUNKATEGORI = ktg.KATEGORIID;
            return RedirectToAction("Index");

        }
    }
}