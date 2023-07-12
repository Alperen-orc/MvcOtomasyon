using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOtomasyon.Models.Siniflar
{
	public class Admin
	{
		[Key]
		public int Adminid { get; set; }
		[Column(TypeName = "Varchar")]
		[StringLength(10)]
		public int KullaniciAd { get; set; }
		[Column(TypeName = "Varchar")]
		[StringLength(10)]
		public int Sifre { get; set; }
		[Column(TypeName = "Char")]
		[StringLength(1)]
		public int Yetki { get; set; }
	}
}