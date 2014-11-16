using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MotorMart.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class ApplicationSettingGetModel
    {
        public int applicationsettingid { get; set; }
    }

    public class ApplicationSettingAddModel
    {
        public int applicationsettingid { get; set; }

        public applicationsetting NewApplicationSetting { get; set; }

        [Required(ErrorMessage = "Setting name is required!")]
        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Value")]
        public string value { get; set; }

    }

    public class ApplicationSettingEditModel
    {
        public int applicationsettingid { get; set; }

        [Required(ErrorMessage = "Setting name is required!")]
        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Value")]
        public string value { get; set; }
    }

    public class ApplicationSettingDeleteModel
    {
        public int applicationsettingid { get; set; }
        public applicationsetting CurrentApplicationSetting { get; set; }
    }
}