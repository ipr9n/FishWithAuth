using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FishWithAuth.Startup))]
namespace FishWithAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
