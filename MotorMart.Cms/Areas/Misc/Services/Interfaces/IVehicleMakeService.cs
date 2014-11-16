using System;
using MotorMart.Cms.Areas.Misc.Models;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Areas.Misc.Services
{
    public interface IVehicleMakeService
    {
        bool AddVehicleMake(VehicleMakeAddModel add);

        bool DeleteVehicleMake(VehicleMakeDeleteModel model);

        bool EditVehicleMake(VehicleMakeEditModel edit);

        bool GetVehicleMake(VehicleMakeGetModel get, out make VehicleMake);

        VehicleMakeAddModel PopulateVehicleMakeAddModel(make Model);

        VehicleMakeEditModel PopulateVehicleMakeEditModel(make Model);

        void PopulateVehicleMakeViewModel(VehicleMakeViewModel model);

        bool VehicleMakeDown(int? Id);

        bool VehicleMakeUp(int? Id);
    }
}
