using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WEBPJ.Startup))]
namespace WEBPJ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
