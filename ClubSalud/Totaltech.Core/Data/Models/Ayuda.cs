using System;
using System.Collections.Generic;

namespace Totaltech.Core
{
	public class Ayuda
	{

		public const string TABLE_NAME = "ayuda";

		public int Folio { get; set; }
		public string Fecha { get; set; }
		public string Descripcion { get; set; }
		public int Id { get; set; }
	}

	public class AyudaPagingModel
	{
		public List<Ayuda> Ayudas { get; set; }
		public int RowCount { get; set; }
	}
}
