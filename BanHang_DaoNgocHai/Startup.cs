using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BanHang_DaoNgocHai.Startup))]
namespace BanHang_DaoNgocHai
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
