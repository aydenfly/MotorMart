using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;
using MotorMart.Cms.Models;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class ApplicationSettingViewModel : AdminViewModel
    {
        public IList<applicationsetting> ApplicationSettingsList;

        public applicationsetting CurrentApplicationSetting { get; set; }

        public ApplicationSettingGetModel get { get; set; }
        public ApplicationSettingAddModel add { get; set; }
        public ApplicationSettingEditModel edit { get; set; }
        public ApplicationSettingDeleteModel delete { get; set; }

        public bool Success { get; set; }
    }
}