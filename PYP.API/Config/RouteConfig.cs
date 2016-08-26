using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Http;
using System.Web.Http.Routing.Constraints;


namespace PYP.API.Config
{
    public class RouteConfig
    {
        public static void RegisterRoutes(HttpConfiguration config)
        {
            // Convention-based routing.
            config.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{key}",
               defaults: new { key = RouteParameter.Optional },
               constraints: new { key = new IntRouteConstraint() }
                );
        }
    }
}
