using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WEBDEVPROJECT.Startup))]
namespace WEBDEVPROJECT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
