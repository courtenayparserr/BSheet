using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SaasTrack.Startup))]
namespace SaasTrack
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
