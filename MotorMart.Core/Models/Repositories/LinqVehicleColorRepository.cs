using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Models
{
    public class LinqVehicleColorRepository : ILinqVehicleColorRepository
    {
        MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();

        public color GetVehicleColor(int ColorId)
        {
            return _datacontext.colors.Where(m => m.colorid == ColorId).FirstOrDefault();
        }

        public color GetVehicleColor(string name)
        {
            return _datacontext.colors.Where(m => m.name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public bool ColorExists(string name)
        {
            return _datacontext.colors.Where(m => m.name.ToLower() == name.ToLower()).Any();
        }
        
        public IList<color> GetVehicleColors()
        {
            return _datacontext.colors.ToList();
        }        

        public void AddVehicleColor(color ColorToAdd)
        {
            _datacontext.colors.InsertOnSubmit(ColorToAdd);
            _datacontext.SubmitChanges();
        }

        public void DeleteVehicleColor(color VehicleColor)
        {
            _datacontext.colors.DeleteOnSubmit(VehicleColor);
            _datacontext.SubmitChanges();
        }
        
        public color GetVehicleColorBelow(int ColorId)
        {
            color relativeVehicleColor = this.GetVehicleColor(ColorId);
            return _datacontext.colors.Where(v => v.sortorder > relativeVehicleColor.sortorder).OrderBy(p => p.sortorder).First();
        }

        public color GetVehicleColorAbove(int ColorId)
        {
            color relativeVehicleColor = this.GetVehicleColor(ColorId);
            return _datacontext.colors.Where(v => v.sortorder < relativeVehicleColor.sortorder).OrderByDescending(p => p.sortorder).First();
        }

        public void Update()
        {
            _datacontext.SubmitChanges();
        }
    }
}