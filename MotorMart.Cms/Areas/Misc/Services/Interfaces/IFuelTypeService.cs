using System;
using MotorMart.Cms.Areas.Misc.Models;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Areas.Misc.Services
{
    public interface IFuelTypeService
    {
        bool AddFuelType(FuelTypeAddModel add);

        bool DeleteFuelType(FuelTypeDeleteModel model);

        bool EditFuelType(FuelTypeEditModel edit);

        bool FuelTypeDown(int? Id);

        bool FuelTypeUp(int? Id);

        bool GetFuelType(FuelTypeGetModel get, out fueltype FuelType);

        FuelTypeAddModel PopulateFuelTypeAddModel(fueltype Model);

        FuelTypeEditModel PopulateFuelTypeEditModel(fueltype Model);

        void PopulateFuelTypeViewModel(FuelTypeViewModel model);
    }
}
