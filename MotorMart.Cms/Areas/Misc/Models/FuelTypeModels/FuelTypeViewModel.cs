using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;
using MotorMart.Cms.Models;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class FuelTypeViewModel : AdminViewModel
    {
        public IList<fueltype> FuelTypesList;

        public fueltype CurrentFuelType { get; set; }

        public FuelTypeGetModel get { get; set; }
        public FuelTypeAddModel add { get; set; }
        public FuelTypeEditModel edit { get; set; }
        public FuelTypeDeleteModel delete { get; set; }

        public bool Success { get; set; }
    }
}