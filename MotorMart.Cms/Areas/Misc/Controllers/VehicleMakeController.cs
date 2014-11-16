using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Cms.Controllers;
using MotorMart.Cms.Areas.Misc.Services;
using MotorMart.Core.Models.Validation;
using MotorMart.Cms.Areas.Misc.Models;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Areas.Misc.Controllers
{
    public class VehicleMakeController : AdminMasterController
    {
        //
        // GET: /Misc/VehicleMake/

        private IVehicleMakeService _vehicleMakeService;

        public VehicleMakeController(IVehicleMakeService vehicleMakeService)
        {
            _vehicleMakeService = vehicleMakeService;
        }

        public VehicleMakeController()
        {
            _vehicleMakeService = new VehicleMakeService(new ModelStateWrapper(this.ModelState));
        }

        #region Vehicle Make Actions

        public ActionResult Index(VehicleMakeViewModel model)
        {
            if(model == null) model = new VehicleMakeViewModel();
            _vehicleMakeService.PopulateVehicleMakeViewModel(model);
            return View(model);
        }

        public ActionResult AddVehicleMake()
        {
            VehicleMakeViewModel model = new VehicleMakeViewModel { get = new VehicleMakeGetModel() };
            _vehicleMakeService.PopulateVehicleMakeViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddVehicleMake(VehicleMakeAddModel add)
        {
            if (_vehicleMakeService.AddVehicleMake(add))
            {
                return RedirectToAction("EditVehicleMake", new { @makeid = add.NewVehicleMake.makeid });
            }
            else
            {
                VehicleMakeViewModel model = new VehicleMakeViewModel { get = new VehicleMakeGetModel(), add = add };
                _vehicleMakeService.PopulateVehicleMakeViewModel(model);
                return View(model);
            }

        }

        public ActionResult EditVehicleMake(VehicleMakeGetModel get)
        {
            make Model;
            if (_vehicleMakeService.GetVehicleMake(get, out Model))
            {
                VehicleMakeViewModel model = new VehicleMakeViewModel { get = get };
                _vehicleMakeService.PopulateVehicleMakeViewModel(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditVehicleMake(VehicleMakeEditModel edit)
        {
            if (_vehicleMakeService.EditVehicleMake(edit))
            {
                return RedirectToAction("EditVehicleMake", new { @makeid = edit.makeid });
            }
            VehicleMakeViewModel model = new VehicleMakeViewModel { get = new VehicleMakeGetModel { makeid = edit.makeid } };
            _vehicleMakeService.PopulateVehicleMakeViewModel(model);
            return View(model);
        }

        public ActionResult DeleteVehicleMakeDialog(VehicleMakeGetModel get)
        {
            VehicleMakeViewModel dialog = new VehicleMakeViewModel { get = get };
            _vehicleMakeService.PopulateVehicleMakeViewModel(dialog);
            return PartialView("DeleteVehicleMakeDialog", dialog);

        }
        [HttpPost]
        public ActionResult DeleteVehicleMake(VehicleMakeDeleteModel delete)
        {
            VehicleMakeViewModel model = new VehicleMakeViewModel { Success = false };
            if (_vehicleMakeService.DeleteVehicleMake(delete))
            {
                model.Success = true;
            }
            return View(model);
        }

        public ActionResult Up(int? MakeId)
        {
            _vehicleMakeService.VehicleMakeUp(MakeId);
            return RedirectToAction("Index");
        }

        public ActionResult Down(int? MakeId)
        {
            _vehicleMakeService.VehicleMakeDown(MakeId);
            return RedirectToAction("Index");
        }

        #endregion
    }
}
