using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Totaltech.Core.Data.Models.Conketa
{
	public class Charge 
	{
		public string id { get; set; }
		public string description { get; set; }
		public int amount { get; set; }
		public int monthly_installments { get; set; }
		public string currency { get; set; }
		public string reference_id { get; set; }
		public string card { get; set; }
		public string status { get; set; }
		public Cash cash { get; set; }
		public PaymentMethod payment_method { get; set; }
		public Bank bank { get; set; }
		public Details details { get; set; }

	
	}

    public class ChargeSend
    {
        public string description { get; set; }
        public int amount { get; set; }
        public int? monthly_installments { get; set; }
        public string currency { get; set; }
        public string reference_id { get; set; }
        public string card { get; set; }
        //public string status { get; set; }
        //public Cash cash { get; set; }
        //public PaymentMethod payment_method { get; set; }
        //public Bank bank { get; set; }
        public Details details { get; set; }


    }
}
