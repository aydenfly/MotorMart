using System;
using MotorMart.Core.Models;
using System.Collections.Generic;

namespace MotorMart.Core.Models
{
    public interface ILinqVehicleMakeRepository
    {
        void AddVehicleMake(make VehicleMakeToAdd);

        void DeleteVehicleMake(make VehicleMake);

        make GetVehicleMake(int MakeId);

        make GetVehicleMakeAbove(int MakelId);

        make GetVehicleMakeBelow(int MakeId);

        IList<make> GetVehicleMakes();

        IList<make> GetVehicleMakes(int MakeId);

        make GetVehicleMake(string name);

        bool VehicleMakeExists(string name);
        
        void Update();
    }
}
