using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BanHang_Admin.Startup))]
namespace BanHang_Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
