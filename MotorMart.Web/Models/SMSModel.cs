using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using MotorMart.Core.Models.Validation.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using MotorMart.Core.Models;

namespace MotorMart.Web.Models
{
    public class SMSModel
    {
        public IList<smscarrier> SMSCarriersList;

        [DisplayName("First name")]
        [Required(ErrorMessage = "This is nice to know!")]
        public string firstname { get; set; }

        [DisplayName("Last name")]
        [Required(ErrorMessage = "This is nice to know!")]
        public string lastname { get; set; }

        [DisplayName("Recipient email address")]
        [Email(ErrorMessage = "That email address doesn't look right...")]
        [Required(ErrorMessage = "This is used to identify your recipient!")]
        public string email { get; set; }

        [DisplayName("Recipient contact number")]
        [Required(ErrorMessage = "This helps map the number to its carrier!")]
        public string telephone { get; set; }

        [DisplayName("What would you like to SMS?")]
        [Required(ErrorMessage = "This is what the SMS recipient will see!")]
        [StringLength(160, ErrorMessage = "SMS's don't exceed 160 characters!")]
        public string message { get; set; }

        public int smscarrierid { get; set; }

        public bool smsSent { get; set; }

    }
}