using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MotorMart.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class TransmissionGetModel
    {
        public int transmissionid { get; set; }
    }

    public class TransmissionAddModel
    {
        public int transmissionid { get; set; }

        public transmission NewTransmission { get; set; }

        [Required(ErrorMessage = "A transmission is required!")]
        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Sort order")]
        public int sortorder { get; set; }
    }

    public class TransmissionEditModel
    {
        public int transmissionid { get; set; }

        [Required(ErrorMessage = "A transmission is required!")]
        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Sort order")]
        public int sortorder { get; set; }
    }

    public class TransmissionDeleteModel
    {
        public int transmissionid { get; set; }
        public transmission CurrentTransmission { get; set; }
    }
}