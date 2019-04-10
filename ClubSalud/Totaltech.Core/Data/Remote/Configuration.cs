namespace Totaltech.Core.Data.Services
{
	public class Configuration
	{
        //http://192.168.1.101/WebApiClubSegurov1_0_1/
        //http://www.totalcase.com.mx/WebApiClubSeguroDEMO4/
        static string baseURL = "http://192.168.1.101/WebApiClubSegurov1_0_1/";

        public const string BASE_WEBAPI_URL = "WebApiClubSegurov1_0_1";
		public const string IMG_URL = "http://192.168.1.101/" + BASE_WEBAPI_URL + "/api/Spartan_File/Files/";

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

