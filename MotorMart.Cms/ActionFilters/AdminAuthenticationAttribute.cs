using System.Web.Mvc;
using MotorMart.Cms.Models;
using MotorMart.Cms.Controllers;

namespace MotorMart.Cms.ActionFilterAttributes
{
    public class AdminAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            AdminMasterController controller = (AdminMasterController)filterContext.Controller;
            controller.PreLoadControllerData();

            bool isadmin = false;
            string Controller = (string)filterContext.RouteData.Values["controller"].ToString().ToLower();
            string Action = (string)filterContext.RouteData.Values["action"].ToString().ToLower();
            string Area = (string)filterContext.RouteData.Values["area"] ?? string.Empty;
           
            // First check if user is logged in and is admin
            if (controller._currentUserAccount != null && controller._currentUserAccount.usergroupid > 0 && controller._currentUserAccount.usergroup.name.ToLower().Trim() == "admin")
            {
                isadmin = true;
            }

            // Not logged in and not admin, redirect to log in page
            if (!isadmin && (string)filterContext.RouteData.Values["controller"] != "login")
            {
                UrlHelper urlHelper = controller.Url;
                RedirectResult res = new RedirectResult(urlHelper.Action("index", "login", new { @area = "account" }));
                filterContext.Result = res;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            // Only do this if we are returning view data, eg. bypass RedirectToRouteResult
            if (filterContext.Result.GetType() == typeof(System.Web.Mvc.ViewResult) || filterContext.Result.GetType() == typeof(System.Web.Mvc.PartialViewResult))
            {
                AdminViewModel viewModel = ((ViewResultBase)filterContext.Result).ViewData.Model as AdminViewModel;

                if (viewModel != null)
                {
                    AdminMasterController controller = (AdminMasterController)filterContext.Controller;
                    controller.SetAdminViewModel(viewModel);
                }
            }
        }
    }
}
