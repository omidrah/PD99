using DataLayer.Context;
using StructureMap.Web.Pipeline;
using System;
using System.Data.Entity;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.DependencyResolution;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            setDbInitializer();
            //Set current Controller factory as StructureMapControllerFactory
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver = IoC.Initialize().GetInstance<Microsoft.AspNet.SignalR.IDependencyResolver>();
                //IoC.Container.GetInstance<Microsoft.AspNet.SignalR.IDependencyResolver>();
        }
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            HttpContextLifecycle.DisposeAndClearAll();
        }

        public class StructureMapControllerFactory : DefaultControllerFactory
        {
            protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                if (controllerType == null)
                {
                    throw new HttpException(404, $"Resource not found : {requestContext.HttpContext.Request.Path}");
                }
                return IoC.Initialize().GetInstance(controllerType) as Controller;
            }
        }

        private static void setDbInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            IoC.Initialize().GetInstance<IUnitOfWork>().ForceDatabaseInitialize();
        }

        protected void Application_BeginRequest(object Sender,EventArgs e)
        {
            var pc = new PersianCulture();
            Thread.CurrentThread.CurrentCulture = pc;
            Thread.CurrentThread.CurrentUICulture = pc;
        }

        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
    }
}
