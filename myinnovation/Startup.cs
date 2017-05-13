using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(myinnovation.Startup))]
namespace myinnovation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
