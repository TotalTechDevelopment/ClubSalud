using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Totaltech.Core.Data.Models;
using Totaltech.Core.Data.Models.Conketa;
using HtmlAgilityPack;

namespace Totaltech.Core.Data.Services
{
    public class Services
    {

        static Services _instance;
        public static Services Instance
        {
            get
            {
                if (_instance == null)
                    new Services();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        TokenManager TokenManager;

        public Services()
        {
            TokenManager = new TokenManager();
        }

        enum CallType
        {
            Get,
            GetList,
            GetAll,
            ListaSelAll,
            Post,
            Put,
            Delete,
            Insert,
            PostCharge,
            Push
        }

        object Lock = new object();


        async Task<HttpClient> GetHttpClient(string table, CallType callType, int start = -1, int rows = -1, string where = null, string order = null, int timpeout = 30)
        {

            HttpClient httpClient = null;
            try
            {
                httpClient = new HttpClient();
            }
            catch
            {
                return null;
            }
            httpClient.Timeout = TimeSpan.FromSeconds(timpeout);
            httpClient.DefaultRequestHeaders.ExpectContinue = true;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("UTF-8"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("UTF-8"));
            string token = await TokenManager.GetToken();

            System.Diagnostics.Debug.WriteLine("TOKEN: " + token);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            string url = Configuration.BaseURL + "api/" + table + "/";
            if (callType == CallType.Get)
            {
                url += "Get";
            }

            if (callType == CallType.GetList)
            {
                url += "GetList";
            }

            if (callType == CallType.GetAll)
            {
                url += "GetAll";
            }
            if (callType == CallType.ListaSelAll)
            {
                url += "ListaSelAll";
            }
            if (callType == CallType.Post || callType == CallType.PostCharge)
            {
                url += "Post";
            }
            if (callType == CallType.Put)
            {
                url += "Put";
            }
            if (callType == CallType.Delete)
            {
                url += "Delete";
            }
            if (callType == CallType.Insert)
            {
                url += "Insert";
            }

            if (callType == CallType.Get || callType == CallType.GetAll || callType == CallType.ListaSelAll || callType == CallType.GetList)
            {
                url += start == -1 ? "" : "?startRowIndex=" + start;
                url += rows == -1 ? "" : "&maximumRows=" + rows;

                if (callType == CallType.GetList && start == -1 && rows == -1)
                    url += (string.IsNullOrEmpty(where) ? "" : "?" + where);
                else if (callType == CallType.Get && start == -1 && rows == -1)
                    url += (string.IsNullOrEmpty(where) ? "" : "?" + where);
                else
                    url += (string.IsNullOrEmpty(where) ? "" : "&Where=" + where);
                url += (string.IsNullOrEmpty(order) ? "" : "&Order=" + order);
            }

            if (callType == CallType.Delete || callType == CallType.Put)
            {
                url += (string.IsNullOrEmpty(where) ? "" : "?Id=" + where);
            }

            if (callType == CallType.PostCharge)
                url = table;

            if (callType == CallType.Push)
                url = table;

            httpClient.BaseAddress = new Uri(url);

            return httpClient;
        }


        async Task<HttpClient> GetHttpClientPostQuery()
        {

            HttpClient httpClient = null;
            try
            {
                httpClient = new HttpClient();
            }
            catch
            {
                return null;
            }
            httpClient.Timeout = TimeSpan.FromSeconds(60);
            httpClient.DefaultRequestHeaders.ExpectContinue = true;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("UTF-8"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("UTF-8"));
            string token = await TokenManager.GetToken();

            System.Diagnostics.Debug.WriteLine("TOKEN: " + token);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            string url = Configuration.BaseURL + "api/Spartan_Query/Post/";

            httpClient.BaseAddress = new Uri(url);

            return httpClient;
        }

        public async Task<T> ListaSelAll<T>(string table, int start = 1, int rows = 1, string where = "", string order = "")
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("get " + table + " where " + where);
                var client = await GetHttpClient(table, CallType.ListaSelAll, start, rows, where, order);
                var response = await client.GetAsync("");
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<T>(result.Trim());
                    return model;
                }
                return default(T);
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(string.Format("{0} {1}", ex.Message, ex.StackTrace));
                return default(T);
            }
        }


        public async Task<string> ExecutePost(QueryModel query)
        {
            try
            {
                var client = await GetHttpClientPostQuery();
                var json = JsonConvert.SerializeObject(query);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("", content);
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var value =  await response.Content.ReadAsStringAsync();

                    if (value != null &&  value != "null")
                        return value?.Trim()?.Replace("\"", "") ?? string.Empty;

                    return string.Empty;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        public async Task<int> PostObjectToTable<T>(object item, string table)
        {
            try
            {
                var client = await GetHttpClient(table, CallType.Post);
                var json = JsonConvert.SerializeObject((T)item);
                System.Diagnostics.Debug.WriteLine("post {0} \n to {1}", json, table);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("", content);
                var result = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("result: {0}", result);
                if (response.IsSuccessStatusCode)
                {

                    int id = 0;
                    if (int.TryParse(result.Replace("\"", ""), out id))
                    {
                        return id;
                    }
                    return -1;
                }
                return -1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<int> PutObjectToTable<T>(object item, string id_str, string table)
        {
            try
            {
                var client = await GetHttpClient(table, CallType.Put, -1, -1, id_str);
                var json = JsonConvert.SerializeObject((T)item, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync("", content);
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {

                    int id = 0;
                    if (int.TryParse(result.Replace("\"", ""), out id))
                    {
                        return id;
                    }
                    return -1;
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<bool> DeleteObject(string id_str, string table)
        {
            try
            {
                var client = await GetHttpClient(table, CallType.Delete, -1, -1, id_str);
                //HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.DeleteAsync("");
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    bool did = false;
                    if (bool.TryParse(result, out did))
                    {
                        return did;
                    }
                    return did;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<T> GetAllObjects<T>(string TABLE_NAME, int min = -1, int max = -1, string where = null, string order = null)
        {

            try
            {
                var client = await GetHttpClient(TABLE_NAME, CallType.GetAll, min, max, where, order);
                var response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<T>(result.Trim());
                    return model;
                }
                return default(T);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }


        public async Task<T> GetObject<T>(string TABLE_NAME, int min = -1, int max = -1, string where = null, string order = null)
        {

            try
            {
                var client = await GetHttpClient(TABLE_NAME, CallType.Get, min, max, where, order);
                var response = await client.GetAsync("");
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {

                    var model = JsonConvert.DeserializeObject<T>(result.Trim());
                    return model;
                }
                return default(T);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }


        public async Task<List<TTArchivo>> GetFotos(List<string> IDs, bool getPhysicalFile = false)
        {
            //try
            //{
            //	var response = await GetObject<List<TTArchivo>>(TTArchivo.TABLE_NAME, -1, -1, string.Format("FolioId={0}&getPhysicalFile={1}", String.Join(",", IDs), getPhysicalFile), "");
            //	return response;
            //}
            //catch (Exception ex)
            //{
            //	System.Diagnostics.Debug.WriteLine(string.Format("{0}", ex.Message));
            //	return null;
            //}

            try
            {
                var client = await GetHttpClient(TTArchivo.TABLE_NAME, CallType.GetList, -1, -1, string.Format("FolioId={0}&getPhysicalFile={1}", String.Join(",", IDs), getPhysicalFile), "");
                var response = await client.GetAsync("");
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {

                    var model = JsonConvert.DeserializeObject<List<TTArchivo>>(result.Trim());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }


            return null;
        }

        public async Task<List<TTArchivo>> PostFotos(List<InputFile> fotos)
        {

            //if (foto.Count == 0)
            //	return new List<TTArchivo>();
            try
            {
                var client = await GetHttpClient(InputFile.TABLE_NAME, CallType.Insert, -1, -1, null, null, 9999);
                var json = JsonConvert.SerializeObject(fotos);
                //System.Diagnostics.Debug.WriteLine(json);
                //DependencyService.Get<ILogHelp>().SaveStringToFile(json, "POST_FOTO_" + DateTime.Now.ToString("hh_mm_ss"));
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("", content);


                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<TTArchivo>>(result);
                    return model;
                }
                return new List<TTArchivo>();
            }
            catch (Exception ex)
            {
                return new List<TTArchivo>();
            }
        }


        public async Task<ChargeResult> MakeCharge(string token, ServicioDeLavadoMaestro servicio, int idCliente = -1)
        {

            //PRIVATE KEY
            //OBC key_AibWmdq7ZkHU87imHz45GQ
            //SYMA key_9Phs2zwzFY8y5gLz7WP7Fw
            string conektaKey = "key_AibWmdq7ZkHU87imHz45GQ";

            Cliente cliente = null;

            var resClientes = await Instance.ListaSelAll<ClientePagingModel>(Cliente.TABLE_NAME, 0, 99999);
            if (resClientes != null && resClientes.RowCount > 0)
            {
                cliente = resClientes.Clientes.Where(x => x.Folio == idCliente).FirstOrDefault();
            }

            var info = ((int)servicio.Costo_del_Servicio == 150) ? "LAVADO_AUTO" : "LAVADO_CAMIONETA";

            var c = new ChargeSend();
            c.amount = (int)(servicio.Costo_del_Servicio * 100);
            c.card = token;
            c.currency = "MXN";
            c.reference_id = info;
            c.description = info;
            c.monthly_installments = null;


            var d = new Details();
            d.name = cliente.Nombre;
            d.phone = cliente.Telefono;
            d.email = cliente.Correo;

            var li = new LineItem();
            li.name = "LAVADO VREEK";
            li.description = "LAVADO VREEK " + info;
            li.unit_price = (int)(servicio.Costo_del_Servicio * 100);
            li.quantity = 1;
            li.sku = info;
            li.category = "LAVADO VREEK";

            d.line_items = new List<LineItem>();
            d.line_items.Add(li);

            var ba = new BillingAddress();
            ba.street1 = "Dr. Santos Sepúlveda 130";
            ba.street2 = "Los Doctores";
            ba.street3 = null;
            ba.city = "Monterrey";
            ba.state = "Nuevo Leon";
            ba.zip = "64710";
            ba.country = "Mexico";
            ba.company_name = null;
            ba.tax_id = null;
            ba.phone = cliente.Telefono;
            ba.email = cliente.Correo;

            d.billing_address = ba;

            var sh = new ShippingAddress();
            sh.street1 = "Dr. Santos Sepúlveda 130";
            sh.street2 = "Los Doctores";
            sh.street3 = null;
            sh.city = "Monterrey";
            sh.country = "Mexico";
            sh.state = "Nuevo Leon";
            sh.zip = "64710";

            var shipment = new Shipping();
            shipment.carrier = "n/a";
            shipment.service = "na";
            shipment.tracking_id = "na";
            shipment.price = 0000;
            shipment.address = sh;

            d.shipment = shipment;

            c.details = d;

            var result = await Instance.PostCharge<ChargeSend>(c, "http://www.totalcase.com.mx/WebApiConektaQC/api/Conekta/onDemand/?key=" + conektaKey);

            return result;
        }

        public async Task<bool> SendPushNotification(string message, string deviceToken, int dispositivo, string tipoApp = "2")
        {

            string url = "http://www.totalcase.com.mx/WebApiVREEKAVA4/api/Mensajes/Push?TokenDispositivo=" + deviceToken + "&Mensaje=" + message + "&TipoDispositivo=" + dispositivo + "&TipoApp=" + tipoApp;

            try
            {
                var client = await GetHttpClient(url, CallType.Push, -1, -1, null, null);
                var response = await client.GetAsync("");
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {


                    //if (dispositivo == 1)
                    //{
                    //	if (result.Contains("XXXX"))
                    //	{
                    //		return true;
                    //	}
                    //	else if (result.Contains("Error en el envío"))
                    //	{
                    //		return false;
                    //	}
                    //	else
                    //	{
                    //		return false;
                    //	}
                    //}
                    //else
                    //{
                    //	if (result.Contains("ZZZZ"))
                    //	{
                    //		return true;
                    //	}
                    //	else
                    //	{
                    //		return false;
                    //	}
                    //}

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }


        public async Task<ChargeResult> PostCharge<T>(object item, string table)
        {
            try
            {
                var client = await GetHttpClient(table, CallType.PostCharge);
                var json = JsonConvert.SerializeObject((T)item);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("", content);
                var result = await response.Content.ReadAsStringAsync();



                if (response.IsSuccessStatusCode)
                {

                    var res = JsonConvert.DeserializeObject<ChargeResult>(result);
                    return res;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> GetImage(int imageId)
        {
            try
            {
                var htmlURL = Configuration.IMG_URL + imageId;
                var imageHTML = await DownloadHTML(htmlURL);
                var imageName = await GetImageNameFromHTML(imageHTML, imageId);
                var imageURL = Configuration.IMG_URL + imageId + "/" + imageName;

                return imageURL;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return "";
        }

        private async Task<string> DownloadHTML(string url)
        {
            var result = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        result = content.ReadAsStringAsync().Result;
                    }
                }
            }

            return result;
        }

        private async Task<string> GetImageNameFromHTML(string html, int? imageId)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var htmlBody = htmlDoc.DocumentNode.ChildNodes.Elements("body").ToList();
            if (htmlBody == null)
            {
                return "";
            }
            var body = htmlBody[0].ChildNodes.ToList().Where(x => x.Name.Equals("pre")).FirstOrDefault();
            var links = body.ChildNodes.ToList().Where(x => x.Name.Equals("a"));

            foreach (var link in links)
            {
                if (link.OuterHtml.Contains(imageId.ToString()))
                {
                    System.Diagnostics.Debug.WriteLine(link.Name);

                    if (link.OuterHtml.Contains(".jpg"))
                    {
                        var fileName = getBetween(link.OuterHtml, "<a href=\"/" + Configuration.BASE_WEBAPI_URL + "/api/Spartan_File/Files/" + imageId + "/", ".jpg");
                        return fileName + ".jpg";
                    }

                    if (link.OuterHtml.Contains(".png"))
                    {
                        var fileName = getBetween(link.OuterHtml, "<a href=\"/" + Configuration.BASE_WEBAPI_URL + "/api/Spartan_File/Files/" + imageId + "/", ".png");
                        return fileName + ".png";
                    }

                    if (link.OuterHtml.Contains(".jpeg"))
                    {
                        var fileName = getBetween(link.OuterHtml, "<a href=\"/" + Configuration.BASE_WEBAPI_URL + "/api/Spartan_File/Files/" + imageId + "/", ".jpeg");
                        return fileName + ".jpeg";
                    }

                }
            }

            return "";
        }

        private string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
    }
}

