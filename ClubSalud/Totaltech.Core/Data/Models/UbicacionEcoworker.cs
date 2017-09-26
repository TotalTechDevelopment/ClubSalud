using System;
namespace Totaltech.Core
{

	public class UbicacionEcoworker
	{
		public const string TABLE_NAME = "Ubicacion_Ecoworker";

		public int Clave { get; set; }
		public int Ecoworker { get; set; }
		public string Latitud { get; set; }
		public string Longitud { get; set; }
		public string Estatus { get; set; }
		public LavaCochess Ecoworker_Lava_Coches { get; set; }
		public int Id { get; set; }
	}
}
