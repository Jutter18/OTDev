using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MealFridge.Startup))]
namespace MealFridge
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
