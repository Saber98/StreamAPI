using Microsoft.Owin;
using Owin;
using StreamApi;

[assembly: OwinStartup(typeof(Startup))]

namespace StreamApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}