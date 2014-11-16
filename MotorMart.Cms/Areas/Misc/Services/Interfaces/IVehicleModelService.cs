using System;
using MotorMart.Cms.Areas.Misc.Models;
using MotorMart.Core.Models;
using MotorMart.Cms.Models;


namespace MotorMart.Cms.Areas.Misc.Services
{
    public interface IVehicleModelService
    {
        bool AddVehicleModel(VehicleModelAddModel add);

        bool DeleteVehicleModel(VehicleModelDeleteModel model);

        bool EditVehicleModel(VehicleModelEditModel edit);

        bool GetVehicleModel(VehicleModelGetModel get, out model VehicleModel);

        VehicleModelAddModel PopulateVehicleModelAddModel(model Model);

        VehicleModelEditModel PopulateVehicleModelEditModel(model Model);

        void PopulateVehicleModelViewModel(VehicleModelViewModel model);

        bool VehicleModelDown(int? Id);

        bool VehicleModelUp(int? Id);
    }
}
