using System;
using System.Collections.Generic;

namespace ClubSalud.Models.ClubSalud
{
   
	public class EstatusEstatusDeUsuario
	{
		public int Clave { get; set; }
		public string Descripcion { get; set; }
		public int Id { get; set; }
	}

	public class SpartanIDSpartanUser
	{
		public int Id_User { get; set; }
		public string Name { get; set; }
		public object Role { get; set; }
		public object Image { get; set; }
		public object Email { get; set; }
		public object Status { get; set; }
		public object Username { get; set; }
		public object Password { get; set; }
		public object Role_Spartan_User_Role { get; set; }
		public object Image_Spartane_File { get; set; }
		public object Status_Spartan_User_Status { get; set; }
		public int Id { get; set; }
	}

	public class User
	{
        public const string TABLE_NAME = "Registro_de_Usuario";
		public int Folio { get; set; }
		public object Fecha_de_Registro { get; set; }
		public object Hora_de_Registro { get; set; }
		public string Nombre { get; set; }
		public string Apellido_Paterno { get; set; }
		public string Apellido_Materno { get; set; }
		public object Nombre_Completo { get; set; }
		public object Foto_de_Perfil { get; set; }
		public string Numero_de_Seguro { get; set; }
		public string Paquete { get; set; }
		public string RFC { get; set; }
		public string Sexo { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public int Codigo_Postal { get; set; }
		public string Colonia { get; set; }
		public string Calle { get; set; }
		public int Numero_Exterior { get; set; }
		public object Numero_Interior { get; set; }
		public string Entre_Calles { get; set; }
		public int Estatus { get; set; }
		public DateTime Vigencia { get; set; }
		public string Clave_de_Acceso { get; set; }
		public string Contrasena { get; set; }
		public int SpartanID { get; set; }
		public string Nombre_del_Titular { get; set; }
		public string Apellido_Paterno_del_Titular { get; set; }
		public string Apellido_Materno_del_Titular { get; set; }
		public int Edad_del_Titular { get; set; }
		public string Sexo_del_Titular { get; set; }
		public string Nombre_de_Contacto { get; set; }
		public string Telefono_Entrega { get; set; }
		public string Email_Entrega { get; set; }
		public int CP_Entrega { get; set; }
		public string Colonia_Entrega { get; set; }
		public string Calle_Entrega { get; set; }
		public int Num_Ext_Entrega { get; set; }
		public object Num_Int_Entrega { get; set; }
		public object Entre_Calles_Entrega { get; set; }
		public object Foto_de_Perfil_Spartane_File { get; set; }
		public EstatusEstatusDeUsuario Estatus_Estatus_de_Usuario { get; set; }
		public SpartanIDSpartanUser SpartanID_Spartan_User { get; set; }
		public int Id { get; set; }
	}

	public class UserPagingModel
	{
		public List<User> Registro_de_Usuarios { get; set; }
		public int RowCount { get; set; }
	}
}
