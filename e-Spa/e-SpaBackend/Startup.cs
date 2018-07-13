using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(e_SpaBackend.Startup))]

namespace e_SpaBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}