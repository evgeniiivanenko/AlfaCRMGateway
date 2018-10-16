using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Utility
{
    public class FormatUtility
    {
        public static decimal Convert(JObject obj, string key)
        {
            var value = obj.GetValue(key).ToString().Trim('{', '}');

            value = value.Replace('.', ',');

            return decimal.Parse(value);

        }

        public static string ConvertToString(JObject obj, string key)
        {
            return obj.GetValue(key).ToString().Trim('{', '}');
        }

        public static int ConvertToInteger(JObject obj, string key)
        {
            var value = obj.GetValue(key).ToString().Trim('{', '}');

            int valueInt = int.Parse(value);

            return valueInt;
        }
    }
}