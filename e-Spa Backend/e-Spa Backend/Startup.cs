using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(e_Spa_Backend.Startup))]

namespace e_Spa_Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}