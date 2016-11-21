using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TechClub.Startup))]
namespace TechClub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
