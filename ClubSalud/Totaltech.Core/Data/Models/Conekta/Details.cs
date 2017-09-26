using System;
using System.Collections.Generic;

namespace Totaltech.Core.Data.Models.Conketa
{
	public class Details
	{
		public string name { get; set; }
		public string phone { get; set; }
		public string email { get; set; }
		public Customer customer { get; set; }
		public List<LineItem> line_items { get; set; }
		public BillingAddress billing_address { get; set; }
        public Shipping shipment { get; set; }
	}
}
