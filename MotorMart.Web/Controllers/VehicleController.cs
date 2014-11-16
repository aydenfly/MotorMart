using System.Web.Mvc;
using MotorMart.Web.Services;
using MotorMart.Core.Models.Validation;
using MotorMart.Web.Models;
using MotorMart.Core.Models;

namespace MotorMart.Web.Controllers
{
    public class VehicleController : MasterController
    {
        private readonly IVehicleService _vehicleService;
        
        public VehicleController()
        {
            _vehicleService = new VehicleService(new ModelStateWrapper(ModelState));
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(VehicleSearchModel vehiclesearch)
        {
            var model = new VehicleSearchViewModel
            {
                vehiclesearch = vehiclesearch
            };
            _vehicleService.PopulateVehicleSearchViewModel(model);
            return View(model);
        }
        
        public ActionResult NewCars(VehicleSearchModel vehiclesearch)
        {
            var model = new VehicleSearchViewModel
            {
                vehiclesearch = vehiclesearch
            };
            _vehicleService.PopulateVehicleSearchViewModel(model);
            vehiclesearch.isnew = true;

            return View(model);
        }


        [HttpGet]
        public ActionResult UsedCars(VehicleSearchModel vehiclesearch)
        {
            var model = new VehicleSearchViewModel
            {
                vehiclesearch = vehiclesearch
            };
            _vehicleService.PopulateVehicleSearchViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult ProcessUsedCars(VehicleSearchModel vehiclesearch)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("UsedCars", vehiclesearch);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Detail(VehicleGetModel get)
        {
            var model = new Models.VehicleViewModel
            {
                get = get
            };

            _vehicleService.PopulateVehicleViewModel(model);

            return View(model);
        }        
    }
}
