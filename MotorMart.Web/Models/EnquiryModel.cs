using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using MotorMart.Core.Models.Validation.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MotorMart.Web.Models
{
    public class EnquiryModel
    {
        [DisplayName("First name")]
        [Required(ErrorMessage = "This is nice to know!")]
        public string firstname { get; set; }

        [DisplayName("Last name")]
        [Required(ErrorMessage = "This is nice to know!")]
        public string lastname { get; set; }

        [DisplayName("Your email address")]
        [Email(ErrorMessage = "That email address doesn't look right...")]
        [Required(ErrorMessage = "This will help us to help you!")]
        public string email { get; set; }

        [DisplayName("Your contact number")]       
        public string telephone { get; set; }

        [DisplayName("What would you like to ask us?")]
        [Required(ErrorMessage = "This information will help us a lot!")]
        [DataType(DataType.MultilineText)]
        public string message { get; set; }

    }
}