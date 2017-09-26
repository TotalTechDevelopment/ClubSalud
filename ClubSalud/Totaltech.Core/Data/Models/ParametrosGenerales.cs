using System;
using System.Collections.Generic;

namespace Totaltech.Core
{
    public class ParametrosGenerales
    {
        public const string TABLE_NAME = "Parametros_Generales";

        public int Folio { get; set; }
        public int? Km_Proximidad { get; set; }
        public int? Tiempo_Maximo_por_Unidad { get; set; }
        public int? Tiempo_Maximo_de_Tolerancia_de_Llegada { get; set; }
        public int? Tiempo_Minimo_de_Cancelacion { get; set; }
        public int Id { get; set; }
    }

    public class ParametrosGeneralesPagingModel
    {
        public List<ParametrosGenerales> Parametros_Generaless { get; set; }
        public int RowCount { get; set; }
    }
}
