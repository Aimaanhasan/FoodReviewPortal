using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodReview.Startup))]
namespace FoodReview
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
