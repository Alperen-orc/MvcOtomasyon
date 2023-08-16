using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOtomasyon.Models.Siniflar;

namespace MvcOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Faturalars.ToList();
            return View(liste);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
		{
            return View();
		}
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
		{
            c.Faturalars.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");
		}
        public ActionResult FaturaGetir(int id)
		{
            var fatura = c.Faturalars.Find(id);
            return View("FaturaGetir", fatura);
		}
        public ActionResult FaturaGuncelle(Faturalar f)
		{
            var fatura = c.Faturalars.Find(f.Faturaid);
            fatura.FaturaSerino = f.FaturaSerino;
            fatura.FaturaSirano = f.FaturaSirano;
            fatura.Saat = f.Saat;
            fatura.Tarih = f.Tarih;
            fatura.TeslimAlan = f.TeslimAlan;
            fatura.TeslimEden = f.TeslimEden;
            fatura.VergiDairesi = f.VergiDairesi;
            c.SaveChanges();
            return RedirectToAction("Index");
		}
        public ActionResult FaturaDetay(int id)
		{
            var deger = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
           
            return View(deger);
        }
        [HttpGet]
        public ActionResult YeniKalem()
		{
            return View();
		}
        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem f)
		{
            c.FaturaKalems.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");
		}

        public ActionResult Dinamik()
		{
            Class4 cs = new Class4();
            cs.deger1 = c.Faturalars.ToList();
            cs.deger2 = c.FaturaKalems.ToList();
            return View(cs);
		}

        public ActionResult FaturaKaydet(string FaturaSerino, string FaturaSirano,DateTime Tarih,string VergiDairesi,string Saat,string TeslimEden,string TeslimAlan,string Toplam,FaturaKalem [] kalemler)
		{
            Faturalar f = new Faturalar();
            f.FaturaSerino = FaturaSerino;
            f.FaturaSirano = FaturaSirano;
            f.Tarih = Tarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimEden = TeslimEden;
            f.TeslimAlan = TeslimAlan;
            f.Toplam = decimal.Parse(Toplam);

            c.Faturalars.Add(f);

			foreach (var x in kalemler)
			{
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = x.Aciklama;
                fk.BirimFİyat = x.BirimFİyat;
                fk.Faturaid = x.Faturaid;
                fk.Miktar = x.Miktar;
                fk.Tutar = x.Tutar;

                c.FaturaKalems.Add(fk);

			}
            c.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);

		}
    }
}