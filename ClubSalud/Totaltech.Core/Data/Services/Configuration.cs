using System;
namespace Totaltech.Core.Data.Services
{
	public class Configuration
	{

		static string baseURL = "http://www.totalcase.com.mx/WebApiVREEKAVA6/";

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

