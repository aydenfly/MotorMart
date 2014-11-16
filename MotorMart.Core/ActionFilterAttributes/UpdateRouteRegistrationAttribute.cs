using System.Web.Mvc;
using MotorMart.Core.Routing;

namespace MotorMart.Core.ActionFilterAttributes
{
    public class UpdateRouteRegistrationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Really this only needs to happen if a sitemap page has been updated or any other content update that affects routing
            RouteHelper.Instance.UpdateRouteRegistration(false);
        }
    }
}
