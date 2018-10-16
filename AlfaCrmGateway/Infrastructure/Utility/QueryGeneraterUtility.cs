using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Utility
{
    public class QueryGeneraterUtility
    {
        #region Private Query

        private const string AuthQuery = "{\"email\": \"{0}\" , \"api_key\": \"{1}\" }";

        #endregion

        public static string GetAuthAlfaCRMData(string email, string apiKey)
        {
            // Ошибка! Строка имеет не верный формат
            //return string.Format(AuthQuery, email,apiKey); 

            return "{\"email\": \"" + email + "\" , \"api_key\": \"" + apiKey + "\" }";
        }
    }
}