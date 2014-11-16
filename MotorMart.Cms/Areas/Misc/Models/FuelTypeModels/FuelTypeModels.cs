using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MotorMart.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class FuelTypeGetModel
    {
        public int fueltypeid { get; set; }
    }

    public class FuelTypeAddModel
    {
        public int fueltypeid { get; set; }

        public fueltype NewFuelType { get; set; }

        [Required(ErrorMessage = "A fuel type is required!")]
        [DisplayName("Type")]
        public string type { get; set; }

        [DisplayName("Sort order")]
        public int sortorder { get; set; }
    }

    public class FuelTypeEditModel
    {
        public int fueltypeid { get; set; }

        [Required(ErrorMessage = "A fuel type is required!")]
        [DisplayName("Type")]
        public string type { get; set; }

        [DisplayName("Sort order")]
        public int sortorder { get; set; }
    }

    public class FuelTypeDeleteModel
    {
        public int fueltypeid { get; set; }
        public fueltype CurrentFuelType { get; set; }
    }
}