using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LollyASPWebForms.Startup))]
namespace LollyASPWebForms
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
