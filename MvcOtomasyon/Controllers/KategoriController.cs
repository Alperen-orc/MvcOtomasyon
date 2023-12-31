﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;

namespace MvcOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = c.Kategoris.ToList().ToPagedList(sayfa, 4);
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
        public ActionResult Deneme()
		{
            Class3 cs = new Class3();
            cs.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAD");
            cs.Urunler = new SelectList(c.Uruns, "Urunid", "UrunAd");
            return View(cs);
		}

        public JsonResult UrunGetir(int p)
		{
            var urunler = (from x in c.Uruns
                           join y in c.Kategoris
                           on x.Kategori.KategoriID equals y.KategoriID
                           where x.Kategori.KategoriID == p
                           select new
                           {
                               Text = x.UrunAd,
                               Value = x.Urunid.ToString()
                           });
            return Json(urunler,JsonRequestBehavior.AllowGet);
		}
    }
}