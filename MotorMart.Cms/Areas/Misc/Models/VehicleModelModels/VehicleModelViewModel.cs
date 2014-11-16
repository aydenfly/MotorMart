using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;
using System.Web.Mvc;
using MotorMart.Cms.Models;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class VehicleModelViewModel :  AdminViewModel
    {
        public SelectList MakeSelect;

        public IList<model> ModelsList;

        public IList<make> MakesList;

        public model CurrentModel { get; set; }

        public VehicleModelGetModel get { get; set; }
        public VehicleModelAddModel add { get; set; }
        public VehicleModelEditModel edit { get; set; }
        public VehicleModelDeleteModel delete { get; set; }

        public bool Success { get; set; }
    }
}