using System;
using System.Collections.Generic;
using Totaltech.Core.Data.Models;

namespace Totaltech.Core
{
	public class ClienteToken
	{

		public const string TABLE_NAME = "cliente_token";

		public int Folio { get; set; }
		public int Cliente_Token_1 { get; set; }
		public int Id_Cliente { get; set; }
		public string Token { get; set; }
		public string Identificador { get; set; }
		public bool Estado_Logico { get; set; }
		public int Cliente { get; set; }
		//public Cliente Cliente_Cliente { get; set; }
		public int Id { get; set; }
		public int Tipo_de_Dispositivo { get; set; }
	}

	public class ClienteTokenPagingModel
	{
		public List<ClienteToken> Cliente_Tokens { get; set; }
		public int RowCount { get; set; }
	}
}
