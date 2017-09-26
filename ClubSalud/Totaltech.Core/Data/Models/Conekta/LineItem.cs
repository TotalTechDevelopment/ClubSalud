using System;

namespace Totaltech.Core.Data.Models.Conketa
{
	public class LineItem
	{
		public string name { get; set; }
		public string description { get; set; }
		public int unit_price { get; set; }
		public int quantity { get; set; }
		public string sku { get; set; }
		public string category { get; set; }
	}
}
