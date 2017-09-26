using System;

namespace Totaltech.Core.Data.Models
{
	public class TTArchivo
	{

		public static string TABLE_NAME = "TTArchivos";

		public int Folio { get; set; }
		public string Nombre { get; set; }
		public byte[] Archivo { get; set; }
		public string ArchivoURL { get; set; }
	}
}

