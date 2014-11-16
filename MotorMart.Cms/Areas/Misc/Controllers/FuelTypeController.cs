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
    public class FuelTypeController : AdminMasterController
    {
        //
        // GET: /Misc/FuelType/

        private IFuelTypeService _fuelTypeService;

        public FuelTypeController(IFuelTypeService bodyTypeService)
        {
            _fuelTypeService = bodyTypeService;
        }

        public FuelTypeController()
        {
            _fuelTypeService = new FuelTypeService(new ModelStateWrapper(this.ModelState));
        }

        #region Fuel Type Actions

        public ActionResult Index()
        {
            FuelTypeViewModel model = new FuelTypeViewModel();
            _fuelTypeService.PopulateFuelTypeViewModel(model);
            return View(model);
        }

        public ActionResult AddFuelType()
        {
            FuelTypeViewModel model = new FuelTypeViewModel { get = new FuelTypeGetModel() };
            _fuelTypeService.PopulateFuelTypeViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddFuelType(FuelTypeAddModel add)
        {
            if (_fuelTypeService.AddFuelType(add))
            {
                return RedirectToAction("EditFuelType", new { @fueltypeid = add.NewFuelType.fueltypeid });
            }
            else
            {
                FuelTypeViewModel model = new FuelTypeViewModel { get = new FuelTypeGetModel(), add = add };
                _fuelTypeService.PopulateFuelTypeViewModel(model);
                return View(model);
            }

        }

        public ActionResult EditFuelType(FuelTypeGetModel get)
        {
            fueltype Model;
            if (_fuelTypeService.GetFuelType(get, out Model))
            {
                FuelTypeViewModel model = new FuelTypeViewModel { get = get };
                _fuelTypeService.PopulateFuelTypeViewModel(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditFuelType(FuelTypeEditModel edit)
        {
            if (_fuelTypeService.EditFuelType(edit))
            {
                return RedirectToAction("EditFuelType", new { @fueltypeid = edit.fueltypeid });
            }
            FuelTypeViewModel model = new FuelTypeViewModel { get = new FuelTypeGetModel { fueltypeid = edit.fueltypeid } };
            _fuelTypeService.PopulateFuelTypeViewModel(model);
            return View(model);
        }

        public ActionResult DeleteFuelTypeDialog(FuelTypeGetModel get)
        {
            FuelTypeViewModel dialog = new FuelTypeViewModel { get = get };
            _fuelTypeService.PopulateFuelTypeViewModel(dialog);
            return PartialView("DeleteFuelTypeDialog", dialog);
        }

        [HttpPost]
        public ActionResult DeleteFuelType(FuelTypeDeleteModel delete)
        {
            FuelTypeViewModel model = new FuelTypeViewModel { Success = false };
            if (_fuelTypeService.DeleteFuelType(delete))
            {
                model.Success = true;
            }
            return View(model);
        }

        public ActionResult Up(int? FuelTypeId)
        {
            _fuelTypeService.FuelTypeUp(FuelTypeId);
            return RedirectToAction("Index");
        }

        public ActionResult Down(int? FuelTypeId)
        {
            _fuelTypeService.FuelTypeDown(FuelTypeId);
            return RedirectToAction("Index");
        }

        #endregion
    }
}
