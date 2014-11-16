using System;
using MotorMart.Core.Models;
using System.Collections.Generic;

namespace MotorMart.Core.Models
{
    public interface ILinqFuelTypeRepository
    {
        void AddFuelType(fueltype FuelTypeToAdd);

        void DeleteFuelType(fueltype FuelType);

        fueltype GetFuelType(int FuelTypeId);

        fueltype GetFuelTypeAbove(int FuelTypeId);

        fueltype GetFuelTypeBelow(int FuelTypeId);

        IList<fueltype> GetFuelTypes();

        IList<fueltype> GetFuelTypes(int FuelTypeId);

        fueltype GetFuelType(string type);

        bool FuelTypeExists(string type);
        

        void Update();
    }
}
