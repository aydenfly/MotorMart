using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Core.Models;
using MotorMart.Cms.Models;


namespace MotorMart.Cms.Areas.Vehicle.Models
{
    public class AdminVehicleDialogViewModel : AdminViewModel
    {
        public VehicleDeleteModel delete { get; set; }  
    }
}