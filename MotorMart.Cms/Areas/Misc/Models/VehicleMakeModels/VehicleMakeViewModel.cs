using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;
using MotorMart.Cms.Models;
using System.Web.Mvc;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class VehicleMakeViewModel : AdminViewModel
    {
        public IList<make> VehicleMakesList;

        public PagedList<make> MakeResults;

        public make CurrentVehicleMake { get; set; }

        public VehicleMakeGetModel get { get; set; }
        public VehicleMakeAddModel add { get; set; }
        public VehicleMakeEditModel edit { get; set; }
        public VehicleMakeDeleteModel delete { get; set; }

        public bool Success { get; set; }

        public int page { get; set; }
    }
}