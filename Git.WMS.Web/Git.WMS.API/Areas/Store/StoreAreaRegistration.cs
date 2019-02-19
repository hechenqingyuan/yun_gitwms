using System.Web.Mvc;

namespace Git.WMS.API.Areas.Store
{
    public class StoreAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Store";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Store_default",
                "Api/Store/{controller}/{action}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
