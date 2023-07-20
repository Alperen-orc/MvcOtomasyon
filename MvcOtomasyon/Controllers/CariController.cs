using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOtomasyon.Models.Siniflar;

namespace MvcOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Carilers.Where(x=>x.Durum==true).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniCari()
		{
            return View();
		}
        [HttpPost]
        public ActionResult YeniCari(Cariler yeni)
		{
            yeni.Durum = true;
            c.Carilers.Add(yeni);
            c.SaveChanges();
            return RedirectToAction("Index");
		}
        public ActionResult CariSil(int id)
		{
            var cr = c.Carilers.Find(id);
            cr.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariGetir(int id)
		{
            var cari = c.Carilers.Find(id);
            return View("CariGetir", cari);
		}
        public ActionResult CariGuncelle(Cariler p)
		{
            if(!ModelState.IsValid)
			{
                return View("CariGetir");
			}
            var cari = c.Carilers.Find(p.Cariid);
            cari.CariAd = p.CariAd;
            cari.CariSoyad = p.CariSoyad;
            cari.CariSehir = p.CariSehir;
            cari.CariMail = p.CariMail;
            c.SaveChanges();
            return RedirectToAction("Index");
		}
        public ActionResult MusteriSatis(int id)
		{
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            var tasi = c.Carilers.Where(x => x.Cariid == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = tasi;
            return View(degerler);
		}
    }
}