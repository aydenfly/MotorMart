using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Web.Models;
using MotorMart.Web.Services;
using MotorMart.Core.Models.Validation;
using System.Text;

namespace MotorMart.Web.Controllers
{
    public class ContactController : MasterController
    {
        //
        // GET: /Contact/

        private readonly IContactService _contactService;

        // GET: /Vehicle/
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public ContactController()
        {
            _contactService = new ContactService(new ModelStateWrapper(this.ModelState));
        }

        public ActionResult Index()
        {
            var model = new ContactViewModel
            {
                enquiry = _contactService.PopulateEnquiryModel(new EnquiryModel())
            };
            return View(model);
        }

        #region General Enquiries

        public ActionResult Enquiry()
        {
            var model = new ContactViewModel
            {
                enquiry = _contactService.PopulateEnquiryModel(new EnquiryModel())
            }; 
            return View(model);
        }

        [HttpPost]
        public ActionResult Enquiry(EnquiryModel enquiry)
        {
            var model = new ContactViewModel { enquiry = enquiry };
            if (_contactService.AddEnquiry(enquiry))
            {
                //TempData.Add(Resources.Resources.EnquirySubmitted, true);
                return Redirect(BuildConfirmationUrl(enquiry));
            }
            return View(model);
        }

        public ActionResult Confirm()
        {
            if (TempData["EnquirySubmitted"] != null)
            {
                var submitted = (bool)TempData["EnquirySubmitted"];
                if (submitted)
                {
                    var model = new ContactViewModel
                    {
                        enquiry = _contactService.PopulateEnquiryModel(new EnquiryModel())
                    };
                    return View(model);
                }
            }
            
            return RedirectToAction("Enquiry");
        }

        #endregion

        #region SMS actions

        public ActionResult SendSms()
        {
            SMSViewModel model = new SMSViewModel
            {
                sms = _contactService.PopulateSMSModel(new SMSModel())
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult SendSms(SMSModel sms)
        {
            SMSViewModel model = new SMSViewModel { sms = sms };
            if (_contactService.SendSMS(sms))
            {
                return View(model);
            }

            sms = _contactService.PopulateSMSModel(sms);            
            return View(model);
        }

        #endregion

        #region Private Methods

        private string BuildConfirmationUrl(EnquiryModel enquiry)
        {
            StringBuilder sb = new StringBuilder();
            if (enquiry != null)
            {
                //contact-us/enquiry-form/thank-you/firstname/{firstname}/lastname/{lastname}/email/{email}/telephone/{telephone}
                sb.Append("/contact-us/enquiry-form/thank-you?");
                sb.Append(!String.IsNullOrEmpty(enquiry.firstname) ? String.Format("/firstname/{0}", enquiry.firstname) : string.Empty);
                sb.Append(!String.IsNullOrEmpty(enquiry.lastname) ? String.Format("/lastname/{0}", enquiry.lastname) : string.Empty);
                sb.Append(!String.IsNullOrEmpty(enquiry.email) ? String.Format("/email/{0}", enquiry.email) : string.Empty);
                sb.Append(!String.IsNullOrEmpty(enquiry.telephone) ? String.Format("/telephone/{0}", enquiry.telephone) : string.Empty);
            }
            return sb.ToString();
        }

        #endregion

    }
}
