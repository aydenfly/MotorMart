using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using MotorMart.Core.Controllers;

namespace MotorMart.Core.ActionFilterAttributes
{
    public class UpdateCookiesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            // Only do this if we are returning view data, eg. bypass RedirectToRouteResult
            if (filterContext.Result.GetType() == typeof(System.Web.Mvc.ViewResult))
            {   
                MasterController controller = (MasterController)filterContext.Controller;
                controller._cookies.LastVisit = DateTime.Now;
            }
        }
    }
}
