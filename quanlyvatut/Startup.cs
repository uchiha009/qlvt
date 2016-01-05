using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(quanlyvatut.Startup))]
namespace quanlyvatut
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
