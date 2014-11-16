using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Core.Models;
using System.Web.Routing;

namespace MotorMart.Core.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        //will need five actionresult methods in here, namely: index, Http400, Http404, Http503, GeneralHttp, BadRequest
        public ActionResult Index()
        {
            this.Response.Clear();
            this.Response.StatusCode = 500;
            return View("GeneralHttp", new ErrorViewModel((Exception)this.RouteData.Values["error"]));
        }

        public ActionResult Http400()
        {
            this.Response.Clear();
            this.Response.StatusCode = 400;
            return View("BadRequest", new ErrorViewModel((Exception)this.RouteData.Values["error"]));
        }

        public ActionResult BadRequest()
        {
            this.Response.Clear();
            this.Response.StatusCode = 400;
            return View(new ErrorViewModel((Exception)this.RouteData.Values["error"]));
        }

        public ActionResult Http500()
        {
            this.Response.Clear();
            this.Response.StatusCode = 500;
            return View(new ErrorViewModel((Exception)this.RouteData.Values["error"]));
        }

        public ActionResult Http503()
        {
            this.Response.Clear();
            this.Response.StatusCode = 503;
            return View(new ErrorViewModel((Exception)this.RouteData.Values["error"]));
        }

        public ActionResult GeneralHttp()
        {
            this.Response.Clear();
            this.Response.StatusCode = 500;
            return View("GeneralHttp", new ErrorViewModel((Exception)this.RouteData.Values["error"]));
        }

    }
}
