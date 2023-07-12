using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOtomasyon.Models.Siniflar
{
	public class Gider
	{
		public int Giderid { get; set; }
		public string Aciklama { get; set; }
		public DateTime Tarih { get; set; }
		public decimal Tutar { get; set; }

	}
}