using System;
using System.Collections.Generic;

namespace Totaltech.Core
{
	public class RegistroDeRecuperacionDeContrasena
	{
		public const string TABLE_NAME = "Recuperacion_de_Contrasena";

		public int Folio { get; }
		public string Fecha { get; set; }
		public string Hora { get; set; }
		public string Usuario { get; set; }
		public string Correo_Electronico { get; set; }
		public string Codigo { get; set; }
		public int Id { get; set; }
	}

	public class ForgotPasswordCodePagingModel
	{
		public List<RegistroDeRecuperacionDeContrasena> Recuperacion_de_Contrasenas { get; set; }
		public int RowCount { get; set; }
	}
}
