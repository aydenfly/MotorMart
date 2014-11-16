using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Cms.Areas.Misc.Models;
using MotorMart.Core.Models;
using MotorMart.Cms.Areas.Misc.Services;
using MotorMart.Core.Models.Validation;
using MotorMart.Cms.Controllers;

namespace MotorMart.Cms.Areas.Misc.Controllers
{
    public class ApplicationSettingController : AdminMasterController
    {
        //
        // GET: /Misc/ApplicationSetting/

        private IApplicationSettingService _applicationService;

        public ApplicationSettingController(IApplicationSettingService applicationService)
        {
            _applicationService = applicationService;
        }

        public ApplicationSettingController()
        {
            _applicationService = new ApplicationSettingService(new ModelStateWrapper(this.ModelState));
        }

        #region Body Type Actions

        public ActionResult Index()
        {
            ApplicationSettingViewModel model = new ApplicationSettingViewModel();
            _applicationService.PopulateApplicationSettingViewModel(model);
            return View(model);
        }

        public ActionResult Add()
        {
            ApplicationSettingViewModel model = new ApplicationSettingViewModel { get = new ApplicationSettingGetModel() };
            _applicationService.PopulateApplicationSettingViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ApplicationSettingAddModel add)
        {
            if (_applicationService.AddApplicationSetting(add))
            {
                return RedirectToAction("Edit", new { @applicationsettingid = add.NewApplicationSetting.applicationsettingid });
            }
            else
            {
                ApplicationSettingViewModel model = new ApplicationSettingViewModel { get = new ApplicationSettingGetModel(), add = add };
                _applicationService.PopulateApplicationSettingViewModel(model);
                return View(model);
            }

        }

        public ActionResult Edit(ApplicationSettingGetModel get)
        {
            applicationsetting ApplicationSetting;
            if (_applicationService.GetApplicationSetting(get, out ApplicationSetting))
            {
                ApplicationSettingViewModel model = new ApplicationSettingViewModel { get = get };
                _applicationService.PopulateApplicationSettingViewModel(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ApplicationSettingEditModel edit)
        {
            if (_applicationService.EditApplicationSetting(edit))
            {
                return RedirectToAction("Edit", new { @applicationsettingid = edit.applicationsettingid });
            }
            ApplicationSettingViewModel model = new ApplicationSettingViewModel { get = new ApplicationSettingGetModel { applicationsettingid = edit.applicationsettingid } };
            _applicationService.PopulateApplicationSettingViewModel(model);
            return View(model);
        }

        public ActionResult DeleteDialog(ApplicationSettingGetModel get)
        {
            ApplicationSettingViewModel dialog = new ApplicationSettingViewModel { get = get };
            _applicationService.PopulateApplicationSettingViewModel(dialog);
            return PartialView("DeleteDialog", dialog);
        }

        [HttpPost]
        public ActionResult Delete(ApplicationSettingDeleteModel delete)
        {
            ApplicationSettingViewModel model = new ApplicationSettingViewModel { Success = false };
            if (_applicationService.DeleteApplicationSetting(delete))
            {
                model.Success = true;
            }
            return View(model);
        }

        #endregion
    }
}
