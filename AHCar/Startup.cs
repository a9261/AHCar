using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AHCar.Startup))]
namespace AHCar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
