using System;
using System.Collections.Generic;

namespace Totaltech.Core.Data.Models
{

	public class TipoDePagoFormaDePago
	{
		public int Folio { get; set; }
		public string Descripcion { get; set; }
		public object Estatus { get; set; }
		public object Estatus_Estatus { get; set; }
		public int Id { get; set; }
	}

	public class EstatusEstatusServicioLavadoMaestro
	{
		public int Folio { get; set; }
		public string Descripcion { get; set; }
		public int Id { get; set; }
	}
	 

	public class TipoDeCancelacionDelLavaCochesTipoDeCancelacionEcoWorker
	{
		public int Clave { get; set; }
		public string Descripcion { get; set; }
		public object Tiene_Costo { get; set; }
		public object Monto_del_Costo { get; set; }
		public int Id { get; set; }
	}

	public class TipoDeCancelacionDelClienteTipoDeCancelacion
	{
		public int Folio { get; set; }
		public string Descripcion { get; set; }
		public object Tiene_Costo { get; set; }
		public object Monto_del_Costo { get; set; }
		public int Id { get; set; }
	}

	public class ServicioDeLavadoMaestro
	{
		public const string TABLE_NAME = "servicio_de_lavado_maestro";

		public int Folio { get; set; }
		public string Fecha_que_Registra { get; set; }
		public string Hora_que_Registra { get; set; }
		public int Usuario_que_Registra { get; set; }
		public int Cliente { get; set; }
		public string Marca { get; set; }
		public string Modelo { get; set; }
		public string Placas { get; set; }
		public int? Lava_Coches { get; set; }
		public string Fecha_del_Servicio { get; set; }
		public string Hora_de_Inicio { get; set; }
		public string Hora_de_Fin { get; set; }
		public string Ciudad { get; set; }
		public string Colonia { get; set; }
		public double Costo_del_Servicio { get; set; }
		public int Tipo_de_Pago { get; set; }
		public string Calificacion { get; set; }
		public string Fecha_de_Pago { get; set; }
		public string Hora_de_Pago { get; set; }
		public int Estatus { get; set; }
		public string Calle { get; set; }
		public int? Foto_evidencia { get; set; }
		public int? Foto_evidencia_2 { get; set; }
		public int? Foto_evidencia_3 { get; set; }
		public string Ubicacion_del_Servicio { get; set; }
		public int? Tipo_de_Cancelacion_del_Lava_Coches { get; set; }
		public string Comentarios_del_Lava_Coches { get; set; }
		public int? Tipo_de_Cancelacion_del_Cliente { get; set; }
		public string Comentarios_del_Cliente { get; set; }
		public TTUsuario Usuario_que_Registra_TTUsuarios { get; set; }
		//public Cliente Cliente_Cliente { get; set; }
		public LavaCochess Lava_Coches_Lava_Coches { get; set; }
		public TipoDePagoFormaDePago Tipo_de_Pago_Forma_de_Pago { get; set; }
		public EstatusEstatusServicioLavadoMaestro Estatus_Estatus_Servicio_Lavado_Maestro { get; set; }
		public TTArchivo Foto_evidencia_TTArchivos { get; set; }
		public TTArchivo Foto_evidencia_2_TTArchivos { get; set; }
		public TTArchivo Foto_evidencia_3_TTArchivos { get; set; }
		public TipoDeCancelacionDelLavaCochesTipoDeCancelacionEcoWorker Tipo_de_Cancelacion_del_Lava_Coches_Tipo_de_Cancelacion_EcoWorker { get; set; }
		public TipoDeCancelacionDelClienteTipoDeCancelacion Tipo_de_Cancelacion_del_Cliente_Tipo_de_Cancelacion { get; set; }
		public int Id { get; set; }

		public DateTime FechaInicioDateTime
		{
			get
			{
				try
				{
					return DateTime.Parse(Fecha_del_Servicio.Substring(0, 10)+"T"+Hora_de_Inicio);
				}
				catch
				{

				}

				if(Fecha_del_Servicio!=null)
					return DateTime.Parse(Fecha_del_Servicio);


				return DateTime.MinValue;
			}
		}

		public string DireccionServicio
		{
			get
			{
				return Calle + "," + Colonia + ", " + Ciudad;
			}
		}

		public string CostoTotal
		{
			get 
			{
				return "$" + Costo_del_Servicio;
			}
		}

	}

	public class ServicioPagingModel
	{
		public List<ServicioDeLavadoMaestro> Servicio_de_Lavado_Maestros { get; set; }
		public int RowCount { get; set; }
	}
}
