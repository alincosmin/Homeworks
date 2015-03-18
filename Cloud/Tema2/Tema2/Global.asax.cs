using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;
using Tema2.Models;

namespace Tema2
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
                ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(
                GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            HttpRuntime.Cache["books"] = new List<Book>
            {
                new Book
                {
                    Authors = new[] {"Cineva"},
                    BookName = "Ceva",
                    Id = 1
                },
                new Book
                {
                    Authors = new[] {"AutA", "AutB"},
                    BookName = "Something",
                    Id = 2
                }
            };
        }
    }
}
