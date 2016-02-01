using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Polagora.Startup))]
namespace Polagora
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
