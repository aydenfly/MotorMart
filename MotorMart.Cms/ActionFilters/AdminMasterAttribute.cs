using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using MotorMart.Core.Models;
using MotorMart.Core.Routing;
using MotorMart.Cms.Models;
using MotorMart.Cms.Controllers;

namespace MotorMart.Cms.ActionFilterAttributes
{
    public class AdminMasterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            AdminMasterController controller = (AdminMasterController)filterContext.Controller;

            if (filterContext.RouteData.Values["RouteDataBinder"] != null)
            {
                //controller.RouteDataBinder = (RouteDataBinder)filterContext.RouteData.Values["RouteDataBinder"];            
            }
            controller.PreLoadControllerData();
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
