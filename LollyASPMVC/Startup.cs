using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LollyASPMVC.Startup))]
namespace LollyASPMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
