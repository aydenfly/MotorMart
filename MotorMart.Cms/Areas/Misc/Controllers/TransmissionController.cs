using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Cms.Controllers;
using MotorMart.Cms.Areas.Misc.Models;
using MotorMart.Core.Models;
using MotorMart.Cms.Areas.Misc.Services;
using MotorMart.Core.Models.Validation;

namespace MotorMart.Cms.Areas.Misc.Controllers
{
    public class TransmissionController : AdminMasterController
    {
        //
        // GET: /Misc/Transmission/

        private ITransmissionService _transmissionService;

        public TransmissionController(ITransmissionService bodyTypeService)
        {
            _transmissionService = bodyTypeService;
        }

        public TransmissionController()
        {
            _transmissionService = new TransmissionService(new ModelStateWrapper(this.ModelState));
        }

        #region Transmission Actions

        public ActionResult Index()
        {
            TransmissionViewModel model = new TransmissionViewModel();
            _transmissionService.PopulateTransmissionViewModel(model);
            return View(model);
        }

        public ActionResult AddTransmission()
        {
            TransmissionViewModel model = new TransmissionViewModel { get = new TransmissionGetModel() };
            _transmissionService.PopulateTransmissionViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddTransmission(TransmissionAddModel add)
        {
            if (_transmissionService.AddTransmission(add))
            {
                return RedirectToAction("EditTransmission", new { @transmissionid = add.NewTransmission.transmissionid });
            }
            else
            {
                TransmissionViewModel model = new TransmissionViewModel { get = new TransmissionGetModel(), add = add };
                _transmissionService.PopulateTransmissionViewModel(model);
                return View(model);
            }

        }

        public ActionResult EditTransmission(TransmissionGetModel get)
        {
            transmission Model;
            if (_transmissionService.GetTransmission(get, out Model))
            {
                TransmissionViewModel model = new TransmissionViewModel { get = get };
                _transmissionService.PopulateTransmissionViewModel(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditTransmission(TransmissionEditModel edit)
        {
            if (_transmissionService.EditTransmission(edit))
            {
                return RedirectToAction("EditTransmission", new { @transmissionid = edit.transmissionid });
            }
            TransmissionViewModel model = new TransmissionViewModel { get = new TransmissionGetModel { transmissionid = edit.transmissionid } };
            _transmissionService.PopulateTransmissionViewModel(model);
            return View(model);
        }

        public ActionResult DeleteTransmissionDialog(TransmissionGetModel get)
        {
            TransmissionViewModel dialog = new TransmissionViewModel { get = get };
            _transmissionService.PopulateTransmissionViewModel(dialog);
            return PartialView("DeleteTransmissionDialog", dialog);
        }

        [HttpPost]
        public ActionResult DeleteTransmission(TransmissionDeleteModel delete)
        {
            TransmissionViewModel model = new TransmissionViewModel { Success = false };
            if (_transmissionService.DeleteTransmission(delete))
            {
                model.Success = true;
            }
            return View(model);
        }

        public ActionResult Up(int? TransmissionId)
        {
            _transmissionService.TransmissionUp(TransmissionId);
            return RedirectToAction("Index");
        }

        public ActionResult Down(int? TransmissionId)
        {
            _transmissionService.TransmissionDown(TransmissionId);
            return RedirectToAction("Index");
        }

        #endregion
    }
}
