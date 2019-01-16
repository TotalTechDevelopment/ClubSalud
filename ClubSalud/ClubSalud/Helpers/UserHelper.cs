using System;
using ClubSalud.Models.ClubSalud;
using System.Linq;
using ClubSalud.DB;

namespace ClubSalud.Helpers
{
    public class UserHelper
    {
        public static void SaveUserInfo(User user)
        {
            var dbConnection = new ClubSaludDatabase();
            try
            {
                user.EmpresaNombre = user.Empresa_Registro_de_Empresa != null ? user.Empresa_Registro_de_Empresa.Nombre : "";
                dbConnection.AddUser(user);
            }
            catch (Exception ex)
            {
                dbConnection.DeleteUser(user.Folio);
                dbConnection.AddUser(user);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public static User CurrentUser()
        {
            try
            {
                var dbConnection = new ClubSaludDatabase();
                var user = dbConnection.GetCurrentUser();

                var actualUser = new User();
                actualUser.Folio = user.Folio;
                actualUser.Fecha_de_Registro = user.Fecha_de_Registro;
                actualUser.Hora_de_Registro = user.Hora_de_Registro;
                actualUser.Nombre = user.Nombre;
                actualUser.Apellido_Paterno = user.Apellido_Paterno;
                actualUser.Apellido_Materno = user.Apellido_Materno;
                actualUser.Nombre_Completo = user.Nombre_Completo;
                actualUser.Foto_de_Perfil = user.Foto_de_Perfil;
                actualUser.Numero_de_Seguro = user.Numero_de_Seguro;
                actualUser.Paquete = user.Paquete;
                actualUser.RFC = user.RFC;
                actualUser.Sexo = user.Sexo;
                actualUser.Telefono = user.Telefono;
                actualUser.Email = user.Email;
                actualUser.Codigo_Postal = user.Codigo_Postal;
                actualUser.Colonia = user.Colonia;
                actualUser.Calle = user.Calle;
                actualUser.Numero_Exterior = user.Numero_Exterior;
                actualUser.Numero_Interior = user.Numero_Interior;
                actualUser.Entre_Calles = user.Entre_Calles;
                actualUser.Estatus = user.Estatus;
                actualUser.Vigencia = user.Vigencia;
                actualUser.Clave_de_Acceso = user.Clave_de_Acceso;
                actualUser.Contrasena = user.Contrasena;
                //actualUser.SpartanID = user.SpartanID;
                actualUser.Nombre_del_Titular = user.Nombre_del_Titular;
                actualUser.Apellido_Paterno_del_Titular = user.Apellido_Paterno_del_Titular;
                actualUser.Apellido_Materno_del_Titular = user.Apellido_Materno_del_Titular;
                actualUser.Edad_del_Titular = user.Edad_del_Titular;
                actualUser.Sexo_del_Titular = user.Sexo_del_Titular;
                actualUser.Nombre_de_Contacto = user.Nombre_de_Contacto;
                actualUser.Telefono_Entrega = user.Telefono_Entrega;
                actualUser.Email_Entrega = user.Email_Entrega;
                actualUser.CP_Entrega = user.CP_Entrega;
                actualUser.Colonia_Entrega = user.Colonia_Entrega;
                actualUser.Calle_Entrega = user.Calle_Entrega;
                actualUser.Num_Ext_Entrega = user.Num_Ext_Entrega;
                actualUser.Num_Int_Entrega = user.Num_Int_Entrega;
                actualUser.Entre_Calles_Entrega = user.Entre_Calles_Entrega;
                //actualUser.Foto_de_Perfil_Spartane_File = user.Foto_de_Perfil_Spartane_File;
                //actualUser.SpartanID = user.SpartanID;
                actualUser.EmpresaNombre = user.EmpresaNombre;
                App.CurrentUser = actualUser;
                return App.CurrentUser;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                return null;
            }
        }

        public static void UpdateUser(User user)
        {
            try
            {
                var dbConnection = new ClubSaludDatabase();
                dbConnection.UpdateUser(user);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public static void Logout()
        {
            var dbConnection = new ClubSaludDatabase();
            dbConnection.DeleteUser(App.CurrentUser.Folio);
            App.CurrentUser = null;
        }
    }
}
