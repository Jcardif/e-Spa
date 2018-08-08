using System.Web.Http;
using Owin;

namespace e_SpaBackend
{
    public partial class Startup
    {
        private void ConfigureWeb(IAppBuilder app)
        {
            HttpConfiguration config=new HttpConfiguration();

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }
    }
}