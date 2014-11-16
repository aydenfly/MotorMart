using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;
using MotorMart.Cms.Models;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class VehicleColorViewModel : AdminViewModel
    {
        public IList<color> ColorsList;

        public color CurrentColor { get; set; }

        public VehicleColorGetModel get { get; set; }
        public VehicleColorAddModel add { get; set; }
        public VehicleColorEditModel edit { get; set; }
        public VehicleColorDeleteModel delete { get; set; }

        public bool Success { get; set; }
    }
}