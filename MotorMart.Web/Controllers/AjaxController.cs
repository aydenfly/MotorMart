using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Web.Services;
using MotorMart.Core.Models.Validation;
using MotorMart.Core.Models;

namespace MotorMart.Web.Controllers
{
    public class AjaxController : Controller
    {
        private IVehicleService _vehicleService;

        // GET: /Vehicle/
        public AjaxController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public AjaxController()
        {
            _vehicleService = new VehicleService(new ModelStateWrapper(this.ModelState));
        }

        public JsonResult GetMakeModels(int? makeid)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            List<model> MakeModels = _vehicleService.GetMakeModels(makeid ?? 0);
            if (MakeModels.Any())
            {
                foreach (var model in MakeModels)
                {
                    items.Add(new SelectListItem { Text = model.name, Value = model.modelid.ToString() });
                }
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}
