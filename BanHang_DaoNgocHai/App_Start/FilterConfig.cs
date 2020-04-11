using System.Web;
using System.Web.Mvc;

namespace BanHang_DaoNgocHai
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
