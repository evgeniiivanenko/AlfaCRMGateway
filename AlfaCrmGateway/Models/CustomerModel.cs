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

namespace ERIP.Sites.AlfaCrmGateway.Models
{
    // Модель Customer из AlfaCRM
    public class CustomerModel
    {
        #region Public Properties

        public string id { get; set; }
        public string custom_contract_no { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string balance { get; set; }
        public string name { get; set; }

        #endregion

        #region Ctor

        public CustomerModel(string item) : this(JObject.Parse(item))
        {

        }

        public CustomerModel(JObject json)
        {
            id = FormatUtility.ConvertToString(json, "id");
            custom_contract_no = FormatUtility.ConvertToString(json, "custom_contract_no");
            //email получаются списком. Брать только первый из списка
            email = GetFirstValue(json, "email");
            //phone получаются списком. Брать только первый из списка
            phone = GetFirstValue(json, "phone");
            balance = FormatUtility.ConvertToString(json, "balance");
            name = FormatUtility.ConvertToString(json, "name");

            if(!string.IsNullOrEmpty(phone))
            {
                phone = phone.Replace("(","");
                phone = phone.Replace(")","");
                phone = phone.Replace("-","");
            }
        }

        #endregion

        #region Private Methods

        private string GetFirstValue(JObject json, string propertyName)
        {
            var token = json.GetValue(propertyName).First;

            if(token == null)
            {
                return string.Empty;
            }

            return token.ToString();
        }

        #endregion
    }
}