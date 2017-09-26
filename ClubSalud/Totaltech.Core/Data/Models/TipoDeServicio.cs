using System;
using System.Collections.Generic;

namespace Totaltech.Core.Data.Models
{
	public class TipoDeServicio
	{
		public const string TABLE_NAME = "tipo_de_servicio";

		public int Folio { get; set; }
		public string Descripcion { get; set; }
		public double Costo { get; set; }
		public string Descripcion_del_Servicio { get; set; }
		public int Id { get; set; }
	}

	public class TipoDeServicioPaginModel
	{
		public List<TipoDeServicio> Tipo_de_Servicios { get; set; }
		public int RowCount { get; set; }
	}
}
