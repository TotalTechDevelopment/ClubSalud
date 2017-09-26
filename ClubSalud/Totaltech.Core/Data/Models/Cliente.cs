using System;
using System.Collections.Generic;
using Realms;

namespace Totaltech.Core.Data.Models
{
	public class UsuarioQueRegistraTTUsuarios
	{
		public int IdUsuario { get; set; }
		public string Nombre { get; set; }
		public object Clave_de_Acceso { get; set; }
		public object Contrasena { get; set; }
		public object Activo { get; set; }
		public object idGrupoUsuarios { get; set; }
		public int Id { get; set; }
	}

	public class Pais
	{
		public int Folio { get; set; }
		public string Nombre { get; set; }
		public object Abreviatura_del_Pais { get; set; }
		public int Id { get; set; }
	}

	public class Estado
	{
		public const string TABLE_NAME = "estado";

		public int Folio { get; set; }
		public string Nombre { get; set; }
		public object Pais { get; set; }
		public object Abreviatura_del_Estado { get; set; }
		public object Pais_Pais { get; set; }
		public int Id { get; set; }
	}

	public class Ciudad
	{
		public const string TABLE_NAME = "ciudad";

		public int Folio { get; set; }
		public string Nombre { get; set; }
		public object Abreviatura_de_la_Ciudad { get; set; }
		public object Estado { get; set; }
		public object Estado_Estado { get; set; }
		public int Id { get; set; }
	}

	public class TipoDePersonaTipoDePersona
	{
		public int Folio { get; set; }
		public string Descripcion { get; set; }
		public int Id { get; set; }
	}

	public class PaisFiscalPais
	{
		public int Folio { get; set; }
		public string Nombre { get; set; }
		public object Abreviatura_del_Pais { get; set; }
		public int Id { get; set; }
	}

	public class EstadoFiscalEstado
	{
		public int Folio { get; set; }
		public string Nombre { get; set; }
		public object Pais { get; set; }
		public object Abreviatura_del_Estado { get; set; }
		public object Pais_Pais { get; set; }
		public int Id { get; set; }
	}

	public class CiudadFiscalCiudad
	{
		public int Folio { get; set; }
		public string Nombre { get; set; }
		public object Abreviatura_de_la_Ciudad { get; set; }
		public object Estado { get; set; }
		public object Estado_Estado { get; set; }
		public int Id { get; set; }
	}

	public class Cliente : Realms.RealmObject
	{
		public const string TABLE_NAME = "cliente";

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
		public string Codigo { get; set; }
		public string Confirmar_Contrasena { get; set; }
		public int Pais { get; set; }
		public int Estado { get; set; }
		public int Ciudad { get; set; }
		public string Colonia { get; set; }
		public string Calle { get; set; }
		public string Numero { get; set; }
		public string Codigo_Postal { get; set; }
		public string RFC { get; set; }
		public string Razon_Social { get; set; }
		public int Tipo_de_Persona { get; set; }
		public int Pais_Fiscal { get; set; }
		public int Estado_Fiscal { get; set; }
		public int Ciudad_Fiscal { get; set; }
		public string Colonia_Fiscal { get; set; }
		public string Calle_Fiscal { get; set; }
		public string Numero_Fiscal { get; set; }
		public string Telefono_Fiscal { get; set; }
		public string Correo_Fiscal { get; set; }
		[Ignored]
		public UsuarioQueRegistraTTUsuarios Usuario_que_Registra_TTUsuarios { get; set; }
		[Ignored]
		public Pais Pais_Pais { get; set; }
		[Ignored]
		public Estado Estado_Estado { get; set; }
		[Ignored]
		public Ciudad Ciudad_Ciudad { get; set; }
		[Ignored]
		public TipoDePersonaTipoDePersona Tipo_de_Persona_Tipo_de_Persona { get; set; }
		[Ignored]
		public PaisFiscalPais Pais_Fiscal_Pais { get; set; }
		[Ignored]
		public EstadoFiscalEstado Estado_Fiscal_Estado { get; set; }
		[Ignored]
		public CiudadFiscalCiudad Ciudad_Fiscal_Ciudad { get; set; }
		public int Id { get; set; }
		public bool IsLoggedIn { get; set; }
	}

	public class ClientePagingModel
	{
		public List<Cliente> Clientes { get; set; }
		public int RowCount { get; set; }
	}

	//public class Pais
	//{
	//	public int Folio { get; set; }
	//	public string Nombre { get; set; }
	//	public string Abreviatura_del_Pais { get; set; }
	//	public int Id { get; set; }
	//}

	//public class Estado
	//{
	//	public const string TABLE_NAME = "estado";

	//	public int Folio { get; set; }
	//	public string Nombre { get; set; }
	//	public int? Pais { get; set; }
	//	public string Abreviatura_del_Estado { get; set; }
	//	public Pais Pais_Pais { get; set; }
	//	public int Id { get; set; }
	//}

	//public class Ciudad
	//{
	//	public const string TABLE_NAME = "ciudad";

	//	public int Folio { get; set; }
	//	public string Nombre { get; set; }
	//	public string Abreviatura_de_la_Ciudad { get; set; }
	//	public int? Estado { get; set; }
	//	public Estado Estado_Estado { get; set; }
	//	public int Id { get; set; }
	//}


	public class CiudadPaginModel
	{
		public List<Ciudad> Ciudads { get; set; }
		public int RowCount { get; set; }
	}

	//public class TipoDePersona
	//{
	//	public int Folio { get; set; }
	//	public string Descripcion { get; set; }
	//	public int Id { get; set; }
	//}

	//public class Cliente : Realms.RealmObject
	//{

	//	public const string TABLE_NAME = "cliente";

	//	public int Folio { get; set; }
	//	public string Fecha_que_Registra { get; set; }
	//	public string Hora_que_Registra { get; set; }
	//	public int? Usuario_que_Registra { get; set; }
	//	public string Nombre { get; set; }
	//	public string Apellido_Paterno { get; set; }
	//	public string Apellido_Materno { get; set; }
	//	public string Telefono { get; set; }
	//	public string Correo { get; set; }
	//	public string Contrasena { get; set; }
	//	public string Codigo { get; set; }
	//	public string Confirmar_Contrasena { get; set; }
	//	public int? Pais { get; set; }
	//	public int? Estado { get; set; }
	//	public int? Ciudad { get; set; }
	//	public string Colonia { get; set; }
	//	public string Calle { get; set; }
	//	public string Numero { get; set; }
	//	public string Codigo_Postal { get; set; }
	//	public string RFC { get; set; }
	//	public string Razon_Social { get; set; }
	//	public int? Tipo_de_Persona { get; set; }
	//	public int? Pais_Fiscal { get; set; }
	//	public int? Estado_Fiscal { get; set; }
	//	public int? Ciudad_Fiscal { get; set; }
	//	public string Colonia_Fiscal { get; set; }
	//	public string Calle_Fiscal { get; set; }
	//	public string Numero_Fiscal { get; set; }
	//	public string Telefono_Fiscal { get; set; }
	//	public string Correo_Fiscal { get; set; }
	//	[Ignored]
	//	public TTUsuario Usuario_que_Registra_TTUsuarios { get; set; }
	//	[Ignored]
	//	public Pais Pais_Pais { get; set; }
	//	[Ignored]
	//	public Estado Estado_Estado { get; set; }
	//	[Ignored]
	//	public Ciudad Ciudad_Ciudad { get; set; }
	//	[Ignored]
	//	public TipoDePersona Tipo_de_Persona_Tipo_de_Persona { get; set; }
	//	[Ignored]
	//	public Pais Pais_Fiscal_Pais { get; set; }
	//	[Ignored]
	//	public Estado Estado_Fiscal_Estado { get; set; }
	//	[Ignored]
	//	public Ciudad Ciudad_Fiscal_Ciudad { get; set; }
	//	public int Id { get; set; }
	//	public bool IsLoggedIn { get; set; }
	//}

	//public class ClientePaginModel
	//{
	//	public List<Cliente> Clientes { get; set; }
	//	public int RowCount { get; set; }
	//}
}
