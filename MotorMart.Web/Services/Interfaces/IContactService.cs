using System;
using MotorMart.Web.Models;

namespace MotorMart.Web.Services
{
    public interface IContactService
    {
        bool AddEnquiry(EnquiryModel enquiry);

        EnquiryModel PopulateEnquiryModel(EnquiryModel model);

        SMSModel PopulateSMSModel(SMSModel model);

        bool SendSMS(SMSModel sms);
    }
}
