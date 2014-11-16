using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Models
{
    public class LinqFuelTypeRepository : ILinqFuelTypeRepository
    {
        MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();

        public fueltype GetFuelType(int FuelTypeId)
        {
            return _datacontext.fueltypes.Where(m => m.fueltypeid == FuelTypeId).FirstOrDefault();
        }
        
        public IList<fueltype> GetFuelTypes()
        {
            return _datacontext.fueltypes.ToList();
        }

        public fueltype GetFuelType(string type)
        {
            return _datacontext.fueltypes.Where(m => m.type.ToLower() == type.ToLower()).FirstOrDefault();
        }

        public bool FuelTypeExists(string type)
        {
            return _datacontext.fueltypes.Where(m => m.type.ToLower() == type.ToLower()).Any();
        }

        public IList<fueltype> GetFuelTypes(int FuelTypeId)
        {
            return _datacontext.fueltypes.Where(m => m.fueltypeid == FuelTypeId).ToList();
        }

        public void AddFuelType(fueltype FuelTypeToAdd)
        {
            _datacontext.fueltypes.InsertOnSubmit(FuelTypeToAdd);
            _datacontext.SubmitChanges();
        }

        public void DeleteFuelType(fueltype FuelType)
        {
            _datacontext.fueltypes.DeleteOnSubmit(FuelType);
            _datacontext.SubmitChanges();
        }
        
        public fueltype GetFuelTypeBelow(int FuelTypeId)
        {
            fueltype relativeFuelType = this.GetFuelType(FuelTypeId);
            return _datacontext.fueltypes.Where(v => v.sortorder > relativeFuelType.sortorder).OrderBy(p => p.sortorder).First();
        }

        public fueltype GetFuelTypeAbove(int FuelTypeId)
        {
            fueltype relativeFuelType = this.GetFuelType(FuelTypeId);
            return _datacontext.fueltypes.Where(v => v.sortorder < relativeFuelType.sortorder).OrderByDescending(p => p.sortorder).First();
        }

        public void Update()
        {
            _datacontext.SubmitChanges();
        }
    }
}