using System;
using System.Collections.Generic;
using Realms;
using Totaltech.Core.Data.Models;

namespace Totaltech.Core
{

	public class TipoDePersonaTipoDePersona
	{
		public int Folio { get; set; }
		public string Descripcion { get; set; }
		public int Id { get; set; }
	}

	public class LavaCochess : Realms.RealmObject
	{
		public const string TABLE_NAME = "lava_coches";

		public int Folio { get; set; }
		public string Fecha_que_Registra { get; set; }
		public string Hora_que_Registra { get; set; }
		public int? Usuario_que_Registra { get; set; }
		public string Nombre { get; set; }
		public string Apellido_Paterno { get; set; }
		public string Apellido_Materno { get; set; }
		public string Telefono { get; set; }
		public string Correo { get; set; }
		public string Contrasena { get; set; }
		public string Confirmar_Contrasena { get; set; }
		public int? Pais { get; set; }
		public int? Estado { get; set; }
		public int? Ciudad { get; set; }
		public string Colonia { get; set; }
		public string Calle { get; set; }
		public string Numero { get; set; }
		public string Codigo_Postal { get; set; }
		public string RFC { get; set; }
		public string Razon_Social { get; set; }
		public int? Tipo_de_Persona { get; set; }
		public int? Pais_Fiscal { get; set; }
		public int? Estado_Fiscal { get; set; }
		public int? Ciudad_Fiscal { get; set; }
		public string Colonia_Fiscal { get; set; }
		public string Calle_Fiscal { get; set; }
		public string Numero_Fiscal { get; set; }
		public string Telefono_Fiscal { get; set; }
		public string Correo_Fiscal { get; set; }
		[Ignored]
		public TTUsuario Usuario_que_Registra_TTUsuarios { get; set; }
		[Ignored]
		public Pais Pais_Pais { get; set; }
		[Ignored]
		public Estado Estado_Estado { get; set; }
		[Ignored]
		public Ciudad Ciudad_Ciudad { get; set; }
		[Ignored]
		public TipoDePersonaTipoDePersona Tipo_de_Persona_Tipo_de_Persona { get; set; }
		[Ignored]
		public Pais Pais_Fiscal_Pais { get; set; }
		[Ignored]
		public Estado Estado_Fiscal_Estado { get; set; }
		[Ignored]
		public Ciudad Ciudad_Fiscal_Ciudad { get; set; }
		public int? Id { get; set; }
		public bool IsLoggedIn { get; set; }
	}

	public class TrabajadorPagingModel
	{
		public List<LavaCochess> Lava_Cochess { get; set; }
		public int RowCount { get; set; }
	}
}
