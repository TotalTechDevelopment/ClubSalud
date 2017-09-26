using System;
using System.Collections.Generic;

namespace Totaltech.Core
{
	public class EcoWorkerToken
	{
		public const string TABLE_NAME = "ecoworker_token";

		public int Folio { get; set; }
		public int EcoWorker_Token_campo { get; set; }
		public int Id_EcoWorker { get; set; }
		public string Token { get; set; }
		public string Identificador { get; set; }
		public bool Estado_Logico { get; set; }
		public int EcoWorker { get; set; }
		public LavaCochess EcoWorker_Lava_Coches { get; set; }
		public int Id { get; set; }
		public int Tipo_de_Dispositivo { get; set; }
	}

	public class EcoWorkerTokenPagingModel
	{
		public List<EcoWorkerToken> EcoWorker_Tokens { get; set; }
		public int RowCount { get; set; }
	}
}
