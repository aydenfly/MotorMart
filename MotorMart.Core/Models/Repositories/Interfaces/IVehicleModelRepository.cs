using System;
using System.Collections.Generic;
using MotorMart.Core.Models;


namespace MotorMart.Cms.Models
{
    public interface IVehicleModelRepository
    {
        void AddVehicleModel(model ModelToAdd);

        void DeleteVehicleModel(model VehicleModel);

        IList<model> GetVehicleMakeModels(int MakeId);

        model GetVehicleModel(int ModelId);

        model GetVehicleModelAbove(int vehicleModelId);

        model GetVehicleModelBelow(int vehicleModelId);

        IList<model> GetVehicleModels();

        model GetVehicleModel(int MakeId, string modelName);

        bool VehicleModelExists(int MakeId, string modelName);

        void Update();
    }
}
