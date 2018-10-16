using System.Web.Optimization;
using ERIP.Sites.AlfaCrmGateway.Controllers;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper;
using ERIP.Sites.AlfaCrmGateway.Infrastructure;
using ERIP.Core.Infrastructure.Extensions;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Services;
using log4net;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TRIINKOM.Infrastructure;

namespace ERIP.Sites.AlfaCrmGateway
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private IAppController _appController;

        public MvcApplication()
        {
            BeginRequest += Application_BeginRequest;
        }

        protected void Application_Start()
        {
            //Kendo
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");

            //Decimal problem resolve
            //            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            //ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());

            // Ninject - IoC initialization
            _appController = new ApplicationController();
            _appController.RegisterConstant(_appController);

            // register DalDapperLib library
            DalDapperLib.Initialize(_appController, ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);

            // log4net
            var configFile = new FileInfo(Server.MapPath("~/Web.config"));
            log4net.Config.XmlConfigurator.Configure(configFile);
            LogService.Current = new Log4NetLogger(LogManager.GetLogger(typeof(MvcApplication)));
            ModelBinders.Binders.DefaultBinder = new TrimModelBinder();
            LogService.QueryLog = new QueryWriteService(_appController);
            // Controller Factory initialization
            ControllerBuilder.Current.SetControllerFactory(
                new NinjectControllerFactory(_appController));

            _appController.RegisterConstant(LogService.Current);

            ClientDataTypeModelValidatorProvider.ResourceClassKey = "MyResources";
            DefaultModelBinder.ResourceClassKey = "MyResources";
        }

        public void Application_Error(Object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            string error = "IP - " + Request.UserHostAddress + "; " + "USER AGENT - " + Request.UserAgent + "; MESSAGE - " + exception;
            LogService.Current.Error(error);
            Server.ClearError();

            var routeData = new RouteData();
            routeData.Values.Add("controller", "ErrorPage");
            routeData.Values.Add("action", "Error");

            if (exception.GetType() == typeof(HttpException))
            {
                routeData.Values.Add("statusCode", ((HttpException)exception).GetHttpCode());
            }
            else
            {
                routeData.Values.Add("statusCode", 500);
            }

            Response.TrySkipIisCustomErrors = true;
            IController controller = new ErrorPageController(_appController);
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            Response.End();
        }

        public void Application_BeginRequest(object sender, System.EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
        }
    }
}