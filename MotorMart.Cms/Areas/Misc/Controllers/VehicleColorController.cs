using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Cms.Controllers;
using MotorMart.Core.Models;
using MotorMart.Cms.Areas.Misc.Models;
using MotorMart.Cms.Areas.Misc.Services;
using MotorMart.Core.Models.Validation;

namespace MotorMart.Cms.Areas.Misc.Controllers
{
    public class VehicleColorController : AdminMasterController
    {
        //
        // GET: /Misc/VehicleColor/

        private IVehicleColorService _vehicleColorService;

        public VehicleColorController(IVehicleColorService bodyTypeService)
        {
            _vehicleColorService = bodyTypeService;
        }

        public VehicleColorController()
        {
            _vehicleColorService = new VehicleColorService(new ModelStateWrapper(this.ModelState));
        }

        #region Vehicle Color Actions

        public ActionResult Index()
        {
            VehicleColorViewModel model = new VehicleColorViewModel();
            _vehicleColorService.PopulateVehicleColorViewModel(model);
            return View(model);
        }

        public ActionResult AddVehicleColor()
        {
            VehicleColorViewModel model = new VehicleColorViewModel { get = new VehicleColorGetModel() };
            _vehicleColorService.PopulateVehicleColorViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddVehicleColor(VehicleColorAddModel add)
        {
            if (_vehicleColorService.AddVehicleColor(add))
            {
                return RedirectToAction("EditVehicleColor", new { @colorid = add.NewVehicleColor.colorid });
            }
            else
            {
                VehicleColorViewModel model = new VehicleColorViewModel { get = new VehicleColorGetModel(), add = add };
                _vehicleColorService.PopulateVehicleColorViewModel(model);
                return View(model);
            }

        }

        public ActionResult EditVehicleColor(VehicleColorGetModel get)
        {
            color Model;
            if (_vehicleColorService.GetVehicleColor(get, out Model))
            {
                VehicleColorViewModel model = new VehicleColorViewModel { get = get };
                _vehicleColorService.PopulateVehicleColorViewModel(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditVehicleColor(VehicleColorEditModel edit)
        {
            if (_vehicleColorService.EditVehicleColor(edit))
            {
                return RedirectToAction("EditVehicleColor", new { @colorid = edit.colorid });
            }
            VehicleColorViewModel model = new VehicleColorViewModel { get = new VehicleColorGetModel { colorid = edit.colorid } };
            _vehicleColorService.PopulateVehicleColorViewModel(model);
            return View(model);
        }

        public ActionResult DeleteVehicleColorDialog(VehicleColorGetModel get)
        {
            VehicleColorViewModel dialog = new VehicleColorViewModel { get = get };
            _vehicleColorService.PopulateVehicleColorViewModel(dialog);
            return PartialView("DeleteVehicleColorDialog", dialog);
        }


        [HttpPost]
        public ActionResult DeleteVehicleColor(VehicleColorDeleteModel delete)
        {
            VehicleColorViewModel model = new VehicleColorViewModel { Success = false };
            if (_vehicleColorService.DeleteVehicleColor(delete))
            {
                model.Success = true;
            }
            return View(model);
        }

        public ActionResult Up(int? ColorId)
        {
            _vehicleColorService.VehicleColorUp(ColorId);
            return RedirectToAction("Index");
        }

        public ActionResult Down(int? ColorId)
        {
            _vehicleColorService.VehicleColorDown(ColorId);
            return RedirectToAction("Index");
        }

        #endregion
    }
}
