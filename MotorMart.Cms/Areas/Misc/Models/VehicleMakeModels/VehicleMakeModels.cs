using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class VehicleMakeGetModel
    {
        public int makeid { get; set; }
    }

    public class VehicleMakeAddModel
    {
        public int makeid { get; set; }

        public make NewVehicleMake { get; set; }

        [Required(ErrorMessage = "A make is required!")]
        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Sort order")]
        public int sortorder { get; set; }

        public string filename { get; set; }

        [DisplayName("Make logo")]
        public HttpPostedFileBase logo { get; set; }


    }

    public class VehicleMakeEditModel
    {
        public int makeid { get; set; }

        [Required(ErrorMessage = "A make is required!")]
        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Sort order")]
        public int sortorder { get; set; }

        public string filename { get; set; }

        [DisplayName("Make logo")]
        public HttpPostedFileBase logo { get; set; }
    }

    public class VehicleMakeDeleteModel
    {
        public int makeid { get; set; }
        public make CurrentMake { get; set; }
    }
}