using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Routing;
using UserData.BusinessLogic.StructureMap;
using UserData.Web;
using UserData.Web.App_Start;

[assembly: System.Web.PreApplicationStartMethod(typeof(AppStart), "PreStart")]

namespace UserData.Web
{

    public static class AppStart
    {
        public static void PreStart()
        {
           RouteTable.Routes.Ignore("elmah.axd");
            RouteTable.Routes.MapHttpRoute(
                name: "#UserDataApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
              );
            StructureMapDependencyResolver.ConfigureDependencies();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Services.Replace(typeof(IHttpControllerActivator), new ServiceActivator(config));

        }
    }
}
