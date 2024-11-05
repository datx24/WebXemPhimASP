using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLNV.Startup))]
namespace QLNV
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
