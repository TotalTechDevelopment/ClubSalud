using System;
namespace Totaltech.Core
{
	public class Cupon : Realms.RealmObject
	{
		public int FolioCupon { get; set; }
		public int CantidadCupon { get; set; }
		public string FechaLimiteDeUso { get; set; }
		public DateTime FechaLimiteUseDateTime
		{
			get
			{
				try
				{
					return DateTime.Parse(FechaLimiteDeUso.Substring(0, 10));
				}
				catch
				{

				}

				if (FechaLimiteDeUso != null)
					return DateTime.Parse(FechaLimiteDeUso);


				return DateTime.MinValue;
			}
		}
		public string CantidadCuponFormatted
		{
			get
			{
				return "$" + CantidadCupon;
			}
		}
	}
}
