using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieWeb.Controllers
{
    public class AuthorizeAttribute_64130299Controller : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Kiểm tra nếu session không chứa thông tin đăng nhập
            if (HttpContext.Current.Session["Username"] == null)
            {
                // Nếu chưa đăng nhập, thêm thông báo lỗi vào TempData
                filterContext.Controller.TempData["ErrorMessage"] = "Bạn cần đăng nhập để truy cập trang này.";

                // Nếu không, chuyển hướng người dùng về trang đăng nhập
                filterContext.Result = new RedirectResult("~/Account_64130299/Login_64130299");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}