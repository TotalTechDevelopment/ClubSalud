using System;
using System.Collections.Generic;

namespace Totaltech.Core
{

	public class Tarjeta
	{
		public const string TABLE_NAME = "detalle_de_tarjetas_del_cliente";

		public int Folio { get; set; }
		public string Numero_de_Tarjeta { get; set; }
		public string Nombre_del_Tarjetahabiente { get; set; }
		public string Vencimiento_Mes { get; set; }
		public string Vencimiento_Ano { get; set; }
		public string Codigo_de_Seguridad { get; set; }
		public int Cliente { get; set; }
		//public object Cliente_Cliente { get; set; }
		public int Id { get; set; }


		public string TarjetaLabel
		{
			get
			{
				if (Numero_de_Tarjeta != null && Numero_de_Tarjeta.Length > 5)
				{
					var str = Numero_de_Tarjeta.Substring(Numero_de_Tarjeta.Length - 4, 4);
					return "****-****-****-"+str;
				}

				return "";
			}
		}

	}

	public class TarjetaPagingModel
	{
		public List<Tarjeta> Detalle_de_Tarjetas_del_Clientes { get; set; }
		public int RowCount { get; set; }
	}
}
