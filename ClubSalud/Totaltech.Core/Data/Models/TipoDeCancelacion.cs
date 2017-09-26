using System;
using System.Collections.Generic;

namespace Totaltech.Core
{
    public class TipoDeCancelacion
    {
        public const string TABLE_NAME = "Tipo_de_Cancelacion";

        public int Folio { get; set; }
        public string Descripcion { get; set; }
        public bool Tiene_Costo { get; set; }
        public double Monto_del_Costo { get; set; }
        public int Id { get; set; }
    }

    public class TipoDeCancelacionPagingModel
    {
        public List<TipoDeCancelacion> Tipo_de_Cancelacions { get; set; }
        public int RowCount { get; set; }
    }
}
