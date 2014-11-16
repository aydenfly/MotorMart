using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Cms.Services;
using MotorMart.Core.Models.Validation;
using MotorMart.Cms.Areas.Misc.Models;
using MotorMart.Cms.Areas.Misc.Services;
using MotorMart.Cms.Areas.Vehicle.Services;
using MotorMart.Cms.Controllers;
using MotorMart.Cms.Models;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Areas.Misc.Controllers
{
    public class MiscController : AdminMasterController
    {
        public ActionResult Index()
        {
            MiscViewModel model = new MiscViewModel();
            return View(model);
        }
        
    }
}
