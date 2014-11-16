using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MotorMart.Core.Controllers;

namespace MotorMart.Core.Routing
{
    public class RouteErrorHelper
    {
        HttpApplication Application { get; set; }

        public RouteErrorHelper(HttpApplication app)
        {
            this.Application = app;
        }

        public void Process()
        {
            if (!Application.Context.IsCustomErrorEnabled)
            {
                //Just display the ASP.NET YSODeath
                return;
            }
            Exception exception = Application.Server.GetLastError();

            Application.Response.Clear();

            HttpException httpException = exception as HttpException;

            RouteData routeData = new RouteData();
            routeData.Values.Add("Controller", "Error");

            if (httpException == null)
            {
                routeData.Values.Add("action", "index");
            }
            else
            {
                switch (httpException.GetHttpCode())
                {
                    case 404: routeData.Values.Add("action", "Http404"); break; //Page Not Found
                    case 503: routeData.Values.Add("action", "Http503"); break; //Maintenance error
                    default: routeData.Values.Add("action", "GeneralHttp"); break;
                }
            }

            //Pass exception details to the target erro view.
            routeData.Values.Add("error", exception);

            //clear the error on server.
            Application.Server.ClearError();

            //Avoid IIS7 getting in the middle
            Application.Response.TrySkipIisCustomErrors = true;

            //Call target Controller and pass the routeData.
            IController errorController = new ErrorController();

            try
            {
                errorController.Execute(new RequestContext(new HttpContextWrapper(Application.Context), routeData));
            }
            catch (Exception)
            {
                //if we are here it means that the URL is unsafe and the only way to handle it gracefully is to redirect to 
                //the (//Controller/View pair that returns 400 - Bad Request).
                this.Application.Response.Redirect("~/error/badrequest");
            }
        }
    }
}