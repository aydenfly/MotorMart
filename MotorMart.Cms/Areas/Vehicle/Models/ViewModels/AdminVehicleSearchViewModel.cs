using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MotorMart.Core.Models;
using MotorMart.Cms.Models;

namespace MotorMart.Cms.Areas.Vehicle.Models
{
    public class AdminVehicleSearchViewModel : AdminViewModel
    {
        public AdminVehicleSearchModel vehiclesearch { get; set; }

        public PagedList<AdminVehicleSearchResult> SearchResults { get; set; }
    }
}
