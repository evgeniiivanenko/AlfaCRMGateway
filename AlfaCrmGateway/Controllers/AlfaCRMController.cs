//using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Data;
//using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.DataObjects.Tables;
//using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities.DataObjects;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories;
//using ERIP.Sites.AlfaCrmGateway.Infrastructure.Extensions;
using ERIP.Sites.AlfaCrmGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using TRIINKOM.Dal.Infrastructure;
using TRIINKOM.Dal.Infrastructure.Utility;
using TRIINKOM.Infrastructure;
using TRIINKOM.Infrastructure.Extensions;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Services;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Utility;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.DTO.Entities;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ERIP.Sites.AlfaCrmGateway.Controllers
{
    public class AlfaCRMController : ControllerBase
    {
        public AlfaCRMController(IAppController appController)
            : base(appController)
        {

        }

        
        public async Task<string> Webhook()
        {
            var token = "qwerty123456";

            var stream = Request.InputStream;

            var form = Request.Form;

            var items = HttpContext.Items;

            foreach(var item in items.Keys)
            {
                LogService.Current.Debug("ITEMS : KEY - " + item.ToString() + "; " + items[item]);
            }

            LogService.Current.Debug("FORM : " + form.ToString());

            var param = Request.Params;
            
            foreach(var key in param.AllKeys)
            {

                string debug = "PARAMS : " + key + "; VALUES : ";

                foreach(var val in param.GetValues(key))
                {
                    debug += val + "; ";
                }

                LogService.Current.Debug(debug.Trim());

            }

            var query = Request.QueryString;

            foreach(var q in query.Keys)
            {
                LogService.Current.Debug("QUERYSTRING : KEY - " + q + "; VALUES - " + query[q.ToString()]);
            }

            LogService.Current.Debug("PARAMS : " + param.ToString());

            // Проверка токена
            var authAlfaCRMRepository = AppController.Get<IAuthAlfaCRMRepository>();

            var authAlfaCRM = authAlfaCRMRepository.GetByToken(token);

            if (authAlfaCRM == null)
            {
                throw new ArgumentException("Даный токен не найден", "token");
            }

            //LogService.Current.Debug("STREAM : " + stream);

            StreamReader reader = new StreamReader(stream);
            
            string text = reader.ReadToEnd();

            LogService.Current.Debug("StreamReader : " + text);

            LogService.QueryLog.WriteRequest(text, (int)QueryType.Webhook, "Webhook от AlfaCRM", authAlfaCRM.ServiceId);

            //LogService.Current.Debug("Полученный webhook от alfaCRM(метод POST) : " + text);

            var json = JObject.Parse(text);

            var eventName = FormatUtility.ConvertToString(json, "event");

            switch (eventName)
            {
                case "update":
                case "delete":
                    return "200";
            }

            var entity_id = FormatUtility.ConvertToInteger(json, "entity_id");

            var fields_new = JObject.Parse((json.GetValue("fields_new").ToString()));

            var customer_id = FormatUtility.ConvertToInteger(fields_new, "customer_id");

            //var branch_id = json.GetValue("branch_id").ToString().Trim('{', '}');

            /*var tokenAlfaCRM = await NetworkService.Authorization(authAlfaCRM);

            if(tokenAlfaCRM == null)
            {
                throw new ArgumentException("Ошибка получения токена от AlfaCRM", "tokenAlfaCRM");
            }

            var sessionAlfaCRMRepository = AppController.Get<ISessionAlfaCRMRepository>();

            int session_id = sessionAlfaCRMRepository.Save(tokenAlfaCRM, authAlfaCRM.Id);*/

            // формирование запроса на API ExpressPay
            // нужно получить токен для выставления счета

            var serviceIntegrationRepository = AppController.Get<IServiceIntegrationRepository>();

            string tokenAPI = serviceIntegrationRepository.GetTokenByServiceId(authAlfaCRM.ServiceId); //"a75b74cbcfe446509e8ee874f421bd64";

            var model = new InvoiceModel()
            {
                Token = tokenAPI,
                AccountNo = entity_id.ToString(),//"100",
                Amount = FormatUtility.Convert(fields_new, "income"),
                Currency = 933,
                Info = FormatUtility.ConvertToString(fields_new, "note")
            };

            string response = "{ \"InvoiceNo\" : 100}"; //= await NetworkService.Invoicing(model, authAlfaCRM.ServiceId);

            var responseJson = JObject.Parse(response);

            JToken error;

            if (!responseJson.TryGetValue("Error", out error))
            {
                var invoiceNo = FormatUtility.ConvertToInteger(responseJson, "InvoiceNo");


                LogService.Current.Debug("InvoiceNo : " + invoiceNo);

               // var alfaCRMEntityRepository = AppController.Get<IAlfaCRMEntityRepository>();

                //var id = alfaCRMEntityRepository.Save(entity_id, customer_id, invoiceNo);
            }

            return "200,OK";
        }
    }
}
