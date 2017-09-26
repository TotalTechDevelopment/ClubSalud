using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Totaltech.Core.Data.Models.Conketa
{
	public class Webhook 
	{
		public string id { get; set; }
		public string url { get; set; }
		public string[] events { get; set; }		
	}
}

