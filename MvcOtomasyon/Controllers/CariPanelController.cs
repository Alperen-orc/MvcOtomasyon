﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOtomasyon.Models.Siniflar;

namespace MvcOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Alici == mail).ToList();
            ViewBag.m = mail;
            var mailid = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            ViewBag.mid = mailid;

            var toplamsatis = c.SatisHarekets.Where(x => x.Cariid == mailid).Count();
            ViewBag.toplamsatis = toplamsatis;

            var toplamtutar = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.ToplamTutar);
            ViewBag.toplamtutar = toplamtutar;

            var toplamurunsayisi = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
            ViewBag.toplamurunsayisi = toplamurunsayisi;

            var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            return View(degerler);
            
        }
        [Authorize]
        public ActionResult Siparislerim()
		{
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.Cariid).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }
        [Authorize]
        public ActionResult GelenMesajlar()
		{
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(x=>x.MesajID).ToList();
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            return View(mesajlar);
		}
        [Authorize]

        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Gönderici == mail).OrderByDescending(x => x.MesajID).ToList();
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult MesajDetay(int id)
		{
            var deger = c.Mesajlars.Where(x => x.MesajID == id).ToList();

            var mail = (string)Session["CariMail"];
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            return View(deger);
		}
        [Authorize]
        [HttpGet]
		public ActionResult YeniMesaj()
		{
            var mail = (string)Session["CariMail"];
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            return View();
		}
		[HttpPost]
        [Authorize]
        public ActionResult YeniMesaj(Mesajlar m)
		{
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            var mail = (string)Session["CariMail"];
            m.Gönderici = mail;


            c.Mesajlars.Add(m);
            c.SaveChanges();
			return View();
		}
        [Authorize]
        public ActionResult KargoTakip(string p)
		{
            var k = from x in c.KargoDetays select x;      
            k = k.Where(y => y.TakipKodu.Contains(p));
            return View(k.ToList());
        }
        [Authorize]
        public ActionResult CariKargoTakip(string id)
		{
            var deger = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();

            return View(deger);
		}
        [Authorize]
        public ActionResult LogOut()
		{
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
		}
        public PartialViewResult Partial1()
		{
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            var cari = c.Carilers.Find(id);
            return PartialView("Partial1",cari);
		}
        public PartialViewResult Partial2()
		{
            var veriler = c.Mesajlars.Where(x => x.Gönderici == "admin").ToList();
            return PartialView(veriler);
		}
        public ActionResult CariBilgiGuncelle(Cariler p)
		{
            var guncelle = c.Carilers.Find(p.Cariid);
            guncelle.CariAd = p.CariAd;
            guncelle.CariSoyad = p.CariSoyad;
            guncelle.CariSifre = p.CariSifre;

            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}