using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Core.Routing;

namespace MotorMart.Web.Controllers
{
    public class SystemController : Controller
    {
        public ActionResult RebuildRoutes()
        {
            RouteHelper.Instance.UpdateRouteRegistration(true);
            return new ContentResult { Content = "Complete - " + DateTime.Now.Ticks, ContentType = "text/plain" };
        }

        public ActionResult MailTo(string subject, string message)
        {
            return Redirect("mailto:?subject=asdasdasdasd");
        }
    }
}
