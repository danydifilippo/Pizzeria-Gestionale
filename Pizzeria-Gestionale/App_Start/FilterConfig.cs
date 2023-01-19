using System.Web;
using System.Web.Mvc;

namespace Pizzeria_Gestionale
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
