using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MotorMart.Core.ActionFilterAttributes;
using MotorMart.Core.Routing;
using MotorMart.Core.Models.Validation.DataAnnotations;

namespace MotorMart.Cms
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleErrorWithElmahAttribute());
        }

        protected void Application_Start()
        {
            RegisterGlobalFilters(GlobalFilters.Filters);
            RouteHelper.Instance.UpdateRouteRegistration(true);
            DataAnnotationsHelper.RegisterAllAdapters();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            RouteErrorHelper errorHelper = new RouteErrorHelper(this);
            errorHelper.Process();
        }
    }
}