using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOtomasyon.Models.Siniflar;

namespace MvcOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index(string p)
        {
            var urun = from x in c.Uruns select x;
            if(!string.IsNullOrEmpty(p))
			{
                urun = urun.Where(y => y.UrunAd.Contains(p));
			}
            return View(urun.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
		{
            List<SelectListItem> deger = (from x in c.Kategoris.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.KategoriAD,
                                              Value = x.KategoriID.ToString()
                                          }).ToList();

            ViewBag.dgr = deger;
    
            return View();
		}
        [HttpPost]
        public ActionResult YeniUrun(Urun u)
        {
            c.Uruns.Add(u);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
		{
            var deger = c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
		}
        public ActionResult UrunGetir(int id)
		{
            List<SelectListItem> deger = (from x in c.Kategoris.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.KategoriAD,
                                              Value = x.KategoriID.ToString()
                                          }).ToList();

            ViewBag.dgr = deger;

            var urundeger = c.Uruns.Find(id);
            return View("UrunGetir", urundeger);
		}
        
        public ActionResult UrunGuncelle(Urun urun)
		{
            var guncelleUrun = c.Uruns.Find(urun.Urunid);
            guncelleUrun.AlisFiyat = urun.AlisFiyat;
            guncelleUrun.Durum = urun.Durum;
            guncelleUrun.Kategoriid = urun.Kategoriid;
            guncelleUrun.SatisFiyat = urun.SatisFiyat;
            guncelleUrun.Stok = urun.Stok;
            guncelleUrun.UrunAd = urun.UrunAd;
            guncelleUrun.UrunGorsel = urun.UrunGorsel;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult UrunListesi()
		{
            var deger = c.Uruns.ToList();
            return View(deger);
		}
    }
}