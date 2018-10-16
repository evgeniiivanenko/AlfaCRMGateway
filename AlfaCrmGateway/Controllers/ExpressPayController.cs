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

namespace ERIP.Sites.AlfaCrmGateway.Controllers
{
    public class ExpressPayController : ControllerBase
    {
        public ExpressPayController(IAppController appController)
            : base(appController)
        {

        }

        public string Index()
        {
            return "Hello";
        }


        [HttpPost]
        public async Task<string> Notification(string id)
        {
            string token = id;
            var stream = Request.InputStream;

            if (token == null)
            {
                throw new ArgumentException("Токен не может быт пустым","token");
            }

            var authAlfaCRMRepository = AppController.Get<IAuthAlfaCRMRepository>();

            var authAlfaCRM = authAlfaCRMRepository.GetByTokenExpressPay(token);

            if(authAlfaCRM == null)
            {
                throw new ArgumentException("Полученный токен не найден!","token");
            }

            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            text = text.Substring(text.IndexOf("{"));

            LogService.QueryLog.WriteRequest(text, (int)QueryType.Notification, "Уведомление о платеже от ExpressPay", authAlfaCRM.ServiceId);

            //LogService.Current.Debug("Уведомление о платеже от ExpressPay : " + text);

            var json = JObject.Parse(text);

            var cmdType = FormatUtility.ConvertToString(json, "CmdType");

            // Если Тип уведомления не 3 то на него можно не реагировать и вернуть серверу статус 200
            if (cmdType != "3")
            {
                return "200,OK";
            }

            var status = FormatUtility.ConvertToString(json, "Status");

            // Нужно реагированить только на оплату
            switch(status)
            {
                case "1": //Ожидает оплату
                case "2": //Просрочен
                case "5": //Отменен
                    return "200,OK";
            }

            var amount = FormatUtility.Convert(json, "Amount");

            // Авторизация в alfaCRM

            var tokenAlfaCRM = await NetworkService.Authorization(authAlfaCRM);

            // Определить, что является лицевым счетом!
            
            var customer_id = FormatUtility.ConvertToString(json, "AccountNo");

            // Получение баланса плательщика

            string response = await NetworkService.GetCustomes(customer_id, authAlfaCRM, tokenAlfaCRM);

            // Отправление изменений

            json = JObject.Parse(response);

            var items = JObject.Parse(json.GetValue("items").ToString().Trim('[',']','\r','\n'));

            var balance = FormatUtility.Convert(items, "balance");

            balance += amount;

            string responseUpdate = await NetworkService.UpdateCustomer(customer_id, tokenAlfaCRM, balance, authAlfaCRM);


            return "200,OK";
        }

    }
}
