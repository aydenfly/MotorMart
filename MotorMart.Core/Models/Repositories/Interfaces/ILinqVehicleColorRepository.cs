using System;
using System.Collections.Generic;
using MotorMart.Core.Models;

namespace MotorMart.Core.Models
{
    public interface ILinqVehicleColorRepository
    {
        void AddVehicleColor(color MakeToAdd);

        void DeleteVehicleColor(color VehicleColor);

        color GetVehicleColor(int ColorId);

        color GetVehicleColor(string name);

        bool ColorExists(string name);

        color GetVehicleColorAbove(int ColorId);

        color GetVehicleColorBelow(int ColorId);

        IList<color> GetVehicleColors();

        void Update();
    }
}
