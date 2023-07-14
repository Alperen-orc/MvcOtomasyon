using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOtomasyon.Models.Siniflar;

namespace MvcOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Kategoris.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
		{
            return View();
		}
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            c.Kategoris.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
		{
            var sil = c.Kategoris.Find(id);
            c.Kategoris.Remove(sil);
            c.SaveChanges();
            return RedirectToAction("Index");
		}
        public ActionResult KategoriGetir(int id)
		{
            var kategori = c.Kategoris.Find(id);
            return View("KategoriGetir", kategori);
		}
        public ActionResult KategoriGuncelle(Kategori k)
		{
            var guncelle = c.Kategoris.Find(k.KategoriID);
            guncelle.KategoriAD = k.KategoriAD;
            c.SaveChanges();
            return RedirectToAction("Index");
		}
    }
}