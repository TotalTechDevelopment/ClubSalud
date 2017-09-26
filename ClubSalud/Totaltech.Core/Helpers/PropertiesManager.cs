using System;
using System.Linq;
using Newtonsoft.Json;

namespace Totaltech.Core.Helpers
{
	public class PropertiesManager
	{
		static string USER_INFO_KEY = "USER_INFO";
		static string APP_LAUNCH_COUNTER_KEY = "APP_LAUNCH_COUNTER_KEY";

		public static bool IsLogedIn()
		{

			// var realm = App.CurrentApp.RealmInstance;

			// var n = realm.All<Conductor>().ToList();
			// if (n.Count > 0)
			// {
			// 	return n.Last().IsLoggedIn;
			// }


			// return false;
			return true;
		}

		//public static async void SaveUserInfo(Conductor user)
		//{
			// var realm = App.CurrentApp.RealmInstance;

			// realm.Write(() =>
			// {
			// 	var n = realm.All<Conductor>().ToList();
			// 	if (n.Count > 0)
			// 	{
			// 		var x = n.Last();
			// 		x.Folio = user.Folio;
			// 		x.Correo = user.Correo;
			// 		x.Contrasena = user.Contrasena;
			// 		x.Confirmar_Contrasena = user.Confirmar_Contrasena;
			// 		x.Contrasena = user.Contrasena;
			// 		x.Fecha_de_Registro = user.Fecha_de_Registro;
			// 		x.Nombre = user.Nombre;
			// 		x.IsLoggedIn = user.IsLoggedIn;
			// 	}
			// 	else {
			// 		var x = realm.CreateObject<Conductor>();
			// 		x.Folio = user.Folio;
			// 		x.Correo = user.Correo;
			// 		x.Contrasena = user.Contrasena;
			// 		x.Confirmar_Contrasena = user.Confirmar_Contrasena;
			// 		x.Contrasena = user.Contrasena;
			// 		x.Fecha_de_Registro = user.Fecha_de_Registro;
			// 		x.Nombre = user.Nombre;
			// 		x.IsLoggedIn = user.IsLoggedIn;
			// 	}
			// });
		//}

		//public static async void SaveUserVehicle(Vehiculo vehiculo)
		//{
			// var realm = App.CurrentApp.RealmInstance;

			// realm.Write(() =>
			// {
			// 	var n = realm.All<Vehiculo>().ToList();
			// 	if (n.Count > 0)
			// 	{
			// 		var x = n.Last();
			// 		x.Folio = vehiculo.Folio;
			// 		x.Fecha_de_Registro = vehiculo.Fecha_de_Registro;
			// 		x.Hora_de_Registro = vehiculo.Hora_de_Registro;
			// 		x.Usuario_que_Registra = vehiculo.Usuario_que_Registra;
			// 		x.Socio_de_Negocio = vehiculo.Socio_de_Negocio;
			// 		x.Marca = vehiculo.Marca;
			// 		x.Modelo = vehiculo.Modelo;
			// 		x.Placas = vehiculo.Placas;
			// 		x.Anio = vehiculo.Anio;
			// 		x.Numero_de_Serie = vehiculo.Numero_de_Serie;
			// 		x.Conductor_del_Vehiculo = vehiculo.Conductor_del_Vehiculo;
			// 		x.Color = vehiculo.Color;
			// 		x.Color_de_Interiores = vehiculo.Color_de_Interiores;
			// 		x.Tarjeta_de_Circulacion = vehiculo.Tarjeta_de_Circulacion;
			// 		x.Registro_Federal_Vehicular = vehiculo.Registro_Federal_Vehicular;
			// 		x.Archivo_del_Seguro = vehiculo.Archivo_del_Seguro;
			// 		x.Copia_de_Factura = vehiculo.Copia_de_Factura;
			// 		x.Estatus = vehiculo.Estatus;
			// 		x.Anexo_de_Poliza_de_Seguro = vehiculo.Anexo_de_Poliza_de_Seguro;
			// 		x.Resultado_de_Autorizacion = vehiculo.Resultado_de_Autorizacion;
			// 		x.Motivo_del_Rechazo = vehiculo.Motivo_del_Rechazo;
			// 		x.Conductor_del_Vehiculo_Registro_de_Conductor_de_Socio = vehiculo.Conductor_del_Vehiculo_Registro_de_Conductor_de_Socio;
			// 	}
			// 	else 
			// 	{
			// 		var x = realm.CreateObject<Vehiculo>();

			// 		x.Folio = vehiculo.Folio;
			// 		x.Fecha_de_Registro = vehiculo.Fecha_de_Registro;
			// 		x.Hora_de_Registro = vehiculo.Hora_de_Registro;
			// 		x.Usuario_que_Registra = vehiculo.Usuario_que_Registra;
			// 		x.Socio_de_Negocio = vehiculo.Socio_de_Negocio;
			// 		x.Marca = vehiculo.Marca;
			// 		x.Modelo = vehiculo.Modelo;
			// 		x.Placas = vehiculo.Placas;
			// 		x.Anio = vehiculo.Anio;
			// 		x.Numero_de_Serie = vehiculo.Numero_de_Serie;
			// 		x.Conductor_del_Vehiculo = vehiculo.Conductor_del_Vehiculo;
			// 		x.Color = vehiculo.Color;
			// 		x.Color_de_Interiores = vehiculo.Color_de_Interiores;
			// 		x.Tarjeta_de_Circulacion = vehiculo.Tarjeta_de_Circulacion;
			// 		x.Registro_Federal_Vehicular = vehiculo.Registro_Federal_Vehicular;
			// 		x.Archivo_del_Seguro = vehiculo.Archivo_del_Seguro;
			// 		x.Copia_de_Factura = vehiculo.Copia_de_Factura;
			// 		x.Estatus = vehiculo.Estatus;
			// 		x.Anexo_de_Poliza_de_Seguro = vehiculo.Anexo_de_Poliza_de_Seguro;
			// 		x.Resultado_de_Autorizacion = vehiculo.Resultado_de_Autorizacion;
			// 		x.Motivo_del_Rechazo = vehiculo.Motivo_del_Rechazo;
			// 		x.Conductor_del_Vehiculo_Registro_de_Conductor_de_Socio = vehiculo.Conductor_del_Vehiculo_Registro_de_Conductor_de_Socio;
			// 	}
			// });
		//}

		//public static object GetUserInfo()
		//{
		//	// var realm = App.CurrentApp.RealmInstance;

		//	// var n = realm.All<Conductor>().ToList();
		//	// if (n.Count > 0)
		//	// 	return n.Last();

		//	return null;
		//}

		//public static async void LogOut()
		//{
		//	//Application.Current.Properties.Remove(USER_INFO_KEY);
		//	//await Application.Current.SavePropertiesAsync();

		//	// var realm = App.CurrentApp.RealmInstance;
		//	// var n = realm.All<Conductor>().ToList();
		//	// realm.Write(() =>
		//	// {
		//	// 	realm.RemoveAll<Conductor>();
		//	// 	//n.Last().IsLoggedIn = false;
		//	// });


		//}
	}
}
