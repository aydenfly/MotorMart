using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Models
{
    public class LinqVehicleModelRepository : IVehicleModelRepository
    {
        MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();

        public model GetVehicleModel(int ModelId)
        {
            return _datacontext.models.Where(m => m.modelid == ModelId).FirstOrDefault();
        }
        
        public IList<model> GetVehicleModels()
        {
            return _datacontext.models.ToList();
        }

        public IList<model> GetVehicleMakeModels(int ModeId)
        {
            return _datacontext.models.Where(m => m.makeid == ModeId).ToList();
        }

        public void AddVehicleModel(model ModelToAdd)
        {
            _datacontext.models.InsertOnSubmit(ModelToAdd);
            _datacontext.SubmitChanges();
        }

        public void DeleteVehicleModel(model VehicleModel)
        {
            _datacontext.models.DeleteOnSubmit(VehicleModel);
            _datacontext.SubmitChanges();
        }
        
        public model GetVehicleModelBelow(int vehicleModelId)
        {
            model relativeVehicleModel = this.GetVehicleModel(vehicleModelId);
            return _datacontext.models.Where(m => m.makeid == relativeVehicleModel.makeid && m.sortorder > relativeVehicleModel.sortorder).OrderByDescending(p => p.sortorder).First();            
        }

        public model GetVehicleModelAbove(int vehicleModelId)
        {
            model relativeVehicleModel = this.GetVehicleModel(vehicleModelId);
            return _datacontext.models.Where(m => m.makeid == relativeVehicleModel.makeid && m.sortorder < relativeVehicleModel.sortorder).OrderByDescending(p => p.sortorder).First();            
        }

        public model GetVehicleModel(int MakeId, string modelName)
        {
            return _datacontext.models.Where(m => m.makeid == MakeId && m.name.ToLower() == modelName.ToLower()).FirstOrDefault();
        }

        public bool VehicleModelExists(int MakeId, string modelName)
        {
            return _datacontext.models.Where(m => m.makeid == MakeId && m.name.ToLower() == modelName.ToLower()).Any();
        }



        public void Update()
        {
            _datacontext.SubmitChanges();
        }
    }
}