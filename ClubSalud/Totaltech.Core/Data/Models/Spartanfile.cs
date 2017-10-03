using System;
namespace Totaltech.Core.Data.Models
{
	public class SpartanFile
	{

        public const string TABLE_NAME = "Spartan_File";

		public int File_Id { get; set; }
		public string Description { get; set; }
		public int? File1 { get; set; }
		public int? File_Size { get; set; }
		public int? Object { get; set; }
		public int? User_Id { get; set; }
		public DateTime? Date_Time { get; set; }
		public byte[] File { set; get; }
	}
}
