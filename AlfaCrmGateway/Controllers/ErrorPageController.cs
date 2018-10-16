using System;
using System.Web.Mvc;
using TRIINKOM.Infrastructure;

namespace ERIP.Sites.AlfaCrmGateway.Controllers
{
    public class ErrorPageController : ControllerBase
    {
        public string Error(int statusCode)
        {
            Response.StatusCode = statusCode;
            return statusCode + " Error";
            
        }

        public ErrorPageController(IAppController appController)
            : base(appController)
        {

        }
    }
}