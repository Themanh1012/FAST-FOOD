using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FAST_FOOD.Areas.Admin.Filters
{
    public class AdminAuthorizeAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting (ActionExecutingContext filterContext)
        {

            var role = filterContext.HttpContext.Session["Role"];
            var area = (string)filterContext.RouteData.DataTokens["area"];

            // Chỉ kiểm tra nếu đang vào khu vực Admin
            if (area == "Admin")
            {
                if (role == null || role.ToString() != "Admin")
                {
                    filterContext.Result = new RedirectResult("~/TaiKhoan/DangNhap");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}