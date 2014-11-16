using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Core.Models.Validation;
using MotorMart.Web.Models;
using MotorMart.Core.Common;
using MotorMart.Core.Models;
using MotorMart.Web.SendSMSService;

namespace MotorMart.Web.Services
{
    public class ContactService : IContactService
    {
        IValidationDictionary _validation;
        ILinqContactRepository _repository;

        public ContactService(IValidationDictionary validation) : this(validation, new LinqContactRepository()) { } 

        public ContactService(IValidationDictionary validation, ILinqContactRepository repository)
        {
            _validation = validation;
            _repository = repository;
        }

        #region IContactService Members

        #region Enquiry Methods

        public EnquiryModel PopulateEnquiryModel(EnquiryModel model)
        {
            if (model == null)
            {
                model = new EnquiryModel
                {
                };
            }            

            return model;
        }

        public bool AddEnquiry(EnquiryModel enquiry)
        {
            if (!_validation.IsValid) 
                return false;

            var enquiryToAdd = new userenquiry
            {
                message = enquiry.message,
                subject = "General Enquiry"
            };

            useraccount User = _repository.GetUserAccountByEmail(enquiry.email);

            if (User == null)
            {
                User = new useraccount
                {
                    firstname = enquiry.firstname,
                    lastname = enquiry.lastname,
                    email = enquiry.email,
                    telephone = enquiry.telephone != null ? enquiry.telephone : string.Empty
                };
            }
            else
            {
                User.firstname = enquiry.firstname;
                User.lastname = enquiry.lastname;
                User.email = enquiry.email;
                User.telephone = enquiry.telephone != null ? enquiry.telephone : string.Empty;
            }

            enquiryToAdd.useraccount = User;

            _repository.AddEnquiry(enquiryToAdd);

            // Send the email
            //emailtemplate email = _repository.GetEmailTemplate(EmailTemplate.DailyDealSubscribeThanks);

            //MailerHelper mail = new MailerHelper(null);
            //mail.SendEmailFromTemplate(email, profile);

            return true;
        }

        #endregion

        #region SMS Methods

        public SMSModel PopulateSMSModel(SMSModel model)
        {
            if (model == null)
            {
                model = new SMSModel
                {
                };
            }
            
            model.SMSCarriersList = _repository.GetSMSCarriersList();
            return model;

        }

        public bool SendSMS(SMSModel sms)
        {
            if (!_validation.IsValid) return false;

            // Send the SMS
            try
            {
                if(sms.smscarrierid > 0)
                {
                    var carrier = _repository.GetSMSCarrierById(sms.smscarrierid);
                    if(carrier != null && !String.IsNullOrEmpty(sms.telephone) && !String.IsNullOrEmpty(sms.message))
                    {
                        string _sendTo = String.Concat(sms.telephone, carrier.carrier);
                        string _sendFrom = "pntege@googlemail.com";
                        string _subject = "MotorMart-NoReply";
                        string _message = sms.message;

                        string sendResponse;

                        //Use SMS Carrier
                        //MailHelper mail = new MailHelper();
                        //mail.GenerateSMS(_sendTo, _sendFrom, _subject, _message);

                        //Use Web service
                        var smsWorld = new SendSMSWorldSoapClient("SendSMSWorldSoap");
                        sendResponse = smsWorld.sendSMS(_sendFrom, "44", sms.telephone, _message); 

                        sms.smsSent = true;
                        return true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }            

            return false;
        }

        #endregion

        #endregion
    }
}