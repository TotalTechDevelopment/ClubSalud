using System;
namespace Totaltech.Core.Data.Models
{
	public class TTUsuario
	{
		public const string TABLE_NAME = "ttusuarios";

		public int IdUsuario { get; set; }
		public string Nombre { get; set; }
		public string Clave_de_Acceso { get; set; }
		public string Contrasena { get; set; }
		public bool? Activo { get; set; }
		public int? idGrupoUsuarios { get; set; }
		public int Id { get; set; }
	}
}
