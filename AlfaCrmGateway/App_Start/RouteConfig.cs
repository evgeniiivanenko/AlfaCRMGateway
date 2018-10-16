using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERIP.Sites.AlfaCrmGateway
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ExpressPayAPI",
                url: "v1/{controller}/{action}/{id}",
                defaults: new {controller = "ExpressPay", action = "Notification", id = UrlParameter.Optional}
                );

            routes.MapRoute(
                name: "API",
                url : "v1/{controller}/{action}"
                );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}