using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOtomasyon.Models.Siniflar;

namespace MvcOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context c = new Context();
        public ActionResult Index()
        {
            var deger = c.Departmans.Where(x=>x.Durum==true).ToList();
            return View(deger);
        }
        [HttpGet]
        public ActionResult DepartmanEkle()
		{
            return View();
		}

        [HttpPost]
        public ActionResult DepartmanEkle(Departman d)
		{
            c.Departmans.Add(d);
            d.Durum = true;
            c.SaveChanges();
            return RedirectToAction("Index");
		}
        public ActionResult DepartmanSil(int id)
		{
            var dep = c.Departmans.Find(id);
            dep.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
		}
        public ActionResult DepartmanGetir(int id)
		{
            var dep = c.Departmans.Find(id);
            return View("DepartmanGetir", dep);
		}
        public ActionResult DepartmanGuncelle(Departman d)
		{
            var dep = c.Departmans.Find(d.Departmanid);
            dep.DepartmanAd = d.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");
		}
        public ActionResult DepartmanDetay(int id)
		{
            var deger = c.Personels.Where(x => x.Departmanid == id).ToList();
            var dpt = c.Departmans.Where(x => x.Departmanid == id).Select(y => y.DepartmanAd).FirstOrDefault();
            ViewBag.dgr = dpt;

            return View(deger);
		}
    }
}