using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Models
{
    public class LinqVehicleMakeRepository : ILinqVehicleMakeRepository
    {
        MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();

        public make GetVehicleMake(int MakeId)
        {
            return _datacontext.makes.Where(m => m.makeid == MakeId).FirstOrDefault();
        }
        
        public IList<make> GetVehicleMakes()
        {
            return _datacontext.makes.ToList();
        }

        public make GetVehicleMake(string name)
        {
            return _datacontext.makes.Where(m => m.name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public bool VehicleMakeExists(string name)
        {
            return _datacontext.makes.Where(m => m.name.ToLower() == name.ToLower()).Any();
        }

        public IList<make> GetVehicleMakes(int MakeId)
        {
            return _datacontext.makes.Where(m => m.makeid == MakeId).ToList();
        }

        public void AddVehicleMake(make MakeToAdd)
        {
            _datacontext.makes.InsertOnSubmit(MakeToAdd);
            _datacontext.SubmitChanges();
        }

        public void DeleteVehicleMake(make VehicleMake)
        {
            _datacontext.makes.DeleteOnSubmit(VehicleMake);
            _datacontext.SubmitChanges();
        }
        
        public make GetVehicleMakeBelow(int MakeId)
        {
            make relativeVehicleMake = this.GetVehicleMake(MakeId);
            return _datacontext.makes.Where(v => v.sortorder > relativeVehicleMake.sortorder).OrderBy(p => p.sortorder).First();
        }

        public make GetVehicleMakeAbove(int MakeId)
        {
            make relativeVehicleMake = this.GetVehicleMake(MakeId);
            return _datacontext.makes.Where(v => v.sortorder < relativeVehicleMake.sortorder).OrderByDescending(p => p.sortorder).First();
        }

        public void Update()
        {
            _datacontext.SubmitChanges();
        }
    }
}