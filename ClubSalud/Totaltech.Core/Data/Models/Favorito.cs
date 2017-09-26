using System;
using System.Collections.Generic;

namespace Totaltech.Core.Data.Models
{ 
	public class Favorito
	{
		public const string TABLE_NAME = "favorito";

		public int Folio { get; set; }
		public int Cliente { get; set; }
		public int Pais { get; set; }
		public int Estado { get; set; }
		public int Ciudad { get; set; }
		public string Colonia { get; set; }
		public string Calle { get; set; }
		public string Numero_Exterior { get; set; }
		public string Numero_Interior { get; set; }
		//public Cliente Cliente_Cliente { get; set; }
		public Pais Pais_Pais { get; set; }
		public Estado Estado_Estado { get; set; }
		public Ciudad Ciudad_Ciudad { get; set; }
		public int Id { get; set; }
		public string Ubicacion { get; set; }

		public string CompleteAddress
		{
			get
			{
				var address = "";

				if (!string.IsNullOrEmpty(Colonia))
				{
					address += Colonia;
				}

				if (!string.IsNullOrEmpty(Calle))
				{
					address += ", " + Calle;
				}

				if (!string.IsNullOrEmpty(Numero_Exterior))
				{
					address += ", " + Numero_Exterior;
				}

				return address;
			}
		}
	}

	public class FavoritoHelper
	{
		public string Estado { get; set; }
		public string Ciudad { get; set; }
	}

	public class FavoritoPagingModel
	{
		public List<Favorito> Favoritos { get; set; }
		public int RowCount { get; set; }
	}
}
