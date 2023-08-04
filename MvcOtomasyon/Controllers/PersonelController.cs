using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOtomasyon.Models.Siniflar;

namespace MvcOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();
        public ActionResult Index()
        {
            var deger = c.Personels.ToList();
            return View(deger);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
		{
            List<SelectListItem> deger = (from x in c.Departmans.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.DepartmanAd,
                                              Value = x.Departmanid.ToString()
                                          }).ToList();

            ViewBag.dgr = deger;

            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
		{
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
		}
        public ActionResult PersonelGetir(int id)
		{
            List<SelectListItem> deger = (from x in c.Departmans.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.DepartmanAd,
                                              Value = x.Departmanid.ToString()
                                          }).ToList();

            ViewBag.dgr = deger;
            var per = c.Personels.Find(id);
            return View("PersonelGetir",per);
		}
        public ActionResult PersonelGuncelle(Personel p)
		{
            var per = c.Personels.Find(p.Personelid);
            per.PersonelAd = p.PersonelAd;
            per.PersonelSoyad = p.PersonelSoyad;
            per.PersonelGorsel = p.PersonelGorsel;
            per.Departmanid = p.Departmanid;
            c.SaveChanges();
            return RedirectToAction("Index");
		}
        public ActionResult PersonelListe()
		{
            var sorgu = c.Personels.ToList();

            return View(sorgu);
		}
    }
}