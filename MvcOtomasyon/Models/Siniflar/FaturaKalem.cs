using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOtomasyon.Models.Siniflar
{
	public class FaturaKalem
	{
		[Key]
		public int FaturaKalemid { get; set; }
		public string Aciklama { get; set; }
		public int Miktar { get; set; }
		public decimal BirimFİyat { get; set; }
		public decimal Tutar { get; set; }
		public Faturalar Faturalar { get; set; }

	}
}