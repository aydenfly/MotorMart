using System;
using MotorMart.Cms.Areas.Misc.Models;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Areas.Misc.Services
{
    public interface IVehicleColorService
    {
        bool AddVehicleColor(VehicleColorAddModel add);

        bool VehicleColorDown(int? Id);

        bool VehicleColorUp(int? Id);

        bool DeleteVehicleColor(VehicleColorDeleteModel model);

        bool EditVehicleColor(VehicleColorEditModel edit);

        bool GetVehicleColor(VehicleColorGetModel get, out color Color);

        void PopulateVehicleColorViewModel(VehicleColorViewModel model);

        VehicleColorAddModel PopulateVehicleColorAddModel(color Model);

        VehicleColorEditModel PopulateVehicleColorEditModel(color Model);
    }
}
