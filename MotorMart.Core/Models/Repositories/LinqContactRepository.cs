using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorMart.Core.Models
{
    public class LinqContactRepository : ILinqContactRepository
    {
        MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();

        #region IEnquiryRepository Members

        public void AddEnquiry(userenquiry enquiryToAdd)
        {
            _datacontext.userenquiries.InsertOnSubmit(enquiryToAdd);
            _datacontext.SubmitChanges();
        }

        public useraccount GetUserAccountByEmail(string email)
        {
            return _datacontext.useraccounts.Where(a => a.email == email).FirstOrDefault();
        }        

        public smscarrier GetSMSCarrierById(int carrierId)
        {
            return _datacontext.smscarriers.Where(c => c.smscarrierid == carrierId).FirstOrDefault();
        }

        public List<smscarrier> GetSMSCarriersList()
        {
            return _datacontext.smscarriers.ToList();
        }

        #endregion
    }
}