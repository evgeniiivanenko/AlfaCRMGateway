using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Utility
{
    internal enum QueryType
    {
        Autorization,
        GetCustomers,
        UpdateCustomer,
        Invoicing,
        Notification,
        Webhook
    }
}