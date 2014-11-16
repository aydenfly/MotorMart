using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Core.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class VehicleModelGetModel
    {
        public int modelid { get; set; }
    }
    
    public class VehicleModelAddModel
    {
        public model NewModel { get; set; }

        public int modelid { get; set; }

        [DisplayName("Make")]
        [Required(ErrorMessage = "Model make is required!")]
        public int makeid { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Model name is required!")]
        public string name { get; set; }
    }

    public class VehicleModelEditModel
    {
        public int modelid { get; set; }

        [DisplayName("Make")]
        [Required(ErrorMessage = "Model make is required!")]
        public int makeid { get; set; }
        
        [DisplayName("Name")]
        [Required(ErrorMessage = "Model name is required!")]
        public string name { get; set; }

    }

    public class VehicleModelDeleteModel
    {
        public model CurrentModel;
        public int makeid { get; set; }
        public int modelid { get; set; }
    }
}