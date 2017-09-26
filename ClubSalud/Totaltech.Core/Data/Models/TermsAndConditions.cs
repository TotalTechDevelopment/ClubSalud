using System;
using System.Collections.Generic;

namespace Totaltech.Core
{

	public class TerminosyCondicioness
	{
		public const string TABLE_NAME = "terminosycondiciones";

		public int Folio { get; set; }
		public string Fecha { get; set; }
		public string Terminos { get; set; }
		public bool Acepto { get; set; }
		public int Id { get; set; }
	}

	public class TermsAndConditionsPagingPage
	{
		public List<TerminosyCondicioness> TerminosyCondicioness { get; set; }
		public int RowCount { get; set; }
	}
}
