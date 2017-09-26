using System;
using System.Collections.Generic;

namespace Totaltech.Core
{
	public class Recomendacioness
	{

		public const string TABLE_NAME = "recomendaciones";

		public int Folio { get; set; }
		public string Fecha { get; set; }
		public string Descripcion { get; set; }
		public int Id { get; set; }
	}

	public class RecomendacionesPagingPage
	{
		public List<Recomendacioness> Recomendacioness { get; set; }
		public int RowCount { get; set; }
	}
}
