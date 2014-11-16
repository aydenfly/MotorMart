using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Web.Models;

namespace MotorMart.Web.Controllers
{
    public class HomeController : MasterController
    {
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            return View(model);
        }
    }
}
