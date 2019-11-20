using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kompanija.Startup))]
namespace Kompanija
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
