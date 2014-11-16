using System;
using MotorMart.Core.Models;
using System.Collections.Generic;

namespace MotorMart.Core.Models
{
    public interface ILinqContactRepository
    {
        void AddEnquiry(userenquiry enquiryToAdd);

        useraccount GetUserAccountByEmail(string email);

        smscarrier GetSMSCarrierById(int carrierId);

        List<smscarrier> GetSMSCarriersList();
    }
}
