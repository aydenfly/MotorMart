using System.Web.Mvc;
using MotorMart.Core.Models;
using MotorMart.Cms.Models;
using MotorMart.Core.Models.Validation;
using MotorMart.Cms.Services;
using MotorMart.Cms.Controllers;
using MotorMart.Cms.Areas.Vehicle.Services;
using MotorMart.Cms.Areas.Vehicle.Models;

namespace MotorMart.Cms.Areas.Vehicle.Controllers
{
    public class VehicleController : AdminMasterController
    {
        private IVehicleService _vehicleService;
        private IAdminService _masterService;
 
        // GET: /Admin/Vehicle/

        public VehicleController(IVehicleService adminVehicleService)
        {
            _vehicleService = adminVehicleService;
        }

        public VehicleController()
        {
            _vehicleService = new VehicleService(new ModelStateWrapper(this.ModelState));
            _masterService = new AdminService(new ModelStateWrapper(this.ModelState));
        }

        public ActionResult Search()
        {
            AdminVehicleSearchViewModel model = new AdminVehicleSearchViewModel
            {
                vehiclesearch = new AdminVehicleSearchModel()
            };
            _vehicleService.PopulateAdminVehicleSearchViewModel(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Search(AdminVehicleSearchModel vehiclesearch)
        {
            AdminVehicleSearchViewModel model = new AdminVehicleSearchViewModel
            {
                vehiclesearch = vehiclesearch
            };
            _vehicleService.PopulateAdminVehicleSearchViewModel(model);

            return View(model);
        }

        public ActionResult Add()
        {
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel() };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(VehicleDetailsAddModel adddetails)
        {
            if (_vehicleService.AddVehicle(adddetails))
            {
                return RedirectToAction("Edit", new { @vehicleid = adddetails.NewVehicle.vehicleid });
            }
            else
            {
                AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel(), adddetails = adddetails };
                _vehicleService.PopulateAdminVehicleViewModel(model);
                return View(model);
            }          
        }

        public ActionResult Edit(VehicleGetModel get)
        {
            if (_vehicleService.GetVehicle(get))
            {
                AdminVehicleViewModel model = new AdminVehicleViewModel { get = get };
                _vehicleService.PopulateAdminVehicleViewModel(model);
                return View(model);
            }

            return RedirectToAction("Search");
        }

        [HttpPost]
        public ActionResult Edit(VehicleDetailsEditModel editdetails)
        {
            _vehicleService.EditVehicleDetails(editdetails);
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = editdetails.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        public ActionResult EditVehicleSummaryDetails(VehicleGetModel get)
        {
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = get.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditVehicleSummaryDetails(VehicleSummaryDetailsEditModel editsummarydetails)
        {
            _vehicleService.EditVehicleSummaryDetails(editsummarydetails);
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = editsummarydetails.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        public ActionResult EditVehicleDimensions(VehicleGetModel get)
        {
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = get.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditVehicleDimensions(VehicleDimensionsEditModel editdimensions)
        {
            _vehicleService.EditDimensions(editdimensions);
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = editdimensions.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        public ActionResult EditPerformanceDetails(VehicleGetModel get)
        {
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = get.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }
        
        [HttpPost]
        public ActionResult EditPerformanceDetails(VehiclePerformanceEditModel editperformance)
        {
            _vehicleService.EditPerformanceDetails(editperformance);
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = editperformance.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        public ActionResult EditVehicleFeatures(VehicleGetModel get)
        {
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = get.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }
        
        [HttpPost]
        public ActionResult EditVehicleFeatures(VehicleFeaturesEditModel editfeatures)
        {
            _vehicleService.EditFeatures(editfeatures);
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = editfeatures.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        public ActionResult EditSafetyDetails(VehicleGetModel get)
        {
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = get.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditSafetyDetails(VehicleSafetyDetailsEditModel editsafetydetails)
        {
            _vehicleService.EditSafetyDetails(editsafetydetails);
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = editsafetydetails.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        public ActionResult AddImage(VehicleGetModel get)
        {
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = get.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddImage(VehicleImageAddModel vehicleimageadd)
        {
            if (_vehicleService.AddImage(vehicleimageadd))
            {
                return RedirectToAction("editimages", new { @vehicleid = vehicleimageadd.vehicleid });
            }
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = vehicleimageadd.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        public ActionResult EditImages(VehicleGetModel get)
        {
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = get.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditImages(VehicleImageEditModel vehicleimageedit)
        {
            _vehicleService.EditImages(vehicleimageedit);
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = vehicleimageedit.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        public ActionResult DeleteImage(VehicleImageDeleteModel vehicleimagedelete)
        {
            _vehicleService.DeleteImage(vehicleimagedelete);
            AdminVehicleViewModel model = new AdminVehicleViewModel { get = new VehicleGetModel { vehicleid = vehicleimagedelete.vehicleid } };
            _vehicleService.PopulateAdminVehicleViewModel(model);
            return View(model);
        }

        public ActionResult DeleteVehicleDialog(VehicleGetModel get)
        {
            AdminVehicleViewModel dialog = new AdminVehicleViewModel { get = get };
            _vehicleService.PopulateAdminVehicleViewModel(dialog);
            return PartialView("DeleteVehicleDialog", dialog);
        }

        [HttpPost]
        public ActionResult Delete(VehicleDeleteModel delete)
        {
            AdminVehicleViewModel model = new AdminVehicleViewModel { Success = false };
            if(_vehicleService.DeleteVehicle(delete))
            {
                model.Success = true;
            }
            return View(model);
        }

        public ActionResult Up(int? Id)
        {
            _vehicleService.VehicleUp(Id);
            return RedirectToAction("Search");
        }

        public ActionResult Down(int? Id)
        {
            _vehicleService.VehicleDown(Id);
            return RedirectToAction("Search");
        }

    }
}
