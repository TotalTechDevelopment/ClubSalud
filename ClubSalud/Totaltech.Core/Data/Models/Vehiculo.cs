using System;
using System.Collections.Generic;

namespace Totaltech.Core.Data.Models
{ 
	
	public class MarcaMarcaDelVehiculo
	{
		public int Folio { get; set; }
		public string Nombre { get; set; }
		public int? Estatus { get; set; }
		public object Estatus_Estatus { get; set; }
		public int Id { get; set; }
	}

	public class VehiculosPorCliente
	{
		public const string TABLE_NAME = "vehiculos_por_cliente";

		public int Folio { get; set; }
		public int Cliente { get; set; }
		public int Marca { get; set; }
		public string Modelo { get; set; }
		public string Color { get; set; }
		public string Placas { get; set; }
		public string Observaciones { get; set; }
		//public Cliente Cliente_Cliente { get; set; }
		public MarcaMarcaDelVehiculo Marca_Marca_del_Vehiculo { get; set; }
		public string MarcaVehiculoAlt { get; set; }
		public int Id { get; set; }
	}

	public class VehiculoPaginModel
	{
		public List<VehiculosPorCliente> Vehiculos_por_Clientes { get; set; }
		public int RowCount { get; set; }
	}
}
