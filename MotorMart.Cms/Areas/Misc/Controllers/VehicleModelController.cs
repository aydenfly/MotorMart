using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Cms.Areas.Vehicle.Services;
using MotorMart.Cms.Areas.Misc.Services;
using MotorMart.Cms.Controllers;
using MotorMart.Core.Models.Validation;
using MotorMart.Cms.Areas.Misc.Models;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Areas.Misc.Controllers
{
    public class VehicleModelController : AdminMasterController
    {
        private IVehicleService _adminVehicleService;
        private IVehicleModelService _vehicleModelService;

        // GET: /Misc/vehiclemodel

        public VehicleModelController(IVehicleModelService vehicleModelService, IVehicleService adminVehicleService)
        {
            _vehicleModelService = vehicleModelService;
            _adminVehicleService = adminVehicleService;
        }

        public VehicleModelController()
        {
            _vehicleModelService = new VehicleModelService(new ModelStateWrapper(this.ModelState));
            _adminVehicleService = new VehicleService(new ModelStateWrapper(this.ModelState));
        }

        #region Vehicle Model Actions

        public ActionResult Index()
        {
            VehicleModelViewModel model = new VehicleModelViewModel();
            _vehicleModelService.PopulateVehicleModelViewModel(model);
            return View(model);
        }

        public ActionResult AddVehicleModel()
        {
            VehicleModelViewModel model = new VehicleModelViewModel { get = new VehicleModelGetModel() };
            _vehicleModelService.PopulateVehicleModelViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddVehicleModel(VehicleModelAddModel add)
        {
            if (_vehicleModelService.AddVehicleModel(add))
            {
                return RedirectToAction("EditVehicleModel", new { @modelid = add.NewModel.modelid });
            }
            else
            {
                VehicleModelViewModel model = new VehicleModelViewModel { get = new VehicleModelGetModel(), add = add };
                _vehicleModelService.PopulateVehicleModelViewModel(model);
                return View(model);
            }

        }

        public ActionResult EditVehicleModel(VehicleModelGetModel get)
        {
            model Model;
            if (_vehicleModelService.GetVehicleModel(get, out Model))
            {
                VehicleModelViewModel model = new VehicleModelViewModel { get = get };
                _vehicleModelService.PopulateVehicleModelViewModel(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditVehicleModel(VehicleModelEditModel edit)
        {
            if (_vehicleModelService.EditVehicleModel(edit))
            {
                return RedirectToAction("EditVehicleModel", new { @modelid = edit.modelid });
            }
            VehicleModelViewModel model = new VehicleModelViewModel { get = new VehicleModelGetModel { modelid = edit.modelid } };
            _vehicleModelService.PopulateVehicleModelViewModel(model);
            return View(model);
        }

        public ActionResult DeleteVehicleModelDialog(VehicleModelGetModel get)
        {
            VehicleModelViewModel dialog = new VehicleModelViewModel { get = get };
            _vehicleModelService.PopulateVehicleModelViewModel(dialog);
            return PartialView("DeleteVehicleModelDialog", dialog);
        }

        [HttpPost]
        public ActionResult DeleteVehicleModel(VehicleModelDeleteModel delete)
        {
            VehicleModelViewModel model = new VehicleModelViewModel { Success = false };
            if (_vehicleModelService.DeleteVehicleModel(delete))
            {
                model.Success = true;
            }
            return View(model);
        }

        public ActionResult Up(int? ModelId)
        {
            _vehicleModelService.VehicleModelUp(ModelId);
            return RedirectToAction("Index");
        }

        public ActionResult Down(int? ModelId)
        {
            _vehicleModelService.VehicleModelDown(ModelId);
            return RedirectToAction("Index");
        }

        #endregion
    }
}
