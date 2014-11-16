using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorMart.Web.Models
{
    public class ContactViewModel : MasterViewModel
    {
        public EnquiryModel enquiry { get; set; }
    }
}