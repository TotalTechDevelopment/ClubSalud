using System;
namespace Totaltech.Core.Data.Services
{
	public class Configuration
	{

		static string baseURL = "http://www.totalcase.com.mx/WebApiClubSeguroDEMO2/";

        public const string BASE_WEBAPI_URL = "WebApiClubSeguroDEMO2";
		public const string IMG_URL = "http://www.totalcase.com.mx/" + BASE_WEBAPI_URL + "/api/Spartan_File/Files/";

		public static string BaseURL
		{
			get
			{
				return baseURL;
			}
			set
			{
				baseURL = value;
			}
		}

		static string user = "admin";

		public static string User
		{
			get
			{
				return user;
			}
			set
			{
				user = value;
			}
		}

		static string password = "admin";

		public static string Password
		{
			get
			{
				return password;
			}
			set
			{
				password = value;
			}
		}
	}
}

