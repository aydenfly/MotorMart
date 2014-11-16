using System;
using System.Collections.Generic;
using MotorMart.Core.Models;
using MotorMart.Web.Models;

namespace MotorMart.Web.Services
{
    public interface IVehicleService
    {
        IList<vehicle> ListVehicles();

        void PopulateVehicleViewModel(MotorMart.Web.Models.VehicleViewModel model);

        void PopulateVehicleSearchViewModel(VehicleSearchViewModel model);

        vehicle GetVehicle(VehicleGetModel get);

        List<model> GetMakeModels(int makeid);
    }
}
