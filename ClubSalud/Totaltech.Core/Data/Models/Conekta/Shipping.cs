using System;

namespace Totaltech.Core.Data.Models.Conketa
{
	public class Shipping
	{
		public string carrier { get; set; }
		public string service { get; set; }
		public int price { get; set; }
		public string tracking_id { get; set; }
		public ShippingAddress address { get; set; }
	}
}
