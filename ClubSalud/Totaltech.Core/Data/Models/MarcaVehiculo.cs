using System;
using System.Collections.Generic;

namespace Totaltech.Core
{
	public class MarcaVehiculo
	{
		public const string TABLE_NAME = "marca_del_vehiculo";

		public class EstatusEstatus
		{
			public int Folio { get; set; }
			public string Descripcion { get; set; }
			public int Id { get; set; }
		}

		public class MarcaDelVehiculo
		{
			public int Folio { get; set; }
			public string Nombre { get; set; }
			public int Estatus { get; set; }
			public EstatusEstatus Estatus_Estatus { get; set; }
			public int Id { get; set; }
		}

		public class MarcaPaginModel
		{
			public List<MarcaDelVehiculo> Marca_del_Vehiculos { get; set; }
			public int RowCount { get; set; }
		}
	}
}
