using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Cms.Controllers;
using MotorMart.Cms.Areas.Misc.Models;
using MotorMart.Cms.Areas.Misc.Services;
using MotorMart.Core.Models.Validation;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Areas.Misc.Controllers
{
    public class DealerController : AdminMasterController
    {
        //
        // GET: /Misc/Dealer/
        private IDealerService _vehicleDealerService;

        public DealerController(IDealerService dealerService)
        {
            _vehicleDealerService = dealerService;
        }

        public DealerController()
        {
            _vehicleDealerService = new DealerService(new ModelStateWrapper(this.ModelState));
        }

        #region Vehicle Make Actions

        public ActionResult Index(DealerViewModel model)
        {
            if(model == null) model = new DealerViewModel();
            _vehicleDealerService.PopulateDealerViewModel(model);
            return View(model);
        }

        public ActionResult AddDealer()
        {
            DealerViewModel model = new DealerViewModel { get = new DealerGetModel() };
            _vehicleDealerService.PopulateDealerViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddDealer(DealerAddModel add)
        {
            if (_vehicleDealerService.AddDealer(add))
            {
                return RedirectToAction("EditDealer", new { @dealerid = add.NewDealer.dealerid });
            }
            else
            {
                DealerViewModel model = new DealerViewModel { get = new DealerGetModel(), add = add };
                _vehicleDealerService.PopulateDealerViewModel(model);
                return View(model);
            }
        }

        public ActionResult EditDealer(DealerGetModel get)
        {
            dealer Dealer;
            if (_vehicleDealerService.GetDealer(get, out Dealer))
            {
                DealerViewModel model = new DealerViewModel { get = get };
                _vehicleDealerService.PopulateDealerViewModel(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditDealer(DealerEditModel edit)
        {
            if (_vehicleDealerService.EditDealer(edit))
            {
                return RedirectToAction("EditDealer", new { @dealerid = edit.dealerid });
            }
            DealerViewModel model = new DealerViewModel { get = new DealerGetModel { dealerid = edit.dealerid } };
            _vehicleDealerService.PopulateDealerViewModel(model);
            return View(model);
        }

        public ActionResult DeleteDealerDialog(DealerGetModel get)
        {
            DealerViewModel dialog = new DealerViewModel { get = get };
            _vehicleDealerService.PopulateDealerViewModel(dialog);
            return PartialView("DeleteDealerDialog", dialog);

        }
        [HttpPost]
        public ActionResult DeleteDealer(DealerDeleteModel delete)
        {
            DealerViewModel model = new DealerViewModel { Success = false };
            if (_vehicleDealerService.DeleteDealer(delete))
            {
                model.Success = true;
            }
            return View(model);
        }

        public ActionResult Up(int? DealerId)
        {
            _vehicleDealerService.DealerUp(DealerId);
            return RedirectToAction("Index");
        }

        public ActionResult Down(int? DealerId)
        {
            _vehicleDealerService.DealerDown(DealerId);
            return RedirectToAction("Index");
        }

        #endregion
    }
}
