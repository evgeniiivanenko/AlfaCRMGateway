using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Services;
using TRIINKOM.Infrastructure;
using TRIINKOM.Infrastructure.Extensions;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IAppController _appController;

        public NinjectControllerFactory(IAppController appController)
        {
            appController.ThrowIfArgumentNull("appController");
            _appController = appController;

            //AddBindings();
        }

        protected override IController GetControllerInstance(
            RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;

            return _appController.Get<IController>(controllerType);
        }
    }

    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // TODO: HACK: для CustomAuthorizeAttribute (переделать на IoC)
            /*var appController = (IAppController)HttpRuntime.Cache["AppController"];

            var rep = appController.Get<IUserRepository>();
            var acl = appController.Get<IAccessControlService>();
            var authService = new AuthService(rep, acl);

            if (authService.CurrentUser == null)
                authService.Logout();*/
        }
    }


    /*
    public class MyAuthorizeAttribute : AuthorizeAttribute { }

    
    public class MyAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IAuthService _authService;


        public MyAuthorizeFilter(IAuthService authService)
        {
            _authService = authService;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            _authService.Logout();
            if (_authService.CurrentUser == null)
            {
                _authService.Logout();
                //filterContext.
            }
            
            //if (!validUser)
            //{
            //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "AccessDenied" }, { "controller", "Error" } });
            //}
        }
    }
     */
}