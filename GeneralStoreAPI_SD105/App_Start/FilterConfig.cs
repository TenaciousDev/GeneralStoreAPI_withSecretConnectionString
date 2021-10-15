using System.Web;
using System.Web.Mvc;

namespace GeneralStoreAPI_SD105
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
