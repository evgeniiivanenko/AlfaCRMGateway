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
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ERIP.Sites.AlfaCrmGateway.Controllers
{

    public class HomeController : ControllerBase
    {
        public string Index()
        {
            return "Hello";
        }

        public async Task<string> TestAutorization()
        {

            var auth = AppController.Get<IAuthAlfaCRM>();
            auth.ApiKey = "beb65996-d2cb-11e8-bb8f-0cc47ae3c526";
            auth.Email = "evgenij.ivanenko@hb.by";
            auth.HostName = "https://legendasport.s20.online";
            auth.Branch = "2";

            auth.Id = 2;

            string token = await NetworkService.Authorization(auth);

            string url = auth.HostName + "/v2api/"+ auth.Branch + "/customer/index";

            var data = new
            {
                page = 0
            };

            string json = JsonConvert.SerializeObject(data);

            int id_req = LogService.QueryLog.WriteRequest(json, (int) QueryType.GetCustomers, "Тестовое получение плательщиков", 4217);

            try
            {
                string response = await NetworkService.SendAlfaCRM(url, token, json, NetworkMethod.POST);

                var jobj = JObject.Parse(response);

                var items = jobj.GetValue("items");

                items.ForEach(x => {
                    var str = x.ToString();

                    var customer = JObject.Parse(str);

                    var cust_model = new CustomerModel(customer);
                });

                LogService.QueryLog.WriteResponse(response, (int)QueryType.GetCustomers, "Тестовое получение плательщиков", 4217, id_req);
            }catch(Exception e)
            {
                LogService.QueryLog.WriteResponse(e.Message, (int)QueryType.GetCustomers, "Ошибка тестового получения плательщиков", 4217, id_req);
                throw e;
            }
            return "200,OK";
        }

        public HomeController(IAppController appController)
            :base(appController)
        {
        }
    }
}
