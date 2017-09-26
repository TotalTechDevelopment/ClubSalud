using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Totaltech.Core.Data.Models.Conketa
{
	public class Plan
	{
		public String id { get; set; }
		public String name { get; set; }
		public int amount { get; set; }
		public String currency { get; set; }
		public String interval { get; set; }
		public int frequency { get; set; }
		public int trial_period_days { get; set; }
		public int expiry_count { get; set; }		
	}

    public class PlanSend
    {
        public String name { get; set; }
        public int amount { get; set; }
        public String currency { get; set; }
        public String interval { get; set; }
        public int frequency { get; set; }
        public int trial_period_days { get; set; }
        public int expiry_count { get; set; }
    }
}

