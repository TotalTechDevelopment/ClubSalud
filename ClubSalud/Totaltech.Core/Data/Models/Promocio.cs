using System;
using System.Collections.Generic;

namespace Totaltech.Core.Data.Models
{

	public class EstatusPromocion
	{
		public int Folio { get; set; }
		public string Descripcion { get; set; }
		public int Id { get; set; }
	}

	public class Promocion
	{
		public const string TABLE_NAME= "promocion";

		public int Folio { get; set; }
		public string Clave { get; set; }
		public string Fecha_de_Inicio { get; set; }
		public string Fecha_de_Vencimiento { get; set; }
		public int Valor_del_Cupon { get; set; }
		public int Estatus { get; set; }
		public int? Foto_de_la_Promocion { get; set; }
		public EstatusPromocion Estatus_Estatus { get; set; }
		public TTArchivo Foto_de_la_Promocion_TTArchivos { get; set; }
		public int Id { get; set; }

		public string Thumbnail
		{
			get
			{
				if (Foto_de_la_Promocion_TTArchivos != null && Foto_de_la_Promocion_TTArchivos.ArchivoURL != null)
					return Foto_de_la_Promocion_TTArchivos.ArchivoURL;

				return null;
			}
		}

		public string ValorFormatted
		{
			get
			{
				return "$" + Valor_del_Cupon;
			}
		}

		public string FechaVencimientoDateTime
		{
			get
			{
				var s1 = Fecha_de_Vencimiento;
				if (s1 != null)
				{
					s1 = s1.Substring(0, 10);
					return DateTime.Parse(s1).ToString();
				}

				return new DateTime().ToString();
			}
		}

		public DateTime Vencimiento
		{
			get
			{
				var s1 = Fecha_de_Vencimiento;
				if (s1 != null)
				{
					s1 = s1.Substring(0, 10);
					return DateTime.Parse(s1);
				}

				return new DateTime();
			}
		}
	}

	public class PromocionPaginModel
	{
		public List<Promocion> Promocions { get; set; }
		public int RowCount { get; set; }
	}
}
