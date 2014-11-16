using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Models
{
    public class LinqTransmissionRepository : ILinqTransmissionRepository
    {
        MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();

        public transmission GetTransmission(int TransmissionId)
        {
            return _datacontext.transmissions.Where(m => m.transmissionid == TransmissionId).FirstOrDefault();
        }

        public transmission GetTransmission(string name)
        {
            return _datacontext.transmissions.Where(m => m.name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public bool TransmissionExists(string name)
        {
            return _datacontext.transmissions.Where(m => m.name.ToLower() == name.ToLower()).Any();
        }
        
        public IList<transmission> GetTransmissions()
        {
            return _datacontext.transmissions.ToList();
        }

        public IList<transmission> GetTransmissions(int TransmissionId)
        {
            return _datacontext.transmissions.Where(m => m.transmissionid == TransmissionId).ToList();
        }

        public void AddTransmission(transmission TransmissionToAdd)
        {
            _datacontext.transmissions.InsertOnSubmit(TransmissionToAdd);
            _datacontext.SubmitChanges();
        }

        public void DeleteTransmission(transmission Transmission)
        {
            _datacontext.transmissions.DeleteOnSubmit(Transmission);
            _datacontext.SubmitChanges();
        }
        
        public transmission GetTransmissionBelow(int TransmissionId)
        {
            transmission relativeTransmission = this.GetTransmission(TransmissionId);
            return _datacontext.transmissions.Where(v => v.sortorder > relativeTransmission.sortorder).OrderBy(p => p.sortorder).First();
        }

        public transmission GetTransmissionAbove(int TransmissionId)
        {
            transmission relativeTransmission = this.GetTransmission(TransmissionId);
            return _datacontext.transmissions.Where(v => v.sortorder < relativeTransmission.sortorder).OrderByDescending(p => p.sortorder).First();
        }

        public void Update()
        {
            _datacontext.SubmitChanges();
        }
    }
}