using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Totaltech.Core.Data.Models.Google;

namespace Totaltech.Core.Helpers.Google
{
	public class GooglePlacesHelper
	{
		const string strAddressSearchUrl = "http://maps.googleapis.com/maps/api/geocode/json?latlng=";

		const string strAutoCompleteGoogleApi = "https://maps.googleapis.com/maps/api/place/autocomplete/json?components=country:mx&input=";
		//browser key for place webservice
		const string strGoogleApiKey = "AIzaSyAq8zllTE4kckhNrN1jQTL33u1ZIPYxBX0";
		const string strGeoCodingUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=";
		const string strDirectionsUrl = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&key=AIzaSyD0tYUxQImWAqrI7HEjWQghJSsTcvN8fUA&language=es-MX&mode=driving";

		public static async Task<GeoCodeJSONClass> GetAddress(double latitud, double longitud)
		{

			var addressResults = await fnDownloadString(strAddressSearchUrl + latitud + "," + longitud + "&sensor=true");
			if (addressResults == "Exception")
			{
				return null;
			}

			return JsonConvert.DeserializeObject<GeoCodeJSONClass>(addressResults);
		}

		public static async Task<List<string>> GetPlaces(string str)
		{
			var list = new List<string>();

			var autoCompleteOptions = await fnDownloadString(strAutoCompleteGoogleApi + str + "&key=" + strGoogleApiKey);
			if (autoCompleteOptions == "Exception")
			{
				return list;
			}
			var objMapClass = JsonConvert.DeserializeObject<GoogleMapPlaceClass>(autoCompleteOptions);
			foreach (var item in objMapClass.predictions)
			{
				list.Add(item.description);
			}
			return list;
		}

		public static async Task<GeoCodeJSONClass> GetLocation(string str)
		{
			var list = new List<string>();

			var strResult = await fnDownloadString(strGeoCodingUrl + str);
			if (strResult == "Exception")
			{
				return null;
			}
			return JsonConvert.DeserializeObject<GeoCodeJSONClass>(strResult);
		}

		public static async Task<GoogleDirectionsModel> GetDirections(string start, string end)
		{
			var url = string.Format(strDirectionsUrl, start, end);
			var strResult = await fnDownloadString(url);
			if (strResult == "Exception")
			{
				return null;
			}
			return JsonConvert.DeserializeObject<GoogleDirectionsModel>(strResult);
		}


		static async Task<string> fnDownloadString(string strUri)
		{
			var client = new HttpClient();
			string strResultData;
			try
			{
				strResultData = await client.GetStringAsync(new Uri(strUri));
				//System.Diagnostics.Debug.WriteLine(strResultData);
			}
			catch
			{
				strResultData = "Exception";
			}
			finally
			{
				client.Dispose();
				client = null;
			}

			return strResultData;
		}

	}
}
