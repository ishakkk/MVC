using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;
namespace MVCSTOK.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index()
        {
            var degerler = db.TBL_URUNLER.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TBL_KATEGORILER.ToList()

                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value=i.KATEGORIID.ToString()
                                             }
                                           ).ToList();
            ViewBag.dgr = degerler; 
         return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TBL_URUNLER p1)
        {
            var ktg = db.TBL_KATEGORILER.Where(m=>m.KATEGORIID==p1.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBL_KATEGORILER = ktg;
            db.TBL_URUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {
            var kategori = db.TBL_URUNLER.Find(id);
            db.TBL_URUNLER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
      public ActionResult UrunGetir(int id)
        {
            var urun = db.TBL_URUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBL_KATEGORILER.ToList()

                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }
                                          ).ToList();
            ViewBag.dgr = degerler;


            return View("UrunGetir",urun);
        }
        public ActionResult Guncelle(TBL_URUNLER p)
        {
            var urun = db.TBL_URUNLER.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
            urun.MARKA = p.MARKA;
            urun.STOK = p.STOK;
            urun.FIYAT = p.FIYAT;
            //urun.URUNKATEGORI = p.URUNKATEGORI;
            var ktgr = db.TBL_KATEGORILER.Where(m=>m.KATEGORIID==p.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktgr.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}