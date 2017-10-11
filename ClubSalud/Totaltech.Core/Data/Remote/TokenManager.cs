using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Totaltech.Core.Data.Services
{

	public class AuthenticationToken
	{
		public string access_token { get; set; }

		public string token_type { get; set; }

		//public DateTime CreatTime { get; set; }

		int _expires_in;
		public int expires_in
		{
			get
			{
				return _expires_in;
			}

			set
			{
				_expires_in = value;
			}
		}

		public DateTime? ExpireTime { get; set; }

		public bool IsExpired
		{
			get
			{
				if (ExpireTime != null && DateTime.Now < ExpireTime)
				{
					return false;
				}

				return true;
			}
		}
	}

	public class TokenManager
	{
		public AuthenticationToken AuthenticationToken;

		public async Task<string> GetToken()
		{
			try
			{
				HttpClient client = new HttpClient();

				if (AuthenticationToken == null)
				{
					var postData = new List<KeyValuePair<string, string>>();
					postData.Add(new KeyValuePair<string, string>("grant_type", "password"));
					postData.Add(new KeyValuePair<string, string>("username", Configuration.User));
					postData.Add(new KeyValuePair<string, string>("password", Configuration.Password));
					HttpContent content = new FormUrlEncodedContent(postData);

					var response = await client.PostAsync(Configuration.BaseURL + "oauth/token", content);
					var result = await response.Content.ReadAsStringAsync();
					AuthenticationToken = JsonConvert.DeserializeObject<AuthenticationToken>(result);
					AuthenticationToken.ExpireTime = DateTime.Now.Add(TimeSpan.FromSeconds(AuthenticationToken.expires_in));
					return AuthenticationToken.access_token;
				}
				else {
					if (AuthenticationToken.IsExpired)
					{
						//HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, Configuration.BaseURL + "api/Token/RefeshTokenMain");
						//requestMessage.Headers.Add("Authorization", AuthenticationToken.token_type + " " + AuthenticationToken.access_token);
						//var response = await client.SendAsync(requestMessage);
						AuthenticationToken = null;
						return await GetToken();
					}
					else {
						return AuthenticationToken.access_token;
					}
				}

				return null;
			}
			catch (Exception ex)
			{
				return "";
			}
		}
	}
}

