using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;

namespace CrystalBallpro.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)

        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        internal static void Register(System.Web.Http.HttpConfiguration configuration)
        {
            throw new NotImplementedException();
        }
    }
}