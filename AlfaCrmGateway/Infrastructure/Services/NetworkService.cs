using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TRIINKOM.Infrastructure;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories;
using Newtonsoft.Json.Linq;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Utility;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities;
using ERIP.Sites.AlfaCrmGateway.Models;
using Newtonsoft.Json;
using System.Web.Configuration;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Services
{

    public enum NetworkMethod
    {
        GET,
        POST
    }

    public class NetworkService
    {
        private static readonly string UrlApi = "https://api.express-pay.by/";
        private static readonly string UrlSandboxApi = "https://sandbox-api.express-pay.by/";

        public static readonly Dictionary<NetworkMethod, string> Methods;

        static NetworkService()
        {

            Methods = new Dictionary<NetworkMethod, string>();

            Methods.Add(NetworkMethod.GET, "GET");
            Methods.Add(NetworkMethod.POST, "POST");
        }

        public static async Task<string> Authorization(IAuthAlfaCRM authAlfaCRM)
        {
            string url = authAlfaCRM.HostName + "/v2api/auth/login";

            string data = QueryGeneraterUtility.GetAuthAlfaCRMData(authAlfaCRM.Email, authAlfaCRM.ApiKey);

            int requestId = LogService.QueryLog.WriteRequest(data, (int)QueryType.Autorization, "Авторизация в системе AlfaCRM", authAlfaCRM.ServiceId);

            try
            {
                var responseString = await Send(url: url, //"https://demo.s20.online/v2api/auth/login",
                                                data: data,//"{\"email\": \""+ authAlfaCRM.Email + "\" , \"api_key\": \"" + authAlfaCRM.ApiKey + "\" }",
                                                method: NetworkMethod.POST);
                LogService.QueryLog.WriteResponse(responseString, (int)QueryType.Autorization, "Авторизация в системе AlfaCRM", authAlfaCRM.ServiceId, requestId);

                var json = JObject.Parse(responseString);

                var tokenJson = json.GetValue("token");

                return tokenJson.ToString().Trim('{', '}');
            }
            catch (Exception e)
            {
                LogService.QueryLog.WriteResponse(e.Message, (int)QueryType.Autorization, "Ошибка авторизация в системе AlfaCRM", authAlfaCRM.ServiceId, requestId);
                throw e;
            }


        }

        public static async Task<string> Send(string url, string data = null, NetworkMethod method = NetworkMethod.POST)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = Methods.First(x => x.Key == method).Value;

            //data = "{\"email\": \"demo@alfacrm.pro\" , \"api_key\": \"d0b98631-c559-11e8-b088-0cc47ae3c526\" }";

            request.ContentType = "application/json";
            request.Accept = "application/json";

            request.Headers["X-My-Custom-Header"] = "the-value";

            //request.ContentLength = byteArray.Length;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {

                streamWriter.Write(data);
                streamWriter.Flush();
            }

            var httpResponse = await request.GetResponseAsync();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }

        public static async Task<string> SendAlfaCRM(string url, string token, string data, NetworkMethod method)
        {
            //string url = auth.HostName + "/v2api/1/customer/index";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = Methods.First(x => x.Key == method).Value;


            request.ContentType = "application/json";
            request.Accept = "application/json";

            request.Headers["X-ALFACRM-TOKEN"] = token;

            //request.ContentLength = byteArray.Length;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {

                streamWriter.Write(data);
                streamWriter.Flush();
            }

            var httpResponse = await request.GetResponseAsync();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }

        public static async Task<string> Invoicing(InvoiceModel model, int serviceId)
        {
            var json = JsonConvert.SerializeObject(model);

            int requestId = LogService.QueryLog.WriteRequest(json.ToString(), (int)QueryType.Invoicing, "Выставление счета в ЕРИП (ExpressPay)", serviceId);

            bool isTest = bool.Parse(WebConfigurationManager.AppSettings["TestMode"]);

            string url = string.Empty;

            url = isTest ? UrlSandboxApi : UrlApi;

            url += "v1/invoices?token=" + model.Token;
            try
            {

                string response = await Send(url, json, NetworkMethod.POST);

                LogService.QueryLog.WriteResponse(response, (int)QueryType.Invoicing, "Выставление счета в ЕРИП (ExpressPay)", serviceId, requestId);

                return response;
            }
            catch (Exception e)
            {
                LogService.QueryLog.WriteResponse(e.Message, (int)QueryType.Invoicing, "Ошибка выставления счета в ЕРИП (ExpressPay)", serviceId, requestId);
                throw e;
            }
        }



        public static async Task<string> GetCustomes(string customer_id, IAuthAlfaCRM auth, string token)
        {
            string url = auth.HostName + "/v2api/" + auth.Branch + "/customer/index";

            var data = new
            {
                id = customer_id,
                page = 0
            };

            string json = JsonConvert.SerializeObject(data);

            string response = await SendAlfaCRM(url, token, json, NetworkMethod.POST);

            return response;
        }

        public static async Task<String> UpdateCustomer(string id, string token, decimal balance, IAuthAlfaCRM auth)
        {
            string url = auth.HostName + "/v2api/1/customer/update?id=" + id;

            var data = new
            {
                balance = balance.ToString().Replace(',', '.')
            };

            string json = JsonConvert.SerializeObject(data);

            string response = await SendAlfaCRM(url, token, json, NetworkMethod.POST);

            return response;
        }
    }
}
