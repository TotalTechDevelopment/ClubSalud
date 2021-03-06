﻿using Newtonsoft.Json;
using SQLite;
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
		public string Role { get; set; }
	}

	public class User
	{
        public static string TABLE_NAME = "Registro_de_Usuario";

		public int Folio { get; set; }
		public string Fecha_de_Registro { get; set; }
		public string Hora_de_Registro { get; set; }
		public string Nombre { get; set; }
		public string Apellido_Paterno { get; set; }
		public string Apellido_Materno { get; set; }
		public string Nombre_Completo { get; set; }
		public int? Foto_de_Perfil { get; set; }
		public string Numero_de_Seguro { get; set; }
		public string Paquete { get; set; }
		public string RFC { get; set; }
		public string Sexo { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public int? Codigo_Postal { get; set; }
		public string Colonia { get; set; }
		public string Calle { get; set; }
		public int? Numero_Exterior { get; set; }
		public string Numero_Interior { get; set; }
		public string Entre_Calles { get; set; }
		public int Estatus { get; set; }
        public string Vigencia { get; set; }
		public string Clave_de_Acceso { get; set; }
		public string Contrasena { get; set; }
		public int? SpartanID { get; set; }
		public string Nombre_del_Titular { get; set; }
		public string Apellido_Paterno_del_Titular { get; set; }
		public string Apellido_Materno_del_Titular { get; set; }
		public int? Edad_del_Titular { get; set; }
		public string Sexo_del_Titular { get; set; }
		public string Nombre_de_Contacto { get; set; }
		public string Telefono_Entrega { get; set; }
		public string Email_Entrega { get; set; }
		public int? CP_Entrega { get; set; }
		public string Colonia_Entrega { get; set; }
		public string Calle_Entrega { get; set; }
		public int? Num_Ext_Entrega { get; set; }
		public string Num_Int_Entrega { get; set; }
		public string Entre_Calles_Entrega { get; set; }
		public string Foto_de_Perfil_Spartane_File { get; set; }

        [JsonIgnore]
        public string EmpresaNombre { get; set; }

        public long? Empresa { get; set; }

        [Ignore]
        public EmpresaRegistro Empresa_Registro_de_Empresa { get; set; }

        //public EstatusEstatusDeUsuario Estatus_Estatus_de_Usuario { get; set; }
        //public SpartanIDSpartanUser SpartanID_Spartan_User { get; set; }
        //public int Id { get; set; }

        [JsonIgnore]
        public string VigenciaFormatted
        {
            get
            {
                try
                {
                    var vigenciaDateTime = DateTime.Parse(Vigencia);
                    return vigenciaDateTime.ToString("dd/MM/yyyy");
                }
                catch (Exception)
                {
                    return "";
                }
                
            }
        }
	}

    public class EmpresaRegistro
    {
        public long Folio { get; set; }
        public string Nombre { get; set; }
    }

    public class UserPagingModel
	{
		public List<User> Registro_de_Usuarios { get; set; }
		public int RowCount { get; set; }
	}
}
