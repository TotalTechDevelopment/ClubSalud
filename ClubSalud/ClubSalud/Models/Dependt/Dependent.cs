using System;
using System.Collections.Generic;

namespace ClubSalud
{
	public class DetalleDeDependientesDeUsuario
	{
        public const string TABLE_NAME = "Detalle_de_Dependientes_de_Usuario";

		public int Clave { get; set; }
		public int Usuario { get; set; }
		public string Nombre { get; set; }
		public string Apellido_Paterno { get; set; }
		public string Apellido_Materno { get; set; }
		public object Nombre_Completo { get; set; }
		public string Parentesco { get; set; }
        public string Foto { get; set; }
		public int Edad { get; set; }
		public string Sexo { get; set; }
		public object Usuario_Registro_de_Usuario { get; set; }
		public object Foto_Spartane_File { get; set; }
		public int Id { get; set; }
	}

	public class DependientePagingModel
	{
		public List<DetalleDeDependientesDeUsuario> Detalle_de_Dependientes_de_Usuarios { get; set; }
		public int RowCount { get; set; }
	}
}
