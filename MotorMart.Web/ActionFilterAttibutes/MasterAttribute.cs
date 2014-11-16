using System.Web.Mvc;
using MotorMart.Web.Models;
using MotorMart.Core.Routing;
using MotorMart.Web.Controllers;
using System;

namespace MotorMart.Web.ActionFilterAttributes
{
    public class MasterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            MasterController controller = (MasterController)filterContext.Controller;

            if (filterContext.RouteData.Values["RouteDataBinder"] != null)
            {
                controller.RouteDataBinder = (RouteDataBinder)filterContext.RouteData.Values["RouteDataBinder"];
            }
            controller.PreLoadControllerData();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            MasterViewModel viewModel = new MasterViewModel();

            if (filterContext.Result.GetType() == typeof(System.Web.Mvc.ViewResult) || filterContext.Result.GetType() == typeof(System.Web.Mvc.PartialViewResult))
            {
                viewModel = ((ViewResultBase)filterContext.Result).ViewData.Model as MasterViewModel;
            }

            if (viewModel != null)
            {
                MasterController controller = (MasterController)filterContext.Controller;
                controller._cookies.LastVisit = DateTime.Now;
                controller.SetViewModel(viewModel);
            }
        }
    }
}
