using System;
using System.Collections.Generic;

namespace Totaltech.Core.Data.Models.Google
{
	public class AddressSearch
	{
		public List<AddressResults> results { get; set; }

		public string status { get; set; }
	}

	public class AddressResults
	{
		public string formatted_address { get; set; }
		public Geometry geometry { get; set; }
	}
	 
}
