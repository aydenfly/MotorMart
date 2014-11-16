using System;
using MotorMart.Cms.Models;
using MotorMart.Core.Models;
using MotorMart.Cms.Areas.Vehicle.Models;


namespace MotorMart.Cms.Areas.Vehicle.Services
{
    public interface IVehicleService
    {
        bool GetVehicle(VehicleGetModel get);

        bool AddImage(VehicleImageAddModel model);

        bool AddVehicle(VehicleDetailsAddModel add);

        bool DeleteImage(VehicleImageDeleteModel model);

        bool DeleteVehicle(VehicleDeleteModel model);

        bool EditDimensions(VehicleDimensionsEditModel editdimensions);

        bool EditFeatures(VehicleFeaturesEditModel editfeatures);

        bool EditImages(VehicleImageEditModel model);

        bool EditPerformanceDetails(VehiclePerformanceEditModel editperformance);

        bool EditSafetyDetails(VehicleSafetyDetailsEditModel editsafetydetails);

        bool EditVehicleDetails(VehicleDetailsEditModel edit);

        bool EditVehicleSummaryDetails(VehicleSummaryDetailsEditModel editsummary);

        AdminVehicleDialogViewModel PopulateAdminVehicleDialogViewModel(VehicleGetModel getModel);

        void PopulateAdminVehicleSearchViewModel(AdminVehicleSearchViewModel model);

        void PopulateAdminVehicleViewModel(AdminVehicleViewModel model);

        bool VehicleDown(int? Id);

        bool VehicleUp(int? Id);
    }
}
