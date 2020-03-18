using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace ASP.NETDesktop.Web {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            config.MapHttpAttributeRoutes();
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Routes.MapHttpRoute(
                name: "DefaultV1Api",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {
                    id = System.Web.Http.RouteParameter.Optional
                }
            );
        }
    }
}
