using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Totaltech.Core.Data.Models.Conketa
{
	public class Customer
	{
		public string id { get; set; }
		public string name { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
		public List<Card> cards { get; set; }
		public Subscription subscription { get; set; }
		public string plan { get; set; }

		public bool logged_in { get; set; }
		public int successful_purchases { get; set; }
		public int created_at { get; set; }
		public int updated_at { get; set; }
		public int offline_payments { get; set; }
		public int score { get; set; }

	}

    public class CustomerSend
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public List<String> cards { get; set; }
    }

}

