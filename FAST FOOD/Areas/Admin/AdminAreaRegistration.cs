using System.Web.Mvc;

namespace FAST_FOOD.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Admin"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Admin_default",
                url: "Admin/{controller}/{action}/{id}",
              defaults: new { action = "Index", id = UrlParameter.Optional },

                namespaces: new[] { "FAST_FOOD.Areas.Admin.Controllers" }  // ⚠️ PHẢI CÓ DÒNG NÀY
            );
        }
    }
}
