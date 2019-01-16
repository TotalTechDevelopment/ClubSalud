using ClubSalud.Models.ClubSalud;
using ClubSalud.Models.SQLite;
using ClubSalud.Providers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ClubSalud.DB
{
    class ClubSaludDatabase
    {
        private SQLiteConnection _connection;

        public ClubSaludDatabase()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<DBUser>();
        }

        public DBUser GetUser(int folio)
        {
            var user = _connection.Table<DBUser>().FirstOrDefault(x => x.Folio == folio);

            return user;
        }

        public DBUser GetCurrentUser()
        {
            var users = new List<DBUser>(_connection.Table<DBUser>());

            if (users.Count > 0)
            {
                var user = users.FirstOrDefault();

                return user;
            }

            return null;
        }

        public void AddUser(User user)
        {
            var dbUser = new DBUser();
            dbUser.Folio = user.Folio;
            dbUser.Fecha_de_Registro = user.Fecha_de_Registro;
            dbUser.Hora_de_Registro = user.Hora_de_Registro;
            dbUser.Nombre = user.Nombre;
            dbUser.Apellido_Paterno = user.Apellido_Paterno;
            dbUser.Apellido_Materno = user.Apellido_Materno;
            dbUser.Nombre_Completo = user.Nombre_Completo;
            dbUser.Foto_de_Perfil = (user.Foto_de_Perfil != null)? (int)user.Foto_de_Perfil : -1;
            dbUser.Numero_de_Seguro = user.Numero_de_Seguro;
            dbUser.Paquete = user.Paquete;
            dbUser.RFC = user.RFC;
            dbUser.Sexo = user.Sexo;
            dbUser.Telefono = user.Telefono;
            dbUser.Email = user.Email;
            dbUser.Codigo_Postal = user.Codigo_Postal != null ? (int)user.Codigo_Postal : 0;
            dbUser.Colonia = user.Colonia;
            dbUser.Calle = user.Calle;
            dbUser.Numero_Exterior = user.Numero_Exterior != null ? (int) user.Numero_Exterior : 0;
            dbUser.Numero_Interior = user.Numero_Interior;
            dbUser.Entre_Calles = user.Entre_Calles;
            dbUser.Estatus = user.Estatus;
            dbUser.Vigencia = user.Vigencia;
            dbUser.Clave_de_Acceso = user.Clave_de_Acceso;
            dbUser.Contrasena = user.Contrasena;
            //dbUser.SpartanID = (int)user.SpartanID;
            dbUser.Nombre_del_Titular = user.Nombre_del_Titular;
            dbUser.Apellido_Paterno_del_Titular = user.Apellido_Paterno_del_Titular;
            dbUser.Apellido_Materno_del_Titular = user.Apellido_Materno_del_Titular;
            dbUser.Edad_del_Titular = user.Edad_del_Titular != null ? (int) user.Edad_del_Titular : 0;
            dbUser.Sexo_del_Titular = user.Sexo_del_Titular;
            dbUser.Nombre_de_Contacto = user.Nombre_de_Contacto;
            dbUser.Telefono_Entrega = user.Telefono_Entrega;
            dbUser.Email_Entrega = user.Email_Entrega;
            dbUser.CP_Entrega = user.CP_Entrega != null ? (int) user.CP_Entrega : 0;
            dbUser.Colonia_Entrega = user.Colonia_Entrega;
            dbUser.Calle_Entrega = user.Calle_Entrega;
            dbUser.Num_Ext_Entrega = user.Num_Ext_Entrega != null ? (int) user.Num_Ext_Entrega : 0;
            dbUser.Num_Int_Entrega = user.Num_Int_Entrega;
            dbUser.Entre_Calles_Entrega = user.Entre_Calles_Entrega;
            dbUser.Foto_de_Perfil_Spartane_File = user.Foto_de_Perfil_Spartane_File;
            dbUser.EmpresaNombre = user.EmpresaNombre;
            _connection.Insert(dbUser);
        }

        public void UpdateUser(User user)
        {
            DeleteUser(user.Folio);
            AddUser(user);
        }

        public void DeleteUser(int folio)
        {
            _connection.Delete<DBUser>(folio);
        }
    }
}
