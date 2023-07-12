using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOtomasyon.Models.Siniflar
{
	public class Admin
	{
		[Key]
		public int Adminid { get; set; }
		public int KullaniciAd { get; set; }
		public int Sifre { get; set; }
		public int Yetki { get; set; }
	}
}