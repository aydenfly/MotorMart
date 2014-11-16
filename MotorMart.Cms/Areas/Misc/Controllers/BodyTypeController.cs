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
    public class BodyTypeController : AdminMasterController
    {
        //
        // GET: /Misc/BodyType/

        private IBodyTypeService _bodyTypeService;

        public BodyTypeController(IBodyTypeService bodyTypeService)
        {
            _bodyTypeService = bodyTypeService;
        }

        public BodyTypeController()
        {
            _bodyTypeService = new BodyTypeService(new ModelStateWrapper(this.ModelState));
        }

        #region Body Type Actions

        public ActionResult Index()
        {
            BodyTypeViewModel model = new BodyTypeViewModel();
            _bodyTypeService.PopulateBodyTypeViewModel(model);
            return View(model);
        }

        public ActionResult AddBodyType()
        {
            BodyTypeViewModel model = new BodyTypeViewModel { get = new BodyTypeGetModel() };
            _bodyTypeService.PopulateBodyTypeViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddBodyType(BodyTypeAddModel add)
        {
            if (_bodyTypeService.AddBodyType(add))
            {
                return RedirectToAction("EditBodyType", new { @bodytypeid = add.NewBodyType.bodytypeid });
            }
            else
            {
                BodyTypeViewModel model = new BodyTypeViewModel { get = new BodyTypeGetModel(), add = add };
                _bodyTypeService.PopulateBodyTypeViewModel(model);
                return View(model);
            }

        }

        public ActionResult EditBodyType(BodyTypeGetModel get)
        {
            bodytype Model;
            if (_bodyTypeService.GetBodyType(get, out Model))
            {
                BodyTypeViewModel model = new BodyTypeViewModel { get = get };
                _bodyTypeService.PopulateBodyTypeViewModel(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditBodyType(BodyTypeEditModel edit)
        {
            if (_bodyTypeService.EditBodyType(edit))
            {
                return RedirectToAction("EditBodyType", new { @bodytypeid = edit.bodytypeid });
            }
            BodyTypeViewModel model = new BodyTypeViewModel { get = new BodyTypeGetModel { bodytypeid = edit.bodytypeid } };
            _bodyTypeService.PopulateBodyTypeViewModel(model);
            return View(model);
        }

        public ActionResult DeleteBodyTypeDialog(BodyTypeGetModel get)
        {
            BodyTypeViewModel dialog = new BodyTypeViewModel { get = get };
            _bodyTypeService.PopulateBodyTypeViewModel(dialog);
            return PartialView("DeleteBodyTypeDialog", dialog);
        }

        [HttpPost]
        public ActionResult DeleteBodyType(BodyTypeDeleteModel delete)
        {
            BodyTypeViewModel model = new BodyTypeViewModel { Success = false };
            if (_bodyTypeService.DeleteBodyType(delete))
            {
                model.Success = true;
            }
            return View(model);
        }

        public ActionResult Up(int? BodyTypeId)
        {
            _bodyTypeService.BodyTypeUp(BodyTypeId);
            return RedirectToAction("Index");
        }

        public ActionResult Down(int? BodyTypeId)
        {
            _bodyTypeService.BodyTypeDown(BodyTypeId);
            return RedirectToAction("Index");
        }

        #endregion
    }
}