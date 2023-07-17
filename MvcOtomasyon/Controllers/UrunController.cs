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
        public ActionResult Index()
        {
            var urun = c.Uruns.ToList();
            return View(urun);
        }
        [HttpGet]
        public ActionResult YeniUrun()
		{
            return View();
		}
        [HttpPost]
        public ActionResult YeniUrun(Urun u)
        {
            c.Uruns.Add(u);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}