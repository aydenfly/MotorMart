using System.Web.Mvc;
using System.Net;
using MotorMart.Core.Common;

namespace MotorMart.Core.ActionFilterAttributes
{
    public class RebuildRoutesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            //Only do this if we are returning view data, eg. bypass RedirectToRouteResult
            if (filterContext.Result.GetType() == typeof(System.Web.Mvc.ViewResult))
            {
                if (GlobalSettings.UpdateRoutes)
                {
                    // Pretty important that this bit works!
                    // Make sure that the RebuildRoutesUrl is set properly 'http://somedomain/system/rebuildroutes'
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GlobalSettings.RebuildRoutesUrl);
                    WebResponse resp = req.GetResponse();
                }
            }
        }
    }
}
