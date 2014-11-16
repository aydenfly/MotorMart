using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MotorMart.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class VehicleColorGetModel
    {
        public int colorid { get; set; }
    }

    public class VehicleColorAddModel
    {
        public int colorid { get; set; }

        public color NewVehicleColor { get; set; }

        [Required(ErrorMessage="A color is required")]
        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Sort order")]
        public int sortorder { get; set; }
    }

    public class VehicleColorEditModel
    {
        public int colorid { get; set; }

        [Required(ErrorMessage = "A color is required")]
        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Sort order")]
        public int sortorder { get; set; }
    }

    public class VehicleColorDeleteModel
    {
        public int colorid { get; set; }
        public color CurrentColor { get; set; }
    }
}