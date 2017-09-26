
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Totaltech.Core.Helpers;

namespace Totaltech.Core.Data.Models
{
	public class InputFile
	{
		public static string TABLE_NAME = "TTArchivos";

		public string FileName { get; set; }

		public string Path { get; set; }

		[JsonConverter(typeof(ByteArrayConverter))]
		public byte[] FileArray { get; set; }
		 
	}

}
