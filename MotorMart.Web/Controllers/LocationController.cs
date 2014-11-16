using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Web.Models;

namespace MotorMart.Web.Controllers
{
    public class LocationController : MasterController
    {
        //
        // GET: /Location/

        public ActionResult Index()
        {
            LocationViewModel model = new LocationViewModel();
            return View(model);
        }

        public ActionResult GetDirections(string locationReference)
        {
            LocationViewModel model = new LocationViewModel();
            return View(model);
        }

    }
}
