using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Totaltech.Core.Data.Models.Conketa
{
	public class Card
	{
		public string id { get; set; }
		public string name { get; set; }
		public string brand { get; set; }
		public string last4 { get; set; }
		public string exp_month { get; set; }
		public string exp_year { get; set; }
		public int created_at { get; set; }
		public string customer_id { get; set; }
		public Customer customer { get; set; }		
	}

    public class CardSend
    {
        public string token { get; set; }
    }
}

